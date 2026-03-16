using Application.Features.Finances.DTOs;
using Application.Features.Finances.Interfaces;


namespace Application.Features.Finances.Services
{
    public class AccountTypeService
    {
        private readonly IAccountTypeRepository _accountTypeRepository;

        /// <summary>
        /// constructor que inyecta el repositorio de tipos de cuenta para que el servicio pueda acceder a los datos relacionados con los 
        /// tipos de cuenta y realizar operaciones consultar tipos de cuenta.
        /// </summary>
        /// <param name="accountTypeRepository"></param>
        public AccountTypeService(IAccountTypeRepository accountTypeRepository)
        {
            _accountTypeRepository = accountTypeRepository;
        }

        /// <summary>
        /// retorna una lista de tipos de cuenta
        /// lo convierte a dto
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AccountTypeDTO?>> GetAllAccountTypesAsync()
        {
            var types = await _accountTypeRepository.GetAllAccountTypesAsync();

            return types.Select(t => t == null ? null : new AccountTypeDTO
            {
                AccountTypeId = t.Id,
                Name = t.Name,
                Abbre = t.Abbre
            }); ;
        }

        /// <summary>
        /// retorna un tipo de cuenta por su id, si no se encuentra devuelve null, si se encuentra devuelve el tipo de cuenta encontrado convertido a dto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AccountTypeDTO?> GetAccountTypeByIdAsync(Guid id)
        {
            var type = await _accountTypeRepository.GetAccountTypeByIdAsync(id);

            return type == null ? null : new AccountTypeDTO
            {
                AccountTypeId = type.Id,
                Name = type.Name,
                Abbre = type.Abbre
            };
        }
    }
}
