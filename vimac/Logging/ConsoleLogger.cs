using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vimac.Logging
{
    public class ConsoleLogger : ILogger
    {
        private static int _lineNumber;

        public void Log(LoggingLevel loggingLevel, string information, params object[] arguments)
        {

            switch (loggingLevel)
            {
                case LoggingLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LoggingLevel.Info:
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                default:
                    Console.ResetColor();
                    break;
            }

            Console.WriteLine("{0} - {1}", _lineNumber++, string.Format(information, arguments));
            Console.ResetColor();
        }
    }
}
