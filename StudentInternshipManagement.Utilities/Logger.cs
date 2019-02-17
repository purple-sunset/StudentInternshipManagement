using System;
using log4net;

namespace StudentInternshipManagement.Utilities
{
    public static class Logger
    {
        private static readonly ILog Log = LogManager.GetLogger("StudentInternshipManagement");

        public static void LogError(Exception ex)
        {
            Log.Error(ex);
        }

        public static void LogWarning(Exception ex)
        {
            Log.Warn(ex);
        }

        public static void LogInfo(Exception ex)
        {
            Log.Info(ex);
        }

        public static void LogFatal(Exception ex)
        {
            Log.Fatal(ex);
        }
    }
}