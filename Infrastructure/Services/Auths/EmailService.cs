using Application.Features.Auth.Interfaces;


namespace Infrastructure.Services.Auths
{
    public class EmailService : IEmailService
    {
        public async Task SendTwoFactorCodeAsync(string email, string code)
        {
            // Aquí luego puedes usar SMTP o AWS SES
            Console.WriteLine($"2FA code for {email}: {code}");
            await Task.CompletedTask;
        }
    }
}
