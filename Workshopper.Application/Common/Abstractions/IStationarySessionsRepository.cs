using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Common.Abstractions;

public interface IStationarySessionsRepository
{
    public Task AddStationarySessionAsync(StationarySession stationarySession);
}