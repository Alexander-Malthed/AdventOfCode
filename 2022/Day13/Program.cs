using System.Text.RegularExpressions;

namespace Day13 {
    internal class Day13 {
        static void Main(string[] args) {
            string[] input = File.ReadAllLines("D:/AdventOfCode/2022/Day13/input.txt");

            Console.WriteLine(Part1(input));
            Console.WriteLine(Part2(input));
        }

        static int Part1(string[] input) {
            int indicesSumOfOrderedPairs = 0;
            int pairIndex = 0;

            for (int i = 0; i < input.Length; i += 3) {
                ++pairIndex;

                ListValue leftValue = CreateValue(input[i][1..]).Item1;
                ListValue rightValue = CreateValue(input[i + 1][1..]).Item1;

                var test = CompareListValues(leftValue, rightValue);
                if (test == 1) {
                    indicesSumOfOrderedPairs += pairIndex;
                }
            }

            return indicesSumOfOrderedPairs;
        }

        static int Part2(string[] input) {
            string[] extendedInput = new string[input.Length + 3];
            input.CopyTo(extendedInput, 0);
            extendedInput[input.Length] = "";
            extendedInput[input.Length + 1] = "[[2]]";
            extendedInput[input.Length + 2] = "[[6]]";

            List<ListValue> list = new();

            for (int i = 0; i < extendedInput.Length; i += 3) {
                list.Add(CreateValue(extendedInput[i][1..]).Item1);
                list.Add(CreateValue(extendedInput[i + 1][1..]).Item1);
            }

            list.Sort((ListValue a, ListValue b) => CompareListValues(b, a));

            int dividerPacket1Index = 0;
            int dividerPacket2Index = 0;

            for (int i = 0; i < list.Count; i++) {

                // list[i] => [[x]]

                // currentPacketValues => [x], ?
                var currentPacketValues = list[i].Values;
                if (currentPacketValues.Count != 1) {
                    continue;
                }

                // subPacket => [x]
                var subPacket = currentPacketValues[0];
                if (subPacket is not ListValue) {
                    continue;
                }

                // subPacketValues => x, ?
                var subPacketValues = ((ListValue)subPacket).Values;
                if (subPacketValues.Count != 1) {
                    continue;
                }

                // integerValue => x
                var integerValue = subPacketValues[0];
                if (integerValue is not IntegerValue) {
                    continue;
                }

                if (((IntegerValue)integerValue).Value == 2) {
                    dividerPacket1Index = i + 1;
                } else if (((IntegerValue)integerValue).Value == 6) {
                    dividerPacket2Index = i + 1;
                }
            }

            return dividerPacket1Index * dividerPacket2Index;
        }

        /// <returns>
        /// ListValue: The pair containing Integers and/or more ListValues. 
        /// int: The number of steps taken forward in the string when creating this specific ListValue instance, 0 for the top ListValue.
        /// </returns>
        static Tuple<ListValue, int> CreateValue(string remaningLine) {
            ListValue list = new ListValue();
            for (int i = 0; i < remaningLine.Length; i++) {
                switch (remaningLine[i]) {
                    case '[':
                        var newValue = CreateValue(remaningLine[(i + 1)..]);
                        list.Values.Add(newValue.Item1);
                        i += newValue.Item2;
                        break;

                    case ']':
                        return new(list, i + 1);

                    case ',':
                        break;

                    default:
                        string valueString = Regex.Match(remaningLine[i..], @"\d+").Value;
                        list.Values.Add(new IntegerValue(int.Parse(valueString)));
                        break;
                }
            }

            return new(list, 0);
        }

        static int CompareListValues(ListValue left, ListValue right) {
            int i = 0;
            while (i < left.Values.Count && i < right.Values.Count) {

                IValue leftListElement = left.Values[i];
                IValue rightListElement = right.Values[i];
                int compareResult;

                if (leftListElement is IntegerValue && rightListElement is IntegerValue) {
                    compareResult = CompareIntegerValues(
                        (IntegerValue)leftListElement, 
                        (IntegerValue)rightListElement);

                } else if (leftListElement is ListValue && rightListElement is ListValue) {
                    compareResult = CompareListValues(
                        (ListValue)leftListElement, 
                        (ListValue)rightListElement);

                } else if (leftListElement is ListValue && rightListElement is IntegerValue) {
                    compareResult = CompareListValues(
                        (ListValue)leftListElement, 
                        new ListValue(rightListElement));

                } else if (((ListValue)rightListElement).Values.Count > 0) {
                    compareResult = CompareListValues(
                        new ListValue(leftListElement), 
                        (ListValue)rightListElement);

                } else {
                    compareResult = -1;
                }

                if (compareResult != 0) {
                    return compareResult;
                } else if (i == left.Values.Count - 1 && i == right.Values.Count - 1) {
                    return 0;
                }

                i++;
            }

            if (i == left.Values.Count) {
                if (left.Values.Count == right.Values.Count) {
                    return 0;
                }
                return 1;
            }
            return -1;
        }

        static int CompareIntegerValues(IntegerValue left, IntegerValue right) {
            if (left.Value == right.Value) {
                return 0;
            }
            return left.Value < right.Value ? 1 : -1;
        }
    }
}