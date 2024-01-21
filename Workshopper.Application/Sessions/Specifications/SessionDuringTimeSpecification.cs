using Workshopper.Application.Common;
using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Sessions.Specifications;

internal class SessionDuringTimeSpecification : Specification<Session>
{
    public SessionDuringTimeSpecification(DateTimeOffset startDateTime, DateTimeOffset endDateTime, Guid creatorId)
    {
        AddFilter(session => (session.StartDateTime <= endDateTime && session.StartDateTime >= startDateTime) ||
                             (session.StartDateTime >= startDateTime && session.StartDateTime <= endDateTime));
        // todo: add creator id
    }
}