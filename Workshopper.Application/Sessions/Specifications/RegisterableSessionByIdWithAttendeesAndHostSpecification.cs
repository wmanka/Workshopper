using Workshopper.Application.Common.Models;
using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Sessions.Specifications;

internal class RegisterableSessionByIdWithAttendeesAndHostSpecification : Specification<Session>
{
    public RegisterableSessionByIdWithAttendeesAndHostSpecification(Guid id)
    {
        AddFilter(session => session.Id == id
                             && !session.IsCanceled);

        AddInclude(session => session.HostProfile);
        AddInclude(session => session.Attendees);
    }
}