using Workshopper.Application.Common.Models;

namespace Workshopper.Infrastructure.Tests.Unit.Common.Persistence;

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

internal class TestDomainEntityByIdAndNameSpecification : Specification<TestDomainEntity>
{
    public TestDomainEntityByIdAndNameSpecification(Guid id, string name)
    {
        AddFilter(x => x.Name == name
                       && x.Id == id);
        // AddFilter(x => x.Id == id); // todo: remove possibility to add multiple filters, rename to SetFilter(?)
    }
}

internal class TestDomainEntityByChildIdSpecification : Specification<TestDomainEntity>
{
    public TestDomainEntityByChildIdSpecification(Guid childId)
    {
        AddFilter(x => x.Child != null && x.Child.Id == childId);
        AddInclude(x => x.Child!);
    }
}

internal class TestDomainEntitySortedByNameSpecification : Specification<TestDomainEntity>
{
    public TestDomainEntitySortedByNameSpecification()
    {
        AddSort(x => x.Name ?? string.Empty);
    }
}