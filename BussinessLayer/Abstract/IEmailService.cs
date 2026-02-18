namespace BussinessLayer.Abstract;

public interface IEmailService
{
    Task SendOfferNotificationEmailAsync(string toEmail, string customerName, string vehicleInfo, decimal minPrice, decimal maxPrice);
}
