using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenFrafsuallyLib.Models.Definition
{
    interface IFrame
    {
        /// <summary>
        /// The frame number in a series of frames
        /// </summary>
        public long FrameNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double StartTimeMilliseconds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double EndTimeMilliseconds { get; set; }

    }
}
