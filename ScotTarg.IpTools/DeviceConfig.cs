using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ScotTarg.IpTools
{
    public class DeviceConfig
    {
        private byte[] deviceConfig = new byte[507];

        public string ReplyIP { get; private set; }
        public int ReplyPort { get; private set; }
        public bool DHCPEnabled
        {
            get { return (deviceConfig[DHCP_ENABLED_INDEX] == 1); }
            set { deviceConfig[DHCP_ENABLED_INDEX] = (byte)((value) ? 1 : 0); }
        }
        public string UniqueID
        {
            get { return GetString(deviceConfig, UNIQUE_ID_INDEX, UNIQUE_ID_LENGTH); }
        }
        public string ProductNr
        {
            get { return GetString(deviceConfig, PRODUCT_NR_INDEX, PRODUCT_NR_LENGTH); }
        }
        public string SystemVersion
        {
            get { return GetString(deviceConfig, SYS_VERSION_INDEX, SYS_VERSION_LENGTH); }
        }
        public string DeviceName
        {
            get { return GetString(deviceConfig, DEV_NAME_INDEX, DEV_NAME_LENGTH); }
            set
            {
                byte[] arr = ToByteArray(value, DEV_NAME_LENGTH);
                for (int n=0; n<DEV_NAME_LENGTH; n++)
                {
                    deviceConfig[DEV_NAME_INDEX + n] = arr[n];
                }
            }
        }
        public int DeviceAddr
        {
            get { return (int)(deviceConfig[DEV_ADDR_INDEX] << 8) | deviceConfig[DEV_ADDR_INDEX + 1]; }
            set
            {
                deviceConfig[DEV_ADDR_INDEX] = (byte)(value >> 8);
                deviceConfig[DEV_ADDR_INDEX + 1] = (byte)(value & 0xFF);
            }
        }
        public string MacAddress
        {
            get
            {
                return deviceConfig[MAC_ADDRESS_INDEX].ToString() + ":" +
                       deviceConfig[MAC_ADDRESS_INDEX + 1].ToString() + ":" +
                       deviceConfig[MAC_ADDRESS_INDEX + 2].ToString() + ":" +
                       deviceConfig[MAC_ADDRESS_INDEX + 3].ToString() + ":" +
                       deviceConfig[MAC_ADDRESS_INDEX + 4].ToString() + ":" +
                       deviceConfig[MAC_ADDRESS_INDEX + 5].ToString();
            }
            set
            {

            }
        }
        public IP IPAddress
        {
            get { return GetIP(deviceConfig, IP_ADDRESS_INDEX); }
            set
            {
                deviceConfig[IP_ADDRESS_INDEX] = value.A1;
                deviceConfig[IP_ADDRESS_INDEX+1] = value.A2;
                deviceConfig[IP_ADDRESS_INDEX+2] = value.A3;
                deviceConfig[IP_ADDRESS_INDEX+3] = value.A4;
            }
        }
        public IP SubnetMask
        {
            get { return GetIP(deviceConfig, SUBNET_MASK_INDEX); }
            set
            {
                deviceConfig[SUBNET_MASK_INDEX] = value.A1;
                deviceConfig[SUBNET_MASK_INDEX + 1] = value.A2;
                deviceConfig[SUBNET_MASK_INDEX + 2] = value.A3;
                deviceConfig[SUBNET_MASK_INDEX + 3] = value.A4;
            }
        }
        public IP DefaultGateway
        {
            get { return GetIP(deviceConfig, GATEWAY_INDEX); }
            set
            {
                deviceConfig[GATEWAY_INDEX] = value.A1;
                deviceConfig[GATEWAY_INDEX + 1] = value.A2;
                deviceConfig[GATEWAY_INDEX + 2] = value.A3;
                deviceConfig[GATEWAY_INDEX + 3] = value.A4;
            }
        }
        public int TcpPort
        {
            get {return (int)(deviceConfig[TCP_PORT_INDEX] << 8) | deviceConfig[TCP_PORT_INDEX + 1]; }
            set
            {
                deviceConfig[TCP_PORT_INDEX] = (byte)(value >> 8);
                deviceConfig[TCP_PORT_INDEX + 1] = (byte)(value & 0xFF);
            }
        }
        public int TimeOut
        {
            get { return (int)(deviceConfig[TIMEOUT_INDEX] << 8) | deviceConfig[TIMEOUT_INDEX + 1]; }
            set
            {
                deviceConfig[TIMEOUT_INDEX] = (byte)(value >> 8);
                deviceConfig[TIMEOUT_INDEX+1] = (byte)(value & 0xFF);
            }
        }
        public struct IP
        {
            public byte A1 { get; set; }
            public byte A2 { get; set; }
            public byte A3 { get; set; }
            public byte A4 { get; set; }

            public IP(byte a, byte b, byte c, byte d)
            {
                A1 = a;
                A2 = b;
                A3 = c;
                A4 = d;
            }

            public IP(byte[] bytes)
            {
                A1 = bytes[0];
                A2 = bytes[1];
                A3 = bytes[2];
                A4 = bytes[3];
            }


            public override string ToString()
            {
                return A1.ToString() + "." + A2.ToString() + "." + A3.ToString() + "." + A4.ToString();
            }
        }

        private static readonly int GET_SET_BYTE_INDEX = 3;      // Get=0x81 Set=0x41
        private static readonly int UNIQUE_ID_INDEX = 4;
        private static readonly int UNIQUE_ID_LENGTH = 16;
        private static readonly int PRODUCT_NR_INDEX = 20;
        private static readonly int PRODUCT_NR_LENGTH = 16;
        private static readonly int SYS_VERSION_INDEX = 36;
        private static readonly int SYS_VERSION_LENGTH = 16;
        private static readonly int DEV_NAME_INDEX = 52;
        private static readonly int DEV_NAME_LENGTH = 16;
        private static readonly int DEV_ADDR_INDEX = 68;        // 16 bit value
        private static readonly int DHCP_ENABLED_INDEX = 85;    // 0=disabled 1=enabled
        private static readonly int MAC_ADDRESS_INDEX = 86;     // 6 bytes
        private static readonly int IP_ADDRESS_INDEX = 92;      // 4 bytes
        private static readonly int SUBNET_MASK_INDEX = 96;     // 4 bytes
        private static readonly int GATEWAY_INDEX = 100;        // 4 bytes
        private static readonly int TCP_PORT_INDEX = 208;       // 16 bit Value
        private static readonly int TIMEOUT_INDEX = 210;        // 16 bit value in seconds for channel 1
        //private static readonly int TIMEOUT_C2_INDEX = 260;     // 16 bit value in seconds for channel 2


        public void AnalyseReply(byte[] msg, IPEndPoint ep)
        {
            ReplyIP = ep.Address.ToString();
            ReplyPort = ep.Port;

            if (msg.Length == 507)
            {
                deviceConfig = msg;
            }
        }

        public byte[] GetCommand()
        {
            deviceConfig[GET_SET_BYTE_INDEX] = 0x41;
            ushort cs = Commands.GetChecksum(deviceConfig.Take(504).ToArray());
            deviceConfig[504] = (byte)(cs >> 8);
            deviceConfig[505] = (byte)(cs & 0xff);
            return deviceConfig;
        }

        private static string GetString(byte[] buffer, int startIndex, int length)
        {
            List<byte> bytes = new List<byte>();
            for (int n = startIndex; n < startIndex + length; n++)
            {
                if (buffer[n] == 0)
                {
                    break;
                }
                bytes.Add(buffer[n]);
            }

            string ret = ASCIIEncoding.ASCII.GetString(bytes.ToArray());
            return ret;
        }

        private static byte[] ToByteArray(string str, int length)
        {
            List<byte> byteArray =  ASCIIEncoding.ASCII.GetBytes(str).ToList();
            if (byteArray.Count>length)
            {
                byteArray = byteArray.Take(length).ToList();
            }
            while(byteArray.Count < length)
            {
                byteArray.Add(0x00);
            }
            return byteArray.ToArray();
        }

        private static IP GetIP(byte[] buffer, int startIndex)
        {
            byte a, b, c, d;
            a = buffer[startIndex];
            b = buffer[startIndex+1];
            c = buffer[startIndex+2];
            d = buffer[startIndex+3];
            return new IP(a, b, c, d);
        }
    }
}
