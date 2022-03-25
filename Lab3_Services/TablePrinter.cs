using System;

namespace Lab3_Services
{
    public class TablePrinter
    {
        private int[] collums;
        private int width;
        public TablePrinter(int[] collums)
        {
            this.collums = collums;
            foreach (int collum in collums)
            {
                width += collum;
            }
        }

        public void PrintRow(string[] strs, ConsoleColor color)
        {
            for (var i = 0; i < strs.Length; i++)
            {
                if (i == 4) continue;
                if (i == 3)
                {
                    Console.ForegroundColor = color;
                    Console.Write("["+ strs[i] + "]" + new string(Convert.ToChar(" "), collums[i] - strs[i].Length));
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                    Console.Write(strs[i] + new string(Convert.ToChar(" "), collums[i] - strs[i].Length));
            }
            Console.WriteLine();
        }

        public void PrintHeader(string head)
        {
            if (width <= head.Length)
            {
                Console.WriteLine(head);
                return;
            }

            int space = (width - head.Length) / 2;
            Console.WriteLine(new string(Convert.ToChar("="), space) + head + new string(Convert.ToChar("="), space));
        }

        public void PrintFooter()
        {
            Console.WriteLine(new string(Convert.ToChar("-"), width));
        }
    }
}