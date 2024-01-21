using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Sessions.Commands.CreateSession;

public static class CreateSessionCommandFactory
{
    public static ICreateSessionCommand CreateSessionCommand(
        DeliveryType deliveryType,
        SessionType sessionType,
        string title,
        string? description,
        DateTimeOffset startDateTime,
        DateTimeOffset endDateTime,
        int places,
        string? link,
        string? address)
    {
        if (deliveryType == DeliveryType.Online)
        {
            return new CreateOnlineSessionCommand
            {
                SessionType = sessionType,
                Title = title,
                Description = description,
                StartDateTime = startDateTime,
                EndDateTime = endDateTime,
                Places = places,
                Link = link!
            };
        }

        if (deliveryType == DeliveryType.Stationary)
        {
            return new CreateStationarySessionCommand
            {
                SessionType = sessionType,
                Title = title,
                Description = description,
                StartDateTime = startDateTime,
                EndDateTime = endDateTime,
                Places = places,
                Address = address!
            };
        }

        throw new InvalidOperationException("Delivery type is not valid");
    }
}