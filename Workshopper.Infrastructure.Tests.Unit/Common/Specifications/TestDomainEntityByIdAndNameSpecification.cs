using Workshopper.Application.Common.Models;
using Workshopper.Infrastructure.Tests.Unit.Common.TestEntities;

namespace Workshopper.Infrastructure.Tests.Unit.Common.Specifications;

internal class TestDomainEntityByIdAndNameSpecification : Specification<TestDomainEntity>
{
    public TestDomainEntityByIdAndNameSpecification(Guid id, string name)
    {
        AddFilter(x => x.Name == name
                       && x.Id == id);
        // AddFilter(x => x.Id == id); // todo: remove possibility to add multiple filters, rename to SetFilter(?)
    }
}