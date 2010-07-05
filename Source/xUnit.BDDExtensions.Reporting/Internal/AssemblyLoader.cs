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
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Xunit.Reporting.Internal
{
    /// <summary>
    ///   A simple loader for assemblies which is able to load
    ///   an assembly by it's assembly name or file name.
    /// </summary>
    public class AssemblyLoader : IAssemblyLoader
    {
        private static string _lastAssemblyLoadPath;

        private static readonly string[] KnownExtensions = new[]
        {
            ".dll", ".exe"
        };

        /// <summary>
        ///   Initializes the <see cref = "AssemblyLoader" /> class.
        /// </summary>
        static AssemblyLoader()
        {
            AppDomain.CurrentDomain.AssemblyResolve += OnAssemblyResolve;
        }

        #region IAssemblyLoader Members

        /// <summary>
        ///   Tries to load an <see cref = "IAssembly" />.
        /// </summary>
        /// <param name = "assemblyName">
        ///   Specifies the name or file name of an assembly.
        /// </param>
        /// <returns>
        ///   The loaded <see cref = "IAssembly" />.
        /// </returns>
        public IAssembly Load(string assemblyName)
        {
            Debug.Assert(!string.IsNullOrEmpty(assemblyName));

            System.Reflection.Assembly assembly;

            if (NameContainsFileExtension(assemblyName))
            {
                if (IsRelativeFileName(assemblyName))
                {
                    assembly = System.Reflection.Assembly.Load(
                        Path.GetFileNameWithoutExtension(assemblyName));
                }
                else
                {
                    assembly = System.Reflection.Assembly.LoadFile(assemblyName);
                    _lastAssemblyLoadPath = Path.GetDirectoryName(assemblyName);
                }
            }
            else
            {
                assembly = System.Reflection.Assembly.Load(assemblyName);
            }

            return new Assembly(assembly);
        }

        #endregion

        private static string BuildFullNameWithLastLoadDirectory(string unqualifiedName, string extension)
        {
            return Path.Combine(
                _lastAssemblyLoadPath,
                string.Concat(unqualifiedName, extension));
        }

        private static string GetUnqualifedAssemblyName(ResolveEventArgs args)
        {
            return args.Name.Substring(0, args.Name.IndexOf(','));
        }

        private static bool IsRelativeFileName(string assemblyName)
        {
            var path = Path.GetDirectoryName(assemblyName);

            return string.IsNullOrEmpty(path) || !Path.IsPathRooted(path);
        }

        private static bool NameContainsFileExtension(string assemblyName)
        {
            var candidateExtension = Path.GetExtension(assemblyName);

            return KnownExtensions.Exists(extension =>
                                          string.Equals(candidateExtension, extension, StringComparison.Ordinal));
        }

        private static System.Reflection.Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (string.IsNullOrEmpty(_lastAssemblyLoadPath))
            {
                return null;
            }

            return TryToLoadFromLastLoadPath(args);
        }

        private static System.Reflection.Assembly TryToLoadFromLastLoadPath(ResolveEventArgs args)
        {
            var nameWithoutVersion = GetUnqualifedAssemblyName(args);

            return (from extension in KnownExtensions
                    select BuildFullNameWithLastLoadDirectory(nameWithoutVersion, extension)
                    into updatedLoadPath where File.Exists(updatedLoadPath) select System.Reflection.Assembly.LoadFile(updatedLoadPath)).FirstOrDefault();
        }
    }
}