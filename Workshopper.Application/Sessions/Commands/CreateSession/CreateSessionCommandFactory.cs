using Workshopper.Domain.Common;
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
        Address? address)
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

        if (deliveryType == DeliveryType.Hybrid)
        {
            throw new NotImplementedException();
        }

        throw new InvalidOperationException("Delivery type is not valid");
    }
}