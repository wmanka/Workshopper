using Workshopper.Application.Common.Models;
using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Sessions.Specifications;

internal class SessionByIdWithAttendeesAndHostProfileSpecification : Specification<Session>
{
    public SessionByIdWithAttendeesAndHostProfileSpecification(Guid id)
    {
        AddFilter(session => session.Id == id);

        AddInclude(session => session.HostProfile);
        AddInclude(session => session.Attendees);
    }
}