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
using System.Collections.Generic;
using Xunit.Internal;

namespace Xunit
{
    /// <summary>
    ///   Base class for specifications.
    /// </summary>
    [RunWith(typeof(XbxRunner))]
    public abstract class StaticContextSpecification : IContextSpecification
    {
        private readonly IFakeEngine _fakeEngine;

        /// <summary>
        ///   Creates a new instance of the <see cref = "StaticContextSpecification" /> class.
        /// </summary>
        protected StaticContextSpecification()
        {
            _fakeEngine = RunnerConfiguration.FakeEngine;
        }

        #region ISpecification Members

        /// <summary>
        ///   Initializes the specification class.
        /// </summary>
        void IContextSpecification.InitializeContext()
        {
            EstablishContext();
            Because();
        }

        /// <summary>
        ///   Cleans up the specification class.
        /// </summary>
        void IContextSpecification.CleanupSpecification()
        {
            AfterEachObservation();
            AfterTheSpecification();
        }

        #endregion

        /// <summary>
        ///   Establishes the context for the specification. In AAA terms this 
        ///   method implements the Arange part.
        /// </summary>
        protected virtual void EstablishContext()
        {
        }

        /// <summary>
        ///   Performs the actual action which should be observed by the specification. 
        ///   In AAA terms this method implements the Act part.
        /// </summary>
        protected abstract void Because();

        [Obsolete("EstablishContext and Because are now executed once for all observations which renders this method useless. Use AfterTheSpecification instead.")]
        protected virtual void AfterEachObservation()
        {
        }

        /// <summary>
        ///   Is called after each observation. Can be used for cleanup.
        /// </summary>
        protected virtual void AfterTheSpecification()
        {
        }

        /// <summary>
        ///   Creates a dependency of the type specified by <typeparamref name = "TInterfaceType" />.
        /// </summary>
        /// <typeparam name = "TInterfaceType">
        ///   The type to create a dependency for. (Should be an interface)
        /// </typeparam>
        /// <returns>
        ///   An newly created instance implementing <typeparamref name = "TInterfaceType" />.
        /// </returns>
        protected TInterfaceType An<TInterfaceType>() where TInterfaceType : class
        {
            return _fakeEngine.Stub<TInterfaceType>();
        }

        /// <summary>
        ///   Creates a list of dependencies of the type specified by <typeparamref name = "TInterfaceType" />.
        /// </summary>
        /// <typeparam name = "TInterfaceType">
        ///   Specifies the dependency type. (Should be an interface).
        /// </typeparam>
        /// <returns>
        ///   An newly created instance implementing <typeparamref name = "TInterfaceType" />.
        /// </returns>
        protected IList<TInterfaceType> Some<TInterfaceType>() where TInterfaceType : class
        {
            return _fakeEngine.CreateStubCollectionOf<TInterfaceType>();
        }
    }
}