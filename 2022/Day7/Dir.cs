namespace Day7 {
    internal class Dir {
        public string Name { get; private set; }
        public Dir? Parent { get; set; }
        public Dictionary<string, Dir> Dirs { get; private set; } = new();
        public Dictionary<string, int> DataFiles { get; private set; } = new();

        public Dir(string name) {
            Name = name;
        }

        public void AddSubDir(Dir dirToAdd) {
            Dirs.Add(dirToAdd.Name, dirToAdd);
            dirToAdd.Parent = this;
        }

        public void AddDataFile(string dataFileName, int dataSize) {
            DataFiles.Add(dataFileName, dataSize);
        }
    }
}
