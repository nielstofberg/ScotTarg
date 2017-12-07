using ScotTarg.TargetTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScotTarg.Sessions
{
    public class ShootingSession
    {
        private List<ShotSeries> _series = new List<ShotSeries>();
        private ShotSeries _sighters;
        private ShotSeries _current;

        private double _distPerCount = 0.022666666666d;
        private int _calcWidth = 13235;


        public int TargetId { get; private set; }
        public int SessionId { get; private set; }
        public string SessionName { get; set; }
        public bool Started { get; private set; } = false;
        public DateTime StartTime { get; private set; }
        public int TargetWidth { get; set; } = 300;
        public Discipline Discipline { get; set; }
        public Position Position { get; set; } = Position.Prone;
        public bool Sighters { get; set; }

        public ShotSeries[] Series { get { return _series.ToArray(); } }

        public ShootingSession(int targetId, int sessionId, Discipline disc)
        {
            TargetId = targetId;
            SessionId = sessionId;
        }

        /// <summary>
        /// Update the constant values used to calculate the shot positions
        /// </summary>
        /// <param name="speedOfSound">340m/s (ish)</param>
        /// <param name="msPerCount">6.666666666666667E-05</param>
        /// <param name="targetWidth">300mm</param>
        public void UpdateConstants(int speedOfSound, double msPerCount)
        {
            _distPerCount = msPerCount * speedOfSound;
            _calcWidth = (int)Math.Round(TargetWidth / _distPerCount, 0);
        }

        public void StartSession()
        {
            StartTime = DateTime.Now;
            Started = true;
            Sighters = true;
            _current = new ShotSeries(1);
            _current.IsSighter = false;
            _sighters = new ShotSeries(0);
            _sighters.IsSighter = true;

        }

        public void EndSession()
        {
            Started = false;
        }

        public void AddShot(ShotData shot)
        {
            if (Started)
            {
                SessionShot ss = new SessionShot(_distPerCount, _calcWidth, shot, Discipline);

                if (Sighters)
                {
                    _sighters.AddShot(ss);
                }
                else
                {
                    if (_current.Count >= 10)
                    {
                        _series.Add(_current);
                        int cid = _current.SeriesId + 1;
                        _current = new ShotSeries(cid);
                        _current.IsSighter = false;
                    }
                    _current.AddShot(ss);
                }
            }
        }

    }

}
