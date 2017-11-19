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
            int refX = int.Parse(txtRefX.Text);
            int refY = int.Parse(txtRefY.Text);
            calculate(refX, refY);
        }

        private void calculate(int refX, int refY)
        { 
            int cc = int.Parse(nudWidth.Text);
            CalculatePoint cp = new CalculatePoint();

            double distFactor = double.Parse(txtDistFactor.Text);

            foreach (DsData.DtShotsCalcRow row in dsData.DtShotsCalc.Rows)
            {
                int TimeA = row.TimeA - (int)nudCorrection.Value;
                int TimeB = row.TimeB + (int)nudCorrection.Value;
                int TimeC = row.TimeC - (int)nudCorrection.Value;
                int TimeD = row.TimeD + (int)nudCorrection.Value;
                int AB = TimeA - TimeB;
                int BC = TimeB - TimeC;
                int CD = TimeD - TimeC;
                int AD = TimeA - TimeD;

                double x = 0, y = 0;
                Point p = CalculatePoint.GetPoint(cc, (double)AB, (double)CD, (double)BC, (double)AD);
                int tlx = CalculatePoint.GetXTopLeft(cc, cc, AB, BC);
                int tly = CalculatePoint.GetYTopLeft(cc, cc, AB, BC);

                x = p.X;
                y = p.Y;
                row.CalcX = (int)x;
                row.CalcY = (int)y;
                row.tlX = tlx;
                row.tlY = tly;

                double dist = Math.Sqrt(Math.Pow(Math.Abs(refX-x),2) + Math.Pow(Math.Abs(refY-y),2));
                row.Dist = Math.Round(dist* distFactor, 2);


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

                btnCalculate_Click(btnCalculate, null);
            }
        }

    }
}
