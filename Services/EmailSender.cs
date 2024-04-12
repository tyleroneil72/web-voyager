using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace web_voyager.Services;

public class EmailSender : IEmailSender
{
    private readonly string _sendGridKey;

    public EmailSender(IConfiguration configuration)
    {
        _sendGridKey = configuration["SendGrid:ApiKey"];
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var client = new SendGridClient(_sendGridKey);
        var from = new EmailAddress("tyleroneildev@gmail.com", "Web Voyager");
        var to = new EmailAddress(email);
        var message = MailHelper.CreateSingleEmail(from, to, subject, htmlMessage, htmlMessage);
        await client.SendEmailAsync(message);
    }
}
