using System;
using System.Collections.Generic;
using System.IO;

namespace Day_12
{
    class Program
    {
        static Dictionary<string, List<string>> allCaves = new Dictionary<string, List<string>>();
        static List<string> paths = new List<string>();
        static string currentMediumCave;

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(@"D:/Code/Day_12.txt");
            List<string> startCave = new List<string>();
            List<string> allSmallCaves = new List<string>();

            foreach (string line in input)
            {
                string[] caves = line.Split('-');

                bool startIncluded = false;
                for (int i = 0; i < 2; i++)
                {
                    if (caves[i] == "start")
                    {
                        startCave.Add(caves[1 - i]);

                        if (!allCaves.ContainsKey(caves[1 - i]))
                        {
                            allCaves.Add(caves[1 - i], new List<string>());
                        }

                        startIncluded = true;
                        break;
                    }
                }

                if (startIncluded)
                {
                    continue;
                }

                for (int i = 0; i < 2; i++)
                {
                    if (!allCaves.ContainsKey(caves[i]))
                    {
                        allCaves.Add(caves[i], new List<string>());

                        if (caves[i] == caves[i].ToLower())
                        {
                            allSmallCaves.Add(caves[i]);
                        }
                    }
                }
                allCaves[caves[0]].Add(caves[1]);
                allCaves[caves[1]].Add(caves[0]);
            }

            List<string> visitedSmallCaves = new List<string>();

            foreach (var smallCave in allSmallCaves)
            {
                currentMediumCave = smallCave;

                foreach (var connectedCave in startCave)
                {
                    string path = string.Empty;
                    ExploreCave(connectedCave, visitedSmallCaves, path, 0);
                }
            }
            Console.WriteLine(paths.Count);
        }

        static void ExploreCave(string thisCave, List<string> previousSmallCaves, string path, int numVisitMedium)
        {
            if (thisCave == "end")
            {
                if (!paths.Contains(path))
                {
                    paths.Add(path);
                }
                return;
            }

            List<string> visitedSmallCaves = new List<string>();
            if (thisCave == thisCave.ToLower())
            {
                if (previousSmallCaves.Contains(thisCave))
                    return;

                if (thisCave == currentMediumCave)
                {
                    if (numVisitMedium > 1)
                        return;

                    numVisitMedium++;
                }
                else
                {
                    visitedSmallCaves.Add(thisCave);
                }
            }

            foreach (var smallCave in previousSmallCaves)
            {
                visitedSmallCaves.Add(smallCave);
            }

            path += thisCave;

            foreach (var connectedCave in allCaves[thisCave])
            {
                ExploreCave(connectedCave, visitedSmallCaves, path, numVisitMedium);
            }
        }
    }
}
