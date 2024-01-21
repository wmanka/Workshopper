using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Common.Interfaces;

public interface IStationarySessionsRepository
{
    public Task AddStationarySessionAsync(StationarySession stationarySession);
}