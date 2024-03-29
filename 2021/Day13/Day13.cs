﻿using System;
using System.Collections.Generic;
using System.IO;

class Day13
{
    static void Main(string[] args)
    {
        string[] input = File.ReadAllLines(@"D:/AdventOfCode/2021/Day13/input.txt");
        List<int[]> dots = new List<int[]>();
        List<Fold> folds = new List<Fold>();

        foreach (var line in input)
        {
            if (line == string.Empty)
                continue;

            if (line[0] != 'f')
            {
                dots.Add(Array.ConvertAll(line.Split(','), int.Parse));
            }
            else
            {
                string[] fold = line.Replace("fold along ", string.Empty).Split('=');
                folds.Add(new Fold(fold[0][0], int.Parse(fold[1])));
            }
        }

        foreach (var fold in folds)
        {
            if (fold.direction == 'x')
            {
                foreach (var dot in dots)
                {
                    if (dot[0] < fold.line)
                        continue;

                    dot[0] = fold.line - (dot[0] - fold.line);
                }
            }
            else
            {
                foreach (var dot in dots)
                {
                    if (dot[1] < fold.line)
                        continue;

                    dot[1] = fold.line - (dot[1] - fold.line);
                }
            }
        }

        // remove duplicates
        for (int i = dots.Count - 1; i >= 0; i--)
        {
            bool duplicate = false;
            for (int j = i - 1; j >= 0; j--)
            {
                if (dots[i][0] == dots[j][0] && dots[i][1] == dots[j][1])
                {
                    duplicate = true;
                    break;
                }
            }

            if (duplicate)
            {
                dots.RemoveAt(i);
            }
        }

        char[,] paper = new char[40, 6];
        for (int i = 0; i < paper.GetLength(0); i++)
        {
            for (int j = 0; j < paper.GetLength(1); j++)
            {
                paper[i, j] = ' ';
            }
        }
        foreach (var dot in dots)
        {
            paper[dot[0], dot[1]] = '#';
        }

        string result = string.Empty;
        for (int i = 0; i < paper.GetLength(1); i++)
        {
            for (int j = 0; j < paper.GetLength(0); j++)
            {
                result += paper[j, i];
            }
            result += "\n";
        }

        File.WriteAllText("D:/AdventOfCode/2021/Day13/result.txt", result);
        Console.WriteLine(result);
    }
}

struct Fold
{
    public char direction;
    public int line;

    public Fold(char dir, int line)
    {
        direction = dir;
        this.line = line;
    }
}