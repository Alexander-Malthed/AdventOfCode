using System.IO;
using System;
using System.Diagnostics;

namespace Day18
{
    class Day18
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            string[] input = File.ReadAllLines("D:/AdventOfCode/2021/Day18/input.txt");

            Pair resultPair;
            
            // Add all lines one by one and reduce between each addition.
            //resultPair = Part1(input);

            // What is the largest magnitude of any sum of two different lines?
            resultPair = Part2(input);
            PrintString(resultPair);

            int result = GetMagnitudeOfFinalSum(resultPair);
            Console.WriteLine("Magnitude: " + result);
            File.WriteAllText("D:/AdventOfCode/2021/Day18/result.txt", $"Result: {result}\nTime: {sw.ElapsedMilliseconds}ms");
            
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds + "ms");
        }

        #region Puzzle Parts
        static Pair Part1(string[] input)
        {
            Heap<Pair> explosionOrder = new Heap<Pair>(100);
            Heap<RegularNumber> splitOrder = new Heap<RegularNumber>(100);
            Reducer.SetExplosionAndSplitOrder(explosionOrder, splitOrder);

            Pair resultPair = new Pair(input[0]);

            for (int i = 1; i < input.Length; i++)
            {
                resultPair = Reducer.ReducePair(new Pair(resultPair, new Pair(input[i])));
            }

            return resultPair;
        }

        static Pair Part2(string[] input)
        {
            Heap<Pair> explosionOrder = new Heap<Pair>(100);
            Heap<RegularNumber> splitOrder = new Heap<RegularNumber>(100);
            Reducer.SetExplosionAndSplitOrder(explosionOrder, splitOrder);

            Pair curResultPair, bestResultPair = new Pair(input[0]);
            int bestMagnitude = 0;

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    curResultPair = Reducer.ReducePair(new Pair(new Pair(input[i]), new Pair(input[j])));
                    int magnitude = GetMagnitudeOfFinalSum(curResultPair);

                    if (magnitude > bestMagnitude)
                    {
                        bestMagnitude = magnitude;
                        bestResultPair = curResultPair;
                    }

                    curResultPair = Reducer.ReducePair(new Pair(new Pair(input[j]), new Pair(input[i])));
                    magnitude = GetMagnitudeOfFinalSum(curResultPair);

                    if (magnitude > bestMagnitude)
                    {
                        bestMagnitude = magnitude;
                        bestResultPair = curResultPair;
                    }
                }
            }

            return bestResultPair;
        }
        #endregion

        #region Magnitude
        static int GetMagnitudeOfFinalSum(Pair outerPair)
        {
            int leftValue = GetMagnitudeOfNumber(outerPair.LeftChild) * 3;
            int rightValue = GetMagnitudeOfNumber(outerPair.RightChild) * 2;

            return leftValue + rightValue;
        }

        static int GetMagnitudeOfNumber(Number number)
        {
            if (number is RegularNumber)
            {
                return ((RegularNumber)number).Value;
            }

            int leftValue = GetMagnitudeOfNumber(((Pair)number).LeftChild) * 3;
            int rightValue = GetMagnitudeOfNumber(((Pair)number).RightChild) * 2;

            return leftValue + rightValue;
        }
        #endregion

        #region Print
        // Since there is no string, it needs to be recreated based on the tree of pairs and numbers.
        static void PrintString(Pair outerPair)
        {
            PrintNumber(outerPair.LeftChild, 4);
            PrintNumber(outerPair.RightChild, 4);
            Console.Write("\n\n");
        }

        static void PrintNumber(Number number, int depth)
        {
            bool isLeftChild = number.ChildState == ChildState.LEFTCHILD;
            if (isLeftChild)
            {
                Console.Write("[");
            }

            if (number is RegularNumber)
            {
                Console.Write(((RegularNumber)number).Value);

                if (isLeftChild)
                {
                    Console.Write(",");
                    return;
                }
            }
            else
            {
                PrintNumber(((Pair)number).LeftChild, depth - 1);
                PrintNumber(((Pair)number).RightChild, depth - 1);
            }

            if (isLeftChild)
            {
                Console.Write(",");
            }
            else
            {
                Console.Write("]");
            }
        }
        #endregion
    }
}