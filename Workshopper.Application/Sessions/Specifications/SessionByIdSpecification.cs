using Workshopper.Application.Common;
using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Sessions.Specifications;

internal class SessionByIdSpecification : Specification<Session>
{
    public SessionByIdSpecification(Guid id)
    {
        AddFilter(session => session.Id == id);
    }
}