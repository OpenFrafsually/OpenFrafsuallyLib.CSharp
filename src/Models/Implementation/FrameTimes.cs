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

using System.Collections;
using System.Collections.Generic;

using OpenFrafsuallyLib.Calculators.Implementation;

using OpenFrafsuallyLib.Models.Definition;

namespace OpenFrafsuallyLib.Models.Implementation
{
    /// <summary>
    /// A class to manage groups of frametimes.
    /// </summary>
    public class FrameTimes : IFrameTimes
    {
         List<FrameTime> IFrameTimes.FrameTimesList { get; set; }

        protected Dictionary<double, FrameTime> SortedFrameTimes;

        protected FrameTimeCalculator _frameTimeCalculator;
        
        
         int IFrameTimes.NumberOfFrames => FrameTimesList.Count;

        /// <summary>
        /// 1.0% Lows Frame rates
        /// </summary>
        double IFrameTimes.OnePercentLowsFps => CalculatePercentileFps(1.0);
        
        /// <summary>
        /// 0.1% Lows Frame rates
        /// </summary>
         double IFrameTimes.ZeroPointOnePercentLowsFps => CalculatePercentileFps(0.1);

         double IFrameTimes.MaximumFps => _frameTimeCalculator.CalculateFramesPerSecond(NumberOfFrames, ((IFrameTime)SortedFrameTimes[NumberOfFrames]).FrameTimeMilliseconds / 1000);

         double IFrameTimes.MinimumFps => _frameTimeCalculator.CalculateFramesPerSecond(0, ((IFrameTime)SortedFrameTimes[NumberOfFrames]).FrameTimeMilliseconds / 1000);

        /// <summary>
        /// 25% Percentile Frame rates
        /// </summary>
         double IFrameTimes.LowerQuartileFps => CalculatePercentileFps(25.0);
        
        /// <summary>
        /// 50% Percentile Frame rates
        /// </summary>
         double IFrameTimes.MedianFps => CalculatePercentileFps(50.0);
        
        /// <summary>
        /// 75% Percentile Frame rates
        /// </summary>
         double IFrameTimes.UpperQuartileFps => CalculatePercentileFps(75.0);

        /// <summary>
        /// 
        /// </summary>
        double IFrameTimes.ArithmeticAverageFps
        {
            get
            {
                double average = 0.0;

                foreach (FrameTime frameTime in FrameTimesList)
                {
                    var seconds = ((IFrameTime)frameTime).frame.TimeMilliseconds / 1000.0;

                    average += ((Calculators.Definition.IFrameTimeCalculator)_frameTimeCalculator).CalculateFramesPerSecond(1, seconds);
                }

                return average / Convert.ToDouble(FrameTimesList.Count);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        double IFrameTimes.GeometricAverageFps
        {
            get
            {
                double average = 0.0;

                foreach (FrameTime frameTime in FrameTimesList)
                {
                    var seconds = ((IFrameTime)frameTime).frame.TimeMilliseconds / 1000.0;

                    average *= ((Calculators.Definition.IFrameTimeCalculator)_frameTimeCalculator).CalculateFramesPerSecond(1, seconds);
                }

                return Math.Pow(average, (1.0 / Convert.ToDouble(FrameTimesList.Count)));
            }
        }

        public FrameTimes()
        {        
            _frameTimeCalculator = new FrameTimeCalculator();

            SortedFrameTimes = new Dictionary<double, FrameTime>();
        }

        public FrameTimes(FrameTime[] frameTimesArray)
        {
            Add(frameTimesArray);
        }
        
        /// <summary>
        /// Adds every  frametime array to the FrameTimes collection.
        /// </summary>
        /// <param name="frameTimesArray"></param>
        public void Add(FrameTime[] frameTimesArray)
        {
            foreach (FrameTime frameTime in frameTimesArray)
            {
               FrameTimesList.Add(frameTime);
            }

            PerformFrameTimeSorting();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="frameTimesList"></param>
        public void Add(List<FrameTime> frameTimesList) => Add(FrameTimesList.ToArray());
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="frameTimesArray"></param>
        public void Remove(FrameTime[] frameTimesArray)
        {
            for (int index = 0; index < frameTimesArray.Length; index++)
            {
                this.FrameTimesList.Remove(frameTimesArray[index]);
            }

            PerformFrameTimeSorting();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="frameTimesList"></param>
        public void Remove(List<FrameTime> frameTimesList) => Remove(FrameTimesList.ToArray());

        public List<FrameTime> ToList()=> FrameTimesList;
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="percentage"></param>
        /// <returns></returns>
        public double CalculatePercentileFps(double percentage)
        {
            var seconds = ((IFrameTime)PercentileOf(percentage)).FrameTimeMilliseconds / 1000.0;
            return ((Calculators.Definition.IFrameTimeCalculator)_frameTimeCalculator).CalculateFramesPerSecond(1, seconds);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="percentage"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public FrameTime PercentileOf(double percentage)
        {
            if(percentage > 100){
                throw new Exception("Error: Inappropriate percentage value (over 100%) provided as parameter.");
            }
            if(percentage < 0){
                throw new Exception("Error: Inappropriate percentage value (less than 0%) provided as parameter.");
            }

            PerformFrameTimeSorting();
            
            //No rounding necessary cos Int32.
            //percentileIndex = Math.Round(percentileIndex, 0, MidpointRounding.ToEven);
            return SortedFrameTimes[Convert.ToInt32(percentage / 100) * FrameTimesList.ToArray().Length];
        }

        protected void PerformFrameTimeSorting()
        {
            SortedFrameTimes.Clear();

            List<FrameTime> SortedFrameTimesList = new List<FrameTime>();

            double previousFrameTimeMilliseconds = 0.0;

            int index = 0;

            foreach(FrameTime frametime in FrameTimesList)
            {
                if(((IFrameTime)frametime).FrameTimeMilliseconds < previousFrameTimeMilliseconds)
                {
                    SortedFrameTimesList.Insert(index, frametime);
                }
                else if(((IFrameTime)frametime).FrameTimeMilliseconds == previousFrameTimeMilliseconds)
                {
                    SortedFrameTimesList.Insert(index + 1, frametime);
                }
                else if(((IFrameTime)frametime).FrameTimeMilliseconds > previousFrameTimeMilliseconds)
                {
                    SortedFrameTimesList.Insert(index + 1, frametime);
                }

                index++;
            }

            for(int sortIndex = 0; sortIndex < SortedFrameTimesList.Count; sortIndex++)
            {
                SortedFrameTimes.Add(sortIndex, SortedFrameTimesList[sortIndex]);
            }
        }
    }
}