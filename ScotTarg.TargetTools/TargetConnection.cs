using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ScotTarg.TargetTools
{
    public class TargetConnection
    {
        private const int BUFFER_SIZE = 100;
        private const int READ_TIMEOUT = 700;

        private uint targetId = 0;
        private TcpClient socket;

        public bool Connected { get { return (socket != null && socket.Connected); } }
        public uint TargetID { get { return targetId; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Unique identifyer for the target being connected to</param>
        public TargetConnection(uint id)
        {
            targetId = id;
        }

        /// <summary>
        /// Connect to the target
        /// </summary>
        /// <param name="address">Format: [ip_address]:[port]</param>
        /// <returns></returns>
        public bool Connect(string address)
        {
            string tcpAddress = address;
            int port = 0;
            if (address.IndexOf(":") < 0)
            {
                throw new Exception("No TCP port specified");
            }
            if (!int.TryParse(address.Substring(address.IndexOf(":") + 1), out port))
            {
                throw new Exception("No TCP port not valid");
            }
            address = address.Substring(0, address.IndexOf(":"));
            socket = new TcpClient();
            socket.ReceiveBufferSize = 20;
            socket.ReceiveTimeout = READ_TIMEOUT;
            try
            {
                socket.Connect(address, port);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Disconnect from the target.
        /// </summary>
        /// <returns></returns>
        public bool Disconnect()
        {
            if (socket != null)
            {
                if (socket.Connected)
                {
                    socket.Close();
                    socket = null;
                }
            }
            return true;
        }

        /// <summary>
        /// Send a target command to the target
        /// </summary>
        /// <param name="cmd"></param>
        public void SendTargetCommand(TargetCommand cmd)
        {
            SendTargetCommand(cmd.GetPacket());
        }

        /// <summary>
        /// Send a command packet (array of bytes) to the target
        /// </summary>
        /// <param name="packet"></param>
        public void SendTargetCommand(byte[] packet)
        {
            if (socket != null && socket.Connected)
            {
                try
                {
                    NetworkStream stream = socket.GetStream();
                    stream.Write(packet, 0, packet.Length);
                }
                catch
                { }
            }
        }

        /// <summary>
        /// Receive a reply from the target.
        /// </summary>
        /// <returns></returns>
        public byte[] GetTargetReply()
        {
            byte[] buffer = new byte[BUFFER_SIZE];
            int index = 0;
            if (socket != null && socket.Connected)
            {
                DateTime to = DateTime.Now.AddMilliseconds(READ_TIMEOUT);
                do
                {
                    if (DateTime.Now > to)
                    {
                        break;
                    }
                    try
                    {
                        NetworkStream stream = socket.GetStream();
                        index = stream.Read(buffer, index, BUFFER_SIZE - index);
                        //read bytes untill a full packet is received
                    }
                    catch
                    { }
                }
                while (!TargetCommand.ValidatePacket(buffer, index));
            }
            Array.Resize(ref buffer, index);
            return buffer;
        }

    }
}
