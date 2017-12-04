using System;

namespace ScotTarg.Dhcp
{
    internal enum OperationalCode : byte
    {
        Request = 0x01,
        Reply = 0x02
    }

    internal enum HardwareType : byte
    {
        Ethernet = 1,
        IEEE802Networks = 6,
        ARCNET = 7,
        FrameRelay = 15,
        AsynchTransferMode1 = 16,
        HDLC = 17,
        FibreChanel = 18,
        AsynchTransferMode2 = 19,
        SerialLine = 20
    }

    public enum Flag : UInt16
    {
        None = 0x0,
        Broadcast = 0x8000
    }

    internal enum OptionCode : byte
    {
        //! RFC 1497 Vendor Extensions
        Pad = 0,                        //!< A single byte used as “filler” to align a subsequent field on a word (two-byte) boundary. Contains no information.
        SunetMask = 1,                  //!< A 32-bit subnet mask being supplied for the client to use on the current network. Must appear in the option list before the Router option if both are present.
        TimeOfset = 2,                  //!< Specifies the time offset of the client's subnet in seconds from Coordinated Universal Time (UTC, formerly Greenwich Mean Time or GMT). Positive values represent areas east of the prime meridian (in the United Kingdom), negative values areas west. Essentially, this is used to indicate the time zone of the subnet.
        Router = 3,                     //!< Specifies a list of 32-bit router addresses for the client to use on the local network. Routers are listed in the order of preference for the client to use.
        TimeServer = 4,                 //!< Specifies a list of time server addresses (per RFC 868) for the client to use on the local network. Servers are listed in the order of preference for the client to use.
        Ien116NameServer = 5,           //!< Specifies a list of IEN-116 name server addresses for the client to use on the local network. Servers are listed in the order of preference for the client to use.
        DnsServer = 6,                  //!< Specifies a list of DNS name server addresses for the client to use on the local network. Servers are listed in the order of preference for the client to use.
        LogServer = 7,                  //!< Specifies a list of MIT-LCS UDP log server addresses for the client to use on the local network. Servers are listed in the order of preference for the client to use.
        CookieServer = 8,               //!< Specifies a list of RFC 865 “cookie” server addresses for the client to use on the local network. Servers are listed in the order of preference for the client to use.
        LprServer = 9,                  //!< Specifies a list of RFC 1179 line printer server addresses for the client to use on the local network. Servers are listed in the order of preference for the client to use.
        ImpressServer = 10,             //!< Specifies a list of Imagen Impress server addresses for the client to use on the local network. Servers are listed in the order of preference for the client to use.
        ResourceLocationServer = 11,    //!< Specifies a list of RFC 887 resource location server addresses for the client to use on the local network. Servers are listed in the order of preference for the client to use.
        HostName = 12,                  //!< Specifies a host name for the client. This may or may not be a DNS host name; see option #15 below.
        BootFileSize = 13,              //!< Specifies the size of the default boot image file for the client, expressed in units of 512 bytes.
        MeritDumpFile = 14,             //!< Specifies the path and filename of the file to which the client should dump its core image in the event that it crashes.
        DomainName = 15,                //!< Specifies the DNS domain name for the client. Compare to option #12.
        SwapServer = 16,                //!< Specifies the address of the client's swap server.
        RootPath = 17,                  //!< Specifies the path name of the client's root disk. This allows the client to access files it may need, using a protocol such as NFS.
        ExtensionsPath = 18,            //!< 

        //! IP Layer Parameters Per Host
        IPForwardingED = 19,            //!< A value of 1 turns on IP forwarding (that is, routing) on a client that is capable of that function; a value of 0 turns it off.
        NonLocalSourceRoutingED = 20,   //!< A value of 1 tells a client capable of routing to allow forwarding of IP datagrams with non-local source routes. A value of 0 tells the client not to allow this.
        PolicyFilter = 21,              //!< A set of address/mask pairs used to filter non-local source-routed datagrams.
        MaximumDatagramSize = 22,       //!< Tells the client the size of the largest datagram that the client should be prepared to reassemble. The minimum value is 576 bytes.
        DefaultIPTimeToLive = 23,       //!< Specifies the default value that the client should use for the Time To Live field in creating IP datagrams.
        PathMTUAgingTimeout = 24,       //!< Specifies the number of seconds the client should use in aging path MTU values determined using Path MTU discovery.
        PathMTUPlateauTable = 25,       //!< Specifies a table of values to be used in performing path MTU discovery.

        //! IP Layer Parameters Per Interface
        InterfaceMTU = 26,              //!< Specifies the maximum transmission unit (MTU) to be used for IP datagrams on this interface. Minimum value is 68.
        AllSubnetsAreLocal = 27,        //!< When set to 1, tells the client that it may assume that all subnets of the IP network it is on have the same MTU as its own subnet. When 0, the client must assume that some subnets may have smaller MTUs than the client's subnet.
        BroadcastAddress = 28,          //!< Tells the client what address it should use for broadcasts on this interface.
        PerformMaskDiscovery = 29,      //!< A value of 1 tells the client that it should use ICMP to discover a subnet mask on the local subnet. A value of 0 tells the client not to perform this discovery.
        MaskSupplier = 30,              //!< Set to 1 to tell the client that it should respond to ICMP subnet mask requests on this interface.
        PerformRouterDiscovery = 31,    //!< A value of 1 tells the client to use the ICMP router discovery process to solicit a local router. A value of 0 tells the client to not do so. Note that DHCP itself can be used to specify one or more local routers using option #3 above.
        RouterSolicitationAddress = 32, //!< Tells the client the address to use as the destination for router solicitations.
        StaticRoute = 33,               //!< Provides the client with a list of static routes it can put into its routing cache. The list consists of a set of IP address pairs; each pair defines a destination and a router to be used to reach the destination.

        //! Link Layer Parameters Per Interface
        TrailerEncapsulation = 34,      //!< When set to 1, tells the client to negotiate the use of trailers, as defined in RFC 893. A value of 0 tells the client not to use this feature.
        ARPCacheTimeout = 35,           //!< Specifies how long, in seconds, the client should hold entries in its ARP cache.
        EthernetEncapsulation = 36,     //!< Tells the client what type of encapsulation to use when transmitting over Ethernet at layer two. If the option value is 0, specifies that Ethernet II encapsulation should be used, per RFC 894; when the value is 1, tells the client to use IEEE 802.3 encapsulation, per RFC 1042.

        //! TCP Parameters
        DefaultTTL = 37,                //!< Specifies the default Time To Live the client should use when sending TCP segments.
        TCPKeepaliveInterval = 38,      //!< Specifies how long (in seconds) the client should wait on an idle TCP connection before sending a “keepalive” message. A value of 0 instructs the client not to send such messages unless specifically instructed to do so by an application.
        TCPKeepaliveGarbage = 39,       //!< When set to 1, tells a client it should send TCP keepalive messages that include an octet of “garbage” for compatibility with implementations that require this.

        //! Application and Service Parameters
        NetInfoServiceDomain = 40,      //!< Specifies the client's NIS domain. Contrast to option #64.
        NetInforServers = 41,           //!< Specifies a list of IP addresses of NIS servers the client may use. Servers are listed in the order of preference for the client to use. Contrast to option #65.
        NTPServers = 42,                //!< Specifies a list of IP addresses of Network Time Protocol servers the client may use. Servers are listed in the order of preference for the client to use.
        VendorSpecificInformation = 43, //!< Allows an arbitrary set of vendor-specific information to be included as a single option within a DHCP or BOOTP message. This information is structured using the same format as the Options or Vend field itself, except that it does not start with a “magic cookie”. See the end of the previous topic for more details.
        NBOverTCPIPNameServers = 44,    //!< Specifies a list of IP addresses of NetBIOS name servers (per RFC 1001/1002) that the client may use. Servers are listed in the order of preference for the client to use.
        NBOverTCPIPDatagramDistSrvrs = 45, //!< Specifies a list of IP addresses of NetBIOS datagram distribution servers (per RFC 1001/1002) that the client may use. Servers are listed in the order of preference for the client to use.
        NBOverTCPIPNodeType = 46,       //!< Tells the client what type of NetBion note type it should be.
        NBOverTCPIPScope = 47,          //!< Specifies the NetBIOS over TCP/IP scope parameter for the client.
        XWindowSystemFontServers = 48,  //!< Specifies a list of IP addresses of X Window System Font servers that the client may use. Servers are listed in the order of preference for the client to use.
        XWindowSystemDisplayManager = 49, //!< Specifies a list of IP addresses of systems running the X Window System Display Manager that the client may use. Addresses are listed in the order of preference for the client to use.
        NetInfoServicePDomain = 64,     //!< Specifies the client's NIS+ domain. Contrast to option #40.
        NetInfoServicePServers = 65,    //!< Specifies a list of IP addresses of NIS+ servers the client may use. Servers are listed in the order of preference for the client to use. Contrast to option #41.
        MobileIPHomeAgent = 68,         //!< Specifies a list of IP addresses of home agents that the client can use in Mobile IP. Agents are listed in the order of preference for the client to use; normally a single agent is specified.
        SMTPServers = 69,               //!< Specifies a list of IP addresses of SMTP servers the client may use. Servers are listed in the order of preference for the client to use.
        POP3Servers = 70,               //!< Specifies a list of IP addresses of POP3 servers the client may use. Servers are listed in the order of preference for the client to use.
        NNTPServers = 71,               //!< Specifies a list of IP addresses of NNTP servers the client may use. Servers are listed in the order of preference for the client to use.
        DefaultWWWServers = 72,         //!< Specifies a list of IP addresses of World Wide Web (HTTP) servers the client may use. Servers are listed in the order of preference for the client to use.
        DefaultFingerServers = 73,      //!< Specifies a list of IP addresses of Finger servers the client may use. Servers are listed in the order of preference for the client to use.
        DefaultIRCServers = 74,         //!< Specifies a list of IP addresses of Internet Relay Chat (IRC) servers the client may use. Servers are listed in the order of preference for the client to use.
        StreetTalkServers = 75,         //!< Specifies a list of IP addresses of StreetTalk servers the client may use. Servers are listed in the order of preference for the client to use.
        STDAServers = 76,               //!< Specifies a list of IP addresses of STDA servers the client may use. Servers are listed in the order of preference for the client to use.

        //! DHCP Extensions
        RequestedIPAddress = 50,        //!< Used in a client's DHCPDISCOVER message to request a particular IP address assignment.
        IPAddressLeaseTime = 51,        //!< Used in a client request to ask a server for a particular DHCP lease duration, or in a server reply to tell the client the offered lease time. It is specified in units of seconds.
        /// <summary>
        /// 1=File field carrying Option Data. 
        /// 2=SName field carrying Option Data. 
        /// 3=Both fields carrying Option Data.
        /// </summary>
        OptionOverload = 52,
        /// <summary>
        /// 1=DHCPDISCOVER
        /// 2=DHCPOFFER
        /// 3=DHCPREQUEST
        /// 4=DHCPDECLINE
        /// 5=DHCPPACK
        /// 6=DHCPNAK
        /// 7=DHCPRELEASE
        /// 8=DHCPINFORM
        /// </summary>
        DHCPMessageType = 53,
        ServerIdentifier = 54,          //!< The IP address of a particular DHCP server. This option is included in messages sent by DHCP servers to identify themselves as the source of the message. It is also used by a client in a DHCPREQUEST message to specify which server’s lease it is accepting.
        ParameterRequestList = 55,      //!< Used by a DHCP client to request a list of particular configuration parameter values from a DHCP server.
        Message = 56,                   //!< Used by a server or client to indicate an error or other message.
        MaximumDHCPMessageSize = 57,    //!< Used by a DHCP client or server to specify the maximum size of DHCP message it is willing to accept. The minimum legal value is 576 bytes.
        T1TimeValue = 58,               //!< Tells the client the value to use for its renewal timer.
        T2TimeValue = 59,               //!< Tells the client what value to use for its rebinding timer.
        VendorClassIdentifier = 60,     //!< Included in a message sent by a DHCP client to specify its vendor and configuration. This may be used to prompt a server to send the correct vendor-specific information using option #43.
        ClientIdentifier = 61,          //!< Used optionally by a client to specify a unique client identification for itself that differs from the DHCP default. This identifier is expected by servers to be unique amongst all DHCP clients and is used to index the DHCP server's configuration parameter database.
        TFTPServerName = 66,            //!< When the DHCP message's SName field has been used for options using the option overload feature, this option may be included to specify the TFTP server name that would normally appear in the SName field.
        BootfileName = 67,              //!< When the DHCP message's File field has been used for options using the option overload feature, this option may be included to specify the bootfile name that would normally appear in the File field.

        End = 255                       //!< End of options
    }

    internal enum DhcpMessageType : byte
    {
        Discover = 1,
        Offer = 2,
        Request = 3,
        Decline = 4,
        ACK = 5,
        NAK = 6,
        Release = 7,
        Inform = 8
    }
}
