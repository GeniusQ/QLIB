using System;
using System.Collections.Generic;
using System.Text;

namespace QLIB.Log
{
    public class LoggerFactory
    {
        private static ILogger _logger = new DebugLogger();
        public static ILogger CreateLogger()
        {
            return _logger;
        }
    }
}
