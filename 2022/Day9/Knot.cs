namespace Day9 {
    public class Knot {
        public int[] Position { get; set; } = new int[2];
        public string id { get; private set; }
        public Knot? ChildKnot { get; set; }

        public Knot(Knot? childKnot, string id) {
            ChildKnot = childKnot;
            this.id = id;
        }
    }
}
