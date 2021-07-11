/*
    Copyright (C) 2021 AluminiumTech

    This library is free software; you can redistribute it and/or
    modify it under the terms of the GNU Lesser General Public
    License as published by the Free Software Foundation; either
    version 2.1 of the License, or (at your option) any later version.

    This library is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
    Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public
    License along with this library; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
    USA
 */

using OpenFrafsuallyLib.Models.Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenFrafsuallyLib.Models
{
    public class Frame : IFrame
    {


        /// <summary>
        /// 
        /// </summary>
        public double TimeMilliseconds => Math.Abs(StartTimeMilliseconds - EndTimeMilliseconds);

        /// <summary>
        /// The horizontal resolution of the frame being rendered measured in pixels.
        /// </summary>
        public int HorizontalResolutionPixels { get; set; }

        /// <summary>
        /// The vertical resolution of the frame being rendered measured in pixels.
        /// </summary>
        public int VerticalResolutionPixels {get; set;}

        /// <summary>
        /// The total number of pixels rendered in this frame, calculated by multiplying the horizontal and vertical resolutions together.
        /// </summary>
        public double TotalNumberOfPixelsRendered => Convert.ToDouble(VerticalResolutionPixels * HorizontalResolutionPixels);

        long IFrame.FrameNumber { get; set; }
        double IFrame.StartTimeMilliseconds { get; set; }
        double IFrame.EndTimeMilliseconds { get; set; }
    }
}
