namespace Day5 {
    internal class Day5 {
        const int mMaxStackHeight = 8;
        const int mNumberOfStacks = 9;
        const int mInstructionsStartLine = 10;

        static void Main(string[] args) {
            string[] input = File.ReadAllLines("D:/AdventOfCode/2022/Day5/input.txt");

            Console.WriteLine(Part1(input));
            Console.WriteLine(Part2(input));
        }

        static string Part1(string[] input) {
            string result = "";
            Stack<char>[] boxes = CreateBoxes(input);

            for (int instructionIndex = mInstructionsStartLine; instructionIndex < input.Length; instructionIndex++) {
                Instruction instruction = new(input[instructionIndex]);
                
                for (int boxIndex = 0; boxIndex < instruction.mNumberOfBoxes; boxIndex++) {
                    boxes[instruction.mToStack].Push(boxes[instruction.mFromStack].Pop());
                }
            }

            foreach (Stack<char> boxStack in boxes) {
                result += boxStack.Peek();
            }

            return result;
        }

        static string Part2(string[] input) {
            string result = "";
            Stack<char>[] boxes = CreateBoxes(input);
            Stack<char> tempStack = new Stack<char>();

            for (int instructionIndex = mInstructionsStartLine; instructionIndex < input.Length; instructionIndex++) {
                Instruction instruction = new(input[instructionIndex]);

                tempStack.Clear();
                for (int boxIndex = 0; boxIndex < instruction.mNumberOfBoxes; boxIndex++) {
                    tempStack.Push(boxes[instruction.mFromStack].Pop());
                }

                foreach (char box in tempStack) {
                    boxes[instruction.mToStack].Push(box);
                }
            }

            foreach (Stack<char> boxStack in boxes) {
                result += boxStack.Peek();
            }

            return result;
        }

        static Stack<char>[] CreateBoxes(string[] input) {
            Stack<char>[] boxes = new Stack<char>[mNumberOfStacks];

            for (int stackIndex = 0; stackIndex < mNumberOfStacks; stackIndex++) {
                boxes[stackIndex] = new Stack<char>();

                for (int curStackHeight = mMaxStackHeight - 1; curStackHeight >= 0; curStackHeight--) {
                    char box = input[curStackHeight][1 + stackIndex * 4];
                    if (box == ' ') {
                        break;
                    }
                    boxes[stackIndex].Push(box);
                }
            }
            return boxes;
        }
    }
}