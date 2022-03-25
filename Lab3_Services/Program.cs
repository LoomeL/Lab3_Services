using System;
using System.Text.RegularExpressions;

namespace Lab3_Services
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            new ServicesRating().Start();
        }

        public static int ReadLineOfRange(int f, int s)
        {
            while (true)
            {
                string line = Console.ReadLine();
                if (line != null && Regex.IsMatch(line, "[0-9]"))
                {
                    int i = Convert.ToInt32(line);
                    if (f <= i && i <= s)
                    {
                        return i;
                    }
                }

                Console.WriteLine($"Введите челое число от {f} до {s}");
            }
        }
    }
}