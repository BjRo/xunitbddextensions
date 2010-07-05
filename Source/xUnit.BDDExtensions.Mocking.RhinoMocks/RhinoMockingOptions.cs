// Copyright 2010 xUnit.BDDExtensions
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
using Rhino.Mocks.Interfaces;
using Xunit.Internal;

namespace Xunit
{
    /// <summary>
    /// A <see cref="IMockingOptions{TReturn}"/> implementation for Rhino.Mocks framework.
    /// </summary>
    /// <typeparam name="TReturnValue">
    /// Specifies the type of the return value.
    /// </typeparam>
    internal class RhinoMockingOptions<TReturnValue> : IMockingOptions<TReturnValue>
    {
        private readonly IMethodOptions<TReturnValue> _methodOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="RhinoMockingOptions&lt;TReturnValue&gt;"/> class.
        /// </summary>
        /// <param name="methodOptions">The method options.</param>
        public RhinoMockingOptions(IMethodOptions<TReturnValue> methodOptions)
        {
            Guard.AgainstArgumentNull(methodOptions, "methodOptions");

            _methodOptions = methodOptions;
        }

        /// <summary>
        /// Sets up the return value of a behavior.
        /// </summary>
        /// <param name="returnValue">
        /// Specifies the return value.
        /// </param>
        /// <returns>
        /// A <see cref="IMockingOptions{TReturn}"/> for further configuration.
        /// </returns>
        public IMockingOptions<TReturnValue> Return(TReturnValue returnValue)
        {
            _methodOptions.Return(returnValue);
            return this;
        }

        /// <summary>
        /// Configures that the invocation of the related behavior
        /// results in the specified <see cref="Exception"/> beeing thrown.
        /// </summary>
        /// <param name="exception">
        /// Specifies the exception which should be thrown when the 
        /// behavior is invoked.
        /// </param>
        /// <returns>
        /// A <see cref="IMockingOptions{TReturn}"/> for further configuration.
        /// </returns>
        public IMockingOptions<TReturnValue> Throw(Exception exception)
        {
            _methodOptions.Throw(exception);
            return this;
        }
    }
}