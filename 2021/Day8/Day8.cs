using System;
using System.IO;

class Day8
{
    static void Main(string[] args)
    {
        //Part1();
        Part2();
    }

    //  aaaa
    // b    c
    // b    c
    //  dddd
    // e    f
    // e    f
    //  gggg

    static void Part1()
    {
        string[] input = File.ReadAllLines(@"D:/AdventOfCode/2021/Day8/input.txt");

        int numOf1 = 0;
        int numOf4 = 0;
        int numOf7 = 0;
        int numOf8 = 0;

        for (int i = 0; i < input.Length; i++)
        {
            string[] segments = input[i].Split('|')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < segments.Length; j++)
            {
                switch (segments[j].Length)
                {
                    case 2: // number 1
                        numOf1++;
                        break;

                    case 4: // number 4
                        numOf4++;
                        break;

                    case 3: // number 7
                        numOf7++;
                        break;

                    case 7: // number 8
                        numOf8++;
                        break;

                    default:
                        break;
                }
            }
        }

        int result = numOf1 + numOf4 + numOf7 + numOf8;
        File.WriteAllText("D:/AdventOfCode/2021/Day8/result.txt", result.ToString());
        Console.WriteLine(result);
    }

    static void Part2()
    {
        string[] input = File.ReadAllLines(@"D:/AdventOfCode/2021/Day8/input.txt");

        int result = 0;

        for (int i = 0; i < input.Length; i++)
        {
            string[] leftAndRight = input[i].Split('|');
            string[] leftNumbers = leftAndRight[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] rightNumbers = leftAndRight[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            Array.Sort(leftNumbers, (x, y) => x.Length.CompareTo(y.Length));

            // Array order: 1, 7, 4, 235, 235, 235, 069, 069, 069, 8
            string foundLetters = "0000000";
            int number3Index = -1;

            // Find segment c and f using number 1 and 6. It only has one of the two, others have both.
            char s1 = leftNumbers[0][0];
            char s2 = leftNumbers[0][1];

            for (int j = 6; j <= 8; j++)
            {
                if (!leftNumbers[j].Contains(s1))
                {
                    foundLetters = foundLetters.Remove(2, 1).Insert(2, s1.ToString());
                    foundLetters = foundLetters.Remove(5, 1).Insert(5, s2.ToString());
                    break;
                }
                else if (!leftNumbers[j].Contains(s2))
                {
                    foundLetters = foundLetters.Remove(2, 1).Insert(2, s2.ToString());
                    foundLetters = foundLetters.Remove(5, 1).Insert(5, s1.ToString());
                    break;
                }
            }

            // Find segment a using number 7.
            foreach (var letter in leftNumbers[1])
            {
                if (!foundLetters.Contains(letter))
                {
                    foundLetters = foundLetters.Remove(0, 1).Insert(0, letter.ToString());
                    break;
                }
            }

            // Find index of number 3. It is on index 3, 4, or 5. Its the only one of those that have both c and f.
            for (int j = 3; j <= 5; j++)
            {
                if (leftNumbers[j].Contains(foundLetters[2]) && leftNumbers[j].Contains(foundLetters[5]))
                {
                    number3Index = j;
                    break;
                }
            }

            // Find the two unknown segments from number 4.
            string unknownSegments = leftNumbers[2].Replace(foundLetters[2].ToString(), string.Empty).Replace(foundLetters[5].ToString(), string.Empty);

            // Which of those does number 3 also have? Thats letter d, other one is segment b. 
            if (leftNumbers[number3Index].Contains(unknownSegments[0]))
            {
                foundLetters = foundLetters.Remove(1, 1).Insert(1, unknownSegments[1].ToString());
                foundLetters = foundLetters.Remove(3, 1).Insert(3, unknownSegments[0].ToString());
            }
            else
            {
                foundLetters = foundLetters.Remove(1, 1).Insert(1, unknownSegments[0].ToString());
                foundLetters = foundLetters.Remove(3, 1).Insert(3, unknownSegments[1].ToString());
            }

            // Last unknown segment from number 3 is g.
            foreach (var segment in leftNumbers[number3Index])
            {
                if (!foundLetters.Contains(segment))
                {
                    foundLetters = foundLetters.Remove(6, 1).Insert(6, segment.ToString());
                    break;
                }
            }

            // Last unknown segment from number 8 is e.
            foreach (var segment in leftNumbers[9])
            {
                if (!foundLetters.Contains(segment))
                {
                    foundLetters = foundLetters.Remove(4, 1).Insert(4, segment.ToString());
                    break;
                }
            }

            // decipher right numbers.
            string value = string.Empty;

            for (int j = 0; j < 4; j++)
            {
                switch (rightNumbers[j].Length)
                {
                    case 2: // 1
                        value += "1";
                        break;

                    case 3: // 7
                        value += "7";
                        break;

                    case 4: // 4
                        value += "4";
                        break;

                    case 5: // 2, 5, 3
                        if (rightNumbers[j].Contains(foundLetters[4])) // If its a 2. It's the only one with segment e.
                        {
                            value += "2";
                        }
                        else if (rightNumbers[j].Contains(foundLetters[1])) // If its a 5. It's the only one with segment b.
                        {
                            value += "5";
                        }
                        else
                        {
                            value += "3";
                        }
                        break;

                    case 6: // 0, 6, 9
                        if (!rightNumbers[j].Contains(foundLetters[3])) // If its a 0. It's the only one that dosn't have segment d.
                        {
                            value += "0";
                        }
                        else if (!rightNumbers[j].Contains(foundLetters[2])) // If its a 6. It's the only one that dosn't have segment c.
                        {
                            value += "6";
                        }
                        else
                        {
                            value += "9";
                        }
                        break;

                    case 7: // 8
                        value += "8";
                        break;

                    default:
                        break;
                }
            }
            result += int.Parse(value);
        }

        File.WriteAllText("D:/AdventOfCode/2021/Day8/result.txt", result.ToString());
        Console.WriteLine(result);
    }
}