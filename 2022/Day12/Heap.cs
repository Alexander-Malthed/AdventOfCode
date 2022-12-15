namespace Day12 {
    // Guarantees that all items added to the heap will have an heap index, and be comparable.
    public interface IHeapItem<T> : IComparable<T> {
        int HeapIndex {
            get;
            set;
        }
    }

    class Heap<T> where T : IHeapItem<T> {
        T[] items;
        int currentItemCount;

        public Heap(int maxHeapSize) {
            items = new T[maxHeapSize];
        }

        public void Add(T item) {
            item.HeapIndex = currentItemCount;
            items[currentItemCount] = item;
            SortUp(item);
            currentItemCount++;
        }

        public T RemoveFirst() {
            T firstItem = items[0];
            currentItemCount--;
            items[0] = items[currentItemCount];
            items[0].HeapIndex = 0;
            SortDown(items[0]);
            return firstItem;
        }

        public void UpdateItem(T item) {
            SortUp(item);
            SortDown(item);
        }

        public int Count {
            get { return currentItemCount; }
        }

        public bool Contains(T item) {
            return Equals(items[item.HeapIndex], item);
        }

        void SortDown(T item) {
            while (true) {
                int childIndexLeft = item.HeapIndex * 2 + 1;
                int childIndexRight = item.HeapIndex * 2 + 2;
                int swapIndex = 0;

                if (childIndexLeft < currentItemCount) {
                    swapIndex = childIndexLeft;

                    if (childIndexRight < currentItemCount &&
                        items[childIndexLeft].CompareTo(items[childIndexRight]) < 0) {
                        swapIndex = childIndexRight;
                    }

                    if (item.CompareTo(items[swapIndex]) < 0) {
                        Swap(item, items[swapIndex]);
                    } else
                        return;
                } else
                    return;
            }
        }

        void SortUp(T item) {
            int parentIndex = (item.HeapIndex - 1) / 2;

            while (true) {
                T ParentItem = items[parentIndex];
                if (item.CompareTo(ParentItem) > 0) {
                    Swap(item, ParentItem);
                } else
                    break;

                parentIndex = (item.HeapIndex - 1) / 2;
            }
        }

        void Swap(T itemA, T itemB) {
            items[itemA.HeapIndex] = itemB;
            items[itemB.HeapIndex] = itemA;
            int itemAIndex = itemA.HeapIndex;
            itemA.HeapIndex = itemB.HeapIndex;
            itemB.HeapIndex = itemAIndex;
        }
    }
}
