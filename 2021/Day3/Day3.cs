using System;
using System.IO;
using System.Collections.Generic;

class Day3
{
    static void Main(string[] args)
    {
        //Part1();
        Part2();
    }

    static void Part1()
    {
        string[] input = File.ReadAllLines("D:/Code/Day_3.txt");

        string gamma = "";
        string epsilon = "";
        int numOfZeros;

        for (int i = 0; i < input[0].Length; i++)
        {
            numOfZeros = 0;
            for (int j = 0; j < input.Length; j++)
            {
                numOfZeros += input[j][i] == '0' ? 1 : 0;
            }

            if (numOfZeros > input.Length / 2)
            {
                gamma += '0';
                epsilon += '1';
            }
            else
            {
                gamma += '1';
                epsilon += '0';
            }
        }

        Console.WriteLine(Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2));
    }

    static void Part2()
    {
        string[] input = File.ReadAllLines("D:/Code/Day_3.txt");

        List<int> validsIndices = new List<int>();
        int oxygen = 0, co2 = 0;
        int numOfZeros, numOfOnes;
        bool findingOxygen = true;
        char removeChar;

        for (int i = 0; i < 2; i++)
        {
            validsIndices.Clear();
            for (int j = 0; j < input.Length; j++)
            {
                validsIndices.Add(j);
            }

            for (int j = 0; j < input[0].Length; j++)
            {
                numOfZeros = 0;
                numOfOnes = 0;

                foreach (var index in validsIndices)
                {
                    if (input[index][j] == '0')
                    {
                        ++numOfZeros;
                    }
                    else
                    {
                        ++numOfOnes;
                    }
                }

                if (findingOxygen)
                {
                    removeChar = numOfZeros > numOfOnes ? '1' : '0';
                }
                else
                {
                    removeChar = numOfZeros > numOfOnes ? '0' : '1';
                }

                for (int k = validsIndices.Count - 1; k >= 0; k--)
                {
                    if (input[validsIndices[k]][j] == removeChar)
                    {
                        validsIndices.RemoveAt(k);
                        if (validsIndices.Count == 1)
                        {
                            if (findingOxygen)
                            {
                                oxygen = Convert.ToInt32(input[validsIndices[0]], 2);
                                break;
                            }
                            else
                            {
                                co2 = Convert.ToInt32(input[validsIndices[0]], 2);
                                break;
                            }
                        }
                    }
                }
            }

            findingOxygen = false;
        }

        Console.WriteLine(oxygen * co2);
    }
}
