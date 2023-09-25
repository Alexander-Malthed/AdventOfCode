namespace Day13 {
    internal class IntegerValue : IValue {
        public int Value { get; private set; } = 0;

        public IntegerValue(int value) {
            Value = value;
        }
    }
}
