using NLog;
using NLog_Test_App.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLog_Test_App
{
    class Program
    {
        //private static ILogger logger = LogManager.GetLogger("Program");
        //private static ILogger loggerFile = LogManager.GetLogger("AdminLogFile");

        private static ILog logger; // = new AdminLog();

        static void Main(string[] args)
        {
            logger = new AdminLog();
            AdminFunction();

            Console.ReadLine();
        }

        private static void AdminFunction()
        {
            logger.AddLog(LogLevel.Trace, "Admin function enter");
            
            Console.WriteLine("Hello from my Main :)");
            logger.AddError("some error occured");

            logger.AddLog(LogLevel.Trace, "Admin function exit");
        }
    }
}
