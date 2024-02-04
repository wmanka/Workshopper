namespace Workshopper.Infrastructure.FeatureFlags;

public static class CustomFeatureFlagOverrides
{
    public readonly static IDictionary<string, object> LocalDictionary = new Dictionary<string, object>
    {
        {
            FeatureFlags.NotificationsEnabled, true
        },
        {
            FeatureFlags.PaymentsEnabled, false
        }
    };
}