using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace Day_15
{
    class Day_15
    {
        static Node[][] map;
        static int mapYLenght, mapXLenght;
        static Heap<Node> open;
        static HashSet<Node> closed = new HashSet<Node>();

        static void Main(string[] args)
        {
            // Construct the Node jagged array
            int[][] intMap = File.ReadAllLines("D:/Code/input.txt")
                .Select((l, i) => l
                    .Select((c, j) => Convert.ToInt32(c.ToString()))
                    .ToArray())
                .ToArray();
            map = ExpandMapForPart2(intMap);
            
            // Setup
            mapYLenght = map.Length;
            mapXLenght = map[0].Length;
            open = new Heap<Node>(mapYLenght * mapXLenght);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Node start = map[0][0];
            Node end = map[mapYLenght - 1][mapXLenght - 1];

            map[0][0].risk = 0;
            open.Add(map[0][0]);
            Node current = map[0][0];

            // A* loop finding the cheapest path
            while (open.Count > 0)
            {
                // The heap allows it to efficently find the best step to take.
                current = open.RemoveFirst();
                closed.Add(current);

                if (current == end)
                    break;

                // Add or update valid neighbours.
                foreach (Node neighbour in GetNeighbours(current))
                {
                    if (closed.Contains(neighbour))
                        continue;

                    int newCostToNeighbour = current.gCost + neighbour.risk;
                    if (newCostToNeighbour < neighbour.gCost || !open.Contains(neighbour))
                    {
                        neighbour.gCost = newCostToNeighbour;
                        neighbour.hCost = mapYLenght - 1 - neighbour.mapY + mapXLenght - 1 - neighbour.mapX;
                        neighbour.parent = current;

                        if (!open.Contains(neighbour))
                            open.Add(neighbour);
                        else
                            open.UpdateItem(neighbour);
                    }
                }
            }
            sw.Stop();

            // Retrace the cheapest path to calculate the result.
            int totalRisk = current.risk;
            while (current.parent != start)
            {
                current = current.parent;
                totalRisk += current.risk;
            }
            Console.WriteLine($"total risk: {totalRisk}");
            Console.WriteLine($"time: {sw.ElapsedMilliseconds}");
        }

        // Copy the puzzle input to a 5*5 grid of duplicates.
        // Each step to the right or down in the 5*5 grid adds 1 to each value in that tile.
        // Incrementing a 9 will loop it back around to a 1.
        static Node[][] ExpandMapForPart2(int[][] oldMap)
        {
            int oldMaxY = oldMap.Length;
            int oldMaxX = oldMap[0].Length;

            Node[][] newMap = new Node[oldMaxY * 5][];
            for (int i = 0; i < newMap.Length; i++)
                newMap[i] = new Node[oldMaxX * 5];

            for (int expandY = 0; expandY < 5; expandY++)
            {
                for (int expandX = 0; expandX < 5; expandX++)
                {
                    for (int i = 0; i < oldMap.Length; i++)
                    {
                        for (int j = 0; j < oldMap[0].Length; j++)
                        {
                            int newRisk = (oldMap[i][j] + expandX + expandY);
                            if (newRisk > 9)
                                newRisk = (newRisk + 1) % 10;

                            int newY = i + (oldMaxY * expandY);
                            int newX = j + (oldMaxX * expandX);
                            newMap[newY][newX] = new Node { risk = newRisk, mapY = newY, mapX = newX };
                        }
                    }
                }
            }

            return newMap;
        }

        static List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();
            if (node.mapY + 1 < mapYLenght)
                neighbours.Add(map[node.mapY + 1][node.mapX]);

            if (node.mapY - 1 >= 0)
                neighbours.Add(map[node.mapY - 1][node.mapX]);

            if (node.mapX + 1 < mapXLenght)
                neighbours.Add(map[node.mapY][node.mapX + 1]);

            if (node.mapX - 1 >= 0)
                neighbours.Add(map[node.mapY][node.mapX - 1]);

            return neighbours;
        }

        static void PrintPath(Node start, Node end)
        {
            List<Node> path = new List<Node>();
            Node current = end;

            while (current != start)
            {
                path.Add(current);
                current = current.parent;
            }
            path.Reverse();

            foreach (Node node in path)
            {
                Console.WriteLine(node.risk);
            }
        }
    }
}
