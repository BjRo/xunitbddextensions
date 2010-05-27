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
using System.Collections;
using NVelocity.Runtime.Directive;

namespace Xunit.Reporting.Core.Generator
{
    /// <summary>
    /// A proxy class for NVelocitys <see cref="DirectiveManager"/>. This was
    /// introduced in order to intercept resource calls in the NVelocity framework.
    /// Some class names are stored in embedded resource files in NVelocity and aren't
    /// up to date when the assembly has been merged with ILMerge. This class
    /// updates the assembly name contained in the assembly qualified names on the fly.
    /// </summary>
    public class DirectiveManagerProxy : IDirectiveManager
    {
        private readonly IAssembly assemblyContainingVelocity;
        private readonly IDirectiveManager directiveManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectiveManagerProxy"/> class.
        /// </summary>
        /// <param name="directiveManager">The directive manager.</param>
        /// <param name="assemblyContainingVelocity">
        /// Specifies the assembly containing the NVelocity types.
        /// </param>
        public DirectiveManagerProxy(
            IDirectiveManager directiveManager,
            IAssembly assemblyContainingVelocity)
        {
            this.directiveManager = directiveManager;
            this.assemblyContainingVelocity = assemblyContainingVelocity;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectiveManagerProxy"/> class.
        /// </summary>
        public DirectiveManagerProxy() : this(
            new DirectiveManager(),
            new Assembly(typeof (DirectiveManager).Assembly))
        {
        }

        #region IDirectiveManager Members

        /// <summary>
        /// Registers a directive type.
        /// </summary>
        /// <param name="directiveTypeName">
        /// Specifies the assembly qualified type name of the directive to register.
        /// </param>
        public void Register(string directiveTypeName)
        {
            var updatedDirective = AssemblyNameNeedsToBeFixed()
                                       ?
                                           UpdateAssemblyName(directiveTypeName)
                                       :
                                           directiveTypeName;

            directiveManager.Register(updatedDirective);
        }

        /// <summary>
        /// Creates a directive.
        /// </summary>
        /// <param name="name">
        /// Specifies the assembly qualified name of the directive.
        /// </param>
        /// <param name="directiveStack">
        /// Specifies the stack of already existing directives.
        /// </param>
        /// <returns>
        /// The created <see cref="Directive"/>.
        /// </returns>
        public Directive Create(string name, Stack directiveStack)
        {
            return directiveManager.Create(name, directiveStack);
        }

        /// <summary>
        /// Checks whether the manager contains a directive of the name
        /// specified by <paramref name="directiveName"/>.
        /// </summary>
        /// <param name="directiveName">The name.</param>
        /// <returns>
        /// 	<c>true</c> if it already contains an directive; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(string directiveName)
        {
            return directiveManager.Contains(directiveName);
        }

        #endregion

        private bool AssemblyNameNeedsToBeFixed()
        {
            return !string.Equals(assemblyContainingVelocity.Name, "NVelocity");
        }

        private static string UpdateAssemblyName(string directiveTypeName)
        {
            return directiveTypeName.Replace(
                ",NVelocity",
                string.Concat(",", typeof (DirectiveManagerProxy).Assembly.GetName().Name));
        }
    }
}