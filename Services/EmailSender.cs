using Microsoft.AspNetCore.Identity.UI.Services;

namespace web_voyager.Services;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        throw new NotImplementedException();
    }
}
