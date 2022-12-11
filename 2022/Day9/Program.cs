namespace Day9 {
    internal class Day9 {
        static void Main(string[] args) {
            string[] input = File.ReadAllLines("D:/AdventOfCode/2022/Day9/input.txt");

            Console.WriteLine(Part1(input));
            Console.WriteLine(Part2(input));
        }

        static int Part1(string[] input) {
            int[] headPos = new int[2];
            int[] tailPos = new int[2];
            HashSet<Tuple<int, int>> visitedPositions = new();

            foreach (string line in input) {
                char direction = line[0];
                int steps = int.Parse(line.Split(' ')[1]);

                switch (direction) {
                    case 'U':
                        UpdatePositionPart1(increment: true, positionToUpdate: ref headPos[1], ref headPos, ref tailPos, steps, ref visitedPositions);
                        break;
                    case 'D':
                        UpdatePositionPart1(increment: false, positionToUpdate: ref headPos[1], ref headPos, ref tailPos, steps, ref visitedPositions);
                        break;
                    case 'R':
                        UpdatePositionPart1(increment: true, positionToUpdate: ref headPos[0], ref headPos, ref tailPos, steps, ref visitedPositions);
                        break;
                    case 'L':
                        UpdatePositionPart1(increment: false, positionToUpdate: ref headPos[0], ref headPos, ref tailPos, steps, ref visitedPositions);
                        break;
                }
            }

            return visitedPositions.Count;
        }

        static void UpdatePositionPart1(bool increment, ref int positionToUpdate, ref int[] headPos, ref int[] tailPos, int steps, ref HashSet<Tuple<int, int>> visitedPositions) {
            int[] headPreviousPos = new int[2];

            for (int step = 0; step < steps; step++) {
                headPos.CopyTo(headPreviousPos, 0);
                positionToUpdate += increment ? 1 : -1;

                if (Math.Abs(headPos[0] - tailPos[0]) > 1
                    || Math.Abs(headPos[1] - tailPos[1]) > 1) {
                    headPreviousPos.CopyTo(tailPos, 0);
                }

                visitedPositions.Add(Tuple.Create(tailPos[0], tailPos[1]));
            }
        }

        static int Part2(string[] input) {
            int numberOfKnots = 10;
            Knot[] knots = new Knot[numberOfKnots];
            knots[numberOfKnots - 1] = new Knot(null, "9"); // Tail.

            for (int i = numberOfKnots - 2; i >= 0; i--) {
                knots[i] = new(childKnot: knots[i + 1], id: i == 0 ? "H" : (i).ToString());
            }

            HashSet<Tuple<int, int>> visitedPositions = new();
            visitedPositions.Add(Tuple.Create(0,0));

            foreach (string line in input) {
                char direction = line[0];
                int steps = int.Parse(line.Split(' ')[1]);

                switch (direction) {
                    case 'U':
                        UpdatePositionPart2(increment: false, positionToUpdate: ref knots[0].Position[1], ref knots[0], ref knots[1], steps, ref visitedPositions);
                        break;
                    case 'D':
                        UpdatePositionPart2(increment: true, positionToUpdate: ref knots[0].Position[1], ref knots[0], ref knots[1], steps, ref visitedPositions);
                        break;
                    case 'R':
                        UpdatePositionPart2(increment: true, positionToUpdate: ref knots[0].Position[0], ref knots[0], ref knots[1], steps, ref visitedPositions);
                        break;
                    case 'L':
                        UpdatePositionPart2(increment: false, positionToUpdate: ref knots[0].Position[0], ref knots[0], ref knots[1], steps, ref visitedPositions);
                        break;
                }
            }

            return visitedPositions.Count;
        }

        static void UpdatePositionPart2(bool increment, ref int positionToUpdate, ref Knot head, ref Knot tail, int steps, ref HashSet<Tuple<int, int>> visitedPositions) {
            for (int step = 0; step < steps; step++) {
                positionToUpdate += increment ? 1 : -1;

                UpdateTailPart2(ref head, ref tail, ref visitedPositions);
            }
        }

        static void UpdateTailPart2(ref Knot head, ref Knot tail, ref HashSet<Tuple<int, int>> visitedPositions) {
            if (Math.Abs(head.Position[0] - tail.Position[0]) + Math.Abs(head.Position[1] - tail.Position[1]) >= 3) {
                tail.Position[0] = head.Position[0] > tail.Position[0] ? tail.Position[0] + 1 : tail.Position[0] - 1;
                tail.Position[1] = head.Position[1] > tail.Position[1] ? tail.Position[1] + 1 : tail.Position[1] - 1;
            } else if (Math.Abs(head.Position[0] - tail.Position[0]) > 1) {
                tail.Position[0] = head.Position[0] > tail.Position[0] ? tail.Position[0] + 1 : tail.Position[0] - 1;
            } else if (Math.Abs(head.Position[1] - tail.Position[1]) > 1) {
                tail.Position[1] = head.Position[1] > tail.Position[1] ? tail.Position[1] + 1 : tail.Position[1] - 1;
            } else {
                return; // If this knot does not move, none of the following knots will move.
            }

            Knot? childKnot = tail.ChildKnot;
            if (childKnot != null) {
                UpdateTailPart2(ref tail, ref childKnot, ref visitedPositions);
            } else {
                visitedPositions.Add(Tuple.Create(tail.Position[0], tail.Position[1]));
            }
        }
    }
}
