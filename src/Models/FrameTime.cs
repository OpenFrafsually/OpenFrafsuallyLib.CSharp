/*
    Copyright (C) 2020 AluminiumTech

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

using System;
using OpenFrafsuallyLib.Calculators.Implementation;

namespace OpenFrafsuallyLib.Models
{
    /// <summary>
    /// A class to model a FrameTime of individual frames.
    /// </summary>
    public class FrameTime
    {
        protected FrameTimeCalculator _frameTimeCalculator;

        public Frame frame { get; set; }

        public FrameTime()
        {
            _frameTimeCalculator = new FrameTimeCalculator();

            frame = new Frame();
        }

        /// <summary>
        /// 
        /// </summary>
        [Obsolete("This function is being considered for removal in a future version.")]
        public double FrameTimeMilliseconds =>
           _frameTimeCalculator.CalculateFrameTimesMilliseconds(_frameTimeCalculator.CalculateFramesPerSecond(1,
               Convert.ToDouble(TimeMilliseconds / 1000.0)));
        
        /// <summary>
        /// 
        /// </summary>
        public double TimeMilliseconds => Math.Abs(frame.StartTimeMilliseconds - frame.EndTimeMilliseconds);
    }
}