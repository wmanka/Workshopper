using Workshopper.Application.Common.Models;
using Workshopper.Infrastructure.Tests.Unit.Common.TestEntities;

namespace Workshopper.Infrastructure.Tests.Unit.Common.Specifications;

internal class TestDomainEntitySortedByNameSpecification : Specification<TestDomainEntity>
{
    public TestDomainEntitySortedByNameSpecification()
    {
        AddSort(x => x.Name ?? string.Empty);
    }
}