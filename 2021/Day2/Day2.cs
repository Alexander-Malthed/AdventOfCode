using System;
using System.IO;

class Day2
{
    static void Main(string[] args)
    {
        string[] input = File.ReadAllLines("D:/AdventOfCode/2021/Day2/input.txt");

        int result;
        //result = Part1(input);
        result = Part2(input);

        File.WriteAllText("D:/AdventOfCode/2021/Day2/result.txt", result.ToString());
        Console.WriteLine(result);
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