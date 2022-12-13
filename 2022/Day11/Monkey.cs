namespace Day11 {
    internal class Monkey {
        public Queue<ulong> items;
        readonly OperationType operation;
        readonly ulong operationNumber;
        public ulong TestNumber { get; private set; }
        readonly Dictionary<int, Monkey> monkeys;
        readonly int testTrueMonkeyIndex, testFalseMonkeyIndex;
        public ulong NumberOfTimesInspected { get; private set; } = 0;
        public static ulong LeastCommonMultiple { get; set; } = 1;

        public Monkey(IEnumerable<ulong> startingItems, OperationType operation, ulong operationNumber, ulong testNumber, ref Dictionary<int, Monkey> monkeys, int testTrueMonkeyIndex, int testFalseMonkeyIndex) {
            items = new(startingItems);
            this.operation = operation;
            this.operationNumber = operationNumber;
            TestNumber = testNumber;
            this.monkeys = monkeys;
            this.testTrueMonkeyIndex = testTrueMonkeyIndex;
            this.testFalseMonkeyIndex = testFalseMonkeyIndex;
        }

        public void PlayTurn(bool part1) {
            int itemCount = items.Count;
            if (part1) {
                for (int itemNumber = 0; itemNumber < itemCount; itemNumber++) {

                    ulong inspectedItem = InspectPart1();
                    Throw(Test(inspectedItem), inspectedItem);
                }
            } else {
                for (int itemNumber = 0; itemNumber < itemCount; itemNumber++) {

                    ulong inspectedItem = InspectPart2();
                    Throw(Test(inspectedItem), inspectedItem);
                }
            }
        }

        public void Catch(ulong item) {
            items.Enqueue(item);
        }

        private ulong InspectPart1() {
            NumberOfTimesInspected++;
            ulong itemToInspect = items.Dequeue();

            switch (operation) {
                case OperationType.ADD_NUMBER:
                    return (itemToInspect + operationNumber) / 3;
                case OperationType.ADD_SELF:
                    return (itemToInspect + itemToInspect) / 3;
                case OperationType.MULTIPLY_NUMBER:
                    return itemToInspect * operationNumber / 3;
                case OperationType.MULTIPLY_SELF:
                    return itemToInspect * itemToInspect / 3;
            }
            return 0;
        }

        private ulong InspectPart2() {
            NumberOfTimesInspected++;
            ulong itemToInspect = items.Dequeue();

            switch (operation) {
                case OperationType.ADD_NUMBER:
                    return (itemToInspect + operationNumber) % LeastCommonMultiple;
                case OperationType.ADD_SELF:
                    return (itemToInspect + itemToInspect) % LeastCommonMultiple;
                case OperationType.MULTIPLY_NUMBER:
                    return itemToInspect * operationNumber % LeastCommonMultiple;
                case OperationType.MULTIPLY_SELF:
                    return itemToInspect * itemToInspect % LeastCommonMultiple;
            }
            return 0;
        }

        private int Test(ulong itemToTest) {
            return itemToTest % TestNumber == 0 ? testTrueMonkeyIndex : testFalseMonkeyIndex;
        }

        private void Throw(int catchMonkeyIndex, ulong itemBeingThrown) {
            monkeys[catchMonkeyIndex].Catch(itemBeingThrown);
        }
    }

    enum OperationType {
        ADD_NUMBER,
        ADD_SELF,
        MULTIPLY_NUMBER,
        MULTIPLY_SELF
    }
}
