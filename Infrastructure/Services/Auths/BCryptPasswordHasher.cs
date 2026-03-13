using Application.Features.Auth.Interfaces;


namespace Infrastructure.Services.Auths
{
    public class BCryptPasswordHasher : IPasswordHasher
    {
        public string Hash(string password)
            => BCrypt.Net.BCrypt.HashPassword(password);



        public bool Verify(string password, string passwordHash)
        {

            try{
                return BCrypt.Net.BCrypt.Verify(password, passwordHash);
            }
            catch
            {
                return false;
            }
        }
        //=> BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
