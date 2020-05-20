using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLog_Test_App.Loggers
{
    public class AdminLog : ILog
    {
        private static ILogger _logger = LogManager.GetLogger("AdminLogger");

        public void AddLog(LogLevel logLevel, string logMessage)
                => _logger.Log(logLevel, logMessage);

        public void AddError(string logMessage)
                => _logger.Error(logMessage);
        
    }
}
