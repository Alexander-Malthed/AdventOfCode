using System;
using System.IO;

class Day5
{
    static void Main(string[] args)
    {
        string[] input = File.ReadAllLines(@"D:/Code/input.txt");

        CleanInput(input);
        int[,] grid = new int[999, 999];
        DrawLines(input, grid);
        Console.WriteLine(CountOverlaps(grid));
    }

    static void CleanInput(string[] input)
    {
        for (int i = 0; i < input.Length; i++)
        {
            char[] separators = new char[] { ' ', '>' };
            string[] temp = input[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);
            input[i] = string.Join("", temp).Trim();
        }
    }

    static void DrawLines(string[] input, int[,] grid)
    {
        char[] separators = new char[] { ',', '-' };
        int x1, y1, x2, y2;
        int start, end;
        foreach (var line in input)
        {
            string[] startAndEnd = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            x1 = int.Parse(startAndEnd[0]);
            y1 = int.Parse(startAndEnd[1]);
            x2 = int.Parse(startAndEnd[2]);
            y2 = int.Parse(startAndEnd[3]);

            if (x1 == x2) // down or up
            {
                if (y1 > y2)
                {
                    start = y2;
                    end = y1;
                }
                else
                {
                    start = y1;
                    end = y2;
                }

                for (int i = start; i <= end; i++)
                {
                    grid[x1, i]++;
                }
            }
            else if (y1 == y2) // left or right
            {
                if (x1 > x2)
                {
                    start = x2;
                    end = x1;
                }
                else
                {
                    start = x1;
                    end = x2;
                }

                for (int i = start; i <= end; i++)
                {
                    grid[i, y1]++;
                }
            }
            else // diagonal
            {
                if (x1 > x2) // going left
                {
                    if (y1 > y2) // decending
                    {
                        int j = y1;
                        for (int i = x1; i >= x2; i--)
                        {
                            grid[i, j--]++;
                        }
                    }
                    else // acending
                    {
                        int j = y1;
                        for (int i = x1; i >= x2; i--)
                        {
                            grid[i, j++]++;
                        }
                    }
                }
                else // going right
                {
                    if (y1 > y2) // decending
                    {
                        int j = y1;
                        for (int i = x1; i <= x2; i++)
                        {
                            grid[i, j--]++;
                        }
                    }
                    else // acending
                    {
                        int j = y1;
                        for (int i = x1; i <= x2; i++)
                        {
                            grid[i, j++]++;
                        }
                    }
                }
            }
        }
    }

    static int CountOverlaps(int[,] grid)
    {
        int numOverlaps = 0;
        foreach (var point in grid)
        {
            if (point >= 2)
            {
                numOverlaps++;
            }
        }
        return numOverlaps;
    }
}