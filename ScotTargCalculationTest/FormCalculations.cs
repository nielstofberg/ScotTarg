using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScotTargCalculationTest
{
    public partial class FormCalculations : Form
    {
        private const char DELIMETER = ',';

        public FormCalculations()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
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
                        string strA = line.Substring(p0, p1 - p0);
                        p0 = p1 + 1;
                        p1 = line.IndexOf(DELIMETER, p0);
                        string strB = line.Substring(p0, p1 - p0);
                        p0 = p1 + 1;
                        p1 = line.IndexOf(DELIMETER, p0);
                        string strC = line.Substring(p0, p1 - p0);
                        p0 = p1 + 1;
                        p1 = line.IndexOf(DELIMETER, p0);
                        string strD = line.Substring(p0, p1 - p0);

                        DsData.DtShotsCalcRow row = dsData.DtShotsCalc.NewDtShotsCalcRow();
                        row.TimeA = int.Parse(strA);
                        row.TimeB = int.Parse(strB);
                        row.TimeC = int.Parse(strC);
                        row.TimeD = int.Parse(strD);
                        dsData.DtShotsCalc.Rows.Add(row);
                    }
                    catch
                    {
                        tr.Close();
                        break;
                    }
                }
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            CalculatePoint cp = new CalculatePoint();
            cp.CalcConst = int.Parse(txtCalcConstant.Text);
            int refX = int.Parse(txtRefX.Text);
            int refY = int.Parse(txtRefY.Text);

            int refXa = int.Parse(txtRefAx.Text);
            int refYa = int.Parse(txtRefAy.Text);
            int refXb = int.Parse(txtRefBx.Text);
            int refYb = int.Parse(txtRefBy.Text);
            int refXc = int.Parse(txtRefCx.Text);
            int refYc = int.Parse(txtRefCy.Text);
            int refXd = int.Parse(txtRefDx.Text);
            int refYd = int.Parse(txtRefDy.Text);

            double distFactor = double.Parse(txtDistFactor.Text);

            foreach (DsData.DtShotsCalcRow row in dsData.DtShotsCalc.Rows)
            {
                int TimeA = row.TimeA;
                int TimeB = row.TimeB;
                int TimeC = row.TimeC;
                int TimeD = row.TimeD;
                int AB = TimeA - TimeB;
                int BC = TimeB - TimeC;
                int CD = TimeC - TimeD;
                int AD = TimeA - TimeD;

                double x = 0, y = 0;
                CalculatePoint.FourPoints fp = cp.FindCoords((double)AB, (double)BC, (double)CD, (double)AD, ref x, ref y);

                row.CalcX = (int)x;
                row.CalcY = (int)y;

                row.CalcXa = fp.Ax;
                row.CalcXb = fp.Bx;
                row.CalcXc = fp.Cx;
                row.CalcXd = fp.Dx;
                row.CalcYa = fp.Ay;
                row.CalcYb = fp.By;
                row.CalcYc = fp.Cy;
                row.CalcYd = fp.Dy;

                double dist = Math.Sqrt(Math.Pow(Math.Abs(refX-x),2) + Math.Pow(Math.Abs(refY-y),2));
                row.Dist = Math.Round(dist* distFactor, 2);

                double distA = Math.Sqrt(Math.Pow(Math.Abs(refXa - fp.Ax), 2) + Math.Pow(Math.Abs(refYa - fp.Ay), 2));
                double distB = Math.Sqrt(Math.Pow(Math.Abs(refXb - fp.Bx), 2) + Math.Pow(Math.Abs(refYb - fp.By), 2));
                double distC = Math.Sqrt(Math.Pow(Math.Abs(refXc - fp.Cx), 2) + Math.Pow(Math.Abs(refYc - fp.Cy), 2));
                double distD = Math.Sqrt(Math.Pow(Math.Abs(refXd - fp.Dx), 2) + Math.Pow(Math.Abs(refYd - fp.Dy), 2));
                row.DistA = Math.Round(distA * distFactor, 2);
                row.DistB = Math.Round(distB * distFactor, 2);
                row.DistC = Math.Round(distC * distFactor, 2);
                row.DistD = Math.Round(distD * distFactor, 2);

            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataRowView drv = (DataRowView)dataGridView1.SelectedRows[0].DataBoundItem;
                DsData.DtShotsCalcRow row = (DsData.DtShotsCalcRow)drv.Row;
                txtRefX.Text = row.CalcX.ToString();
                txtRefY.Text = row.CalcY.ToString();

                txtRefAx.Text = row.CalcXa.ToString();
                txtRefAy.Text = row.CalcYa.ToString();
                txtRefBx.Text = row.CalcXb.ToString();
                txtRefBy.Text = row.CalcYb.ToString();
                txtRefCx.Text = row.CalcXc.ToString();
                txtRefCy.Text = row.CalcYc.ToString();
                txtRefDx.Text = row.CalcXd.ToString();
                txtRefDy.Text = row.CalcYd.ToString();

                btnCalculate_Click(btnCalculate, null);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CalculatePoint cp = new CalculatePoint();
            DsData.DtShotsCalcRow row = (DsData.DtShotsCalcRow)((DataRowView)dataGridView1.SelectedRows[0].DataBoundItem).Row;

            int xdif = 100000;
            int ydif = 100000;
            int bestXConst = 0;
            int bestYConst = 0;

            int TimeA = row.TimeA;
            int TimeB = row.TimeB;
            int TimeC = row.TimeC;
            int TimeD = row.TimeD;
            int AB = TimeA - TimeB;
            int BC = TimeB - TimeC;
            int CD = TimeC - TimeD;
            int AD = TimeA - TimeD;
            int startConst = int.Parse(txtCalcConstant.Text);
            int lastConst = 0;
            for (int constant = startConst; constant < 20000; constant++)
            {
                double x = 0, y = 0;
                cp.CalcConst = constant;
                lastConst = constant;
                CalculatePoint.FourPoints fp = cp.FindCoords((double)AB, (double)BC, (double)CD, (double)AD, ref x, ref y);
                int[] Xs = new int[] { fp.Ax, fp.Bx, fp.Cx, fp.Dx };
                int[] Ys = new int[] { fp.Ay, fp.By, fp.Cy, fp.Dy };

                int thisXdif = 0;
                int thisYdif = 0;
                foreach (int val in Xs)
                {
                    for (int r = 0; r < 4; r++)
                    {
                        int tempX = Math.Abs(val - Xs[r]);
                        if (tempX > thisXdif)
                        {
                            thisXdif = tempX;
                        }
                    }
                }
                if (thisXdif < xdif)
                {
                    xdif = thisXdif;
                    bestXConst = constant;
                }

                foreach (int val in Ys)
                {
                    for (int r = 0; r < 4; r++)
                    {
                        int tempy = Math.Abs(val - Xs[r]);
                        if (tempy > thisYdif)
                        {
                            thisYdif = tempy;
                        }
                    }
                }
                if (thisYdif < ydif)
                {
                    ydif = thisYdif;
                    bestYConst = constant;
                }


            }
            txtBestConstX.Text = bestXConst.ToString();
            txtBestConstY.Text = bestYConst.ToString();
        }
    }
}
