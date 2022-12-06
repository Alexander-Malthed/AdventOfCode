internal class Day6 {
    static void Main(string[] args) {
        string input = File.ReadAllText("D:/AdventOfCode/2022/Day6/input.txt");

        Console.WriteLine(Part1And2(input, 4));
        Console.WriteLine(Part1And2(input, 14));
    }

    static int Part1And2(string input, int markerSize) {
        HashSet<char> seenLetters = new();

        for (int letterIndex = 0; letterIndex < input.Length - markerSize - 1; letterIndex++) {
            seenLetters.Clear();

            for (int lettersToCompareIndex = letterIndex; lettersToCompareIndex < letterIndex + markerSize; lettersToCompareIndex++) {
                seenLetters.Add(input[lettersToCompareIndex]);
            }

            if (seenLetters.Count == markerSize) {
                return markerSize + letterIndex;
            }
        }

        return -1;
    }
}