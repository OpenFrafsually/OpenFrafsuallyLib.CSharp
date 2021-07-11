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

using OpenFrafsuallyLib.DataSources.Definition;

using OpenFrafsuallyLib.Models;
using OpenFrafsuallyLib.Models.Implementation;

namespace OpenFrafsuallyLib.DataSources.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class FrapsDataSourceProvider : IDataSourceProvider
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathToFrapsFrameTimesFile"></param>
        /// <returns></returns>
        public FrameTime[] GetFrameTimeArrayFromDataSource(string pathToFrapsFrameTimesFile)
        {
            CsvDataSourceProvider csvDataSourceProvider = new CsvDataSourceProvider();
            return csvDataSourceProvider.GetDataFromColumns(pathToFrapsFrameTimesFile, "Time (ms)", "Frame");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathToDataSource">Path to the fraps `frametimes.csv` file.</param>
        /// <returns></returns>
        public FrameTimes GetFrameTimesFromDataSource(string pathToDataSource)
        {
            return new FrameTimes(GetFrameTimeArrayFromDataSource(pathToDataSource));
        }
    }
}