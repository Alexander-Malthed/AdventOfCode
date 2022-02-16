using System.Collections.Generic;
using System;

namespace Day16
{
    class Packet
    {
        public string DataString { get; private set; }
        public int Version { get; private set; }
        public int Id { get; private set; }
        public ulong Value { get; set; }
        public List<Packet> SubPackets { get; private set; } = new List<Packet>();

        public Packet(string data)
        {
            DecodePacket(data);
        }

        private Packet DecodePacket(string data)
        {
            // bits 1,2,3
            Version = Convert.ToInt32(data.Substring(0, 3), 2);

            // bits 4,5,6
            Id = Convert.ToInt32(data.Substring(3, 3), 2);

            if (Id == 4)
            {
                int bitLength = 0;
                CalcLiteral(data.Substring(6, data.Length - 6), out bitLength);
                DataString = data.Substring(0, 6 + bitLength);
                return this;
            }

            // bit 7, length type
            if (int.Parse(data[6].ToString()) == 0)
            {
                AddSubpacketsLengthTypeZero(data);
            }
            else
            {
                AddSubpacketsLengthTypeOne(data);
            }

            return this;
        }

        private void CalcLiteral(string data, out int bitLength)
        {
            string appendedBits = string.Empty;
            string fiveBits;
            bitLength = 0;
            int itr = 0;

            while (true)
            {
                fiveBits = data.Substring(itr * 5, 5);
                appendedBits += fiveBits.Remove(0, 1);

                if (int.Parse(fiveBits[0].ToString()) == 0)
                {
                    bitLength = (1 + itr) * 5;
                    break;
                }
                else
                {
                    itr++;
                }
            }

            Value = Convert.ToUInt64(appendedBits, 2);
        }

        private void AddSubpacketsLengthTypeZero(string data)
        {
            int lengthInBits = Convert.ToInt32(data.Substring(7, 15), 2);
            int subPacketStartIndex = 22;

            while (lengthInBits > 0)
            {
                Packet subPacket = new Packet(data.Substring(subPacketStartIndex, data.Length - subPacketStartIndex));
                lengthInBits -= subPacket.DataString.Length;
                subPacketStartIndex += subPacket.DataString.Length;
                SubPackets.Add(subPacket);
            }

            DataString = data.Substring(0, subPacketStartIndex);
        }

        private void AddSubpacketsLengthTypeOne(string data)
        {
            int numOfsubPackets = Convert.ToInt32(data.Substring(7, 11), 2);
            int subPacketStartIndex = 18;

            for (int i = 0; i < numOfsubPackets; i++)
            {
                Packet subPacket = new Packet(data.Substring(subPacketStartIndex, data.Length - subPacketStartIndex));
                subPacketStartIndex += subPacket.DataString.Length;
                SubPackets.Add(subPacket);
            }

            DataString = data.Substring(0, subPacketStartIndex);
        }
    }
}