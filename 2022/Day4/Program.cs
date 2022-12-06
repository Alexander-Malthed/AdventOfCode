internal class Day4 {
    static void Main(string[] args) {
        string[] input = File.ReadAllLines("D:/AdventOfCode/2022/Day4/input.txt");

        //Console.WriteLine(Part1(input));
        Console.WriteLine(Part2(input));
    }

    static int Part1(string[] input) {
        int result = 0;

        foreach (string line in input) {
            int[] leftPairSections = Array.ConvertAll(line.Split(',')[0].Split('-'), int.Parse);
            int[] rightPairSections = Array.ConvertAll(line.Split(',')[1].Split('-'), int.Parse);

            if (leftPairSections[0] <= rightPairSections[0] && leftPairSections[1] >= rightPairSections[1]
                || leftPairSections[0] >= rightPairSections[0] && leftPairSections[1] <= rightPairSections[1]) {
                result++;
            }
        }

        return result;
    }

    static int Part2(string[] input) {
        int result = 0;

        foreach (string line in input) {
            int[] leftPairSections = Array.ConvertAll(line.Split(',')[0].Split('-'), int.Parse);
            int[] rightPairSections = Array.ConvertAll(line.Split(',')[1].Split('-'), int.Parse);

            if (leftPairSections[0] <= rightPairSections[0] && leftPairSections[1] >= rightPairSections[0]
                || rightPairSections[0] <= leftPairSections[0] && rightPairSections[1] >= leftPairSections[0]) {
                result++;
            }
        }

        return result;
    }
}