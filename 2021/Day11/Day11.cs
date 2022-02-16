using System;
using System.IO;

class Day11
{
    static int flashes = 0;
    static int[,] grid;
    static bool[,] hasFlashedThisTurn;
    static int xMax = 0;
    static int yMax = 0;

    static void Main(string[] args)
    {
        string[] input = File.ReadAllLines(@"D:/AdventOfCode/2021/Day11/input.txt");

        grid = new int[input[0].Length, input.Length];
        hasFlashedThisTurn = new bool[input[0].Length, input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[0].Length; j++)
            {
                grid[i, j] = int.Parse(input[input.Length - 1 - j][i].ToString());
            }
        }

        xMax = grid.GetLength(0) - 1;
        yMax = grid.GetLength(1) - 1;

        int numberOfFlashes = 0;
        bool flashesInSync = false;
        while (!flashesInSync)
        {
            numberOfFlashes++;

            // Increment and flash
            for (int i = 0; i <= xMax; i++)
            {
                for (int j = 0; j <= yMax; j++)
                {
                    if (++grid[i, j] > 9)
                    {
                        Flash(i, j);
                    }
                }
            }

            flashesInSync = true;
            // Reset before next flash
            for (int i = 0; i <= xMax; i++)
            {
                for (int j = 0; j <= yMax; j++)
                {
                    if (grid[i, j] > 9)
                    {
                        grid[i, j] = 0;
                        hasFlashedThisTurn[i, j] = false;
                    }
                    else
                    {
                        flashesInSync = false;
                    }
                }
            }
        }

        //PrintGrid();
        File.WriteAllText("D:/AdventOfCode/2021/Day11/result.txt", numberOfFlashes.ToString());
        Console.WriteLine("First step in sync: " + numberOfFlashes);
    }

    static void PrintGrid()
    {
        for (int i = 0; i < grid.GetLength(1); i++)
        {
            for (int j = 0; j < grid.GetLength(0); j++)
            {
                Console.Write(grid[j, grid.GetLength(1) - 1 - i]);
            }
            Console.Write("\n");
        }
    }

    static void Flash(int x, int y)
    {
        if (hasFlashedThisTurn[x, y])
            return;

        hasFlashedThisTurn[x, y] = true;
        flashes++;

        // right
        if (x + 1 <= xMax)
        {
            if (++grid[x + 1, y] > 9)
            {
                Flash(x + 1, y);
            }

            // right up
            if (y + 1 <= yMax)
            {
                if (++grid[x + 1, y + 1] > 9)
                {
                    Flash(x + 1, y + 1);
                }
            }

            // right down
            if (y - 1 >= 0)
            {
                if (++grid[x + 1, y - 1] > 9)
                {
                    Flash(x + 1, y - 1);
                }
            }
        }

        // left
        if (x - 1 >= 0)
        {
            if (++grid[x - 1, y] > 9)
            {
                Flash(x - 1, y);
            }

            // left up
            if (y + 1 <= yMax)
            {
                if (++grid[x - 1, y + 1] > 9)
                {
                    Flash(x - 1, y + 1);
                }
            }

            // left down
            if (y - 1 >= 0)
            {
                if (++grid[x - 1, y - 1] > 9)
                {
                    Flash(x - 1, y - 1);
                }
            }
        }

        // up
        if (y + 1 <= yMax)
        {
            if (++grid[x, y + 1] > 9)
            {
                Flash(x, y + 1);
            }
        }

        // down
        if (y - 1 >= 0)
        {
            if (++grid[x, y - 1] > 9)
            {
                Flash(x, y - 1);
            }
        }
    }
}