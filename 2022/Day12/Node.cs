namespace Day12 {
    class Node : IHeapItem<Node> {
        public int mapY;
        public int mapX;
        public int height; // The height of this area in the heightmap.

        public Node? parent;
        public int gCost = 0; // The accumulated length of the path from the start node to this node.
        public int hCost = 0; // The distance from this node to the end node.

        public int FCost { get { return gCost + hCost; } }

        public int HeapIndex { get; set; } 

        public Node(int height, int mapY, int mapX) {
            this.height = height;
            this.mapY = mapY;
            this.mapX = mapX;
        }

        public int CompareTo(Node otherNode) {
            int compare = FCost.CompareTo(otherNode.FCost);
            if (compare == 0) {
                compare = hCost.CompareTo(otherNode.hCost);
            }

            return -compare;
        }

        public void ResetForNewRound() {
            parent = null;
            gCost = int.MaxValue;
            hCost = int.MaxValue;
            HeapIndex = 0;
        }
    }
}
