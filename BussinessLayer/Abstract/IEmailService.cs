namespace BussinessLayer.Abstract;

public interface IEmailService
{
    Task SendVerificationEmailAsync(string toEmail, string userName, string verificationCode);
}
