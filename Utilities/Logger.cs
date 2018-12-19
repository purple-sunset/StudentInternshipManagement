using System;
using log4net;

namespace Utilities
{
    public static class Logger
    {
        private static ILog log = LogManager.GetLogger("StudentInternshipManagement");

        public static void LogError(Exception ex)
        {
            log.Error(ex);
        }

        public static void LogWarning(Exception ex)
        {
            log.Warn(ex);
        }

        public static void LogInfo(Exception ex)
        {
            log.Info(ex);
        }

        public static void LogFatal(Exception ex)
        {
            log.Fatal(ex);
        }
    }
}
