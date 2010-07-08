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

namespace Xunit.Reporting.Internal.Generator
{
    /// <summary>
    ///   A simple abstraction for writing to a file.
    /// </summary>
    public interface IFileWriter
    {
        /// <summary>
        ///   Writes the content supplied via <paramref name = "contentWriter" />
        ///   to the file specified via <see cref = "fileName" />.
        /// </summary>
        /// <param name = "fileName">
        ///   Specifies the filename to write to.
        /// </param>
        /// <param name = "contentWriter">
        ///   Specifies the writer handler which is called
        ///   to fill the file with content.
        /// </param>
        void Write(string fileName, Action<TextWriter> contentWriter);
    }
}