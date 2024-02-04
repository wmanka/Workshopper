using ConfigCat.Client;

namespace Workshopper.Api.Notifications.FeatureFlags;

public class FeatureFlagsProcessor : IGlobalPreProcessor
{
    private readonly IConfigCatClient _configCatClient;

    public FeatureFlagsProcessor(IConfigCatClient configCatClient)
    {
        _configCatClient = configCatClient;
    }

    public async Task PreProcessAsync(IPreProcessorContext ctx, CancellationToken ct)
    {
        var notificationsEnabled = await _configCatClient.GetValueAsync(
            key: "notifications-enabled",
            defaultValue: false,
            cancellationToken: ct);

        if (notificationsEnabled)
        {
            return;
        }

        await ctx.HttpContext.Response.SendOkAsync(ct);
    }
}