using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScotTarg.TargetTools
{
    public class ShotData
    {
        #region Public properties

        public uint TargetId { get; set; }
        public uint ShotId { get; set; }
        public DateTime ShotTime { get; set; }
        public int Correction { get; private set; }

        /// <summary>
        /// Time for left bottom Sensor
        /// </summary>
        public int Sensor1Value { get; set; }

        /// <summary>
        /// Time for left top Sensor
        /// </summary>
        public int Sensor2Value { get; set; }

        /// <summary>
        /// Time for right top Sensor
        /// </summary>
        public int Sensor3Value { get; set; }

        /// <summary>
        /// Time for right bottom Sensor
        /// </summary>
        public int Sensor4Value { get; set; }

        public int LeftDiff { get { return Sensor1Value - Sensor2Value; } }
        public int TopDiff { get { return Sensor2Value - Sensor3Value; } }
        public int RightDiff { get { return Sensor4Value - Sensor3Value; } }
        public int BottomDiff { get { return Sensor1Value - Sensor4Value; } }
        public int LeftCorrected { get { return LeftDiff - Correction; } }
        public int TopCorrected { get { return TopDiff + Correction; } }
        public int RightCorrected { get { return RightDiff + Correction; } }
        public int BottomCorrected { get { return BottomDiff - Correction; } }


        public int Calculated_X { get; private set; }
        public int Calculated_Y { get; private set; }

        #endregion // Public properties


        /// <summary>
        /// Constructor
        /// </summary>
        public ShotData()
        {
            TargetId = 0;
            ShotId = 0;
            Correction = 0;
            ShotTime = new DateTime();
            Sensor1Value = 0;
            Sensor2Value = 0;
            Sensor3Value = 0;
            Sensor4Value = 0;
            Calculated_X = -1;
            Calculated_Y = -1;
        }

        /// <summary>
        /// Constructor that initialises values
        /// </summary>
        public ShotData(uint targetId, uint shotId, DateTime time, int val1, int val2, int val3, int val4)
        {
            TargetId = targetId;
            ShotId = shotId;
            ShotTime = time;
            Sensor1Value = val1;
            Sensor2Value = val2;
            Sensor3Value = val3;
            Sensor4Value = val4;
            Correction = 0;
            Calculated_X = -1;
            Calculated_Y = -1;
        }

        /// <summary>
        /// Calculate the X and Y values of the shot
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void DoCalculation(int width, int height)
        {
            Correction = CalculatePoint.GetCorrectionValue(height, width, LeftDiff, RightDiff, TopDiff, BottomDiff);
            Point p = CalculatePoint.GetPoint(width, (double)LeftCorrected, (double)RightCorrected, (double)TopCorrected, (double)BottomCorrected);
            Calculated_X = p.X;
            Calculated_Y = p.Y;
        }
    }
}
