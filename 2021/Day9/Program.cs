using System;
using System.IO;
using System.Collections.Generic;

namespace Day_9
{
    class Program
    {
        static void Main(string[] args)
        {
            Part2();
        }

        static void Part1()
        {
            string[] input = File.ReadAllLines(@"D:/Code/Day_9.txt");
            int[,] grid = new int[input.Length, input[0].Length];

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    grid[i, j] = int.Parse(input[input.Length - 1 - i][j].ToString());
                }
            }

            int result = 0;
            int currentNum = 0;
            int gridXMax = grid.GetLength(0) - 1, gridYMax = grid.GetLength(1) - 1;

            for (int i = 0; i <= gridXMax; i++)
            {
                for (int j = 0; j <= gridYMax; j++)
                {
                    currentNum = grid[i, j];

                    if ((i + 1 > gridXMax || grid[i + 1, j] > currentNum) &&
                        (i - 1 < 0 || grid[i - 1, j] > currentNum) &&
                        (j + 1 > gridYMax || grid[i, j + 1] > currentNum) &&
                        (j - 1 < 0 || grid[i, j - 1] > currentNum))
                    {
                        result += currentNum + 1;
                    }
                }
            }


            Console.WriteLine(result);
        }

        static void Part2()
        {
            string[] input = File.ReadAllLines(@"D:/Code/Day_9.txt");
            int[,] grid = new int[input.Length, input[0].Length];

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    grid[i, j] = int.Parse(input[input.Length - 1 - i][j].ToString());
                }
            }

            int currentNum = 0;
            int currentX = 0, currentY = 0;
            int gridXMax = grid.GetLength(0) - 1, gridYMax = grid.GetLength(1) - 1;

            Dictionary<string, int> basins = new Dictionary<string, int>();

            for (int i = 0; i <= gridXMax; i++)
            {
                for (int j = 0; j <= gridYMax; j++)
                {
                    if (grid[i,j] == 9)
                    {
                        continue;
                    }

                    currentX = i;
                    currentY = j;
                    currentNum = grid[currentX, currentY];

                    while (true)
                    {
                        // if its a low point.
                        if ((currentX + 1 > gridXMax || grid[currentX + 1, currentY] > currentNum) &&
                            (currentX - 1 < 0 || grid[currentX - 1, currentY] > currentNum) &&
                            (currentY + 1 > gridYMax || grid[currentX, currentY + 1] > currentNum) &&
                            (currentY - 1 < 0 || grid[currentX, currentY - 1] > currentNum))
                        {
                            string key = currentY.ToString() + currentX.ToString();
                            if (basins.ContainsKey(key))
                            {
                                basins[key]++;
                            }
                            else
                            {
                                basins.Add(key, 1);
                            }
                            break;
                        }
                        // else find which direction to go.
                        else if (currentX + 1 <= gridXMax && grid[currentX + 1, currentY] < currentNum)
                        {
                            currentX++;
                        }
                        else if (currentX - 1 >= 0 && grid[currentX - 1, currentY] < currentNum)
                        {
                            currentX--;
                        }
                        else if (currentY + 1 <= gridYMax && grid[currentX, currentY + 1] < currentNum)
                        {
                            currentY++;
                        }
                        else if (currentY - 1 >= 0 && grid[currentX, currentY - 1] < currentNum)
                        {
                            currentY--;
                        }
                        currentNum = grid[currentX, currentY];
                    }
                }
            }
            int[] orderedBasins = new int[basins.Count];
            int itr = 0;
            foreach (var item in basins)
            {
                orderedBasins[itr++] = item.Value;
            }
            Array.Sort(orderedBasins, (a, b) => b.CompareTo(a));

            Console.WriteLine(orderedBasins[0] * orderedBasins[1] * orderedBasins[2]);
        }
    }
}
