using System;
using System.Collections.Generic;
using System.Text;

namespace ECP.Util.Common.Extensions
{
    public static class ConsoleExtendsions
    {
        public static void WriteLineSuccess(string format, params object[] arg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(format, arg);
            Console.ResetColor();
        }
        public static void WriteLineError(string format, params object[] arg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(format, arg);
            Console.ResetColor();
        }
    }
}
