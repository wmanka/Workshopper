namespace Workshopper.Infrastructure.FeatureFlags;

public static class CustomFlagOverrides
{
    public readonly static IDictionary<string, object> LocalDictionary = new Dictionary<string, object>
    {
        {
            "notificationsEnabled", true
        }
    };
}