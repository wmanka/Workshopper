using Workshopper.Domain.Common;
using Workshopper.Infrastructure.Tests.Unit.Common.Persistence;

namespace Workshopper.Infrastructure.Tests.Unit.Common.TestEntities;

internal sealed class TestDomainEntity : DomainEntity
{
    public TestDomainEntity(
        Guid id,
        string? name = null,
        ChildTestDomainEntity? child = null)
        : base(id)
    {
        Name = name;
        Child = child;
    }

    public string? Name { get; set; }

    public ChildTestDomainEntity? Child { get; private set; }
}