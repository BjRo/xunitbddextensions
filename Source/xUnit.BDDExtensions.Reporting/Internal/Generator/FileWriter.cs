// Copyright 2010 xUnit.BDDExtensions
//   
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   
//       http://www.apache.org/licenses/LICENSE-2.0
//   
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//  
using System;
using System.IO;
using Xunit.Reporting.Internal.Configuration;

namespace Xunit.Reporting.Internal.Generator
{
    /// <summary>
    ///   A simple file writer.
    /// </summary>
    public class FileWriter : IFileWriter
    {
        private readonly IArguments _arguments;
        private readonly IFileSystemHelper _fileSystemHelper;

        /// <summary>
        ///   Creates a new instance of the <see cref = "FileWriter" /> class.
        /// </summary>
        /// <param name = "arguments">
        ///   Specifies the arguments for this writer.
        /// </param>
        /// <param name = "fileSystemHelper">
        ///   Specifies the helper for 
        /// </param>
        public FileWriter(IArguments arguments, IFileSystemHelper fileSystemHelper)
        {
            _arguments = arguments;
            _fileSystemHelper = fileSystemHelper;
        }

        #region IFileWriter Members

        /// <summary>
        ///   Writes the content supplied via <paramref name = "contentWriter" />
        ///   to the file specified via <see cref = "fileName" />.
        /// </summary>
        /// <param name = "fileName">Specifies the filename to write to.</param>
        /// <param name = "contentWriter">Specifies the writer handler which is called
        ///   to fill the file with content.</param>
        public void Write(string fileName, Action<TextWriter> contentWriter)
        {
            var targetFile = GetTargetFileName(fileName);

            using (var stream = new FileStream(targetFile, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (var streamWriter = new StreamWriter(stream))
                {
                    contentWriter(streamWriter);
                    stream.Flush();
                }
            }
        }

        #endregion

        private string GetTargetFileName(string fileName)
        {
            return Path.Combine(
                GetTargetPath(),
                fileName);
        }

        private string GetTargetPath()
        {
            var targetPath = _arguments.Get(ArgumentKeys.TargetPath);

            if (string.IsNullOrEmpty(targetPath))
            {
                return GetDefaultPath();
            }

            if (targetPath.IndexOfAny(Path.GetInvalidPathChars()) > 0)
            {
                return GetDefaultPath();
            }

            if (!Path.IsPathRooted(targetPath))
            {
                targetPath = _fileSystemHelper.GetFullPath(targetPath);
            }

            if (_fileSystemHelper.DirectoryExists(targetPath))
            {
                return targetPath;
            }

            return GetDefaultPath();
        }

        private static string GetDefaultPath()
        {
            return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        }
    }
}