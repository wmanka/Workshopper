using Workshopper.Infrastructure.Common.Persistence;

namespace Workshopper.Infrastructure.Tests.Unit.Common.Persistence;

public class SpecificationEvaluatorTests
{
    private readonly IQueryable<TestDomainEntity> _queryable =
        new List<TestDomainEntity>
        {
            new TestDomainEntity(
                id: Guid.Parse("f03e22e5-f385-4f04-8eea-201ae8c74e2a"),
                name: "ABCD",
                child: new ChildTestDomainEntity(Guid.Parse("fc677acb-34f4-428e-8f30-83e9361e1df1"))),
            new TestDomainEntity(Guid.Parse("f42dc004-800e-4e38-af9e-fc574919cc04"), "ABCD"),
            new TestDomainEntity(Guid.Parse("f42dc004-800e-4e38-af9e-fc574919cc04"), "XYZ"),
            new TestDomainEntity(
                id: Guid.Parse("8b827c87-869f-4001-a66d-fb9ea6d6cd59"),
                name: null,
                child: new ChildTestDomainEntity(Guid.Parse("6af3abcc-e136-4783-9306-4f38036be33b"))),
        }.AsQueryable();

    [Theory]
    [InlineData("ABCD", 2)]
    [InlineData("XYZ", 1)]
    [InlineData(null, 1)]
    [InlineData("1234", 0)]
    public void GetQuery_ShouldReturnFilteredItems_WhenFilterSpecified(string name, byte count)
    {
        var specification = new TestDomainEntityByNameSpecification(name);

        var result = SpecificationEvaluator.GetQuery(_queryable, specification);

        result.Should().HaveCount(count);
        result.Should().OnlyContain(x => x.Name == name);
    }

    [Theory]
    [InlineData("f03e22e5-f385-4f04-8eea-201ae8c74e2a", "ABCD", 1)]
    [InlineData("f42dc004-800e-4e38-af9e-fc574919cc04", "XYZ", 1)]
    [InlineData("3fe33536-5ae0-4c8b-bc91-28489588d494", "XYZ", 0)]
    public void GetQuery_ShouldReturnFilteredItems_WhenMultipleFiltersSpecified(string id, string name, byte count)
    {
        var idGuid = Guid.Parse(id);
        var specification = new TestDomainEntityByIdAndNameSpecification(idGuid, name);

        var result = SpecificationEvaluator.GetQuery(_queryable, specification);

        result.Should().HaveCount(count);
        result.Should().OnlyContain(x => x.Id == idGuid && x.Name == name);
    }

    [Fact]
    public void GetQuery_ShouldReturnAllItems_WhenFilterIsNull()
    {
        var specification = new TestDomainEntityByNameSpecification();

        var result = SpecificationEvaluator.GetQuery(_queryable, specification);

        result.Should().HaveCount(_queryable.Count());
    }

    [Fact]
    public void GetQuery_ShouldReturnItemsWithIncludedEntities_WhenIncludeSpecified()
    {
        var childId = Guid.Parse("fc677acb-34f4-428e-8f30-83e9361e1df1");
        var specification = new TestDomainEntityByChildIdSpecification(childId);

        var result = SpecificationEvaluator.GetQuery(_queryable, specification);

        result.Should().HaveCount(1);
        result.Should().ContainSingle(x => x.Child != null);
    }

    [Fact]
    public void GetQuery_ShouldReturnSortedItems_WhenSortSpecified()
    {
        var specification = new TestDomainEntitySortedByNameSpecification();

        var result = SpecificationEvaluator.GetQuery(_queryable, specification);

        result.Should().BeInAscendingOrder(x => x.Name);
    }
}