namespace Day18
{
    public enum ChildState { NONE, LEFTCHILD, RIGHTCHILD }

    class Number : IHeapItem<Number>
    {
        public Pair ParentPair { get; set; }
        public int Length { get; set; } // Length represents the length a string would have had if there were one. Used during decoding to increment index.
        public ChildState ChildState { get; set; }
        public int HeapIndex { get; set; }
        public int distanceFromLeft { get; set; } // The number of RegularNumbers to the left of this number.
        public bool Valid { get; set; } = true; // Exploding a pair invalidates the Regularnumbers within.

        public void SetParent(Pair parent, ChildState childState)
        {
            ParentPair = parent;
            ChildState = childState;
            parent.SetChild(this, childState);
            UpdateDistance();
        }

        public int UpdateDistance()
        {
            distanceFromLeft = 0;
            Number curNumber = this;

            while (curNumber != null)
            {
                if (curNumber.ChildState == ChildState.RIGHTCHILD)
                {
                    distanceFromLeft += FindNumberCount(curNumber.ParentPair.LeftChild);
                }

                curNumber = curNumber.ParentPair;
            }

            return distanceFromLeft;
        }

        private int FindNumberCount(Number number)
        {
            if (number is RegularNumber)
            {
                return 1;
            }

            int count = FindNumberCount(((Pair)number).LeftChild);
            count += FindNumberCount(((Pair)number).RightChild);
            return count;
        }

        public int CompareTo(Number otherNumber)
        {
            UpdateDistance();
            otherNumber.UpdateDistance();
            return distanceFromLeft.CompareTo(otherNumber.distanceFromLeft);
        }
    }
}