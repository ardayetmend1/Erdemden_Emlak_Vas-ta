using System.Net;
using System.Net.Mail;
using BussinessLayer.Abstract;
using BussinessLayer.Settings;
using Microsoft.Extensions.Options;

namespace BussinessLayer.Concrete;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendVerificationEmailAsync(string toEmail, string userName, string verificationCode)
    {
        using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
        {
            Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
            Subject = "E-posta Doğrulama - Erdem Otomotiv Emlak",
            IsBodyHtml = true,
            Body = $@"
                <div style='font-family: Arial, sans-serif; max-width: 500px; margin: 0 auto; padding: 30px; background: #f8f9fa; border-radius: 12px;'>
                    <div style='text-align: center; margin-bottom: 24px;'>
                        <h2 style='color: #1B3C87; margin: 0;'>Erdem Otomotiv - Emlak</h2>
                    </div>
                    <div style='background: white; padding: 24px; border-radius: 8px; border: 1px solid #e5e7eb;'>
                        <p style='color: #374151; font-size: 16px;'>Merhaba <strong>{userName}</strong>,</p>
                        <p style='color: #6b7280; font-size: 14px;'>Hesabınızı doğrulamak için aşağıdaki kodu kullanın:</p>
                        <div style='text-align: center; margin: 24px 0;'>
                            <div style='display: inline-block; background: #1B3C87; color: white; font-size: 32px; font-weight: bold; letter-spacing: 8px; padding: 16px 32px; border-radius: 8px;'>
                                {verificationCode}
                            </div>
                        </div>
                        <p style='color: #9ca3af; font-size: 12px; text-align: center;'>Bu kod 10 dakika içinde geçerliliğini yitirecektir.</p>
                    </div>
                    <p style='color: #9ca3af; font-size: 11px; text-align: center; margin-top: 16px;'>Bu e-postayı siz talep etmediyseniz lütfen dikkate almayın.</p>
                </div>"
        };

        mailMessage.To.Add(toEmail);
        await client.SendMailAsync(mailMessage);
    }
}
