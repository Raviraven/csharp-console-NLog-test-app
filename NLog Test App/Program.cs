using NLog;
using NLog_Test_App.Loggers;
using NLog_Test_App.NLog;
using System;
using System.Linq;

namespace NLog_Test_App
{
    class Program
    {
        private static ILog logger;

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
            try
            {
                CustomConfig.SetConfiguration();
                logger = new AdminLog();
                logger.AddLog(LogLevel.Info, "Program started");
                AdminFunction();

                logger = new RegularLog();
                RegularFunction();

                for (int i =0; i < 5; i++)
                {
                    processingFile($"File_{i}");
                }
                logger.AddLog(LogLevel.Info, "Program ended");
            }
            catch(Exception e)
            {
                logger.AddLog(LogLevel.Fatal, $"somtin is no yes, {e.Message}");
            }
            finally {
                LogManager.Shutdown();
            }

            Console.WriteLine("And it's done");

            string trace = "trace";

            Console.WriteLine($"Log level to string: {LogLevel.Trace.Name}");
            Console.WriteLine($"string and log level comparision: {trace.ToLower() == LogLevel.Trace.Name.ToLower()}");

            Console.WriteLine($"{LogLevel.AllLevels.First(n => n.Name.ToLower() == trace.ToLower()).Name}");

            //Console.ReadLine();
        }

        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Console.WriteLine("exitting");
            LogManager.Shutdown();
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

        private static void processingFile(string fileName)
        {
            LogManager.Configuration.Variables["FILENAME"] = fileName;

            logger.AddLog(LogLevel.Trace, "Entered into processingFile function");
            logger.AddLog(LogLevel.Info, "File processing started");
            Console.WriteLine($"Just processing a file: {fileName}");
            logger.AddError("Error during processing a file");

            logger.AddLog(LogLevel.Info, "File processing ended");
        }
    }
}
