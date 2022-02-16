using System;
using System.IO;
using System.Linq;

namespace Day16
{
    class Day16
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText("D:/AdventOfCode/2021/Day16/input.txt");
            input = string.Join(string.Empty, input.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

            Packet outerPacket = new Packet(input);
            ulong result = 0;

            // Part 1
            //result = (ulong)GetSumOfAllVersionNumbers(outerPacket);
            
            // Part 2
            result = EvaluatePacket(outerPacket);

            Console.WriteLine(result);
            File.WriteAllText("D:/AdventOfCode/2021/Day16/result.txt", result.ToString());
        }

        // Part 1
        static int GetSumOfAllVersionNumbers(Packet packet)
        {
            int result = packet.Version;
            foreach (Packet subPacket in packet.SubPackets)
            {
                result += GetSumOfAllVersionNumbers(subPacket);
            }

            return result;
        }

        // Part 2
        static ulong EvaluatePacket(Packet packet)
        {
            switch (packet.Id)
            {
                case 0:
                    return Sum(packet);

                case 1:
                    return Product(packet);

                case 2:
                    return Minimum(packet);

                case 3:
                    return Maximum(packet);

                case 4:
                    return packet.Value;

                case 5:
                    return Greater(packet);

                case 6:
                    return Less(packet);

                case 7:
                    return Equal(packet);

                default:
                    return 0;
            }
        }

        static ulong Sum(Packet packet)
        {
            ulong result = 0;

            foreach (Packet subPacket in packet.SubPackets)
            {
                result += EvaluatePacket(subPacket);
            }

            packet.Value = result;
            return result;
        }

        static ulong Product(Packet packet)
        {
            ulong result = EvaluatePacket(packet.SubPackets[0]);

            for (int i = 1; i < packet.SubPackets.Count; i++)
            {
                result *= EvaluatePacket(packet.SubPackets[i]);
            }

            packet.Value = result;
            return result;
        }

        static ulong Minimum(Packet packet)
        {
            ulong minValue = EvaluatePacket(packet.SubPackets[0]);

            for (int i = 1; i < packet.SubPackets.Count; i++)
            {
                ulong curValue = EvaluatePacket(packet.SubPackets[i]);
                if (curValue < minValue)
                {
                    minValue = curValue;
                }
            }

            packet.Value = minValue;
            return minValue;
        }

        static ulong Maximum(Packet packet)
        {
            ulong maxValue = EvaluatePacket(packet.SubPackets[0]);

            for (int i = 1; i < packet.SubPackets.Count; i++)
            {
                ulong curValue = EvaluatePacket(packet.SubPackets[i]);
                if (curValue > maxValue)
                {
                    maxValue = curValue;
                }
            }

            packet.Value = maxValue;
            return maxValue;
        }

        static ulong Greater(Packet packet)
        {
            ulong firstValue = EvaluatePacket(packet.SubPackets[0]);
            ulong secondValue = EvaluatePacket(packet.SubPackets[1]);

            ulong result = (ulong)(firstValue > secondValue ? 1 : 0);
            packet.Value = result;
            return result;
        }

        static ulong Less(Packet packet)
        {
            ulong firstValue = EvaluatePacket(packet.SubPackets[0]);
            ulong secondValue = EvaluatePacket(packet.SubPackets[1]);

            ulong result = (ulong)(firstValue < secondValue ? 1 : 0);
            packet.Value = result;
            return result;
        }

        static ulong Equal(Packet packet)
        {
            ulong firstValue = EvaluatePacket(packet.SubPackets[0]);
            ulong secondValue = EvaluatePacket(packet.SubPackets[1]);

            ulong result = (ulong)(firstValue == secondValue ? 1 : 0);
            packet.Value = result;
            return result;
        }
    }
}