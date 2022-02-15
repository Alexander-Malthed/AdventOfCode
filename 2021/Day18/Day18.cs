using System.IO;
using System;
using System.Diagnostics;

namespace Day18
{
    class Day18
    {
        static Heap<Pair> explosionOrder = new Heap<Pair>(100);
        static Heap<RegularNumber> splitOrder = new Heap<RegularNumber>(100);

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

        static Pair Part1(string[] input)
        {
            Pair resultPair = new Pair(input[0]);

            for (int i = 1; i < input.Length; i++)
            {
                resultPair = ReducePair(new Pair(resultPair, new Pair(input[i])));
            }

            return resultPair;
        }

        static Pair Part2(string[] input)
        {
            Pair curResultPair, bestResultPair = new Pair(input[0]);
            int bestMagnitude = 0;

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    curResultPair = ReducePair(new Pair(new Pair(input[i]), new Pair(input[j])));
                    int magnitude = GetMagnitudeOfFinalSum(curResultPair);

                    if (magnitude > bestMagnitude)
                    {
                        bestMagnitude = magnitude;
                        bestResultPair = curResultPair;
                    }

                    curResultPair = ReducePair(new Pair(new Pair(input[j]), new Pair(input[i])));
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

        static Pair ReducePair(Pair pairToReduce)
        {
            explosionOrder = new Heap<Pair>(100);
            splitOrder = new Heap<RegularNumber>(100);

            FindAllPairsToExplode(pairToReduce, 4);

            while (explosionOrder.Count > 0 || splitOrder.Count > 0)
            {
                if (explosionOrder.Count > 0)
                {
                    Pair pairToExplode = explosionOrder.RemoveFirst();

                    //Console.WriteLine($"exploded [{((RegularNumber)pairToExplode.LeftChild).Value},{((RegularNumber)pairToExplode.RightChild).Value}]");
                    ExplodePair(pairToExplode);
                    //PrintString(pairToReduce);
                    continue;
                }

                if (splitOrder.Count > 0)
                {
                    RegularNumber numberToSplit = splitOrder.RemoveFirst();

                    if (!numberToSplit.Valid)
                    {
                        continue;
                    }

                    //Console.WriteLine($"splitted [{numberToSplit.Value}]");
                    SplitRegularNumber(numberToSplit);
                    //PrintString(pairToReduce);
                }
            }

            return pairToReduce;
        }

        #region Actions
        // Add the left value of the pair into the immediate number to the left.
        // Add the right value of the pair into the immediate number to the right.
        // Replace pair with a RegularNumber with the value 0.
        // Exploding [1,2] in [3,[[1,2],4]] results in [4,[0,6]].
        static void ExplodePair(Pair explodingPair)
        {
            // Add to right number. Check if that number should be split.
            RegularNumber closestNumber = FindClosestRegularNumberRight(explodingPair);
            if (closestNumber != null)
            {
                closestNumber.Value += ((RegularNumber)explodingPair.RightChild).Value;

                if (closestNumber.Value > 9)
                {
                    closestNumber.UpdateDistance();

                    if (!splitOrder.Contains(closestNumber))
                    {
                        splitOrder.Add(closestNumber);
                    }
                }
            }

            // Add to left number. Check if that number should be split.
            closestNumber = FindClosestRegularNumberLeft(explodingPair);
            if (closestNumber != null)
            {
                closestNumber.Value += ((RegularNumber)explodingPair.LeftChild).Value;

                if (closestNumber.Value > 9)
                {
                    closestNumber.UpdateDistance();

                    if (!splitOrder.Contains(closestNumber))
                    {
                        splitOrder.Add(closestNumber);
                    }
                }
            }

            // Replace the pair with a 0.
            new RegularNumber(0, explodingPair.ParentPair, explodingPair.ChildState);

            explodingPair.RightChild.Valid = false;
            explodingPair.LeftChild.Valid = false;
            explodingPair = null;
        }

        // Numbers above 9 gets split into a pair following the rule [floor(n/2), ceil(n/2)].
        // 15 becomes [7,8].
        static void SplitRegularNumber(RegularNumber numberToExplode)
        {
            // Create pair.
            int leftValue = (int)Math.Floor((float)numberToExplode.Value / 2);
            int rightValue = (int)Math.Ceiling((float)numberToExplode.Value / 2);
            Pair newPair = new Pair(leftValue, rightValue, numberToExplode.ParentPair, numberToExplode.ChildState);

            // Check if the new pair should explode.
            Pair parent = newPair.ParentPair;
            int numOfParents = 1;
            while (parent.ParentPair != null)
            {
                parent = parent.ParentPair;
                if (++numOfParents == 4)
                {
                    newPair.UpdateDistance();

                    if (!explosionOrder.Contains(newPair))
                    {
                        explosionOrder.Add(newPair);
                    }

                    break;
                }
            }

            // Check if the new numbers should be split.
            RegularNumber leftNumber = (RegularNumber)newPair.LeftChild;
            RegularNumber rightNumber = (RegularNumber)newPair.RightChild;

            if (leftNumber.Value > 9)
            {
                splitOrder.Add(leftNumber);
            }

            if (rightNumber.Value > 9)
            {
                splitOrder.Add(rightNumber);
            }
        }
        #endregion

        #region Find
        #region FindClosestNumber
        static RegularNumber FindClosestRegularNumberRight(Pair explodingPair)
        {
            Pair parent = explodingPair.ParentPair;
            Pair curSearchPair;

            if (explodingPair.ChildState == ChildState.LEFTCHILD)
            {
                if (parent.RightChild is RegularNumber)
                {
                    return (RegularNumber)parent.RightChild;
                }

                curSearchPair = (Pair)parent.RightChild;
            }
            else
            {
                while (true)
                {
                    switch (parent.ChildState)
                    {
                        case ChildState.NONE:
                            return null;

                        case ChildState.LEFTCHILD:
                            break;

                        case ChildState.RIGHTCHILD:
                            parent = parent.ParentPair;
                            continue;

                        default:
                            break;
                    }

                    break;
                }

                if (parent.ParentPair.RightChild is RegularNumber)
                {
                    return (RegularNumber)parent.ParentPair.RightChild;
                }

                curSearchPair = (Pair)parent.ParentPair.RightChild;
            }

            while (curSearchPair.LeftChild is Pair)
            {
                curSearchPair = (Pair)curSearchPair.LeftChild;
            }

            return (RegularNumber)curSearchPair.LeftChild;
        }

        static RegularNumber FindClosestRegularNumberLeft(Pair explodingPair)
        {
            Pair parent = explodingPair.ParentPair;
            Pair curSearchPair;

            if (explodingPair.ChildState == ChildState.RIGHTCHILD)
            {
                if (parent.LeftChild is RegularNumber)
                {
                    return (RegularNumber)parent.LeftChild;
                }

                curSearchPair = (Pair)parent.LeftChild;
            }
            else
            {
                while (true)
                {
                    switch (parent.ChildState)
                    {
                        case ChildState.NONE:
                            return null;

                        case ChildState.LEFTCHILD:
                            parent = parent.ParentPair;
                            continue;

                        case ChildState.RIGHTCHILD:
                            break;

                        default:
                            break;
                    }

                    break;
                }

                if (parent.ParentPair.LeftChild is RegularNumber)
                {
                    return (RegularNumber)parent.ParentPair.LeftChild;
                }

                curSearchPair = (Pair)parent.ParentPair.LeftChild;
            }

            while (curSearchPair.RightChild is Pair)
            {
                curSearchPair = (Pair)curSearchPair.RightChild;
            }

            return (RegularNumber)curSearchPair.RightChild;
        }
        #endregion

        static void FindAllPairsToExplode(Pair pair, int depth)
        {
            if (depth <= 0)
            {
                if (pair.LeftChild is RegularNumber && pair.RightChild is RegularNumber)
                {
                    pair.UpdateDistance();
                    explosionOrder.Add(pair);
                    return;
                }
            }

            if (pair.RightChild is Pair)
            {
                FindAllPairsToExplode((Pair)pair.RightChild, depth - 1);
            }

            if (pair.LeftChild is Pair)
            {
                FindAllPairsToExplode((Pair)pair.LeftChild, depth - 1);
            }
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
            if (number is Pair)
            {
                if (explosionOrder.Contains((Pair)number))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            bool isLeftChild = number.ChildState == ChildState.LEFTCHILD;
            if (isLeftChild)
            {
                Console.Write("[");
            }
            if (number is RegularNumber)
            {
                ConsoleColor temp = Console.ForegroundColor;
                if (splitOrder.Contains((RegularNumber)number))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.Write(((RegularNumber)number).Value);
                Console.ForegroundColor = temp;


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
            Console.ForegroundColor = ConsoleColor.White;
        }
        #endregion
    }
}