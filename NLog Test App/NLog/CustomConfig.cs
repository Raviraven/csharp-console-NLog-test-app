using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Targets.Wrappers;

namespace NLog_Test_App.NLog
{

    public static class CustomConfig
    {
        public static void SetConfiguration()
        {
            string directory = "${basedir}\\${shortdate}\\${shortdate}_";
            string logFileName = "FileWithSomeCustomName";
            string globalLogFile = "GlobalLog";

            string basicLogLayout = "[${date}] [${level}] - ${message}";
            string errorLogLayout = "[${date}] [${level}] - ${callsite:className=True:methodName=True:includeNamespace=false} - ${message}";

            string filenameVariable = "${var:FILENAME}";

            string smtpUsername = "";
            string smtpPassword = "";

            LoggingConfiguration configuration = new LoggingConfiguration();

            #region targets configuration
            FileTarget errorFileTarget = new FileTarget()
            {
                Name="ErrorsPerFile",
                Layout = "[${date}] ${logger} - [${level}] - ${callsite} : ${message}"
            };

            errorFileTarget.FileName = $"{directory}{logFileName}.log";
            
            FileTarget globalFileTarget = new FileTarget()
            {
                Name="GlobalLogFile",
                Layout = "[${date}] ${logger} - [${level}] : ${message}"
            };

            globalFileTarget.FileName = $"{directory}{globalLogFile}.log";

            ColoredConsoleTarget consoleTarget = new ColoredConsoleTarget()
            {
                Name="ColoredConsoleGlobal",
                Layout = "[${date}] ${logger} - [${level}] : ${message}"
            };

            MailTarget mailTarget = new MailTarget()
            {
                SmtpServer = "smtp.gmail.com",
                SmtpPort = 587,
                EnableSsl = true,
                SmtpAuthentication = SmtpAuthenticationMode.Basic,
                SmtpUserName = smtpUsername,
                SmtpPassword = smtpPassword,
                From = smtpUsername,
                To = smtpUsername,
                Subject = "test nlog mail sending from code target",
                AddNewLines = true,
                Layout = "[${date}] ${logger} - [${level}] : ${message}"
            };

            AsyncTargetWrapper mailAsync = new AsyncTargetWrapper()
            {
                WrappedTarget = mailTarget,
                Name="AsyncMail"
            };

            FileTarget errorLogPerAction = new FileTarget()
            {
                FileName = $"{directory}ErrorLogFor_${{var:FILENAME}}.log",
                Header = $"Processing file: {filenameVariable}, error output:",
                Layout = errorLogLayout
            };

            FileTarget globalLogPerAction = new FileTarget()
            {
                Header = "Processing file: ${var:FILENAME}, log output:",
                FileName = $"{directory}GlobalLogFor_{filenameVariable}.log",
                Layout = basicLogLayout
            };

            #endregion

            configuration.AddRule(LogLevel.Warn, LogLevel.Off, errorFileTarget);
            configuration.AddRuleForAllLevels(globalFileTarget);
            configuration.AddRuleForAllLevels(consoleTarget);
            //configuration.AddRuleForAllLevels(mailAsync);
            configuration.AddRuleForOneLevel(LogLevel.Error, errorLogPerAction);
            configuration.AddRuleForAllLevels(globalLogPerAction);

            LogManager.Configuration = configuration;

        }
    }
}
