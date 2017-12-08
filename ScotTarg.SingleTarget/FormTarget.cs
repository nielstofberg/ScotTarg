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
            CreateSession();
            RedrawTarget();
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
            if (_lastShot.ShotId == e.ShotData.ShotId)
            {
                return;
            }
            _lastShot = e.ShotData;
            if (_session != null && _session.Started)
            {
                _session.AddShot(e.ShotData);
                RedrawTarget();
            }
        }

        private void RedrawTarget()
        {
            if (_session == null) return;

            bool dec = cmboScoring.SelectedIndex == 0;
            string[,] seriesScores = _session.GetSeriesScores(dec);
            lblSessionTitle.Text = _session.SessionName;
            lblSeriesTitle.Text = _session.SeriesTitle;

            lblScore1.Text = seriesScores[0, 0];
            lbl1X.Text = seriesScores[0, 1];
            lblScore2.Text = seriesScores[1, 0];
            lbl2X.Text = seriesScores[1, 1];
            lblScore3.Text = seriesScores[2, 0];
            lbl3X.Text = seriesScores[2, 1];
            lblScore4.Text = seriesScores[3, 0];
            lbl4X.Text = seriesScores[3, 1];
            lblScore5.Text = seriesScores[4, 0];
            lbl5X.Text = seriesScores[4, 1];
            lblScore6.Text = seriesScores[5, 0];
            lbl6X.Text = seriesScores[5, 1];
            lblScore7.Text = seriesScores[6, 0];
            lbl7X.Text = seriesScores[6, 1];
            lblScore8.Text = seriesScores[7, 0];
            lbl8X.Text = seriesScores[7, 1];
            lblScore9.Text = seriesScores[8, 0];
            lbl9X.Text = seriesScores[8, 1];
            lblScore10.Text = seriesScores[9, 0];
            lbl10X.Text = seriesScores[9, 1];

            float agg = _session.GetAggregate(dec);
            lblSeriesTotal.Text = _session.GetSeriesTotal(dec).ToString();
            lblSeriesX.Text = _session.GetSeriesXCount().ToString();
            lblAggregate.Text = agg.ToString();
            lblTotalX.Text = _session.GetTotalXCount().ToString();
            if (agg > 0)
            {
                lblAverage.Text = (agg / _session.ShotCount() * 10).ToString("###.#");
            }

            TargetGraphic graph = new Sessions.TargetGraphic();
            pb1.Image = graph.GetTargetImage(rings);
        }

        int rings = 10;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            rings -= 3;
            if (rings < 2)
            {
                rings = 10;
            }
            RedrawTarget();
        }
    }
}
