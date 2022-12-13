namespace Day11 {
    using System.Text.RegularExpressions;
    internal class Day11 {
        static void Main(string[] args) {
            string[] input = File.ReadAllLines("D:/AdventOfCode/2022/Day11/input.txt");

            //Console.WriteLine(Part1(input));
            Console.WriteLine(Part2(input));
        }

        static ulong Part1(string[] input) {
            Dictionary<int, Monkey> monkeys = new();

            CreateMonkeys(input, ref monkeys);

            for (int round = 0; round < 20; round++) {
                for (int i = 0; i < monkeys.Count; i++) {
                    monkeys[i].PlayTurn(true);
                }
            }

            KeyValuePair<int,Monkey>[] sortedMonkeys = (from monkey in monkeys orderby monkey.Value.NumberOfTimesInspected descending select monkey).ToArray();
            return sortedMonkeys[0].Value.NumberOfTimesInspected * sortedMonkeys[1].Value.NumberOfTimesInspected;
        }

        static ulong Part2(string[] input) {
            Dictionary<int, Monkey> monkeys = new();

            CreateMonkeys(input, ref monkeys);

            for (int i = 0; i < monkeys.Count; i++) {
                Monkey.LeastCommonMultiple *= monkeys[i].TestNumber;
            }

            for (int round = 0; round < 10000; round++) {
                for (int i = 0; i < monkeys.Count; i++) {
                    monkeys[i].PlayTurn(false);
                }
            }

            KeyValuePair<int, Monkey>[] sortedMonkeys = (from monkey in monkeys orderby monkey.Value.NumberOfTimesInspected descending select monkey).ToArray();
            return sortedMonkeys[0].Value.NumberOfTimesInspected * sortedMonkeys[1].Value.NumberOfTimesInspected;
        }

        static void CreateMonkeys(string[] input, ref Dictionary<int,Monkey> monkeys) {
            int currentMonkey = 0;

            for (int i = 0; i < input.Length; i += 7) {

                IEnumerable<ulong> startingItems = Regex.Matches(input[i + 1], @"\d+").Select(m => ulong.Parse(m.Value));
                Match numberFoundInOperation = Regex.Match(input[i + 2], @"\d+");
                OperationType operationType = Regex.Match(input[i + 2], @"[+]").Success
                        ? numberFoundInOperation.Success ? OperationType.ADD_NUMBER : OperationType.ADD_SELF
                        : numberFoundInOperation.Success ? OperationType.MULTIPLY_NUMBER : OperationType.MULTIPLY_SELF;
                ulong operationNumber = numberFoundInOperation.Success ? ulong.Parse(numberFoundInOperation.Value) : 0;
                ulong testNumber = ulong.Parse(Regex.Match(input[i + 3], @"\d+").Value);
                int testTrueMonkeyIndex = int.Parse(Regex.Match(input[i + 4], @"\d+").Value);
                int testFalseMonkeyIndex = int.Parse(Regex.Match(input[i + 5], @"\d+").Value);

                monkeys[currentMonkey++] = new Monkey(
                    startingItems,
                    operationType,
                    operationNumber,
                    testNumber,
                    ref monkeys,
                    testTrueMonkeyIndex,
                    testFalseMonkeyIndex);
            }
        }
    }
}