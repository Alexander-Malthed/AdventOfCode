using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_14
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(@"D:/Code/input.txt");
            Dictionary<string, char> rules = new Dictionary<string, char>();
            Dictionary<string, ulong> pairs = new Dictionary<string, ulong>();

            for (int i = 2; i < input.Length; i++)
            {
                input[i] = input[i].Replace(" ", string.Empty);
                string[] rule = input[i].Split("->");
                rules.Add(rule[0], rule[1][0]);
                pairs.Add(rule[0], 0);
            }

            for (int i = 0; i < input[0].Length - 1; i++)
            {
                pairs[input[0][i].ToString() + input[0][i + 1].ToString()]++;
            }

            for (int step = 0; step < 40; step++)
            {
                string[] unalteredPairs = pairs.Keys.ToArray();
                ulong[] unalteredValues = pairs.Values.ToArray();

                for (int i = 0; i < unalteredPairs.Length; i++)
                {
                    string pair = unalteredPairs[i];
                    ulong value = unalteredValues[i];

                    if (pairs[pair] <= 0)
                        continue;

                    pairs[pair[0].ToString() + rules[pair].ToString()] += value;
                    pairs[rules[pair].ToString() + pair[1].ToString()] += value;
                    pairs[pair] -= value;
                }
            }

            Dictionary<char, ulong> occurences = new Dictionary<char, ulong>();

            foreach (var pair in pairs)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (occurences.ContainsKey(pair.Key[i]))
                    {
                        occurences[pair.Key[i]] += pair.Value;
                    }
                    else
                    {
                        occurences.Add(pair.Key[i], pair.Value);
                    }
                }
            }

            ulong least = ulong.MaxValue;
            ulong most = 0;
            foreach (var pair in occurences)
            {
                ulong numberOfOccurence = (pair.Value + 1) / 2;

                System.Console.WriteLine($"{pair.Key}: {numberOfOccurence}");
                
                if (numberOfOccurence < least)
                {
                    least = numberOfOccurence;
                }
                else if (numberOfOccurence > most)
                {
                    most = numberOfOccurence;
                }
            }

            System.Console.WriteLine("Least common: " + least);
            System.Console.WriteLine("Most common: " + most);
            System.Console.WriteLine("Result: " + (most - least));
        }
    }
}
