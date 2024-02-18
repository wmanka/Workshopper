using Workshopper.Application.Common.Models;
using Workshopper.Infrastructure.Tests.Unit.Common.TestEntities;

namespace Workshopper.Infrastructure.Tests.Unit.Common.Specifications;

internal class TestDomainEntityByChildIdSpecification : Specification<TestDomainEntity>
{
    public TestDomainEntityByChildIdSpecification(Guid childId)
    {
        AddFilter(x => x.Child != null && x.Child.Id == childId);
        AddInclude(x => x.Child!);
    }
}