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

            string smtpUsername = "";
            string smtpPassword = "";

            LoggingConfiguration configuration = new LoggingConfiguration();


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

            configuration.AddRule(LogLevel.Warn, LogLevel.Off, errorFileTarget);
            configuration.AddRuleForAllLevels(globalFileTarget);
            configuration.AddRuleForAllLevels(consoleTarget);
            configuration.AddRuleForAllLevels(mailAsync);

            LogManager.Configuration = configuration;

        }
    }
}
