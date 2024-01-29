using Workshopper.Application.Common.Models;
using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Sessions.Specifications;

internal class SessionDuringTimeSpecification : Specification<Session>
{
    public SessionDuringTimeSpecification(DateTimeOffset startDateTime, DateTimeOffset endDateTime, Guid userProfileId)
    {
        AddFilter(session => ((session.StartDateTime <= endDateTime && session.StartDateTime >= startDateTime)
                              || (session.StartDateTime >= startDateTime && session.StartDateTime <= endDateTime))
                             && (session.HostProfileId == userProfileId
                                 || session.Attendees.Any(x => x.Id == userProfileId))
                             && !session.IsCanceled);

        AddInclude(session => session.Attendees);
    }
}