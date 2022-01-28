using System;
using System.IO;

class Day4
{
    static void Main(string[] args)
    {
        string[] input = File.ReadAllLines("D:/Code/input.txt");

        int[] numbers = Array.ConvertAll(input[0].Split(','), element => int.Parse(element));
        CleanInput(input);
        int[,,] boards = GetBoards(input);
        bool[,,] markedBoards = new bool[boards.GetLength(0), boards.GetLength(1), boards.GetLength(2)];

        int num, itr = 0;
        bool bingo = false;
        int bingoIndex = 0, bingoNumber = 0;
        bool[] bingoBoards = new bool[boards.GetLength(0)];
        while (itr < numbers.Length && !bingo)
        {
            num = numbers[itr];

            for (int i = 0; i < boards.GetLength(0); i++)
            {
                if (bingoBoards[i])
                {
                    continue;
                }
                for (int j = 0; j < boards.GetLength(1); j++)
                {
                    for (int k = 0; k < boards.GetLength(2); k++)
                    {
                        if (boards[i, j, k] == num)
                        {
                            markedBoards[i, j, k] = true;
                            bool bingoCheck = true;
                            for (int l = 0; l < boards.GetLength(1); l++)
                            {
                                if (!markedBoards[i, l, k])
                                {
                                    bingoCheck = false;
                                    break;
                                }
                            }
                            if (bingoCheck)
                            {
                                bingoIndex = i;
                                bingoNumber = num;
                                bingoBoards[i] = true;
                                if (Array.TrueForAll(bingoBoards, b => b))
                                {
                                    bingo = true;
                                    Console.WriteLine($"Board {i} is the last to win.");
                                }
                            }

                            bingoCheck = true;
                            for (int l = 0; l < boards.GetLength(2); l++)
                            {
                                if (!markedBoards[i, j, l])
                                {
                                    bingoCheck = false;
                                    break;
                                }
                            }
                            if (bingoCheck)
                            {
                                bingoIndex = i;
                                bingoNumber = num;
                                bingoBoards[i] = true;
                                if (Array.TrueForAll(bingoBoards, b => b))
                                {
                                    bingo = true;
                                    Console.WriteLine($"Board {i} is the last to win.");
                                }
                            }
                        }
                    }
                }
            }
            itr++;
        }

        PrintBoards(markedBoards);

        int result = 0;
        for (int i = 0; i < markedBoards.GetLength(1); i++)
        {
            for (int j = 0; j < markedBoards.GetLength(2); j++)
            {
                if (!markedBoards[bingoIndex, i, j])
                {
                    result += boards[bingoIndex, i, j];
                }
            }
        }
        Console.WriteLine(result * bingoNumber);
    }

    static void CleanInput(string[] input)
    {
        for (int i = 0; i < (input.Length - 1) / 6; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                int itr = 0;

                for (int k = 0; k < 5; k++)
                {
                    if (input[2 + i * 6 + j][itr] == ' ')
                    {
                        input[2 + i * 6 + j] = input[2 + i * 6 + j].Remove(itr, 1);
                        itr += 2;
                    }
                    else
                    {
                        itr += 3;
                    }
                }
            }
        }
    }

    static int[,,] GetBoards(string[] input)
    {
        int[,,] boards = new int[(input.Length - 1) / 6, 5, 5];

        for (int i = 0; i < boards.GetLength(0); i++)
        {
            for (int j = 0; j < boards.GetLength(1); j++)
            {
                for (int k = 0; k < boards.GetLength(2); k++)
                {
                    boards[i, j, k] = int.Parse(input[2 + i * 6 + j].Split(' ')[k]);
                }
            }
        }
        return boards;
    }

    static void PrintBoards(int[,,] boards)
    {
        for (int i = 0; i < boards.GetLength(0); i++)
        {
            for (int j = 0; j < boards.GetLength(1); j++)
            {
                for (int k = 0; k < boards.GetLength(2); k++)
                {
                    Console.Write(boards[i, j, k] + " ");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }
    }

    static void PrintBoards(bool[,,] boards)
    {
        for (int i = 0; i < boards.GetLength(0); i++)
        {
            Console.WriteLine($"Board: {i}");
            for (int j = 0; j < boards.GetLength(1); j++)
            {
                for (int k = 0; k < boards.GetLength(2); k++)
                {
                    Console.Write(boards[i, j, k] + " ");
                }
                Console.Write("\n");
            }
            Console.Write("\n");
        }
    }
}