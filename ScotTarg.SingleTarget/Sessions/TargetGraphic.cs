using ScotTarg.TargetTools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScotTarg.Sessions
{
    public class TargetGraphic
    {
        public const int WIDTH = 800;
        public Discipline Discipline { get; private set; } = new Discipline();
        public Size TargetSize { get; set; } = new Size(WIDTH, WIDTH);

        public TargetGraphic()
        {
        }

        public TargetGraphic(Discipline disc)
        {
            Discipline = disc;
        }

        /// <summary>
        /// Create a bitmap with a graphical representation of a target for the selected Discipline
        /// </summary>
        /// <param name="lastRing"></param>
        /// <returns></returns>
        public Bitmap GetTargetImage(int lastRing)
        {
            float dpmm = (WIDTH - 10) / Discipline.Rings[lastRing - 1]; // Calculate dots per millimeter
            float ringDia = Discipline.XRing * dpmm;
            float blackDia = Discipline.BlackDiameter * dpmm;
            float fontsize = dpmm * 2;
            Bitmap bmp = new Bitmap(WIDTH, WIDTH);
            Graphics graph = Graphics.FromImage(bmp);

            int xMiddle = TargetSize.Width / 2;
            int yMiddle = TargetSize.Height / 2;
            Font fnt = new Font("", fontsize);
            graph.FillRectangle(Brushes.LightGray, 0, 0, WIDTH, WIDTH);
            Pen blackPen = new Pen(Color.Black, 2);
            Pen whitePen = new Pen(Color.White, 2);

            if (blackDia > (WIDTH - 10)) blackDia = WIDTH - 10;
            if (lastRing > 10) lastRing  = 10;

            graph.FillPie(Brushes.Black, xMiddle - (blackDia / 2), yMiddle - (blackDia / 2), blackDia, blackDia, 0, 360);

            graph.DrawArc(whitePen, xMiddle - (ringDia / 2), yMiddle - (ringDia / 2), ringDia, ringDia, 0, 360);
            for (int x = 0; x < lastRing; x++)
            {
                Pen p = (ringDia < blackDia) ? whitePen : blackPen;
                ringDia = Discipline.Rings[x] * dpmm;
                graph.DrawArc(p, xMiddle - (ringDia / 2), yMiddle - (ringDia / 2), ringDia, ringDia, 0, 360);
                if (x > 1)
                {
                    graph.DrawString((10 - x).ToString(), fnt, Brushes.White, xMiddle - (ringDia / 2), yMiddle - (fontsize * 0.75f));
                    graph.DrawString((10 - x).ToString(), fnt, Brushes.White, xMiddle + (ringDia / 2) - fontsize - 5, yMiddle - (fontsize * 0.75f));
                    graph.DrawString((10 - x).ToString(), fnt, Brushes.White, xMiddle - (fontsize / 2), yMiddle - (ringDia / 2));
                    graph.DrawString((10 - x).ToString(), fnt, Brushes.White, xMiddle - (fontsize / 2), yMiddle + (ringDia / 2) - fontsize * 1.5f);
                }
            }


            return bmp;
        }

        public void AddShot(ref Bitmap bmp, SessionShot shot, int lastRing, Color color)
        {
            float dpmm = (WIDTH - 10) / Discipline.Rings[lastRing - 1]; // Calculate dots per millimeter
            int xMiddle = bmp.Width / 2;
            int yMiddle = bmp.Height / 2;

            float xPos = (float)(xMiddle + (shot.X_mm * dpmm));
            float yPos = (float)(xMiddle + (shot.Y_mm * dpmm));
            float diameter = Discipline.AmmoDiameter * dpmm;

            Graphics graph = Graphics.FromImage(bmp);
            Brush brush = new SolidBrush(color);

            graph.FillPie(brush, xPos - (diameter / 2), yPos - (diameter / 2), diameter, diameter, 0, 360);

        }

    }
}
