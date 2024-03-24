using Microsoft.Extensions.Logging;
using ZLogger;
using ZLogger.Providers;

namespace ServerCore
{
    public static class LoggerConfig
    {

        public static ILoggerFactory Factory;

        public static void CreateLogger(string logFilePath)
        {
            if (Factory == null)
            {
                Create(logFilePath);
            }
        }

        private static void Create(string logFilePath)
        {
            Factory = LoggerFactory.Create(logging =>
            {
                logging.SetMinimumLevel(LogLevel.Trace);

                logging.AddZLoggerFile(logFilePath);

                logging.AddZLoggerRollingFile(options =>
                {
                    // File name determined by parameters to be rotated
                    options.FilePathSelector = (timestamp, sequenceNumber) => $"logs/{timestamp.ToLocalTime():yyyy-MM-dd}_{sequenceNumber:000}.log";

                    // The period of time for which you want to rotate files at time intervals.
                    options.RollingInterval = RollingInterval.Day;

                    // Limit of size if you want to rotate by file size. (KB)
                    options.RollingSizeKB = 1024;
                });

                // Add ZLogger provider to ILoggingBuilder
                logging.AddZLoggerConsole(options =>
                {
                    options.UseJsonFormatter(formatter =>
                    {
                        formatter.IncludeProperties = IncludeProperties.ParameterKeyValues;
                    });
                });

                // Output Structured Logging, setup options
                // logging.AddZLoggerConsole(options => options.UseJsonFormatter());
            });
        }
    }
}
