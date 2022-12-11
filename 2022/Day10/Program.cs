internal class Day10 {
    static void Main(string[] args) {
        string[] input = File.ReadAllLines("D:/AdventOfCode/2022/Day10/input.txt");

        Console.WriteLine(Part1(input));
        Part2(input);
    }

    static int Part1(string[] input) {
        int cycles = 0;
        int x = 1;
        int nextCycleToTriggerCheck = 20;
        const int cycleCheckDelta = 40;
        int totalSignalStrength = 0;

        foreach (string line in input) {
            if (line[0] == 'a') {
                for (int i = 0; i < 2; i++) {
                    TriggerNextCycleAndUpdateSignalStrength(ref cycles, ref nextCycleToTriggerCheck, cycleCheckDelta, ref totalSignalStrength, x);
                }
                x += int.Parse(line.Split(' ')[1]);
            } else {
                TriggerNextCycleAndUpdateSignalStrength(ref cycles, ref nextCycleToTriggerCheck, cycleCheckDelta, ref totalSignalStrength, x);
            }
        }

        return totalSignalStrength;
    }

    static void TriggerNextCycleAndUpdateSignalStrength(ref int cycles, ref int nextCycleToTriggerCheck, int cycleCheckDelta, ref int totalSignalStrength, int x) {
        if (++cycles == nextCycleToTriggerCheck) {
            nextCycleToTriggerCheck += cycleCheckDelta;
            totalSignalStrength += cycles * x;
        }
    }

    static void Part2(string[] input) {
        int spritePos = 1;
        int cycles = 0;
        int nextNewLineCycle = 41;
        const int nextNewLineCycleDelta = 40;
        int currentPixelPosX = 0;

        foreach (string line in input) {
            if (line[0] == 'a') {
                for (int i = 0; i < 2; i++) {
                    TriggerNextCycleAndDrawPixel(spritePos, ref cycles, ref nextNewLineCycle, nextNewLineCycleDelta, ref currentPixelPosX);
                }
                spritePos += int.Parse(line.Split(' ')[1]);
            } else {
                TriggerNextCycleAndDrawPixel(spritePos, ref cycles, ref nextNewLineCycle, nextNewLineCycleDelta, ref currentPixelPosX);
            }
        }
    }

    static void TriggerNextCycleAndDrawPixel(int spritePos, ref int cycles, ref int nextNewLineCycle, int newLineCycleDelta, ref int currentPixelPosX) {
        if (++cycles == nextNewLineCycle) {
            Console.Write("\n");
            nextNewLineCycle += newLineCycleDelta;
            currentPixelPosX = 0;
        } else {
            currentPixelPosX++;
        }

        if (currentPixelPosX == spritePos - 1
            || currentPixelPosX == spritePos
            || currentPixelPosX == spritePos + 1) {
            Console.Write("#");
        } else {
            Console.Write(".");
        }
    }
}