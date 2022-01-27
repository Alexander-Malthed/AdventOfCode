using System;
using System.IO;

namespace Day_6
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllText(@"D:/Code/Day_6.txt").Split(',');
            ulong[] fish = new ulong[9];

            // count starting numbers
            foreach (var value in input)
                fish[int.Parse(value)]++;

            for (int i = 0; i < fish.Length; i++)
            {
                Console.WriteLine($"number of {i}: " + fish[i]);
            }
            Console.WriteLine("--------------");
            ulong temp1, temp2;
            int placeIndex;
            for (ulong i = 0; i < 80; i++)
            {
                placeIndex = 7;
                temp1 = fish[8];
                while (placeIndex > 0)
                {
                    temp2 = fish[placeIndex];
                    fish[placeIndex] = temp1;
                    placeIndex--;

                    temp1 = fish[placeIndex];
                    fish[placeIndex] = temp2;
                    placeIndex--;
                }

                fish[8] = temp1;
                fish[6] += temp1;
            }

            ulong result = 0;
            for (int i = 0; i < fish.Length; i++)
            {
                Console.WriteLine($"number of {i}: " + fish[i]);
                result += fish[i];
            }
            Console.WriteLine("Total fish: " + result);
        }
    }
}
