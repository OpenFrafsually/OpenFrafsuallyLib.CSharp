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
using OpenFrafsuallyLib.Calculators.Definition;

namespace OpenFrafsuallyLib.Calculators.Implementation
{
    /// <summary>
    /// A class to implement the IFrameTimeCalculator interface.
    /// </summary>
    public class FrameTimeCalculator : IFrameTimeCalculator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fps"></param>
        /// <returns></returns>
        double IFrameTimeCalculator.CalculateSecondsPerFrame(double fps) => (1.0 / fps);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frames"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        double IFrameTimeCalculator.CalculateSecondsPerFrame(long frames, double seconds) => (seconds / Convert.ToDouble(frames));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frames"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        double IFrameTimeCalculator.CalculateFramesPerSecond(long frames, double seconds) => (Convert.ToDouble(frames) / seconds);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frametimes"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        double IFrameTimeCalculator.CalculateFramesPerSecondWithFrameTimes(double frametimes, double seconds) => ((seconds * 1000.0) / frametimes);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fps"></param>
        /// <returns></returns>
        double IFrameTimeCalculator.CalculateFrameTimesMilliseconds(double fps) => (1000.0 / fps);
    }
}