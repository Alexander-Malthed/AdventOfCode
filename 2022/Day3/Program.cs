internal class Day3 {
    static void Main(string[] args) {
        string[] input = File.ReadAllLines("D:/AdventOfCode/2022/Day3/input.txt");

        Console.WriteLine(Part1(input));
        Console.WriteLine(Part2(input));
    }

    static int Part1(string[] input) {
        int sumOfPriorities = 0;

        foreach (string line in input) {
            int halfLength = line.Length / 2;
            string leftPart = line.Substring(0, halfLength);
            string rightPart = line.Substring(halfLength, halfLength);

            foreach (char letter in leftPart) {
                if (rightPart.Contains(letter)) {
                    sumOfPriorities += GetPriority(letter);
                    break;
                }
            }
        }
        return sumOfPriorities;
    }

    static int Part2(string[] input) {
        int sumOfPriorities = 0;

        for (int i = 0; i < input.Length; i += 3) {
            foreach (char letter in input[i]) {
                if (input[i + 1].Contains(letter) && input[i + 2].Contains(letter)) {
                    sumOfPriorities += GetPriority(letter);
                    break;
                }
            }
        }

        return sumOfPriorities;
    }

    static int GetPriority(char letter) {
        return letter.ToString() == letter.ToString().ToLower() ? letter - 96 : letter - 38;
    }
}