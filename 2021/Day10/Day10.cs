using System;
using System.Collections.Generic;
using System.IO;

class Day10
{
    static void Main(string[] args)
    {
        //Part1();
        Part2();
    }

    static void Part1()
    {
        string[] input = File.ReadAllLines(@"D:/Code/input.txt");

        string chunkOrder = string.Empty;
        bool lineIsCorrupted = false;
        string syntaxErrors = string.Empty;

        for (int i = 0; i < input.Length; i++)
        {
            chunkOrder = string.Empty;
            foreach (char c in input[i])
            {
                switch (c)
                {
                    case '(':
                    case '[':
                    case '{':
                    case '<':
                        chunkOrder += c;
                        break;

                    case ')':
                        if (chunkOrder.Length > 0 && chunkOrder[chunkOrder.Length - 1] == '(')
                        {
                            chunkOrder = chunkOrder.Remove(chunkOrder.Length - 1);
                        }
                        else
                        {
                            Console.WriteLine($"Expected {GetOpposite(chunkOrder[chunkOrder.Length - 1])}, but found {c} instead.");
                            lineIsCorrupted = true;
                            syntaxErrors += c;
                        }
                        break;

                    case ']':
                        if (chunkOrder.Length > 0 && chunkOrder[chunkOrder.Length - 1] == '[')
                        {
                            chunkOrder = chunkOrder.Remove(chunkOrder.Length - 1);
                        }
                        else
                        {
                            Console.WriteLine($"Expected {GetOpposite(chunkOrder[chunkOrder.Length - 1])}, but found {c} instead.");
                            lineIsCorrupted = true;
                            syntaxErrors += c;
                        }
                        break;

                    case '}':
                        if (chunkOrder.Length > 0 && chunkOrder[chunkOrder.Length - 1] == '{')
                        {
                            chunkOrder = chunkOrder.Remove(chunkOrder.Length - 1);
                        }
                        else
                        {
                            Console.WriteLine($"Expected {GetOpposite(chunkOrder[chunkOrder.Length - 1])}, but found {c} instead.");
                            lineIsCorrupted = true;
                            syntaxErrors += c;
                        }
                        break;

                    case '>':
                        if (chunkOrder.Length > 0 && chunkOrder[chunkOrder.Length - 1] == '<')
                        {
                            chunkOrder = chunkOrder.Remove(chunkOrder.Length - 1);
                        }
                        else
                        {
                            Console.WriteLine($"Expected {GetOpposite(chunkOrder[chunkOrder.Length - 1])}, but found {c} instead.");
                            lineIsCorrupted = true;
                            syntaxErrors += c;
                        }
                        break;

                    default:
                        break;
                }

                if (lineIsCorrupted)
                {
                    lineIsCorrupted = false;
                    break;
                }
            }
        }

        int result = 0;
        foreach (char error in syntaxErrors)
        {
            switch (error)
            {
                case ')':
                    result += 3;
                    break;

                case ']':
                    result += 57;
                    break;

                case '}':
                    result += 1197;
                    break;

                case '>':
                    result += 25137;
                    break;

                default:
                    break;
            }
        }

        Console.WriteLine(result);
    }

    static void Part2()
    {
        string[] input = File.ReadAllLines(@"D:/Code/input.txt");

        List<ulong> scores = new List<ulong>();

        string chunkOrder = string.Empty;
        bool lineIsCorrupted = false;
        string syntaxErrors = string.Empty;

        for (int i = 0; i < input.Length; i++)
        {
            lineIsCorrupted = false;
            chunkOrder = string.Empty;
            foreach (char c in input[i])
            {
                switch (c)
                {
                    case '(':
                    case '[':
                    case '{':
                    case '<':
                        chunkOrder += c;
                        break;

                    case ')':
                        if (chunkOrder.Length > 0 && chunkOrder[chunkOrder.Length - 1] == '(')
                        {
                            chunkOrder = chunkOrder.Remove(chunkOrder.Length - 1);
                        }
                        else
                        {
                            lineIsCorrupted = true;
                            syntaxErrors += c;
                        }
                        break;

                    case ']':
                        if (chunkOrder.Length > 0 && chunkOrder[chunkOrder.Length - 1] == '[')
                        {
                            chunkOrder = chunkOrder.Remove(chunkOrder.Length - 1);
                        }
                        else
                        {
                            lineIsCorrupted = true;
                            syntaxErrors += c;
                        }
                        break;

                    case '}':
                        if (chunkOrder.Length > 0 && chunkOrder[chunkOrder.Length - 1] == '{')
                        {
                            chunkOrder = chunkOrder.Remove(chunkOrder.Length - 1);
                        }
                        else
                        {
                            lineIsCorrupted = true;
                            syntaxErrors += c;
                        }
                        break;

                    case '>':
                        if (chunkOrder.Length > 0 && chunkOrder[chunkOrder.Length - 1] == '<')
                        {
                            chunkOrder = chunkOrder.Remove(chunkOrder.Length - 1);
                        }
                        else
                        {
                            lineIsCorrupted = true;
                            syntaxErrors += c;
                        }
                        break;

                    default:
                        break;
                }

                if (lineIsCorrupted)
                {
                    break;
                }
            }

            if (!lineIsCorrupted)
            {
                ulong score = 0;
                for (int j = chunkOrder.Length - 1; j >= 0; j--)
                {
                    score *= 5;
                    switch (chunkOrder[j])
                    {
                        case '(':
                            score++;
                            break;

                        case '[':
                            score += 2;
                            break;

                        case '{':
                            score += 3;
                            break;

                        case '<':
                            score += 4;
                            break;

                        default:
                            break;
                    }
                }
                scores.Add(score);
            }
        }

        scores.Sort();
        ulong middleScore = scores[scores.Count / 2];
        Console.WriteLine("Middlescore: " + middleScore);
    }

    static char GetOpposite(char c)
    {
        switch (c)
        {
            case '(':
                return ')';
            case ')':
                return '(';
            case '[':
                return ']';
            case ']':
                return '[';
            case '{':
                return '}';
            case '}':
                return '{';
            case '<':
                return '>';
            case '>':
                return '<';
            default:
                return '?';
        }
    }
}