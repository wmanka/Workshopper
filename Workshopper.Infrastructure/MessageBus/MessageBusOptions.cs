namespace Workshopper.Infrastructure.MessageBus;

public class MessageBusOptions
{
    public const string SectionName = "MessageBus";

    public string Host { get; init; } = null!;

    public string VirtualHost { get; init; } = null!;

    public string User { get; init; } = null!;

    public string Password { get; init; } = null!;
}