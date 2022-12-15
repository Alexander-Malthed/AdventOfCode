namespace Day12 {
    class Day12 {
        static void Main(string[] args) {
            string[] input = File.ReadAllLines("D:/AdventOfCode/2022/Day12/input.txt");

            Console.WriteLine(Part1(input));
            Console.WriteLine(Part2(input));
        }

        static int Part1(string[] input) {
            Node start = null;
            Node end = null;

            Node[][] map = CreateMapPart1(input, ref start, ref end);

            return AStarFindShortestPathFromStart(start, ref end, ref map);
        }

        static Node[][] CreateMapPart1(string[] input, ref Node start, ref Node end) {
            int mapYLength = input.Length;
            int mapXLength = input[0].Length;
            Node[][] map = new Node[mapYLength][];
            for (int y = 0; y < input.Length; y++) {
                map[y] = new Node[mapXLength];
                for (int x = 0; x < input[0].Length; x++) {
                    map[y][x] = new Node(GetHeightOfCharacter(input[y][x]), mapY: y, mapX: x);

                    switch (input[y][x]) {
                        case 'S':
                            start = map[y][x];
                            break;
                        case 'E':
                            end = map[y][x];
                            break;
                        default:
                            break;
                    }
                }
            }
            return map;
        }

        static int AStarFindShortestPathFromStart(Node start, ref Node end, ref Node[][] map) {
            int mapYLength = map.Length;
            int mapXLength = map[0].Length;

            Heap<Node> open = new Heap<Node>(mapYLength * mapXLength);
            HashSet<Node> closed = new();

            open.Add(start);
            Node current = start;

            // A* loop finding the shortest path
            while (open.Count > 0) {
                current = open.RemoveFirst();
                closed.Add(current);

                if (current == end)
                    break;

                // Add or update valid neighbours.
                foreach (Node neighbour in GetNeighbours(ref current, mapYLength, mapXLength, ref map)) {
                    if (closed.Contains(neighbour))
                        continue;

                    int newCostToNeighbour = neighbour.height - current.height <= 1 ? current.gCost + 1 : int.MaxValue;

                    // Can't climb 2 height points at once.
                    if (neighbour.height - current.height > 1) {
                        continue;
                    }

                    if (newCostToNeighbour < neighbour.gCost || !open.Contains(neighbour)) {
                        neighbour.gCost = newCostToNeighbour;
                        neighbour.hCost = Math.Abs(neighbour.mapY - end.mapY) + Math.Abs(neighbour.mapX - end.mapX);
                        neighbour.parent = current;

                        if (!open.Contains(neighbour))
                            open.Add(neighbour);
                        else
                            open.UpdateItem(neighbour);
                    }
                }
            }

            return CountStepsInPath(start, end);
        }

        static int Part2(string[] input) {
            Node start = null;
            Node[][] map = CreateMapPart2(input, ref start);

            return AStarFindShortestPathToAnyStartFromEnd(start, ref map);
        }

        static Node[][] CreateMapPart2(string[] input, ref Node start) {
            int mapYLength = input.Length;
            int mapXLength = input[0].Length;
            Node[][] map = new Node[mapYLength][];
            for (int y = 0; y < input.Length; y++) {
                map[y] = new Node[mapXLength];
                for (int x = 0; x < input[0].Length; x++) {
                    map[y][x] = new Node(GetHeightOfCharacter(input[y][x]), mapY: y, mapX: x);

                    if (input[y][x] == 'E') {
                        start = map[y][x];
                    }
                }
            }
            return map;
        }

        static int AStarFindShortestPathToAnyStartFromEnd(Node start, ref Node[][] map) {
            int mapYLength = map.Length;
            int mapXLength = map[0].Length;

            Heap<Node> open = new Heap<Node>(mapYLength * mapXLength);
            HashSet<Node> closed = new();

            open.Add(start);
            Node current = start;

            // A* loop finding the shortest path
            while (open.Count > 0) {
                current = open.RemoveFirst();
                closed.Add(current);

                if (current.height == 0)
                    break;

                // Add or update valid neighbours.
                foreach (Node neighbour in GetNeighbours(ref current, mapYLength, mapXLength, ref map)) {
                    if (closed.Contains(neighbour))
                        continue;

                    // Can't climb 2 height points at once.
                    if (current.height - neighbour.height > 1) {
                        continue;
                    }

                    int newCostToNeighbour = current.gCost + 1;
                    if (newCostToNeighbour < neighbour.gCost || !open.Contains(neighbour)) {
                        neighbour.gCost = newCostToNeighbour;
                        neighbour.parent = current;

                        if (!open.Contains(neighbour))
                            open.Add(neighbour);
                        else
                            open.UpdateItem(neighbour);
                    }
                }
            }

            return current.gCost;
        }

        static int GetHeightOfCharacter(char character) {
            switch (character) {
                case 'S':
                    return 0;
                case 'E':
                    return 25;
                default:
                    return character - 97;
            }
        }

        static List<Node> GetNeighbours(ref Node node, int mapYLength, int mapXLength, ref Node[][] map) {
            List<Node> neighbours = new();
            if (node.mapY + 1 < mapYLength)
                neighbours.Add(map[node.mapY + 1][node.mapX]);

            if (node.mapY - 1 >= 0)
                neighbours.Add(map[node.mapY - 1][node.mapX]);

            if (node.mapX + 1 < mapXLength)
                neighbours.Add(map[node.mapY][node.mapX + 1]);

            if (node.mapX - 1 >= 0)
                neighbours.Add(map[node.mapY][node.mapX - 1]);

            return neighbours;
        }

        static int CountStepsInPath(Node start, Node end) {
            List<Node> path = new();
            Node current = end;

            while (current != start) {
                path.Add(current);
                current = current.parent;
            }
            return path.Count;
        }

        static void PrintPath(Node start, Node end) {
            List<Node> path = new();
            Node current = end;

            while (current != start) {
                path.Add(current);
                current = current.parent;
            }
            path.Reverse();

            foreach (Node node in path) {
                Console.WriteLine((char)(node.height + 97) + " at: " + node.mapY + ", " + node.mapX);
            }
        }
    }
}