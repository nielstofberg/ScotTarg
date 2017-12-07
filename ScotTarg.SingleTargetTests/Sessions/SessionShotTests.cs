using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScotTarg.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScotTarg.Sessions.Tests
{
    [TestClass()]
    public class SessionShotTests
    {
        [TestMethod()]
        public void CalcScoreTest()
        {
            float dist = 3.2946f;
            float score = SessionShot.CalcScore(new Discipline(), dist);
            
        }
    }
}