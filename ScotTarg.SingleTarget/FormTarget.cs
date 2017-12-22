#define TESTING 

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
        private const bool TESTING = true;
        private const string CONNECT_STR = "Connect";
        private const string DISCONNECT_STR = "Disconnect";

        private bool _connected = false;
        private LocalNetwork _network = new LocalNetwork();
        private TargetManager _target = new TargetManager(1);
        private Discipline _25yrd = new Discipline();
        private ShootingSession _session = null;
        private ShotData _lastShot = new ShotData();

#if TESTING
        private int testshotindex = 0;
        private ShotData[] testShots = new ShotData[] {
            new ShotData(1,1, DateTime.Now, 803, 872, 382, 0),
            new ShotData(1,2, DateTime.Now, 0,270,1065,581),
            new ShotData(1,3, DateTime.Now, 504,340,50,0),
            new ShotData(1,4, DateTime.Now, 362,2410,2309,0),
            new ShotData(1,5, DateTime.Now, 358,1241,1152,0),
            new ShotData(1,6, DateTime.Now, 972,0,689,1346),
            new ShotData(1,7, DateTime.Now, 56,0,1729,1589),
            new ShotData(1,8, DateTime.Now, 2153,1295,0,634),
            new ShotData(1,9, DateTime.Now, 1840,2740,1400,0),
            new ShotData(1,10, DateTime.Now, 2216,941,0,1110)
        };
#endif

        public FormTarget()
        {
            InitializeComponent();
            foreach (Position pos in Enum.GetValues(typeof(Position)))
            {
                cmboPosition.Items.Add(pos.ToString());
            }
            cmboPosition.SelectedIndex = 0;
            _network.DeviceFound += Network_DeviceFound;
            _target.OnHitRecorded += _target_OnHitRecorded;
        }

        private void FormTarget_Load(object sender, EventArgs e)
        {
            timer1.Start();
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
                btnSighter.Visible = false;
            }
            else
            {
                try
                {
                    timer1.Stop();
                    string portName = cmboTargets.Text;
#if !TESTING
                    _target.Connect(portName);
#endif
                    _connected = true;
                    btnConnect.Text = DISCONNECT_STR;
                    if (_session != null)
                    {
                        _session.TargetId = _target.TargetId;
                    }
                    btnSighter.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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

        /// <summary>
        /// Update target display with curren shot information from _session
        /// </summary>
        private void RedrawTarget()
        {
            if (_session == null) return;

            bool dec = btnChkDec.Checked;
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
            Bitmap bmp = graph.GetTargetImage(rings);
            ShotSeries ser = (_session.Sighters) ? _session.SighterSeries : _session.CurrentSeries;
            if (ser != null)
            {
                int x = 0;
                foreach (var shot in ser.Shots)
                {
                    if (x++ < ser.Shots.Length - 1)
                    {
                        graph.AddShot(ref bmp, shot, rings, Color.Green);
                    }
                    else
                    {
                        graph.AddShot(ref bmp, shot, rings, Color.Red);
                    }
                }
            }
            pb1.Image = bmp;
        }

        int rings = 10;
        private void btnZoom_Click(object sender, EventArgs e)
        {
            rings -= 3;
            if (rings < 2)
            {
                rings = 10;
            }
            RedrawTarget();
        }

        private void btnHitTest_Click(object sender, EventArgs e)
        {
#if TESTING
            if (testshotindex >= testShots.Length)
            {
                testshotindex = 0;
            }
            _target_OnHitRecorded(null, new ShotRecordedEventArgs(testShots[testshotindex++]));
#endif
        }

        private void btnSighter_Click(object sender, EventArgs e)
        {
            if (_session == null) return;
            if (_session.Sighters)
            {
                _session.Sighters = false;
                btnSighter.Text = "COMP";
            }
            else
            {
                _session.Sighters = true;
                btnSighter.Text = "SIGHTING";
            }
            RedrawTarget();
        }

        private void btnChkDecInt_Click(object sender, EventArgs e)
        {
            btnChkDec.Checked = (sender == btnChkDec);
            btnChkInt.Checked = (sender == btnChkInt);
            RedrawTarget();
        }

        private void btnOpenSession_Click(object sender, EventArgs e)
        {
            FormSession session = new FormSession();
            if (session.ShowDialog(this) == DialogResult.OK)
            {
                _session = session.Session;
                if (_connected)
                {
                    _session.TargetId = _target.TargetId;
                }
                _session.Position = (Position)Enum.Parse(typeof(Position), cmboPosition.Text);
                lblTargetType.Text = _session.Discipline.Description + " @ " + _session.TargetDistance.ToString() + "m";
                _session.StartSession();
                RedrawTarget();
            }
        }

        private void cmboPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_session != null)
            {
                _session.Position = (Position)Enum.Parse(typeof(Position),cmboPosition.Text);
            }
        }
    }
}
