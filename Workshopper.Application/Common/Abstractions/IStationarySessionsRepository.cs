using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Common.Abstractions;

public interface IStationarySessionsRepository
{
    Task AddStationarySessionAsync(StationarySession stationarySession);
}