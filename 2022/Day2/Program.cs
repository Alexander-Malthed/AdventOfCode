namespace Day2 {
    internal class Day2 {
        static readonly Dictionary<char, char[]> mOpponentChoiceResults = new Dictionary<char, char[]> {
            { 'A', new char[] {'Z', 'X', 'Y' } }, // 0: Player lose, 1: Draw, 2: Player win.
            { 'B', new char[] {'X', 'Y', 'Z'} },
            { 'C', new char[] {'Y', 'Z', 'X'} }
        };

        static readonly Dictionary<char, int> mPlayerChoiceScores = new Dictionary<char, int> {
            { 'X', 1 },
            { 'Y', 2 },
            { 'Z', 3 }
        };

        static readonly Dictionary<char, int> mMatchOutcomePlayerChoiceIndices = new Dictionary<char, int> {
            { 'X', 0 },
            { 'Y', 1 },
            { 'Z', 2 }
        };

        static readonly int mWinScore = 6, mDrawScore = 3;

        static void Main(string[] args) {
            string[] input = File.ReadAllLines("D:/AdventOfCode/2022/Day2/input.txt");

            Console.WriteLine(Part1(input));
            Console.WriteLine(Part2(input));
        }

        static int Part1(string[] input) {
            int playerTotalScore = 0;

            foreach (string line in input) {
                char opponentChoice = line[0];
                char playerChoice = line[2];

                playerTotalScore += mPlayerChoiceScores[playerChoice];

                if (mOpponentChoiceResults[opponentChoice][2] == playerChoice) { // Player won.
                    playerTotalScore += mWinScore;
                } else if (mOpponentChoiceResults[opponentChoice][1] == playerChoice) { // Draw
                    playerTotalScore += mDrawScore;
                }
            }
            return playerTotalScore;
        }

        static int Part2(string[] input) {
            int playerTotalScore = 0;

            foreach (string line in input) {
                char opponentChoice = line[0];
                char matchOutcome = line[2];

                int playerChoiceIndex = mMatchOutcomePlayerChoiceIndices[matchOutcome];
                char playerChoice = mOpponentChoiceResults[opponentChoice][playerChoiceIndex];

                playerTotalScore += mPlayerChoiceScores[playerChoice];

                if (mOpponentChoiceResults[opponentChoice][2] == playerChoice) { // Player won.
                    playerTotalScore += mWinScore;
                } else if (mOpponentChoiceResults[opponentChoice][1] == playerChoice) { // Draw
                    playerTotalScore += mDrawScore;
                }
            }


            return playerTotalScore;
        }
    }
}