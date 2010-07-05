// Copyright 2008 Björn Rochel - http://www.bjro.de/ 
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Xunit.Reporting.Internal
{
    /// <summary>
    /// Role interface for a class which is able to load
    /// an assembly by it's assembly name or file name.
    /// </summary>
    public interface IAssemblyLoader
    {
        /// <summary>
        /// Tries to load an <see cref="IAssembly"/>.
        /// </summary>
        /// <param name="assemblyName">
        /// Specifies the name or file name of an assembly.
        /// </param>
        /// <returns>
        /// The loaded <see cref="IAssembly"/>.
        /// </returns>
        IAssembly Load(string assemblyName);
    }
}