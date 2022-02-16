namespace Day18
{
    class RegularNumber : Number, IHeapItem<RegularNumber>
    {
        public int Value { get; set; }

        public RegularNumber(char value, Pair parent, ChildState childState)
        {
            Value = int.Parse(value.ToString());
            Length = 1;
            SetParent(parent, childState);
            UpdateDistance();
        }

        public RegularNumber(int value, Pair parent, ChildState childState)
        {
            Value = value;
            Length = 1;
            SetParent(parent, childState);
            UpdateDistance();
        }

        public int CompareTo(RegularNumber otherNumber)
        {
            UpdateDistance();
            otherNumber.UpdateDistance();
            return -distanceFromLeft.CompareTo(otherNumber.distanceFromLeft);
        }
    }
}