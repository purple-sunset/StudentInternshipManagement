using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Utilities
{
    public class Logger
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
