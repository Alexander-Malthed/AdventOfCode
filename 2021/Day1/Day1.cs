using System;
using System.IO;

class Day1
{
    static void Main(string[] args)
    {
        int[] input = Array.ConvertAll(File.ReadAllLines("D:/Code/input.txt"), s => int.Parse(s));
        int result = 0;

        for (int i = 0; i < input.Length - 3; i++)
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