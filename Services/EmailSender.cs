using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace web_voyager.Services;

public class EmailSender : IEmailSender
{
    private readonly ILogger<EmailSender> _logger;
    private readonly string _apiKey;

    public EmailSender(IConfiguration config, ILogger<EmailSender> logger)
    {
        _apiKey = config.GetValue<string>("SendGrid:ApiKey") ?? throw new ArgumentNullException("SendGrid API Key not found.");
        _logger = logger;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        if (string.IsNullOrEmpty(_apiKey))
        {
            _logger.LogError("SendGrid API Key is null or empty.");
            throw new InvalidOperationException("SendGrid API Key is not set.");
        }

        try
        {
            var client = new SendGridClient(_apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("tyleroneildev@gmail.com", "Web Voyager"),
                Subject = subject,
                HtmlContent = htmlMessage
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking to ensure the content of the email is not altered.
            msg.SetClickTracking(false, false);

            var response = await client.SendEmailAsync(msg);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Email to {email} was sent successfully.");
            }
            else
            {
                _logger.LogError($"Failed to send email to {email}. Status Code: {response.StatusCode}");
                throw new Exception($"Failed to send email. Status Code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while sending email to {email}.");
            throw;
        }
    }
}
