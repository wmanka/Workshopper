namespace Workshopper.Infrastructure.FeatureFlags;

public class FeatureFlagsOptions
{
    public const string SectionName = "FeatureFlagsSettings";

    public string Key { get; init; } = null!;

    public int PollingInterval { get; init; }
}