using ScotTarg.IpTools;
using ScotTarg.TargetTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScotTarg.SingleTarget
{
    public partial class FormTarget : Form
    {
        private const string CONNECT_STR = "Connect";
        private const string DISCONNECT_STR = "Disconnect";
        private const int CONNECT_INTERVAL = 1000;
        private const int DISCONNECT_INTERVAL = 5000;
        private double _distPerCount = 0.022666666666d;
        private int _calcWidth = 13235;

        private bool _connected = false;
        private LocalNetwork _network = new LocalNetwork();
        private TargetComms _target = new TargetComms();
        private List<SessionShot> _shots = new List<SessionShot>();



        public FormTarget()
        {
            InitializeComponent();
            CalculateConstants();
            _network.DeviceFound += Network_DeviceFound;
            _target.OnHitRecorded += _target_OnHitRecorded;
        }

        private void CalculateConstants()
        {
            _distPerCount = Properties.Settings.Default.MsPerCount * Properties.Settings.Default.SpeedOfSound;
            _calcWidth = (int)Math.Round(Properties.Settings.Default.TargetWidth / _distPerCount, 0);
        }

        private void FormTarget_Load(object sender, EventArgs e)
        {
            timer1.Start();
            _network.GetDevices();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _network.GetDevices();
        }

        private void Network_DeviceFound(object sender, DeviceFoundEventArgs e)
        {
            string toAdd = e.NewDevice.ReplyIP + ":" + e.NewDevice.TcpPort;
            if (!cmboTargets.Items.Contains(toAdd))
            {
                cmboTargets.Items.Add(e.NewDevice.ReplyIP + ":" + e.NewDevice.TcpPort);
                cmboTargets.SelectedIndex = cmboTargets.Items.Count - 1;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (_connected)
            {
                _target.Disconnect();
                _connected = false;
                btnConnect.Text = CONNECT_STR;
                timer1.Start();
            }
            else
            {
                try
                {
                    timer1.Stop();
                    string portName = cmboTargets.Text;
                    _target.Connect(portName);
                    _connected = true;
                    btnConnect.Text = DISCONNECT_STR;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void _target_OnHitRecorded(object sender, ShotRecordedEventArgs e)
        {
            if (!_shots.Exists(s => s.ShotData.ShotId == e.ShotData.ShotId))
            {
                _shots.Add(new SingleTarget.SessionShot(_distPerCount, _calcWidth, e.ShotData));
            }
        }

        private void RedrawTarget()
        {

        }
    }
}
