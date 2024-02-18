using Workshopper.Domain.Common;

namespace Workshopper.Infrastructure.Tests.Unit.Common.Persistence;

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

internal sealed class ChildTestDomainEntity : DomainEntity
{
    public ChildTestDomainEntity(Guid id) : base(id)
    {
    }
}