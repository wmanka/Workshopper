namespace Workshopper.Tests.Architecture;

public class LayersTests : BaseTest
{
    [Fact]
    public void DomainLayer_Should_NotDependOnAnyOtherLayer()
    {
        var result = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOnAny([
                ..ApplicationAssemblies,
                ..InfrastructureAssemblies,
                ..PresentationAssemblies
            ])
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void ApplicationLayer_Should_DependOnlyOnDomainLayer()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOnAny([
                ..InfrastructureAssemblies,
                ..PresentationAssemblies
            ])
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void InfrastructureLayer_Should_DependOnlyOnApplicationLayer()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .Should()
            .NotHaveDependencyOnAny([
                ..PresentationAssemblies
            ])
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}