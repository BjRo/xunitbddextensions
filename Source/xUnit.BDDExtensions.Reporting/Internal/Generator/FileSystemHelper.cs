// Copyright 2009 Björn Rochel - http://www.bjro.de/ 
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//  
//      http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
using System.IO;

namespace Xunit.Reporting.Internal.Generator
{
    /// <summary>
    /// A simple helper class for file system access.
    /// </summary>
    public class FileSystemHelper : IFileSystemHelper
    {
        #region IFileSystemHelper Members

        /// <summary>
        /// Checks whether the directory specified via <paramref name="path"/>
        /// exists.
        /// </summary>
        /// <param name="path">Specifies the full path to a path.</param>
        /// <returns>
        /// 	<c>true</c> if the path exists. Otherwise <c>false</c>.
        /// </returns>
        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// Gets the absolute path representing the same
        /// path as the relative path supplied via <paramref name="relativePath"/>.
        /// </summary>
        /// <param name="relativePath">Specifies a relative path.</param>
        /// <returns>
        /// An absolute representation of <paramref name="relativePath"/>.
        /// </returns>
        public string GetFullPath(string relativePath)
        {
            return Path.GetFullPath(relativePath);
        }

        #endregion
    }
}