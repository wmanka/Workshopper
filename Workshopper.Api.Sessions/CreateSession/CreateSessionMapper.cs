using Workshopper.Api.Sessions.Contracts.Sessions;
using Workshopper.Application.Sessions.Commands.CreateOnlineSession;
using Workshopper.Application.Sessions.Commands.CreateStationarySession;
using SessionType = Workshopper.Domain.Sessions.SessionType;

namespace Workshopper.Api.Sessions.CreateSession;

public static class CreateSessionMapper
{
    public static CreateOnlineSessionCommand ToCreateOnlineSessionCommand(CreateSessionRequest request)
    {
        return new CreateOnlineSessionCommand
        {
            SessionType = SessionType.FromName(request.SessionType.ToString()),
            Title = request.Title,
            Description = request.Description,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            Places = request.Places,
            Link = request.Link!
        };
    }

    public static CreateStationarySessionCommand ToCreateStationarySessionCommand(CreateSessionRequest request)
    {
        return new CreateStationarySessionCommand
        {
            SessionType = SessionType.FromName(request.SessionType.ToString()),
            Title = request.Title,
            Description = request.Description,
            StartDateTime = request.StartDateTime,
            EndDateTime = request.EndDateTime,
            Places = request.Places,
            Address = request.Address!
        };
    }
}