class Day1 {
    static void Main(string[] args) {
        string[] input = File.ReadAllLines("D:/AdventOfCode/2022/Day1/input.txt");

        Console.WriteLine(Part1(input));
        Console.WriteLine(Part2(input));
    }

    static int Part1(string[] input) {
        int currentCaloriesSum = 0, mostCalories = 0;

        for (int i = 0; i < input.Length; i++) {
            if (input[i] != string.Empty) {
                currentCaloriesSum += int.Parse(input[i]);
                continue;
            }

            if (currentCaloriesSum > mostCalories) {
                mostCalories = currentCaloriesSum;
            }

            currentCaloriesSum = 0;
        }

        return mostCalories;
    }

    static int Part2(string[] input) {
        int currentCaloriesSum = 0;
        List<int> elfCalories = new List<int>();

        for (int i = 0; i < input.Length; i++) {
            if (input[i] != string.Empty) {
                currentCaloriesSum += int.Parse(input[i]);
                continue;
            }

            elfCalories.Add(currentCaloriesSum);
            currentCaloriesSum = 0;
        }

        elfCalories.Sort();
        return elfCalories[^1] + elfCalories[^2] + elfCalories[^3];
    }
}