using System.Collections;
using ConfigCat.Client;
using Microsoft.Extensions.Logging;
using LogLevel = ConfigCat.Client.LogLevel;

namespace Workshopper.Infrastructure.FeatureFlags;

public class FeatureFlagsLoggerAdapter : IConfigCatLogger
{
    private readonly ILogger _logger;

    public FeatureFlagsLoggerAdapter(ILogger<ConfigCatClient> logger)
    {
        _logger = logger;
    }

    public LogLevel LogLevel
    {
        get => LogLevel.Debug;
        set { }
    }

    public void Log(LogLevel level, LogEventId eventId, ref FormattableLogMessage message, Exception? exception = null)
    {
        var logLevel = level switch
        {
            LogLevel.Error => Microsoft.Extensions.Logging.LogLevel.Error,
            LogLevel.Warning => Microsoft.Extensions.Logging.LogLevel.Warning,
            LogLevel.Info => Microsoft.Extensions.Logging.LogLevel.Information,
            LogLevel.Debug => Microsoft.Extensions.Logging.LogLevel.Debug,
            _ => Microsoft.Extensions.Logging.LogLevel.None
        };

        var logValues = new LogValues(ref message);

        _logger.Log(logLevel, eventId.Id, state: logValues, exception, static (state, _) => state.Message.ToString());

        message = logValues.Message;
    }

    private sealed class LogValues : IReadOnlyList<KeyValuePair<string, object?>>
    {
        public LogValues(ref FormattableLogMessage message)
        {
            Message = message;
        }

        public FormattableLogMessage Message { get; private set; }

        public KeyValuePair<string, object?> this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException(nameof(index));
                }

                return index == Count - 1
                    ? new KeyValuePair<string, object?>("{OriginalFormat}", Message.Format)
                    : new KeyValuePair<string, object?>(Message.ArgNames![index], Message.ArgValues![index]);
            }
        }

        public int Count => (Message.ArgNames?.Length ?? 0) + 1;

        public IEnumerator<KeyValuePair<string, object?>> GetEnumerator()
        {
            for (int i = 0, n = Count; i < n; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString() => Message.InvariantFormattedMessage;
    }
}