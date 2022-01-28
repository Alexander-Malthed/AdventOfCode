using System;
using System.IO;

class Day7
{
    static void Main(string[] args)
    {
        //Part1();
        Part2();
    }

    #region Part1
    static void Part1()
    {
        int[] input = Array.ConvertAll(File.ReadAllText(@"D:/Code/input.txt").Split(','), int.Parse);
        Array.Sort(input);

        int totalFuel = 0;
        int lowest = input[0];
        int middleValue = 0;
        int highest = input[input.Length - 1];
        int belowTotal, aboveTotal;
        int middleIndex = 0;

        bool calculating = true;
        while (calculating)
        {
            middleValue = (lowest + highest + 1) / 2;
            CalcTotals(input, middleValue, out middleIndex, out belowTotal, out aboveTotal);

            if (belowTotal < aboveTotal)
            {
                SetNumbers(input, middleValue, 0, middleIndex);
                lowest = middleValue;
                totalFuel += belowTotal;
            }
            else
            {
                SetNumbers(input, middleValue, middleIndex, input.Length);
                highest = middleValue;
                totalFuel += aboveTotal;
            }

            if (highest - lowest == 1)
            {
                totalFuel += FindFewestOfTwoAndComplete(input);
                calculating = false;
            }
        }

        //Console.WriteLine("Number: " + input[0]);
        //Console.WriteLine("Fuel: " + totalFuel);
    }

    static void CalcTotals(int[] input, int middleValue, out int middleIndex, out int belowTotal, out int aboveTotal)
    {
        belowTotal = 0;
        aboveTotal = 0;
        middleIndex = 0;

        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] < middleValue)
            {
                belowTotal += middleValue - input[i];
            }
            else
            {
                middleIndex = i;
                for (int j = middleIndex; j < input.Length; j++)
                {
                    aboveTotal += input[j] - middleValue;
                }
                break;
            }
        }
    }

    static void SetNumbers(int[] input, int newValue, int startIndex, int stopIndex)
    {
        for (int i = startIndex; i < stopIndex; i++)
        {
            input[i] = newValue;
        }
    }

    /// <summary>
    /// When the difference between lowest and highest are 1, this method will complete the array (all elements are the same number).
    /// Returns the fuel cost to move all of the fewer numbers one step.
    /// </summary>
    static int FindFewestOfTwoAndComplete(int[] input)
    {
        int lowNumber = input[0], highNumber = 0;
        int numberOfLowNumbers = 1, numberOfHighNumbers = 0;
        int middleIndex = 0;

        for (int i = 1; i < input.Length; i++)
        {
            if (input[i] == lowNumber)
            {
                numberOfLowNumbers++;
            }
            else
            {
                numberOfHighNumbers = input.Length - i;
                middleIndex = i;
                highNumber = input[i];
                break;
            }
        }

        if (numberOfLowNumbers < numberOfHighNumbers)
        {
            SetNumbers(input, highNumber, 0, middleIndex);
            return numberOfLowNumbers;
        }
        else
        {
            SetNumbers(input, lowNumber, middleIndex, input.Length);
            return numberOfHighNumbers;
        }
    }
    #endregion

    static void Part2()
    {
        int[] input = Array.ConvertAll(File.ReadAllText(@"D:/Code/input.txt").Split(','), int.Parse);
        Array.Sort(input);

        int totalFuel = 0;
        int cheapestFuel = int.MaxValue;
        int cheapestValue = 0;

        for (int i = 0; i < input[input.Length - 1]; i++)
        {
            totalFuel = 0;
            for (int j = 0; j < input.Length; j++)
            {
                int n = Math.Abs(input[j] - i);
                totalFuel += n;
                //totalFuel += (n * n + n) / 2;
            }

            if (totalFuel < cheapestFuel)
            {
                cheapestValue = i;
                cheapestFuel = totalFuel;
            }
        }

        //Console.WriteLine("Index: " + cheapestValue);
        //Console.WriteLine("Fuel: " + cheapestFuel);
    }
}