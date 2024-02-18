using Workshopper.Application.Common.Models;
using Workshopper.Infrastructure.Tests.Unit.Common.TestEntities;

namespace Workshopper.Infrastructure.Tests.Unit.Common.Specifications;

internal class TestDomainEntityByNameSpecification : Specification<TestDomainEntity>
{
    public TestDomainEntityByNameSpecification()
    {
    }

    public TestDomainEntityByNameSpecification(string name)
    {
        AddFilter(x => x.Name == name);
    }
}