namespace Day7 {
    internal class Day7 {

        static int mSizeSumOfAllDirsWithSizeBelow100k = 0;

        static int mSizeOfSmallestDirToRemove = int.MaxValue;
        static int mMinSpaceToRemove = 0;
        const int kTotalSpace = 70000000;
        const int kUpdateSize = 30000000;

        static readonly Dir mRoot = new("/");

        static void Main(string[] args) {
            string[] input = File.ReadAllLines("D:/AdventOfCode/2022/Day7/input.txt");

            //Console.WriteLine(Part1And2(input, isPart1: true));
            Console.WriteLine(Part1And2(input, isPart1: false));
        }

        static int Part1And2(string[] input, bool isPart1) {
            Dir currentDir = mRoot;

            for (int i = 1; i < input.Length; i++) {
                string[] lineParts = input[i].Split(' ');

                if (lineParts[1] == "cd") {
                    currentDir = lineParts[2] == ".." ? currentDir.Parent : currentDir.Dirs[lineParts[2]];
                    continue;
                }

                i += AddItemsToDir(ref currentDir, ref input, i + 1);
            }

            if (isPart1) {
                GetSizeOfDir_Part1(mRoot);
                return mSizeSumOfAllDirsWithSizeBelow100k;
            } else {
                int remainingSpace = kTotalSpace - GetSizeOfDir_Part2(mRoot);
                mMinSpaceToRemove = kUpdateSize - remainingSpace;
                GetSizeOfDir_Part2(mRoot);
                return mSizeOfSmallestDirToRemove;
            }
        }

        /// <returns>The number of items added.</returns>
        static int AddItemsToDir(ref Dir currentDir, ref string[] input, int startInputIndex) {
            int numberOfItemsAdded = 0;

            for (int i = startInputIndex; i < input.Length; i++) {
                if (input[i][0] == '$') {
                    break;
                }

                string[] valueAndName = input[i].Split(' ');

                if (valueAndName[0] == "dir") {
                    currentDir.AddSubDir(new Dir(valueAndName[1]));
                } else {
                    currentDir.AddDataFile(valueAndName[1], int.Parse(valueAndName[0]));
                }
                numberOfItemsAdded++;
            }

            return numberOfItemsAdded;
        }

        static int GetSizeOfDir_Part1(Dir currentDir) {
            int size = 0;

            foreach (var subDir in currentDir.Dirs) {
                size += GetSizeOfDir_Part1(subDir.Value);
            }

            foreach (var dataFile in currentDir.DataFiles) {
                size += dataFile.Value;
            }

            if (size < 100_000) {
                mSizeSumOfAllDirsWithSizeBelow100k += size;
            }

            return size;
        }

        static int GetSizeOfDir_Part2(Dir currentDir) {
            int size = 0;

            foreach (var subDir in currentDir.Dirs) {
                size += GetSizeOfDir_Part2(subDir.Value);
            }

            foreach (var dataFile in currentDir.DataFiles) {
                size += dataFile.Value;
            }

            if (mMinSpaceToRemove == 0) {
                return size;
            }

            if (size > mMinSpaceToRemove && size < mSizeOfSmallestDirToRemove) {
                mSizeOfSmallestDirToRemove = size;
            }

            return size;
        }
    }
}