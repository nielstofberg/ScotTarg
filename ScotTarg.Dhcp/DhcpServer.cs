using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScotTarg.Dhcp
{
    /// <summary>
    /// This class acts as a dhcp server.
    /// When Start is called on an instance of the class, it starts listening for DHCP requests.
    /// Whenever a request is received, a new IP address is issued to the client allong with the other
    /// requested network info such as: Subnet mask, Dns Server and default gateway.
    /// </summary>
    public class DhcpServer
    {
        private const int SERVER_PORT = 67;
        private const int CLIENT_PORT = 68;

        private IPEndPoint broadcastAddressIn = new IPEndPoint(IPAddress.Any, SERVER_PORT);
        private IPEndPoint broadcastAddressOut = new IPEndPoint(IPAddress.Broadcast, CLIENT_PORT);
        private UdpClient clientIn;
        private Thread responseThread;
        private bool replyActive = false;
        private List<DhcpProcess> requests = new List<DhcpProcess>();


        private uint _leaseTime = 84400; //4 hours
        private IPAddress _dhcpServer = IPAddress.Parse("0.0.0.0");
        private IPAddress _gateway = IPAddress.Parse("0.0.0.0");
        private IPAddress _dns = IPAddress.Parse("0.0.0.0");
        private IPAddress _subnetMask = IPAddress.Parse("255.255.255.0");
        private IPAddress _firstIp = IPAddress.Parse("0.0.0.0");
        private IPAddress _lastIp = IPAddress.Parse("0.0.0.0");
        private List<Assignment> assigned = new List<Assignment>();
        private IPAddress _nextIp = IPAddress.Parse("0.0.0.0");

        public struct Assignment
        {
            public DateTime AssignTime;
            public string MACAddress;
            public IPAddress Address;

            public Assignment(string mac, IPAddress addr)
            {
                MACAddress = mac;
                Address = addr;
                AssignTime = DateTime.Now;
            }
        }

        public Assignment[] AssignedAddresses
        {
            get { return assigned.ToArray(); }
        }

        /// <summary>
        /// Indicates if the server is running or not
        /// </summary>
        public bool IsRunning
        {
            get { return replyActive; }
        }

        /// <summary>
        /// Lease time of IP address to client (in seconds)
        /// </summary>
        public uint LeaseTime
        {
            get { return _leaseTime; }
            set { _leaseTime = value; }
        }

        /// <summary>
        /// The IP address of the DHCP server
        /// (IE the address of the machine this server is running on)
        /// </summary>
        public string DhcpServerAddress
        {
            get { return _dhcpServer.ToString(); }
            set { _dhcpServer = IPAddress.Parse(value); }
        }

        /// <summary>
        /// Router (Default Gateway) address
        /// </summary>
        public string Router
        {
            get { return _gateway.ToString(); }
            set { _gateway = IPAddress.Parse(value); }
        }

        /// <summary>
        /// DNS Server address
        /// </summary>
        public string DndServer
        {
            get { return _dns.ToString(); }
            set { _dns = IPAddress.Parse(value); }
        }

        /// <summary>
        /// Subnet Mask
        /// </summary>
        public string SubnetMask
        {
            get { return _subnetMask.ToString(); }
            set { _subnetMask = IPAddress.Parse(value); }
        }

        /// <summary>
        /// Start of IP range to supply to clients
        /// </summary>
        public string FirstIpAddress
        {
            get { return _firstIp.ToString(); }
            set
            {
                if (!replyActive)
                {
                    _firstIp = IPAddress.Parse(value);
                    _nextIp = IPAddress.Parse(value);
                }
                else
                {
                    throw new Exception("Can't change IP range while the server is running");
                }
            }
        }

        /// <summary>
        /// Last IP address to assign to a client
        /// </summary>
        public string LastIpAddress
        {
            get { return _lastIp.ToString(); }
            set { _lastIp = IPAddress.Parse(value); }
        }

        public event EventHandler<string> IpAddressAssigned;
        public event EventHandler ServerStopped;

        public DhcpServer()
        {
        }

        public void StartServer()
        {
            if (!replyActive)
            {

                CreateClient();
                clientIn.BeginReceive(new AsyncCallback(received), null);
                responseThread = new Thread(ResponseProcess);
                responseThread.Start();
            }
        }

        public void StopServer()
        {
            if (replyActive)
            {
                clientIn.Close();
                replyActive = false;
                responseThread.Join(1000);
            }
        }

        private void CreateClient()
        {
            clientIn = new UdpClient();
            clientIn.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            clientIn.ExclusiveAddressUse = false; // only if you want to send/receive on same machine.
            clientIn.Client.Bind(broadcastAddressIn);
        }

        private void received(IAsyncResult res)
        {
            try
            {
                IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] rec = clientIn.EndReceive(res, ref RemoteIpEndPoint);
                if (rec.Length > 240)
                {
                    DhcpPacket packet = DhcpPacket.Parse(rec);
                    packet.RemoteEndPoint = RemoteIpEndPoint;

                    if (packet.MessageType == DhcpMessageType.Discover)
                    {
                        requests.Add(new DhcpProcess(packet));
                    }
                    else if (packet.MessageType == DhcpMessageType.Request)
                    {
                        //Find the right process probably based on MAC Address
                        DhcpProcess p = DhcpProcess.GetProcessByMAC(requests.ToArray(), packet.ClientMACAddress);
                        //Confirm that the packet is in "Offer" state
                        if (p != null && p.CurrentStatus == DhcpMessageType.Offer)
                        {
                            //Add packet to request
                            p.AddRequest(packet);
                        }
                    }
                    else
                    {

                    }

                    clientIn.BeginReceive(new AsyncCallback(received), null);
                }
            }
            catch (Exception)
            {
                // !Hitting this exception handler will stop the server
                replyActive = false;
            }
        }

        private void ResponseProcess()
        {
            replyActive = true;
            while (replyActive)
            {
                for (int n = 0; n < requests.Count; n++)
                {
                    DhcpProcess p = requests[n];
                    if (p.CurrentStatus == DhcpMessageType.Discover)
                    {
                        CreateOffer(p);
                    }
                    else if (p.CurrentStatus == DhcpMessageType.Request)
                    {
                        CreateAck(p);
                    }
                }
                Thread.Sleep(1);
            }
            ServerStopped?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Greate [and send] the Offer packet for the client.
        /// In reality, this function should be refined. It should look at the ParameterRequestList from the client,
        /// and send back what was requested. But it doesn't. It allways sends the the basic information. 
        /// I don't know if this will allways work, but it does for my application so feel free to elaborate if requred.
        /// </summary>
        /// <param name="p"></param>
        private void CreateOffer(DhcpProcess p)
        {
            DhcpPacket packet = DhcpPacket.NewReplyPacket(p.Discover);
            packet.SIAddr = _dhcpServer.GetAddressBytes();
            packet.YIAddr = GetNextIpBytes(packet.ClientMACAddress);

            packet.AddOption(new Dhcp.DhcpOption(OptionCode.DHCPMessageType, 1, new byte[] { (byte)DhcpMessageType.Offer }));
            packet.AddOption(new Dhcp.DhcpOption(OptionCode.SunetMask, 4, _subnetMask.GetAddressBytes()));
            packet.AddOption(new Dhcp.DhcpOption(OptionCode.Router, 4, _gateway.GetAddressBytes()));
            packet.AddOption(new Dhcp.DhcpOption(OptionCode.DnsServer, 4, _dns.GetAddressBytes()));
            packet.AddOption(new Dhcp.DhcpOption(OptionCode.IPAddressLeaseTime, 4, _leaseTime.GetBytes()));
            packet.AddOption(new Dhcp.DhcpOption(OptionCode.T1TimeValue, 4, _leaseTime.GetBytes()));
            packet.AddOption(new Dhcp.DhcpOption(OptionCode.T2TimeValue, 4, _leaseTime.GetBytes()));
            p.AddOffer(packet);
            SendReply(packet.GetBytes());
        }

        /// <summary>
        /// Greate [and send] the Offer packet for the client.
        /// In reality, this function should be refined. It should look at the parameters in the request packet and ACK those.
        /// But it doesn't. It allways sends the the basic information. 
        /// I don't know if this will allways work, but it does for my application so feel free to elaborate if requred.
        /// </summary>
        /// <param name="p"></param>
        private void CreateAck(DhcpProcess p)
        {
            DhcpPacket packet = DhcpPacket.NewReplyPacket(p.Discover);
            packet.YIAddr = p.Offer.YIAddr;
            packet.SIAddr = p.Offer.SIAddr;

            packet.AddOption(new Dhcp.DhcpOption(OptionCode.DHCPMessageType, 1, new byte[] { (byte)DhcpMessageType.ACK }));
            packet.AddOption(new Dhcp.DhcpOption(OptionCode.SunetMask, 4, _subnetMask.GetAddressBytes()));
            packet.AddOption(new Dhcp.DhcpOption(OptionCode.Router, 4, _gateway.GetAddressBytes()));
            packet.AddOption(new Dhcp.DhcpOption(OptionCode.DnsServer, 4, _dns.GetAddressBytes()));
            packet.AddOption(new Dhcp.DhcpOption(OptionCode.IPAddressLeaseTime, 4, _leaseTime.GetBytes()));
            packet.AddOption(new Dhcp.DhcpOption(OptionCode.T1TimeValue, 4, _leaseTime.GetBytes()));
            packet.AddOption(new Dhcp.DhcpOption(OptionCode.T2TimeValue, 4, _leaseTime.GetBytes()));
            p.AddAck(packet);
            SendReply(packet.GetBytes());

            assigned.Add(new Dhcp.DhcpServer.Assignment(packet.ClientMACAddress, new IPAddress(packet.YIAddr)));
            string completeString = p.ClientName + " [" + packet.ClientMACAddress + "]  Assigned Address" + packet.AssignedIpAddress;
            IpAddressAssigned?.Invoke(this, completeString);
        }

        private void SendReply(byte[] msg)
        {
            clientIn.Send(msg, msg.Length, broadcastAddressOut);
        }

        private byte[] GetNextIpBytes(string macAddress)
        {
            byte[] data;
            Assignment a = assigned.Find(r => r.MACAddress == macAddress);
            if (a.Address == null)
            {
                data = _nextIp.GetAddressBytes();
                UInt32 v = (UInt32)(data[0] << 24) | (UInt32)(data[1] << 16) | (UInt32)(data[2] << 8) | (UInt32)(data[3] << 0);
                v += 1;
                _nextIp = new IPAddress(v.GetBytes());
            }
            else
            {
                data = a.Address.GetAddressBytes();
            }
            return data;
        }
    }
}
