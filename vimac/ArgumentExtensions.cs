using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vimac
{
    public static class ArgumentExtensions
    {
        private const int DefaultIndex = -1;

        public static string GetArgumentValue(this string[] arguments, Argument argumentSwitch,
            string defaultValue = null)
        {
            var index = arguments.GetArgumentIndex(argumentSwitch);

            return index == DefaultIndex && arguments.Length >= index
                ? defaultValue
                : arguments[index + 1];
        }

        public static bool HasArgumentValue(this string[] arguments, Argument argumentSwitch)
        {
            return arguments.GetArgumentIndex(argumentSwitch) != DefaultIndex;
        }

        private static int GetArgumentIndex(this IReadOnlyList<string> arguments, Argument argumentSwitch)
        {
            if (argumentSwitch == null) return DefaultIndex;

            for (var index = 0; index < arguments.Count; index++)
            {
                if (!arguments[index].StartsWith("-")) continue;

                if (argumentSwitch.Matches(arguments[index]))
                {
                    return index;
                }
            }

            return DefaultIndex;
        }
    }
}
