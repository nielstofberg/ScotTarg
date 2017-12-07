using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ScotTarg.IpTools
{
    /// <summary>
    /// The primary purpose of this class is to find targets on the local network.
    /// The class include means to enumerate local network interface cards in case 
    /// there are more than one on the system.
    /// </summary>
    public class LocalNetwork
    {
        private bool listening = false;
        private int PORT = 864; //1501;
        private UdpClient udpSocket;
        private IPEndPoint broadcastEp;
        private IPEndPoint localEp = null;
        private string[] localNIC = new string[0];
        private List<DeviceConfig> devices = new List<DeviceConfig>();
        private Timer timer;

        public string SelectedLocalAddress
        {
            get { return (localEp == null) ? string.Empty : localEp.Address.ToString(); }
            set { localEp = new IPEndPoint(IPAddress.Parse(value), PORT); }
        }

        public string[] LocalAddresses
        {
            get { return localNIC; }
        }

        public DeviceConfig[] Devices
        {
            get { return devices.ToArray(); }
        }

        public event EventHandler<DeviceFoundEventArgs> DeviceFound;

        public LocalNetwork()
        {
            broadcastEp = new IPEndPoint(IPAddress.Broadcast, PORT);
            localEp = new IPEndPoint(IPAddress.Any, PORT);
            localNIC = GetLocalIp();
            timer = new Timer(2000);
            timer.Elapsed += Timer_Elapsed;
        }

        /// <summary>
        /// Timer elapsed event handler.
        /// Stops listening for replies from devices
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            if (udpSocket != null)
            {
                udpSocket.Close();
                udpSocket = null;
                listening = false;
            }
        }

        /// <summary>
        /// Sends a UDP broadcast asking all devices to identify themselves.
        /// Start Asynch listining for all devices on the network.
        /// </summary>
        public void GetDevices()
        {
            if (listening)
            {
                return;
            }
            if (localEp == null)
            {
                throw new Exception("No local Address selected");
            }
            if (udpSocket != null)
            {
                udpSocket.Close();
                udpSocket = null;
            }
            try
            {
                devices.Clear();
                listening = true;
                udpSocket = new UdpClient(localEp);
                StartListening();
                udpSocket.Send(Commands.GET_CONFIG, Commands.GET_CONFIG.Length, broadcastEp);
                timer.Start();
            }
            catch( Exception ex)
            {
                listening = false;
                throw ex;
            }
        }

        /// <summary>
        /// Start waiting (asynch) for reply from devices
        /// </summary>
        private void StartListening()
        {
            if (udpSocket != null)
            {
                this.udpSocket.BeginReceive(Receive, new object());
            }
        }

        /// <summary>
        /// Callback function for Asynch listening.
        /// </summary>
        /// <param name="ar"></param>
        private void Receive(IAsyncResult ar)
        {
            try
            {            
                IPEndPoint ip = new IPEndPoint(IPAddress.Any, 15000);
                byte[] bytes = udpSocket.EndReceive(ar, ref ip);
                if (localNIC.Contains(ip.Address.ToString()))
                {
                    return;
                }
                DeviceConfig config = new DeviceConfig();
                config.AnalyseReply(bytes, ip);
                devices.Add(config);
                StartListening();
                if (DeviceFound != null)
                {
                    RaiseEventOnUIThread(DeviceFound, new DeviceFoundEventArgs(config));
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                StartListening();
            }
        }

        /// <summary>
        /// Update the settings of a device to the settings in the DeviceConfig instance.
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public bool UpdateDevice(DeviceConfig device)
        {
            if (listening)
            {
                return false;
            }
            if (localEp == null)
            {
                throw new Exception("No local Address selected");
            }
            if (udpSocket != null)
            {
                udpSocket.Close();
                udpSocket = null;
            }

            byte[] msg = device.GetCommand();
            udpSocket = new UdpClient(localEp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(device.ReplyIP), device.ReplyPort);
            udpSocket.Send(msg, msg.Length, ep);
            udpSocket.Close();
            udpSocket = null;
            return true;
        }

        /// <summary>
        /// This function broadcasts a command that sets all devices to DHCP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool SetAllDevicesToDHCP()
        {
            if (listening)
            {
                return false;
            }
            if (localEp == null)
            {
                throw new Exception("No local Address selected");
            }
            if (udpSocket != null)
            {
                udpSocket.Close();
                udpSocket = null;
            }
            udpSocket = new UdpClient(localEp);
            udpSocket.Send(Commands.SET_DHCP, Commands.SET_DHCP.Length, broadcastEp);
            udpSocket.Close();
            udpSocket = null;
            return true;
        }

        /// <summary>
        /// This function broadcasts a command that sets all devices to a static IP
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool SetAllDevicesToStaticIp()
        {
            if (listening)
            {
                return false;
            }
            if (localEp == null)
            {
                throw new Exception("No local Address selected");
            }
            if (udpSocket != null)
            {
                udpSocket.Close();
                udpSocket = null;
            }
            udpSocket = new UdpClient(localEp);
            udpSocket.Send(Commands.SET_STATIC_IP, Commands.SET_STATIC_IP.Length, broadcastEp);
            udpSocket.Close();
            udpSocket = null;
            return true;
        }

        /// <summary>
        /// Load localNIC with the local IP addresses for the current PC
        /// </summary>
        private static string[] GetLocalIp()
        {
            List<String> ipList = new List<string>();

            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipList.Add(ip.Address.ToString());
                        }
                    }
                }
            }


            return ipList.ToArray();
        }

        /// <summary>
        /// Raise an event making sure it is raised on the UI thread
        /// </summary>
        /// <param name="theEvent"></param>
        /// <param name="args"></param>
        private void RaiseEventOnUIThread(Delegate theEvent, object args)
        {
            foreach (Delegate d in theEvent.GetInvocationList())
            {
                ISynchronizeInvoke syncer = d.Target as ISynchronizeInvoke;
                if (syncer == null)
                {
                    d.DynamicInvoke(new object[] { this, args });
                }
                else
                {
                    syncer.BeginInvoke(d, new object[] {this, args });  // cleanup omitted
                }
            }
        }
    }
}
