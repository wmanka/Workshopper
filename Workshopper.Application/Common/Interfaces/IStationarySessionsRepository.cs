using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Common.Interfaces;

public interface IStationarySessionsRepository
{
    public Task AddSessionAsync(StationarySession session);
}