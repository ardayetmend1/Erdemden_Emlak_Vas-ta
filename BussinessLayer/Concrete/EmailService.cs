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

    public async Task SendOfferNotificationEmailAsync(
        string toEmail, string customerName, string vehicleInfo,
        decimal minPrice, decimal maxPrice)
    {
        using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.SmtpPort)
        {
            Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword),
            EnableSsl = true
        };

        var culture = new System.Globalization.CultureInfo("tr-TR");
        var formattedMin = minPrice.ToString("N0", culture);
        var formattedMax = maxPrice.ToString("N0", culture);

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
            Subject = "Aracınız İçin Fiyat Teklifi - Erdem Otomotiv",
            IsBodyHtml = true,
            Body = $@"
                <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; padding: 30px; background: #f8f9fa; border-radius: 12px;'>
                    <div style='text-align: center; margin-bottom: 24px;'>
                        <h2 style='color: #1B3C87; margin: 0;'>Erdem Otomotiv - Emlak</h2>
                    </div>
                    <div style='background: white; padding: 24px; border-radius: 8px; border: 1px solid #e5e7eb;'>
                        <p style='color: #374151; font-size: 16px;'>Sayın <strong>{customerName}</strong>,</p>
                        <p style='color: #6b7280; font-size: 14px;'>Aracınız için değerlendirmemiz tamamlanmıştır. Teklifimiz aşağıdaki gibidir:</p>

                        <div style='background: #fef3c7; padding: 16px; border-radius: 8px; margin: 20px 0; text-align: center;'>
                            <p style='color: #92400e; font-size: 12px; margin: 0 0 8px 0; font-weight: bold; text-transform: uppercase;'>Araç</p>
                            <p style='color: #374151; font-size: 16px; font-weight: bold; margin: 0;'>{vehicleInfo}</p>
                        </div>

                        <div style='background: #ecfdf5; padding: 20px; border-radius: 8px; text-align: center; margin: 20px 0;'>
                            <p style='color: #065f46; font-size: 12px; margin: 0 0 8px 0; font-weight: bold; text-transform: uppercase;'>Fiyat Teklifi</p>
                            <p style='color: #059669; font-size: 28px; font-weight: bold; margin: 0;'>{formattedMin} ₺ - {formattedMax} ₺</p>
                        </div>

                        <p style='color: #6b7280; font-size: 14px; margin-top: 20px;'>Teklifimizi hesabınıza giriş yaparak <strong>Tekliflerim</strong> sayfasından kabul veya reddedebilirsiniz.</p>
                    </div>
                    <p style='color: #9ca3af; font-size: 11px; text-align: center; margin-top: 16px;'>Erdem Otomotiv - Emlak | Bu e-posta otomatik olarak gönderilmiştir.</p>
                </div>"
        };

        mailMessage.To.Add(toEmail);
        await client.SendMailAsync(mailMessage);
    }
}
