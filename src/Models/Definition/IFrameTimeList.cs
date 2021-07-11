using OpenFrafsuallyLib.Calculators.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenFrafsuallyLib.Models.Definition
{
    interface IFrameTimeList
    {
        protected Dictionary<double, FrameTime> SortedFrameTimes { get; set; }

        protected FrameTimeCalculator _frameTimeCalculator { get; set; }


        protected void PerformFrameTimeSorting();

        public List<FrameTime> FrameTimesList { get; set; }

        public int NumberOfFrames { get; }

        public double CalculatePercentileFps(double percentage);

        public FrameTime PercentileOf(double percentage);
    }
}
