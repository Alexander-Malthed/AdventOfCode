namespace Day13 {
    internal class ListValue : IValue {
        public List<IValue> Values { get; set; } = new List<IValue>();

        public ListValue() { }

        public ListValue(IValue singleValueToAdd) {
            Values.Add(singleValueToAdd);
        }
    }
}
