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
        public string SeriesTitle
        {
            get { return (Sighters) ? "Sighting" : "Series " + (_series.Count + 1).ToString(); }
        }

        public ShotSeries[] Series { get { return _series.ToArray(); } }
        public ShotSeries CurrentSeries { get { return _current; } }
        public ShotSeries SighterSeries { get { return _sighters; } }

        public ShootingSession(int targetId, int sessionId, Discipline disc)
        {
            TargetId = targetId;
            SessionId = sessionId;
            Discipline = disc;
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

        public string[,] GetSeriesScores(bool dec)
        {
            string[,] scores = new string[10, 2];
            ShotSeries series = (Sighters) ? _sighters : _current;
            if (series == null) return scores;
            int startIndex = (series.Count > 10) ? (series.Count - 10) : 0;
            for (int x = 0; x < 10; x++)
            {
                if (series.Count > startIndex + x)
                {
                    if (dec)
                    {
                        scores[x, 0] = series.Shots[startIndex + x].Score.ToString("##.0");
                    }
                    else
                    {
                        scores[x, 0] = ((int)series.Shots[startIndex + x].Score).ToString("##");
                    }
                    scores[x, 1] = (series.Shots[startIndex + x].Score > 10.5f) ? "X" : string.Empty;
                }
            }
            return scores;
        }

        public float GetSeriesTotal(bool dec)
        {
            if (_current != null)
            {
                return GetSeriesTotal(dec, _current);
            }
            else
            {
                return 0f;
            }
        }

        public float GetAggregate(bool dec)
        {
            float val = GetSeriesTotal(dec);
            foreach (ShotSeries ser in Series)
            {
                val += GetSeriesTotal(dec, ser);
            }
            return val;
        }

        private float GetSeriesTotal(bool dec, ShotSeries ser)
        {
            float val = 0;
            foreach (SessionShot score in ser.Shots)
            {
                if (dec)
                {
                    val += score.Score;
                }
                else
                {
                    val += (int)score.Score;
                }
            }
            return val;
        }

        public int GetSeriesXCount()
        {
            if (_current != null)
            {
                return GetSeriesXCount(_current);
            }
            else return 0;
        }

        private int GetSeriesXCount(ShotSeries ser)
        {
            int val = 0;
            foreach (SessionShot score in ser.Shots)
            {
                if (score.Score > 10.4f)
                {
                    val += 1;
                }
            }
            return val;
        }

        public int GetTotalXCount()
        {
            int val = GetSeriesXCount();
            foreach (ShotSeries ser in Series)
            {
                val += GetSeriesXCount(ser);
            }
            return val;
        }

        public int ShotCount()
        {
            if (_series != null)
            {
                return (_series.Count * 10) + _current.Count;
            }
            else
            {
                return 0;
            }
        }
    }

}
