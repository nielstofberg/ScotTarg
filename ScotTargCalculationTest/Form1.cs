using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScotTargCalculationTest
{
    public partial class Form1 : Form
    {
        private const int RES_INCREASE_FACTOR = 1;
        private CalculatePoint cp = new CalculatePoint();
        private Comms comms = new Comms();
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
            cp.CalcConst = pbGrid.Width;
            string[] ports = SerialPort.GetPortNames();
            comms.OnHitRecorded += on_HitRecorded;

        }

        private void on_HitRecorded(object sender, Comms.HitRecordedEventArgs e)
        {
            DrawGrid();                 //! Clear the previous shot from the grid

            cp.CalcConst = int.Parse(txtTimeWidth.Text);
            TimeA = e.TimeA;
            TimeB = e.TimeB;
            TimeC = e.TimeC;
            TimeD = e.TimeD;
            AB = TimeA - TimeB;
            BC = TimeB - TimeC;
            CD = TimeC - TimeD;
            AD = TimeA - TimeD;

            txtTimeA.Text = TimeA.ToString();
            txtTimeB.Text = TimeB.ToString();
            txtTimeC.Text = TimeC.ToString();
            txtTimeD.Text = TimeD.ToString();

            
            txtDiffAB.Text = AB.ToString();
            txtDiffBC.Text = BC.ToString();
            txtDiffCD.Text = CD.ToString();
            txtDiffAD.Text = AD.ToString();

            double x = 0, y = 0;
            cp.FindCoords((double)AB, (double)BC, (double)CD, (double)AD, ref x, ref y);

            txtCalculatedX.Text = x.ToString();
            txtCalculatedY.Text = y.ToString();

            DsData.DtShotsRow row = dsData.DtShots.NewDtShotsRow();
            row.TimeA = TimeA;
            row.TimeB = TimeB;
            row.TimeC = TimeC;
            row.TimeD = TimeD;
            row.CalcX = (int)x;
            row.CalcY = (int)y;
            dsData.DtShots.AddDtShotsRow(row);

            x = x / cp.CalcConst * pbGrid.Width;
            y = y / cp.CalcConst * pbGrid.Width;

            txtPlottedX.Text = x.ToString();
            txtPlottedY.Text = y.ToString();

            DrawHitPoint(new Point((int)Math.Round(x, 0), (int)Math.Round(y, 0)));

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            DrawGrid();
        }

        /// <summary>
        /// Draws the fixed fraphics on the grid
        /// </summary>
        private void DrawGrid()
        {
            try
            {
                pbGrid.Width = int.Parse(txtTargetWidth.Text);
                pbGrid.Height = pbGrid.Width;
                pbGrid.Left = this.Width - pbGrid.Width - 30;
                cp.CalcConst = pbGrid.Width;
            }
            catch { }

            Bitmap img = new Bitmap(pbGrid.Width, pbGrid.Height);
            Graphics gr = Graphics.FromImage(img);

            int xMiddle = img.Width / 2;
            int yMiddle = img.Height / 2;


            Pen pen = new Pen(Color.Black, 1);

            gr.DrawLine(pen, 0, yMiddle, img.Width, yMiddle);   // X line
            gr.DrawLine(pen, xMiddle, 0, xMiddle, img.Height);  // Y line

            for (int x = 1; x < 88; x++)
            {
                int xPlus = xMiddle + (5 * x);
                int xMinus = xMiddle - (5 * x);
                int yPlus = yMiddle - (5 * x);
                int yMinus = yMiddle + (5 * x);
                int yTop = yMiddle - 5;
                int yBottom = yMiddle +5;
                int xLeft = xMiddle - 5;
                int xRight = xMiddle + 5;

                gr.DrawLine(pen, xPlus, yTop, xPlus, yBottom);
                gr.DrawLine(pen, xMinus, yTop, xMinus, yBottom);

                gr.DrawLine(pen, xLeft, yPlus, xRight, yPlus);
                gr.DrawLine(pen, xLeft, yMinus, xRight, yMinus);

            }
            pbGrid.Image = img;
            DrawMics();
        }

        /// <summary>
        /// Draws Mic Images in the corners of the grid
        /// </summary>
        private void DrawMics()
        {
            Image img = pbGrid.Image;
            Graphics gr = Graphics.FromImage(img);
            int r = 40;
            gr.FillEllipse(Brushes.Black, -(r / 2), -(r / 2), r, r);
            gr.FillEllipse(Brushes.Black, -(r / 2), img.Height - (r / 2), r, r);
            gr.FillEllipse(Brushes.Black, img.Width - (r / 2), -(r / 2), r, r);
            gr.FillEllipse(Brushes.Black, img.Width - (r / 2), img.Height - (r / 2), r, r);
            gr.DrawString("A", this.Font, Brushes.White, 1, img.Height-16, StringFormat.GenericDefault);
            gr.DrawString("B", this.Font, Brushes.White, 1, 1, StringFormat.GenericDefault);
            gr.DrawString("C", this.Font, Brushes.White, img.Width-16, 1, StringFormat.GenericDefault);
            gr.DrawString("D", this.Font, Brushes.White, img.Width - 16, img.Height - 16, StringFormat.GenericDefault);
        }

        /// <summary>
        /// Draws the hit point on the grid with the coordinates of the point next to it.
        /// </summary>
        /// <param name="e"></param>
        private void DrawHitPoint(Point e)
        {
            try
            {
                Graphics gr = Graphics.FromImage(pbGrid.Image);
                Pen pen = new Pen(Color.Red, 1);
                Font font = new Font(this.Font.FontFamily, 10);

                int x = (e.X - (pbGrid.Image.Width / 2)) * RES_INCREASE_FACTOR;
                int y = (e.Y - (pbGrid.Image.Height / 2)) * -RES_INCREASE_FACTOR;

                gr.FillEllipse(Brushes.Red, e.X - 5, e.Y - 5, 10, 10);
                gr.DrawString("(" + x + "," + y + ")", font, Brushes.Red, e.X + 10, e.Y);
            }
            catch { }
        }

        /// <summary>
        /// This function takes the actual coordinates of the mouse click and then calculates the direct distance from each microphone
        /// For simplicity (and accuracy), there is no conversion from distance to time. IE time = distance.  
        /// </summary>
        /// <param name="e">Point of the mouse click</param>
        /// <param name="t1">Time/distance to corner 1 (mic B)</param>
        /// <param name="t2">Time/distance to corner 2 (mic C)</param>
        /// <param name="t3">Time/distance to corner 3 (mic D)</param>
        /// <param name="t4">Time/distance to corner 4 (mic A)</param>
        private void CalculateTimimg(Point e, ref double t1, ref double t2, ref double t3, ref double t4)
        {
            int cornerX = pbGrid.Image.Width / 2 * RES_INCREASE_FACTOR;
            int cornerY = pbGrid.Image.Height / 2 * RES_INCREASE_FACTOR;
            int x = (e.X - (pbGrid.Image.Width / 2)) * RES_INCREASE_FACTOR;
            int y = (e.Y - (pbGrid.Image.Height / 2)) * -RES_INCREASE_FACTOR;

            int addToX = 0;
            int addToY = 0;

            // Time1 Calculation (Bottom Left corner)
            addToX = (x <= 0) ? 0 : Math.Abs(x) * 2;
            addToY = (y <= 0) ? 0 : Math.Abs(y) * 2;
            t1 = Math.Sqrt(Math.Pow(cornerX + addToX - Math.Abs(x), 2) + Math.Pow(cornerY + addToY - Math.Abs(y), 2));

            // Time2 Calculation (Top Left corner)
            addToX = (x <= 0) ? 0 : Math.Abs(x) * 2;
            addToY = (y >= 0) ? 0 : Math.Abs(y) * 2;
            t2 = Math.Sqrt(Math.Pow(cornerX + addToX - Math.Abs(x), 2) + Math.Pow(cornerY + addToY - Math.Abs(y), 2));

            // Time3 Calculation (Top Right corner)
            addToX = (x >= 0) ? 0 : Math.Abs(x) * 2;
            addToY = (y >= 0) ? 0 : Math.Abs(y) * 2;
            t3 = Math.Sqrt(Math.Pow(cornerX + addToX - Math.Abs(x), 2) + Math.Pow(cornerY + addToY - Math.Abs(y), 2));

            // Time4 Calculation (Bottom Right corner)
            addToX = (x >= 0) ? 0 : Math.Abs(x) * 2;
            addToY = (y <= 0) ? 0 : Math.Abs(y) * 2;
            t4 = Math.Sqrt(Math.Pow(cornerX + addToX - Math.Abs(x), 2) + Math.Pow(cornerY + addToY - Math.Abs(y), 2));


            double deduct = GetLowestValue(t1, t2, t3, t4);

            t1 = Math.Round(t1 - deduct, 0);
            t2 = Math.Round(t2 - deduct, 0);
            t3 = Math.Round(t3 - deduct, 0);
            t4 = Math.Round(t4 - deduct, 0);
        }

        /// <summary>
        /// Get the lowest value of the four timings
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <param name="t3"></param>
        /// <param name="t4"></param>
        /// <returns></returns>
        private double GetLowestValue(double t1,double t2,double t3,double t4)
        {
            double val = 0;
            if (t1 <= t2 && t1 <= t3 && t1 <= t4)
            {
                val = t1;
            }
            else if (t2 <= t1 && t2 <= t3 && t2 <= t4)
            {
                val = t2;
            }
            else if (t3 <= t1 && t3 <= t2 && t3 <= t4)
            {
                val = t3;
            }
            else
            {
                val = t4;
            }
            return val;
        }

        /// <summary>
        /// Event occures when the user clicks anywhere in the grid. This represents the shot hitting the target
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbGrid_MouseClick(object sender, MouseEventArgs e)
        {
            cp.CalcConst = pbGrid.Width;
            
            DrawGrid();                 //! Clear the previous shot from the grid
            DrawHitPoint(e.Location);   //! Draw the actual hit point on the grid
            double t1=0, t2=0, t3=0, t4=0;
            CalculateTimimg(e.Location, ref t1, ref t2, ref t3, ref t4);    //! Calculate the mic timings

            TimeA = (int)t1;
            TimeB = (int)t2;
            TimeC = (int)t3;
            TimeD = (int)t4;

            AB = TimeA - TimeB;
            BC = TimeB - TimeC;
            CD = TimeC - TimeD;
            AD = TimeA - TimeD;

            txtSelX.Text = e.X.ToString();
            txtSelY.Text = e.Y.ToString();

            txtTA.Text = TimeA.ToString();// +" (" + Math.Round(t1, 0) + ")";
            txtTB.Text = TimeB.ToString();// + " (" + Math.Round(t2, 0) + ")";
            txtTC.Text = TimeC.ToString();// + " (" + Math.Round(t3, 0) + ")";
            txtTD.Text = TimeD.ToString();// + " (" + Math.Round(t4, 0) + ")";

            txtTdoaAB.Text = AB.ToString();
            txtTdoaBC.Text = BC.ToString();
            txtTdoaCD.Text = CD.ToString();
            txtTdoaAD.Text = AD.ToString();

            double x = 0, y = 0;
            cp.FindCoords((double)AB / RES_INCREASE_FACTOR, (double)BC / RES_INCREASE_FACTOR, (double)CD / RES_INCREASE_FACTOR, (double)AD / RES_INCREASE_FACTOR, ref x, ref y);

            x = Math.Round(x);
            y = Math.Round(y);

            txtCalcX.Text = x.ToString();
            txtCalcY.Text = y.ToString();
            txtDiffX.Text = (e.X - x).ToString();
            txtDiffY.Text = (e.Y - y).ToString();

            DrawHitPoint(new Point((int)x, (int)y));

        }

        private void txtTargetSize_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                int.Parse(txtTargetWidth.Text);
            }
            catch
            {
                e.Cancel = true;
            }
        }

        private void txtTargetSize_Validated(object sender, EventArgs e)
        {
            DrawGrid();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            int testX, testY;
            try
            {
                testX = int.Parse(txtTextX.Text);
                testY = int.Parse(txtTextY.Text);
            }
            catch
            {
                testX = 400;
                testY = 400;
            }
            MouseEventArgs mea = new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 1, testX, testY, 0);
            pbGrid_MouseClick(pbGrid, mea);
        }

        bool started = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (started)
            {
                comms.StopListening();
                started = false;
            }
            else
            {
                string portName = "COM4";
                int baudRate = 9600;
                comms.StartListening(portName, baudRate);
                started = true;
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

    }
}
