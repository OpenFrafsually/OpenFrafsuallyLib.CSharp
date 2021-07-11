using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenFrafsuallyLib.Models.Definition
{
    interface IFrameTime
    {

      public Frame frame { get; set; }

      public double FrameTimeMilliseconds { get; }
    }
}
