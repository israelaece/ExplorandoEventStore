using System;

namespace Dominio
{
    public static class Log
    {
        public static void Red(object message, int padding = 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(new string(' ', padding) + message);
            Console.ResetColor();
        }

        public static void Green(object message, int padding = 0)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(new string(' ', padding) + message);
            Console.ResetColor();
        }

        public static void Yellow(object message, int padding = 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(new string(' ', padding) + message);
            Console.ResetColor();
        }

        public static void NewLine()
        {
            Console.WriteLine();
        }
    }
}