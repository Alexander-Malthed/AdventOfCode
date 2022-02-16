using System;
using System.IO;

class Day17
{
    static int[] targetAreaXBounds, targetAreaYBounds;

    static void Main(string[] args)
    {
        string[] input = File.ReadAllText("D:/AdventOfCode/2021/Day17/input.txt").Substring(13).Split(", ");
        targetAreaXBounds = Array.ConvertAll(input[0].Substring(2).Split(".."), int.Parse);
        targetAreaYBounds = Array.ConvertAll(input[1].Substring(2).Split(".."), int.Parse);

        int result;

        // Part 1 doesn't really need any code and could be solved by hand in a matter of seconds.
        //result = Part1();

        // Part 2 is trickier, but brute force with some restrictions works great in this case.
        result = Part2();

        Console.WriteLine(result);
        File.WriteAllText("D:/AdventOfCode/2021/Day17/result.txt", result.ToString());
    }

    static int Part1()
    {
        // This assumes the target area is below y=0. Which is true for all test cases and the given input.

        // When the initial y velocity is positive, we know that it will eventually stop by y=0,
        // and that for the next step it will have a y velocity of negative its initial velocity - 1.
        // A bigger initial y velocity, will yield a bigger negative y velocity after having reached y=0.

        // Then what is the longest negative step it can take away from y=0 on its way down, and still hit?
        // The step that reaches the very bottom of the target area.

        // Taking the y value of the bottom line of the target area, adding one,
        // and taking the absolute value of that gives us the initial y velocity to shoot with.

        // Then to reach the peak (say for example initial y velocity is 3) we add 3+2+1, or 3(3+1)/2.

        int initialYVelocity = Math.Abs(targetAreaYBounds[0] + 1);
        int peakYPos = initialYVelocity * (initialYVelocity + 1) / 2;
        return peakYPos;
    }

    static int Part2()
    {
        // Initial x velocity range:
        // Min = Minimum xVel needed to reach the left bound at all. 
        // To find this we can roughly invert the algorithm used when we find the peakYPos in part 1.
        // Max = xVel that reaches the right bound on the first step.

        // Initial y velocity range:
        // Min: yVel that reaches the bottom line on the first step.
        // Max: First part of part 1.

        int minXVel = (int)Math.Round(Math.Sqrt(targetAreaXBounds[0] * 2));
        int maxXVel = targetAreaXBounds[1];

        int minYVel = targetAreaYBounds[0];
        int maxYVel = Math.Abs(targetAreaYBounds[0] + 1);

        int result = 0;

        for (int x = minXVel; x <= maxXVel; x++)
        {
            for (int y = minYVel; y <= maxYVel; y++)
            {
                if (CheckTrajectory(x, y))
                {
                    result++;
                }
            }
        }

        return result;
    }

    static bool CheckTrajectory(int initialXVel, int initialYVel)
    {
        int xVel = initialXVel, yVel = initialYVel;
        int xPos = 0, yPos = 0;

        while (xPos <= targetAreaXBounds[1] && yPos >= targetAreaYBounds[0])
        {
            if (xPos >= targetAreaXBounds[0] && yPos <= targetAreaYBounds[1])
            {
                return true;
            }

            xPos += xVel;
            yPos += yVel;

            xVel = xVel > 0 ? xVel - 1 : 0;
            yVel--;
        }

        return false;
    }
}