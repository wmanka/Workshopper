using Workshopper.Application.Common;
using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Sessions.Specifications;

internal class SessionDuringTimeSpecification : Specification<Session>
{
    public SessionDuringTimeSpecification(DateTimeOffset startDateTime, DateTimeOffset endDateTime)
    {
        AddFilter(session => (session.StartDateTime <= endDateTime && session.StartDateTime >= startDateTime) ||
                             (session.StartDateTime >= startDateTime && session.StartDateTime <= endDateTime));
    }
}