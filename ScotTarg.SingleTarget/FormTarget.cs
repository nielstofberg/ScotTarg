using ScotTarg.IpTools;
using ScotTarg.Sessions;
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

        private bool _connected = false;
        private LocalNetwork _network = new LocalNetwork();
        private TargetManager _target = new TargetManager(1);
        private Discipline _25yrd = new Discipline();
        private ShootingSession _session = null;
        private ShotData _lastShot = new ShotData();


        public FormTarget()
        {
            InitializeComponent();
            cmboScoring.SelectedIndex = 0;
            _network.DeviceFound += Network_DeviceFound;
            _target.OnHitRecorded += _target_OnHitRecorded;
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
                _session.EndSession();
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
                    CreateSession();
                    _session.StartSession();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CreateSession()
        {
            _session = new Sessions.ShootingSession(_target.TargetId, 1, _25yrd);
            _session.SessionName = "My FirstSession";
            _session.Position = Position.Prone;
            _session.TargetWidth = Properties.Settings.Default.TargetWidth;
            _session.Sighters = true;
            _session.UpdateConstants(Properties.Settings.Default.SpeedOfSound,
                Properties.Settings.Default.MsPerCount);
        }

        private void _target_OnHitRecorded(object sender, ShotRecordedEventArgs e)
        {
            if (_lastShot == e.ShotData)
            {
                return;
            }
            _lastShot = e.ShotData;
            if (_session != null && _session.Started)
            {
                _session.AddShot(e.ShotData);
            }

            //Update page data;
        }

        private void RedrawTarget()
        {

        }

    }
}
