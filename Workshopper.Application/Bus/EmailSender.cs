using Microsoft.Extensions.Logging;

namespace Workshopper.Application.Bus;

public class EmailSender : IEmailSender
{
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(ILogger<EmailSender> logger)
    {
        _logger = logger;
    }

    public async Task SendAsync(Email email)
    {
        _logger.LogInformation("Email sent");
    }
}