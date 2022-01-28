namespace Day15
{
    class Node : IHeapItem<Node>
    {
        public int mapY;
        public int mapX;
        public int risk; // The cost of stepping on this node.

        public Node parent;
        public int gCost; // The accumulated risk of the path from the start node to this node.
        public int hCost; // The distance to the end node.
        
        int heapIndex;

        public int fCost { get { return gCost + hCost; } }

        public int HeapIndex { get { return heapIndex; } set { heapIndex = value; } }

        public int CompareTo(Node otherNode)
        {
            int compare = fCost.CompareTo(otherNode.fCost);
            if (compare == 0)
            {
                compare = hCost.CompareTo(otherNode.hCost);
            }

            return -compare;
        }
    }
}