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
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using OpenFrafsuallyLib.DataSources.Definition;
using OpenFrafsuallyLib.Models;

namespace OpenFrafsuallyLib.DataSources.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class CsvDataSourceProvider : ICsvDataSourceProvider
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathToDataSource"></param>
        /// <param name="timeColumnName"></param>
        /// <param name="frameNumberColumnName"></param>
        /// <returns></returns>
        public FrameTime[] GetDataFromColumns(string pathToDataSource, string timeColumnName, string frameNumberColumnName)
        {
            try
            {
                List<FrameTime> frameTimes = new List<FrameTime>();
            
                double previousTime = 0.0;
            
                using var reader = new StreamReader(pathToDataSource);
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        var currentTime = csv.GetField<double>(timeColumnName);
                        var frameNumber = csv.GetField<long>(frameNumberColumnName);

                        FrameTime frameTime = new FrameTime
                        {
                            FrameNumber = frameNumber,
                            StartTimeMilliseconds = previousTime,
                            EndTimeMilliseconds = currentTime
                        };

                        previousTime = currentTime;
                    
                        frameTimes.Add(frameTime);
                    }
                }

                return frameTimes.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception(ex.ToString());
            }
        }
    }
}