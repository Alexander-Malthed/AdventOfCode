namespace Day5 {
    struct Instruction {
        public int mNumberOfBoxes;
        public int mFromStack;
        public int mToStack;

        public Instruction(string instructionString) {
            int[] instructionNumbers = Array.ConvertAll(instructionString.Remove(0, 5).Replace(" from ", ",").Replace(" to ", ",").Split(','), int.Parse);
            mNumberOfBoxes = instructionNumbers[0];
            mFromStack = instructionNumbers[1] - 1;
            mToStack = instructionNumbers[2] - 1;
        }
    }
}
