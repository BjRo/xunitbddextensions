//  Copyright 2010 xUnit.BDDExtensions
//    
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License. 
//  You may obtain a copy of the License at
//    
//        http://www.apache.org/licenses/LICENSE-2.0
//    
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
//  implied. See the License for the specific language governing permissions and
//  limitations under the License.  
//  
using System;
using Xunit.Internal;

namespace Xunit
{
    /// <summary>
    /// Static accessor for the current runner configuration.
    /// </summary>
    public class RunnerConfiguration : IRunnerConfiguration
    {
        internal static readonly IRunnerConfiguration Instance = new RunnerConfiguration();
        private IFakeEngine _fakeEngine;

        /// <summary>
        /// Gets the currently active <see cref="IFakeEngine"/>.
        /// </summary>
        public static IFakeEngine FakeEngine
        {
            get { return Instance.FakeEngine; }
        }

        #region IRunnerConfiguration Members

        /// <summary>
        /// Gets the currently active <see cref="IFakeEngine"/>.
        /// </summary>
        /// <value></value>
        IFakeEngine IRunnerConfiguration.FakeEngine
        {
            get { return _fakeEngine; }
        }


        /// <summary>
        /// Specifies the fake engine to be used.
        /// </summary>
        /// <param name="fakeEngine">The fake engine.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="fakeEngine"/> is <c>null</c>.
        /// </exception>
        public void FakeEngineIs(IFakeEngine fakeEngine)
        {
            Guard.AgainstArgumentNull(fakeEngine, "FakeEngine");

            _fakeEngine = fakeEngine;
        }

        /// <summary>
        /// Specifies the fake engine type to be used for faking.
        /// This must be a type that implements <see cref="IFakeEngine"/>
        /// and has a parameterless public constructor.
        /// </summary>
        /// <param name="fakeEngineType">Type of the fake engine.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="fakeEngineType"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        public void FakeEngineIs(Type fakeEngineType)
        {
            Guard.AgainstArgumentNull(fakeEngineType, "fakeEngineType");
            Guard.ArgumentAssignableTo(fakeEngineType, typeof (IFakeEngine));

            _fakeEngine = (IFakeEngine) Activator.CreateInstance(fakeEngineType);
        }

        #endregion
    }
}