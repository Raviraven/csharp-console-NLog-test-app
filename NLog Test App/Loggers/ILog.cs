using NLog;

namespace NLog_Test_App.Loggers
{
    public interface ILog
    {
        void AddLog(LogLevel logLevel, string logMessage);
        void AddError(string logMessage);
    }
}
