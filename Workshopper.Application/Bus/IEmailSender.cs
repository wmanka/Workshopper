namespace Workshopper.Application.Bus;

public interface IEmailSender
{
    Task SendAsync(Email email);
}