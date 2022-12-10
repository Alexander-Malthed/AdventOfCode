internal class Day8 {
    static int gridSize;

    static void Main(string[] args) {
        string[] input = File.ReadAllLines("D:/AdventOfCode/2022/Day8/input.txt");
        gridSize = input.Length;

        //Console.WriteLine(Part1(input));
        Console.WriteLine(Part2(input));
    }

    static int Part1(string[] input) {
        HashSet<Tuple<int, int>> visibleTreesPositions = new();

        AddGridEdgeTrees(ref input, ref visibleTreesPositions);
        AddTreesByLookingFromLeft(ref input, ref visibleTreesPositions);
        AddTreesByLookingFromRight(ref input, ref visibleTreesPositions);
        AddTreesByLookingFromAbove(ref input, ref visibleTreesPositions);
        AddTreesByLookingFromBelow(ref input, ref visibleTreesPositions);

        return visibleTreesPositions.Count;
    }

    static int Part2(string[] input) {
        int highestScore = 0;

        for (int row = 0; row < gridSize; row++) {
            for (int column = 0; column < gridSize; column++) {
                int treeScore = GetScoreOfTree(row, column, ref input);

                if (treeScore > highestScore) {
                    highestScore = treeScore;
                }
            }
        }

        return highestScore;
    }

    static void AddGridEdgeTrees(ref string[] input, ref HashSet<Tuple<int, int>> visibleTreesPositions) {
        for (int row = 0; row < 2; row++) {
            for (int column = 0; column < gridSize; column++) {
                visibleTreesPositions.Add(Tuple.Create(row * (gridSize - 1), column));
            }
        }

        for (int column = 0; column < 2; column++) {
            for (int row = 0; row < gridSize; row++) {
                visibleTreesPositions.Add(Tuple.Create(row, column * (gridSize - 1)));
            }
        }
    }

    static void AddTreesByLookingFromLeft(ref string[] input, ref HashSet<Tuple<int, int>> visibleTreesPositions) {
        for (int row = 1; row < gridSize - 1; row++) {
            int highestEncounteredTreeForRow = int.Parse(input[row][0].ToString());
            if (highestEncounteredTreeForRow == 9) {
                break;
            }

            for (int column = 1; column < gridSize - 1; column++) {
                int treeHeight = int.Parse(input[row][column].ToString());

                if (treeHeight > highestEncounteredTreeForRow) {
                    highestEncounteredTreeForRow = treeHeight;
                    visibleTreesPositions.Add(Tuple.Create(row, column));
                }

                if (treeHeight == 9) {
                    break;
                }
            }
        }
    }

    static void AddTreesByLookingFromRight(ref string[] input, ref HashSet<Tuple<int, int>> visibleTreesPositions) {
        for (int row = 1; row < gridSize - 1; row++) {
            int highestEncounteredTreeForRow = int.Parse(input[row][gridSize - 1].ToString());
            if (highestEncounteredTreeForRow == 9) {
                break;
            }

            for (int column = gridSize - 2; column > 0; column--) {
                int treeHeight = int.Parse(input[row][column].ToString());

                if (treeHeight > highestEncounteredTreeForRow) {
                    highestEncounteredTreeForRow = treeHeight;
                    visibleTreesPositions.Add(Tuple.Create(row, column));
                }

                if (treeHeight == 9) {
                    break;
                }
            }
        }
    }

    static void AddTreesByLookingFromAbove(ref string[] input, ref HashSet<Tuple<int, int>> visibleTreesPositions) {
        for (int column = 1; column < gridSize - 1; column++) {
            int highestEncounteredTreeForColumn = int.Parse(input[0][column].ToString());
            if (highestEncounteredTreeForColumn == 9) {
                break;
            }

            for (int row = 1; row < gridSize - 1; row++) {
                int treeHeight = int.Parse(input[row][column].ToString());

                if (treeHeight > highestEncounteredTreeForColumn) {
                    highestEncounteredTreeForColumn = treeHeight;
                    visibleTreesPositions.Add(Tuple.Create(row, column));
                }

                if (treeHeight == 9) {
                    break;
                }
            }
        }
    }

    static void AddTreesByLookingFromBelow(ref string[] input, ref HashSet<Tuple<int, int>> visibleTreesPositions) {
        for (int column = 1; column < gridSize - 1; column++) {
            int highestEncounteredTreeForColumn = int.Parse(input[gridSize - 1][column].ToString());
            if (highestEncounteredTreeForColumn == 9) {
                break;
            }

            for (int row = gridSize - 2; row > 0; row--) {
                int treeHeight = int.Parse(input[row][column].ToString());

                if (treeHeight > highestEncounteredTreeForColumn) {
                    highestEncounteredTreeForColumn = treeHeight;
                    visibleTreesPositions.Add(Tuple.Create(row, column));
                }

                if (treeHeight == 9) {
                    break;
                }
            }
        }
    }

    static int GetScoreOfTree(int thisTreeRow, int thisTreeColumn, ref string[] input) {
        int thisTreeheight = input[thisTreeRow][thisTreeColumn];

        // Looking up
        int viewDistanceUp = 0;
        for (int row = thisTreeRow - 1; row >= 0; row--) {
            if (input[row][thisTreeColumn] >= thisTreeheight) {
                viewDistanceUp++;
                break;
            }
            viewDistanceUp++;
        }

        // Looking down
        int viewDistanceDown = 0;
        for (int row = thisTreeRow + 1; row < gridSize; row++) {
            if (input[row][thisTreeColumn] >= thisTreeheight) {
                viewDistanceDown++;
                break;
            }
            viewDistanceDown++;
        }

        // Looking left
        int viewDistanceLeft = 0;
        for (int column = thisTreeColumn - 1; column >= 0; column--) {
            if (input[thisTreeRow][column] >= thisTreeheight) {
                viewDistanceLeft++;
                break;
            }
            viewDistanceLeft++;
        }

        // Looking right
        int viewDistanceRight = 0;
        for (int column = thisTreeColumn + 1; column < gridSize; column++) {
            if (input[thisTreeRow][column] >= thisTreeheight) {
                viewDistanceRight++;
                break;
            }
            viewDistanceRight++;
        }

        return (viewDistanceUp == 0 ? 1 : viewDistanceUp)
            * (viewDistanceDown == 0 ? 1 : viewDistanceDown)
            * (viewDistanceLeft == 0 ? 1 : viewDistanceLeft)
            * (viewDistanceRight == 0 ? 1 : viewDistanceRight);
    }
}