using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string[] input = File.ReadAllLines("D:/Code/Day_2.txt");
       
        //Console.WriteLine(Part1(input));
        Console.WriteLine(Part2(input));
        
        Console.ReadKey();
    }

    static int Part1(string[] input)
    {
        int x = 0, y = 0;

        for (int i = 0; i < input.Length; i++)
        {
            var cmd = input[i].Split(" ");
            int mag = int.Parse(cmd[1]);

            switch (cmd[0][0])
            {
                case 'f':
                    x += mag;
                    break;

                case 'd':
                    y += mag;
                    break;

                case 'u':
                    y -= mag;
                    break;

                default:
                    break;
            }
        }
        return x * y;
    }

    static int Part2(string[] input)
    {
        int x = 0, y = 0;
        int aim = 0;

        for (int i = 0; i < input.Length; i++)
        {
            var cmd = input[i].Split(" ");
            int mag = int.Parse(cmd[1]);

            switch (cmd[0][0])
            {
                case 'f':
                    x += mag;
                    y += aim * mag;
                    break;

                case 'd':
                    aim += mag;
                    break;

                case 'u':
                    aim -= mag;
                    break;

                default:
                    break;
            }
        }
        return x * y;
    }
}