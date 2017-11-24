using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScotTarg.TargetTools
{
    /// <summary>
    /// This class represents a command that is sent to or from the target.
    /// It holds all the elements contained in a target comms packet and provides
    /// helper functions to process the comms packet.
    /// </summary>
    public class TargetCommand
    {
        private const byte OPEN_CHAR = 60;
        private const byte CLOSE_CHAR = 62;
        private const byte ACK_CHAR = 0x06;
        private const byte NAK_CHAR = 0x15;

        public byte CommandByte { get; set; }
        public bool Reply { get; set; }
        public bool ACK { get; set; }
        public byte[] Data { get; set; }
        public int Length
        {
            get { return Data.Length + 4; }
        }

        public TargetCommand()
        {
            CommandByte = 0;
            Reply = false;
            ACK = false;
            Data = new byte[0];
        }

        /// <summary>
        /// Converts the TargetCommand to a byte array that can be sent to the target
        /// </summary>
        /// <returns></returns>
        public byte[] GetPacket()
        {
            byte[] packet = new byte[Data.Length + 4];
            packet[0] = OPEN_CHAR;
            packet[1] = (byte)Length;
            packet[2] = CommandByte;
            packet[Data.Length + 3] = CLOSE_CHAR;
            for (int n = 0; n < Data.Length; n++)
            {
                packet[n + 3] = Data[n];
            }
            return packet;
        }

        /// <summary>
        /// Converts a data packet to a TargetCommand Instance.
        /// Throws an exception if the packet is not valid.
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        public static TargetCommand Parse(byte[] packet)
        {
            TargetCommand cmd = new TargetCommand();
            try
            {
                int startIndex = 0;
                int endIndex = 0;
                int dataIndex = 0;

                if (!TargetCommand.ValidatePacket(packet))
                {
                    throw new Exception("Not a Valid packet");
                }
                for (int n = 0; n < packet.Length; n++)
                {
                    if (packet[n] == OPEN_CHAR)
                    {
                        startIndex = n;
                        break;
                    }
                }
                dataIndex = startIndex + 2;
                if (packet[startIndex + 2] == ACK_CHAR || packet[startIndex + 2] == NAK_CHAR)
                {
                    cmd.CommandByte = packet[startIndex + 3];
                    cmd.Reply = true;
                    cmd.ACK = packet[startIndex + 2] == ACK_CHAR;
                    dataIndex = startIndex + 4;
                }
                else
                {
                    cmd.CommandByte = packet[startIndex + 2];
                    dataIndex = startIndex + 3;
                }
                endIndex = startIndex + packet[startIndex + 1] - 1;
                cmd.Data = new byte[packet[startIndex + 1] - 4];
                for (int n = 0; n < cmd.Data.Length; n++)
                {
                    cmd.Data[n] = packet[dataIndex + n];
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Packet does not contain a valid command", ex);
            }
            return cmd;
        }

        /// <summary>
        /// Check that the buffer contains a valid target comms packet
        /// </summary>
        /// <param name="packet"></param>
        /// <returns></returns>
        public static bool ValidatePacket(byte[] packet)
        {
            return ValidatePacket(packet, packet.Length);
        }

        /// <summary>
        /// Check that the buffer contains a valid target comms packet within the given length
        /// </summary>
        /// <param name="packet"></param>
        /// <param name="len"></param>
        /// <returns>true if buffer contains a valid packet</returns>
        public static bool ValidatePacket(byte[] packet, int len)
        {
            int startIndex = 0;
            for (int x = 0; x < len; x++)
            {
                if (packet[x] == OPEN_CHAR)
                {
                    startIndex = x;
                    break;
                }
            }
            if (len < startIndex + 4)
            {
                return false;
            }
            else if (len < packet[startIndex + 1])
            {
                return false;
            }
            else if (packet[packet[startIndex + 1] - 1] != CLOSE_CHAR)
            {
                return false;
            }
            return true;
        }
    }
}
