using Microsoft.AspNetCore.Identity.UI.Services;

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
        throw new NotImplementedException();
    }
}
