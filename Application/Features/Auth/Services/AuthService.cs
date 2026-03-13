using System.Security.Cryptography;

using Application.Auth.DTOs;
using Application.Features.Auth.Interfaces;
using Application.Features.Users.Interfaces;
using Domain.Features.Auth.Entities;
using Domain.Features.Users.Entities;



namespace Application.Features.Auth.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITwoFactorRepository _twoFactorRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;


        /// <summary>
        /// metodo constructor que recibe las dependencias necesarias para manejar la autenticación, como el repositorio de usuarios, el repositorio de códigos 2FA, el hasher de contraseñas, el servicio de tokens y el servicio de correo electrónico.
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="twoFactorRepository"></param>
        /// <param name="passwordHasher"></param>
        /// <param name="tokenService"></param>
        /// <param name="emailService"></param>
        public AuthService(
            IUserRepository userRepository,
            ITwoFactorRepository twoFactorRepository,
            IPasswordHasher passwordHasher,
            ITokenService tokenService,
            IEmailService emailService,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _twoFactorRepository = twoFactorRepository;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
            _emailService = emailService;
            _refreshTokenRepository = refreshTokenRepository;
        }


        /// <summary>
        /// permite a un usuario iniciar sesión. Primero verifica si el usuario existe y si su cuenta está activa. Luego, valida la contraseña utilizando el hasher de contraseñas. Si el usuario tiene habilitada la autenticación de dos factores (2FA)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                throw new Exception("Correo no encontrado.");

            if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
                throw new Exception("Credenciales inválidas.");

            // Si 2FA está activo: generar + guardar + enviar, NO tokens todavía
            if (user.TwoFactorEnabled)
            {
                //invalidar códigos activos anteriores (si los hay)
                await _twoFactorRepository.InvalidateActiveCodesAsync(user.UserID);
                await _twoFactorRepository.SaveChangesAsync();


                var code = Generate6DigitCode();
                var twoFactor = new TwoFactorCode(
                    user.UserID,
                    code,
                    1,
                    DateTime.UtcNow.AddMinutes(5)
                );

                await _twoFactorRepository.AddCodeAsync(twoFactor);
                await _twoFactorRepository.SaveChangesAsync();

                await _emailService.SendTwoFactorCodeAsync(user.Email, code);

                return new LoginResponse
                {
                    RequiresTwoFactor = true,
                    TwoFactorId = twoFactor.Id
                };
            }

            // Si 2FA NO está activo: entregar tokens
            return await GenerateTokensAsync(user);
        }


        /// <summary>
        /// permite generar un Access Token y un Refresh Token para un usuario autenticado.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private async Task<LoginResponse> GenerateTokensAsync(User user)
        {
            // 1️ Generar Access Token usando tu ITokenService
            var accessToken = _tokenService.GenerateAccessToken(user);

            // 2️ Generar Refresh Token seguro (64 bytes aleatorios)
            var refreshValue = Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64)
            );

            // 3️ Crear entidad RefreshToken
            var refresh = new RefreshToken(
                user.UserID,
                refreshValue,
                DateTime.UtcNow.AddDays(7) // duración del refresh token
            );

            // 4️ Guardarlo en base de datos
            await _refreshTokenRepository.AddAsync(refresh);
            await _refreshTokenRepository.SaveChangesAsync();

            // 5️ Devolver respuesta
            return new LoginResponse
            {
                RequiresTwoFactor = false,
                AccessToken = accessToken,
                RefreshToken = refreshValue
            };
        }

        /// <summary>
        /// registra a un nuevo usuario en el sistema. 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task RegisterAsync(RegisterRequest request)
        {
            var existing = await _userRepository.GetByEmailAsync(request.Email);
            if (existing != null)
                throw new Exception("El usuario ya existe.");

            var hash = _passwordHasher.Hash(request.Password);

            var user = new User(
                request.Email,
                request.Username,
                hash,
                request.FullName
            );

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        /// <summary>
        /// verifica el código de autenticación de dos factores (2FA) proporcionado por el usuario. 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<LoginResponse> VerifyTwoFactorAsync(VerifyTwoFactorRequest request)
        {
            //busca el código 2FA en la base de datos utilizando el identificador único (twoFactorId) proporcionado en la solicitud.
            var record = await _twoFactorRepository.GetByIdAsync(request.TwoFactorId);

            //si no existe retorna codigo invalid
            if (record == null)
                throw new Exception("Código inválido.");


            // si el código no coincide, retorna error
            if (record.Code != request.Code)
                throw new Exception("Código inválido.");


            //si el código ya fue utilizado, retorna error
            if (record.TwoFactorStatusID == 2)
                throw new Exception("El código ya fue utilizado.");

            //si el código fue reemplazado por uno nuevo, retorna error
            if (record.TwoFactorStatusID == 3)
                throw new Exception("Este código fue reemplazado por uno nuevo.");

            //si el código ha expirado, marca como expirado y retorna error
            if (record.IsExpired())
            {
                record.MarkAsExpired();
                await _twoFactorRepository.SaveChangesAsync();

                throw new Exception("El código ha expirado.");
            }



            //flujo exitoso: marcar código como usado, generar tokens y retornar
            record.MarkAsUsed();
            await _twoFactorRepository.SaveChangesAsync();

            var user = await _userRepository.GetByIdAsync(record.UserID);

            return await GenerateTokensAsync(user);
        }

        /// <summary>
        /// refresca el Access Token utilizando un Refresh Token válido.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<LoginResponse> RefreshAsync(RefreshTokenRequest request)
        {
            // 1️ Buscar refresh token en BD
            var storedToken = await _refreshTokenRepository
                .GetByTokenAsync(request.RefreshToken);

            if (storedToken == null || !storedToken.IsActive())
                throw new Exception("Refresh token inválido o expirado.");

            // 2️ Buscar usuario asociado
            var user = await _userRepository
                .GetByIdAsync(storedToken.UserID);

            if (user == null)
                throw new Exception("Usuario no encontrado.");

            // 3️ Generar nuevo AccessToken
            var newAccessToken = _tokenService.GenerateAccessToken(user);

            return new LoginResponse
            {
                RequiresTwoFactor = false,
                AccessToken = newAccessToken,
                RefreshToken = request.RefreshToken // reutilizamos el mismo por ahora
            };
        }


        /// <summary>
        /// genera codigo seguro de 6 digitos
        /// </summary>
        /// <returns></returns>
        private string Generate6DigitCode()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[4];
            rng.GetBytes(bytes);

            var value = BitConverter.ToUInt32(bytes, 0) % 900000 + 100000;
            return value.ToString();
        }
    }
}
