using NLog;
using NLog_Test_App.Loggers;
using NLog_Test_App.NLog;
using System;

namespace NLog_Test_App
{
    class Program
    {
        private static ILog logger;

        static void Main(string[] args)
        {
            try
            {
                CustomConfig.SetConfiguration();
                logger = new AdminLog();
                AdminFunction();

                logger = new RegularLog();
                RegularFunction();
            }
            finally {
                LogManager.Shutdown();
            }

            Console.WriteLine("And it's done");
            Console.ReadLine();
        }

        private static void AdminFunction()
        {
            logger.AddLog(LogLevel.Trace, "Admin function enter");
            
            Console.WriteLine("Hello from my Main :)");
            logger.AddError("some error occured");

            logger.AddLog(LogLevel.Trace, "Admin function exit");
        }

        private static void RegularFunction()
        {
            logger.AddLog(LogLevel.Trace, "Regular function enter");

            Console.WriteLine("Some string from regular function");
            logger.AddError("Error in rehular func");

            logger.AddLog(LogLevel.Trace, "Regular function exit");
        }
    }
}
