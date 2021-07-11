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

using OpenFrafsuallyLib.Models.Definition;

namespace OpenFrafsuallyLib.Models
{
    /// <summary>
    /// A class to model a FrameTime of individual frames.
    /// </summary>
    public class FrameTime : IFrameTime
    {
        protected FrameTimeCalculator _frameTimeCalculator;

        /// <summary>
        /// 
        /// </summary>
        Frame IFrameTime.frame { get; set; }

        public FrameTime()
        {
            _frameTimeCalculator = new FrameTimeCalculator();

            ((IFrameTime)this).frame = new Frame();
        }

        /// <summary>
        /// 
        /// </summary>
        double IFrameTime.FrameTimeMilliseconds =>
           ((Calculators.Definition.IFrameTimeCalculator)_frameTimeCalculator).CalculateFrameTimesMilliseconds(((Calculators.Definition.IFrameTimeCalculator)_frameTimeCalculator).CalculateFramesPerSecond(1,
               Convert.ToDouble(((IFrameTime)this).frame.TimeMilliseconds / 1000.0)));
       
    }
}