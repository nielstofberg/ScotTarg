using ScotTarg.IpTools;
using ScotTarg.TargetTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScotTargCalculationTest
{
    public partial class Form1 : Form
    {
        private static readonly Color NCOLOR = Color.DarkSlateGray;
        private static readonly Color LCOLOR = Color.Red;
        private static readonly Color LEFT_CURVE_COLOR = Color.Green;
        private static readonly Color RIGHT_CURVE_COLOR = Color.Blue;
        private static readonly Color TOP_CURVE_COLOR = Color.Purple;
        private static readonly Color BOTTOM_CURVE_COLOR = Color.Orange;

        private const char DELIMETER = ',';
        private const int NSIZE = 10;
        private const int LSIZE = 20;
        private const int WIDTH = 600;
        private const int GRIDSCALE = 5;
        private const int SCALESIZE = 3;

        private int lastShotId = -1;
        private LocalNetwork network = new LocalNetwork();
        private Comms comms = new Comms();
        private CommandHandler commsHandler = new CommandHandler();
        private Bitmap gridImg;
        private Graphics gr;
        int TimeA = 0;
        int TimeB = 0;
        int TimeC = 0;
        int TimeD = 0;

        int AB = 0;
        int CD = 0;
        int AD = 0;
        int BC = 0;

        public Form1()
        {
            InitializeComponent();
            comms.OnMessageReceived += on_MessageReceived;
            commsHandler.OnHitRecorded += on_HitRecorded;
            network.DeviceFound += Network_DeviceFound;
        }

        private void on_MessageReceived(object sender, PacketReceivedEventArgs e)
        {
            commsHandler.ProcessCommand(e.Received);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DrawGrid();
            network.GetDevices();
        }

        private void Network_DeviceFound(object sender, DeviceFoundEventArgs e)
        {
            cmboPorts.Items.Add(e.NewDevice.ReplyIP + ":" + e.NewDevice.TcpPort);
            cmboPorts.SelectedIndex = cmboPorts.Items.Count - 1;
        }


        #region Drawing functions

        /// <summary>
        /// Draws the fixed fraphics on the grid
        /// </summary>
        private void DrawGrid()
        {
            int gridWidth = (int)nudWidth.Value;

            gridImg = new Bitmap(gridWidth, gridWidth);
            gr = Graphics.FromImage(gridImg);

            //Graphics gr = Graphics.FromImage(gridImg);

            int xMiddle = gridWidth / 2;
            int yMiddle = gridWidth / 2;

            gr.FillRectangle(Brushes.LightGray, 0, 0, gridWidth, gridWidth);
            Pen pen = new Pen(Color.Black, 1);

            gr.DrawLine(pen, 0, yMiddle, gridWidth, yMiddle);   // X line
            gr.DrawLine(pen, xMiddle, 0, xMiddle, gridWidth);  // Y line
            int z = Width / 2 / GRIDSCALE;
            for (int x = 1; x < z; x++)
            {
                int xPlus = xMiddle + (GRIDSCALE * x);
                int xMinus = xMiddle - (GRIDSCALE * x);
                int yPlus = yMiddle - (GRIDSCALE * x);
                int yMinus = yMiddle + (GRIDSCALE * x);
                int yTop = yMiddle - SCALESIZE;
                int yBottom = yMiddle + SCALESIZE;
                int xLeft = xMiddle - SCALESIZE;
                int xRight = xMiddle + SCALESIZE;

                gr.DrawLine(pen, xPlus, yTop, xPlus, yBottom);
                gr.DrawLine(pen, xMinus, yTop, xMinus, yBottom);

                gr.DrawLine(pen, xLeft, yPlus, xRight, yPlus);
                gr.DrawLine(pen, xLeft, yMinus, xRight, yMinus);

            }
            pbGrid.Image = gridImg;
            DrawMics();
        }

        /// <summary>
        /// Draws Mic Images in the corners of the grid
        /// </summary>
        private void DrawMics()
        {
            int gridWidth = (int)nudWidth.Value;

            Image img = gridImg;
            //Graphics gr = Graphics.FromImage(img);
            int r = 40;
            gr.FillPie(Brushes.Black, -(r / 2), -(r / 2), r, r,0,90);
            gr.FillPie(Brushes.Black, -(r / 2), gridWidth - (r / 2), r, r, 270, 90);
            gr.FillPie(Brushes.Black, gridWidth - (r / 2), -(r / 2), r, r, 90, 90);
            gr.FillPie(Brushes.Black, gridWidth - (r / 2), gridWidth - (r / 2), r, r,180,90);
            gr.DrawString("A", this.Font, Brushes.White, 1, gridWidth - 16, StringFormat.GenericDefault);
            gr.DrawString("B", this.Font, Brushes.White, 1, 1, StringFormat.GenericDefault);
            gr.DrawString("C", this.Font, Brushes.White, gridWidth - 16, 1, StringFormat.GenericDefault);
            gr.DrawString("D", this.Font, Brushes.White, gridWidth - 16, gridWidth - 16, StringFormat.GenericDefault);
        }

        /// <summary>
        /// Draws the hit point on the grid with the coordinates of the point next to it.
        /// </summary>
        /// <param name="e"></param>
        private void DrawHitPoint(Coordinates e, int id, Color color, int size)
        {
            try
            {
                int mmWidth = 300;
                float shotsize = (float)pbGrid.Image.Width / (float)mmWidth * 4.0f;
                size = (int)shotsize;
                //Graphics gr = Graphics.FromImage(gridImg);
                //Pen pen = new Pen(Color.Red, 1);
                Brush brush = new SolidBrush(color);
                Font font = new Font(this.Font.FontFamily, 10);

                //int x = (e.X - (pbGrid.Image.Width / 2)) * RES_INCREASE_FACTOR;
                //int y = (e.Y - (pbGrid.Image.Height / 2)) * -RES_INCREASE_FACTOR;
                int x = e.X;
                int y = e.Y;

                gr.FillEllipse(brush, e.X - (size/2), e.Y - (size / 2), size, size);
                gr.DrawString("(" + id + ")", font, brush, e.X + size, e.Y);
            }
            catch { }
        }

        /// <summary>
        /// Draw a curve on the grid based on a series of points
        /// </summary>
        /// <param name="points"></param>
        /// <param name="color"></param>
        private void DrawCurve(Coordinates[] points, Color color)
        {
            try
            {
                Pen pen = new Pen(color, 1f);
                gr.DrawCurve(pen, (GetPoints(points)));
            }
            catch { }
        }

        Point[] GetPoints(Coordinates[] points)
        {
            Point[] retVal = new Point[points.Length];
            for (int x = 0; x < points.Length; x++)
            {
                retVal[x].X = points[x].X;
                retVal[x].Y = points[x].Y;
            }
            return retVal;
        }

        #endregion // Drawing functions

       /// <summary>
        /// Event occures when the user clicks anywhere in the grid. This represents the shot hitting the target
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbGrid_MouseClick(object sender, MouseEventArgs e)
        {
            GC.Collect();
            int gridWidth = (int)nudWidth.Value;
            DrawGrid();                 //! Clear the previous shot from the grid

            int tx = 0;
            int ty = 0;
            if (pbGrid.Image.Width > pbGrid.Width)
            {
                tx = ((pbGrid.Image.Width - pbGrid.Width) / 2) + e.X;
            }
            else
            {
                tx = e.X - (pbGrid.Width - pbGrid.Image.Width) / 2;
            }
            if (pbGrid.Image.Height > pbGrid.Height)
            {
                ty = ((pbGrid.Image.Height - pbGrid.Height) / 2) + e.Y;
            }
            else
            {
                ty = e.Y - ((pbGrid.Height - pbGrid.Image.Height) / 2);
            }

            Coordinates location = new Coordinates(tx, ty);
            DrawHitPoint(location, 0, NCOLOR, NSIZE);   //! Draw the actual hit point on the grid

            double t1 = 0, t2 = 0, t3 = 0, t4 = 0;
            PointToTime.Timings t = PointToTime.GetTimingsForPoint(tx, ty, gridWidth);
            t1 = t.TimeA;
            t2 = t.TimeB;
            t3 = t.TimeC;
            t4 = t.TimeD;

            TimeA = (int)t1;
            TimeB = (int)t2;
            TimeC = (int)t3;
            TimeD = (int)t4;

            AB = TimeA - TimeB;
            BC = TimeB - TimeC;
            CD = TimeD - TimeC;
            AD = TimeA - TimeD;

            txtSelX.Text = tx.ToString();
            txtSelY.Text = ty.ToString();

            txtTA.Text = TimeA.ToString();
            txtTB.Text = TimeB.ToString();
            txtTC.Text = TimeC.ToString();
            txtTD.Text = TimeD.ToString();

            txtTdoaAB.Text = AB.ToString();
            txtTdoaBC.Text = BC.ToString();
            txtTdoaCD.Text = CD.ToString();
            txtTdoaAD.Text = AD.ToString();

            Coordinates p = CalculatePoint.GetPoint(gridWidth, (double)AB, (double)CD, (double)BC, (double)AD);

            txtCalcX.Text = p.X.ToString();
            txtCalcY.Text = p.Y.ToString();
            txtDiffX.Text = (tx - p.X).ToString();
            txtDiffY.Text = (ty - p.Y).ToString();

            DrawHitPoint(p, 0, LCOLOR, NSIZE);

            Coordinates[] abPoints = CalculatePoint.GetGraphPointsH(gridWidth, gridWidth, AB, Side.Left);
            Coordinates[] cdPoints = CalculatePoint.GetGraphPointsH(gridWidth, gridWidth, CD, Side.Right);
            Coordinates[] bcPoints = CalculatePoint.GetGraphPointsV(gridWidth, gridWidth, BC, Side.Top);
            Coordinates[] adPoints = CalculatePoint.GetGraphPointsV(gridWidth, gridWidth, AD, Side.Bottom);

            DrawCurve(abPoints, Color.Green);
            DrawCurve(cdPoints, Color.Blue);
            DrawCurve(bcPoints, Color.Purple);
            DrawCurve(adPoints, Color.Orange);
        }

        /// <summary>
        /// Eventhandler for the Comms.OnHitRecorded event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void on_HitRecorded(object sender, ShotRecordedEventArgs e)
        {
            ShotData shot = e.ShotData;
            if (shot.ShotId == lastShotId)
            {
                return;
            }
            else
            {
                lastShotId = (int)shot.ShotId;
            }
            int cc = (int)nudTimeWidth.Value;

            shot.DoCalculation(cc, cc);

            txtTimeA.Text = shot.Sensor1Value.ToString();
            txtTimeB.Text = shot.Sensor2Value.ToString();
            txtTimeC.Text = shot.Sensor3Value.ToString();
            txtTimeD.Text = shot.Sensor4Value.ToString();

            txtDiffAB.Text = shot.LeftDiff.ToString();
            txtDiffBC.Text = shot.TopDiff.ToString();
            txtDiffCD.Text = shot.RightDiff.ToString();
            txtDiffAD.Text = shot.BottomDiff.ToString();

            txtCalculatedX.Text = shot.Calculated_X.ToString();
            txtCalculatedY.Text = shot.Calculated_Y.ToString();

            DsData.DtShotsRow row = dsData.DtShots.NewDtShotsRow();
            row.Id = (int)shot.ShotId;
            row.Time = shot.ShotTime;
            row.TimeA = shot.Sensor1Value;
            row.TimeB = shot.Sensor2Value;
            row.TimeC = shot.Sensor3Value;
            row.TimeD = shot.Sensor4Value;
            row.CalcX = shot.Calculated_X;
            row.CalcY = shot.Calculated_Y;
            row.Correction = shot.Correction;
            dsData.DtShots.AddDtShotsRow(row);

            ReDrawShots();
        }

        /// <summary>
        /// Recalculates all XY positions in the datagrid after txtTimeWidth has been changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTimeWidth_Validated(object sender, EventArgs e)
        {
            int cc = (int)nudTimeWidth.Value;
            DateTime dt = new DateTime();
            foreach( DsData.DtShotsRow row in dsData.DtShots.Rows)
            {
                ShotData shot = new ShotData(1, 1, dt, row.TimeA, row.TimeB, row.TimeC, row.TimeD);
                shot.DoCalculation(cc, cc);
                row.Correction = shot.Correction;
                row.CalcX = shot.Calculated_X;
                row.CalcY = shot.Calculated_Y;
            }
            ReDrawShots();
        }

        /// <summary>
        /// Valitates that the value enterred in txtTargetSize is a valid int value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTargetSize_Validating(object sender, CancelEventArgs e)
        {
            int gridWidth = (int)nudWidth.Value;
        }

        /// <summary>
        /// Redraws the target area after the new value in txtTargetSize has been changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTargetSize_Validated(object sender, EventArgs e)
        {
            DrawGrid();
        }

        bool started = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (started)
            {
                timer1.Stop();
                comms.StopListening();
                started = false;
                button1.Text = "Connect";
            }
            else
            {
                try
                {
                    string portName = cmboPorts.Text;
                    int baudRate = 9600;
                    comms.StartListening(portName, baudRate);
                    started = true;
                    button1.Text = "Disconnect";
                    timer1.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            comms.StopListening();
        }

        FormCalculations formCalc = new FormCalculations();
        private void btnCalcForm_Click(object sender, EventArgs e)
        {
            if (!formCalc.Visible)
            {
                formCalc = new FormCalculations();
                formCalc.Show(this);
            }
        }

        /// <summary>
        /// Re-Draw the grin and all the current shots
        /// </summary>
        private void ReDrawShots()
        {
            int gridWidth = (int)nudWidth.Value;
            int cc = (int)nudTimeWidth.Value;

            DrawGrid();
            int rowCount = dsData.DtShots.Rows.Count;
            for (int x = 0; x < rowCount; x++)
            {
                DsData.DtShotsRow row = (DsData.DtShotsRow)dsData.DtShots.Rows[x];
                double px = row.CalcX; 
                double py = row.CalcY; 

                px = px / cc * gridWidth;
                py = py / cc * gridWidth;


                bool lastone = (x >= (rowCount - 1));
                int size =  (lastone) ? LSIZE : NSIZE;
                Color col = (lastone) ? LCOLOR : NCOLOR;

                DrawHitPoint(new Coordinates((int)Math.Round(px, 0), (int)Math.Round(py, 0)), row.Id,col, size);
            }
        }

        /// <summary>
        /// Import previously exported shots
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                dsData.DtShots.Clear();
                TextReader tr = new StreamReader(openFileDialog1.OpenFile());
                while (true)
                {
                    try
                    {
                        string line = tr.ReadLine();
                        int p0 = 0;
                        int p1 = line.IndexOf(DELIMETER);
                        string strId = line.Substring(p0, p1 - p0);
                        p0 = p1 + 1;
                        p1 = line.IndexOf(DELIMETER, p0);
                        string strA = line.Substring(p0, p1-p0);
                        p0 = p1 + 1;
                        p1 = line.IndexOf(DELIMETER, p0);
                        string strB = line.Substring(p0, p1 - p0);
                        p0 = p1 + 1;
                        p1 = line.IndexOf(DELIMETER, p0);
                        string strC = line.Substring(p0, p1 - p0);
                        p0 = p1 + 1;
                        p1 = line.IndexOf(DELIMETER, p0);
                        string strD = line.Substring(p0, p1 - p0);

                        DsData.DtShotsRow row = dsData.DtShots.NewDtShotsRow();
                        row.TimeA = int.Parse(strA);
                        row.TimeB = int.Parse(strB);
                        row.TimeC = int.Parse(strC);
                        row.TimeD = int.Parse(strD);
                        dsData.DtShots.Rows.Add(row);
                    }
                    catch
                    {
                        tr.Close();
                        break;
                    }
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                TextWriter tw = new StreamWriter(saveFileDialog1.OpenFile());
                foreach (DsData.DtShotsRow row in dsData.DtShots)
                {
                    tw.Write(row.Id);
                    tw.Write(DELIMETER);
                    tw.Write(row.TimeA);
                    tw.Write(DELIMETER);
                    tw.Write(row.TimeB);
                    tw.Write(DELIMETER);
                    tw.Write(row.TimeC);
                    tw.Write(DELIMETER);
                    tw.Write(row.TimeD);
                    tw.Write(DELIMETER);
                    tw.Write("\r\n");
                }
                tw.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int cc = (int)nudTimeWidth.Value;
                DsData.DtShotsRow row = (DsData.DtShotsRow)((DataRowView)dataGridView1.Rows[e.RowIndex].DataBoundItem).Row;
                ShotData shot = new ShotData(1, 1, new DateTime(), row.TimeA, row.TimeB, row.TimeC, row.TimeD);
                shot.DoCalculation(cc, cc);

                Coordinates[] abPoints = CalculatePoint.GetGraphPointsH(cc, cc, shot.LeftCorrected, Side.Left);
                Coordinates[] bcPoints = CalculatePoint.GetGraphPointsV(cc, cc, shot.TopCorrected, Side.Top);
                Coordinates[] cdPoints = CalculatePoint.GetGraphPointsH(cc, cc, shot.RightCorrected, Side.Right);
                Coordinates[] adPoints = CalculatePoint.GetGraphPointsV(cc, cc, shot.BottomCorrected, Side.Bottom);

                RecalcPointsToGrid(ref abPoints);
                RecalcPointsToGrid(ref bcPoints);
                RecalcPointsToGrid(ref cdPoints);
                RecalcPointsToGrid(ref adPoints);
                ReDrawShots();
                DrawCurve(abPoints, LEFT_CURVE_COLOR);
                DrawCurve(cdPoints, RIGHT_CURVE_COLOR);
                DrawCurve(bcPoints, TOP_CURVE_COLOR);
                DrawCurve(adPoints, BOTTOM_CURVE_COLOR);
            }
        }

        private void RecalcPointsToGrid(ref Coordinates[] points)
        {
            int cc = (int)nudTimeWidth.Value;
            int gridWidth = (int)nudWidth.Value;

            for (int n = 0; n < points.Length; n++)
            {
                double px = points[n].X;
                double py = points[n].Y;

                points[n].X = (int)(px / cc * gridWidth);
                points[n].Y = (int)(py / cc * gridWidth);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            comms.KeepAlive();
        }
    }
}
