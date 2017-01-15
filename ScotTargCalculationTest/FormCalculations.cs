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
        public FormCalculations()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                char TAB = '\t';
                Stream stream = openFileDialog1.OpenFile();
                StreamReader sr = new StreamReader(stream);
                while (!sr.EndOfStream)
                {
                    int counter = 0;
                    string line = sr.ReadLine();
                    if (line.Count(c => c == TAB) == 3)
                    {
                        DsData.DtShotsCalcRow row = dsData.DtShotsCalc.NewDtShotsCalcRow();
                        int a = int.Parse(line.Substring(counter, line.IndexOf(TAB, counter) - counter));
                        counter = line.IndexOf(TAB, counter) + 1;
                        int b = int.Parse(line.Substring(counter, line.IndexOf(TAB, counter) - counter));
                        counter = line.IndexOf(TAB, counter) + 1;
                        int c = int.Parse(line.Substring(counter, line.IndexOf(TAB, counter) - counter));
                        counter = line.IndexOf(TAB, counter) + 1;
                        int d = int.Parse(line.Substring(counter));

                        row.TimeA = a;
                        row.TimeB = b;
                        row.TimeC = c;
                        row.TimeD = d;
                        dsData.DtShotsCalc.Rows.Add(row);
                    }
                }
                sr.Close();
                stream.Close();
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            CalculatePoint cp = new CalculatePoint();
            cp.CalcConst = int.Parse(txtCalcConstant.Text);
            int refX = int.Parse(txtRefX.Text);
            int refY = int.Parse(txtRefY.Text);

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
                cp.FindCoords((double)AB, (double)BC, (double)CD, (double)AD, ref x, ref y);

                row.CalcX = (int)x;
                row.CalcY = (int)y;

                double dist = Math.Sqrt(Math.Pow(Math.Abs(refX-x),2) + Math.Pow(Math.Abs(refY-y),2));
                row.Dist = Math.Round(dist,0);
            }
        }


    }
}
