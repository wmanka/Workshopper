using Workshopper.Domain.Common;

namespace Workshopper.Infrastructure.Tests.Unit.Common.TestEntities;

internal sealed class ChildTestDomainEntity : DomainEntity
{
    public ChildTestDomainEntity(Guid id) : base(id)
    {
    }
}