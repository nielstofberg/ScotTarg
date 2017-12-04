using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScotTarg.Dhcp
{
    internal class DhcpProcess
    {
        internal string ClientMacAddress
        {
            get { return Discover.ClientMACAddress; }
        }
        internal string ClientName
        {
            get { return Discover.HostName; }
        }

        internal DhcpMessageType CurrentStatus { get; set; }
        internal DhcpPacket Discover { get; private set; }
        internal DhcpPacket Offer { get; private set; }
        internal DhcpPacket Request { get; private set; }
        internal DhcpPacket ACK { get; private set; }

        internal DhcpProcess(DhcpPacket discover)
        {
            if (discover.MessageType != DhcpMessageType.Discover)
            {
                throw new Exception();
            }
            Discover = discover;
            CurrentStatus = DhcpMessageType.Discover;
        }

        internal void AddOffer(DhcpPacket offer)
        {
            if (offer.MessageType != DhcpMessageType.Offer)
            {
                throw new Exception();
            }
            Offer = offer;
            CurrentStatus = DhcpMessageType.Offer;
        }

        internal void AddRequest(DhcpPacket req)
        {
            if (req.MessageType != DhcpMessageType.Request)
            {
                throw new Exception();
            }
            Request = req;
            CurrentStatus = DhcpMessageType.Request;
        }

        internal void AddAck(DhcpPacket ack)
        {
            if (ack.MessageType != DhcpMessageType.ACK)
            {
                throw new Exception();
            }
            ACK = ack;
            CurrentStatus = DhcpMessageType.ACK;
        }

        internal static DhcpProcess GetProcessByMAC(DhcpProcess[] processes, string mac)
        {
            DhcpProcess ret = null;
            DhcpProcess[] ps = processes.Where(p => p.ClientMacAddress == mac).ToArray();
            if (ps.Length > 0)
            {
                ret = ps[ps.Length - 1];
            }
            return ret;
        } 
    }
}
