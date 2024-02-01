namespace Workshopper.Infrastructure.FeatureFlags;

public class FeatureFlagsSettings
{
    public const string SectionName = "FeatureFlagsSettings";

    public string Key { get; init; } = null!;

    public int PollingInterval { get; init; }
}