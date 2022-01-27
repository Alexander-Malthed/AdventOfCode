using System;
using System.IO;

namespace Day_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = Array.ConvertAll(File.ReadAllLines("D:/Code/Day_1.txt"), s => int.Parse(s));
            int result = 0;

            for (int i = 0; i < input.Length-3; i++)
            {
                if (input[i + 3] > input[i])
                {
                    result++;
                }
            }

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
