using NLog;

namespace NLog_Test_App.Loggers
{
    public class RegularLog : ILog
    {
        private static ILogger logger = LogManager.GetLogger("RegularLogger");

        public void AddError(string logMessage)
            => logger.Error(logMessage);
        

        public void AddLog(LogLevel logLevel, string logMessage)
            => logger.Log(logLevel, logMessage);
    }
}
