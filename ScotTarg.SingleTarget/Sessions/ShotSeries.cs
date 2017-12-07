using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScotTarg.Sessions
{
    public class ShotSeries
    {
        private List<SessionShot> _shots = new List<SessionShot>();

        public int SeriesId { get; private set; }
        public DateTime StartTime { get; set; }
        public bool IsSighter { get; set; } = true;
        public Position Position { get; set; } = Position.Prone;
        public SessionShot[] Shots { get { return _shots.ToArray(); } }
        public int Count { get { return _shots.Count; } }

        public ShotSeries(int id)
        {
            SeriesId = id;
        }

        public void AddShot(SessionShot shot)
        {
            _shots.Add(shot);
        }
    }

    public enum Position
    {
        Prone,
        Standing,
        Kneeling,
        Sitting
    }
}
