namespace Day18
{
    class Pair : Number, IHeapItem<Pair>
    {
        public Number LeftChild { get; set; }
        public Number RightChild { get; set; }

        // Used for adding two pairs.
        public Pair(Pair leftPair, Pair rightPair)
        {
            LeftChild = leftPair;
            LeftChild.SetParent(this, ChildState.LEFTCHILD);

            RightChild = rightPair;
            RightChild.SetParent(this, ChildState.RIGHTCHILD);

            Length = LeftChild.Length + RightChild.Length + 3;
            UpdateDistance();
        }

        // Used for splitting a number into a pair.
        public Pair(int leftNumber, int rightNumber, Pair parent, ChildState childState)
        {
            LeftChild = new RegularNumber(leftNumber, this, ChildState.LEFTCHILD);
            RightChild = new RegularNumber(rightNumber, this, ChildState.RIGHTCHILD);

            SetParent(parent, childState);

            Length = 5;
            UpdateDistance();
        }

        // Used when initiating decoding.
        public Pair(string input)
        {
            DecodeString(input);
            UpdateDistance();
        }

        // Used during decoding.
        public Pair(string input, Pair parent, ChildState childState)
        {
            SetParent(parent, childState);
            DecodeString(input);
            UpdateDistance();
        }

        public Number DecodeString(string remainingString)
        {
            int curIndex = 1;
            LeftChild = CreateNumber(remainingString, curIndex, ChildState.LEFTCHILD);

            curIndex += LeftChild.Length + 1;
            RightChild = CreateNumber(remainingString, curIndex, ChildState.RIGHTCHILD);

            Length = LeftChild.Length + RightChild.Length + 3;
            return this;
        }

        private Number CreateNumber(string remainingString, int curIndex, ChildState childState)
        {
            return remainingString[curIndex] == '[' ?
                (Number)new Pair(remainingString.Substring(curIndex), this, childState) :
                (Number)new RegularNumber(remainingString[curIndex], this, childState);
        }

        public void SetChild(Number newChild, ChildState childState)
        {
            switch (childState)
            {
                case ChildState.NONE:
                    break;

                case ChildState.LEFTCHILD:
                    LeftChild = newChild;
                    break;

                case ChildState.RIGHTCHILD:
                    RightChild = newChild;
                    break;

                default:
                    break;
            }
        }

        public int CompareTo(Pair otherNumber)
        {
            int compare = distanceFromLeft.CompareTo(otherNumber.distanceFromLeft);
            
            // If both numbers have an equal number of numbers to the left of themselves,
            // then choose the one nested deepest.
            if (compare == 0)
            {
                int thisParentCounter = 0;
                Pair curParent = ParentPair;

                if (curParent != null)
                {
                    thisParentCounter++;
                }

                while (curParent.ParentPair != null)
                {
                    curParent = curParent.ParentPair;
                    thisParentCounter++;
                }

                int OtherParentCounter = 0;
                curParent = otherNumber.ParentPair;

                if (curParent != null)
                {
                    OtherParentCounter++;
                }

                while (curParent.ParentPair != null)
                {
                    curParent = curParent.ParentPair;
                    OtherParentCounter++;
                }
                compare = thisParentCounter.CompareTo(OtherParentCounter);
            }

            return -compare;
        }
    }
}