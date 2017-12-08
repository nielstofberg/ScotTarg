using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScotTarg.Sessions
{
    public class Discipline
    {
        public string Name { get; set; } = "NSRA 25 Yard Rifle";
        public string Description { get; set; } = "NSRA 25 Yard Rifle";
        public float AmmoDiameter { get; set; } = 5.6f;
        public float[] Rings { get; set; } = new float[] { 12.92f, 20.23f, 27.55f, 34.86f, 42.18f, 49.49f, 56.81f, 64.12f, 71.44f, 78.75f };
        public float XRing { get; set; } = 1.0f;
        public float BlackDiameter { get; set; } = 49.49f;
        public float AimingMark { get; set; } = 51.39f;
        public float Distance { get; set; } = 22.86f;
        public ScoringDirection Direction { get; set; } = ScoringDirection.Outward;

        public Discipline()
        {

        }

        public enum ScoringDirection
        {
            Inward,
            Outward
        }
    }
}
