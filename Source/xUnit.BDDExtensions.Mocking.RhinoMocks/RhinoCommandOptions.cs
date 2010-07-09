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
using Rhino.Mocks.Interfaces;
using Xunit.Internal;

namespace Xunit
{
    /// <summary>
    ///   A <see cref = "ICommandOptions" /> implementation for the Rhino.Mocks framework.
    /// </summary>
    internal class RhinoCommandOptions : ICommandOptions
    {
        private readonly IMethodOptions<object> _methodOptions;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "RhinoCommandOptions" /> class.
        /// </summary>
        /// <param name = "methodOptions">The method options.</param>
        public RhinoCommandOptions(IMethodOptions<object> methodOptions)
        {
            Guard.AgainstArgumentNull(methodOptions, "methodOptions");

            _methodOptions = methodOptions;
        }

        #region ICommandOptions Members

        /// <summary>
        ///   Configures that the invocation of the related behavior
        ///   results in the specified <see cref = "Exception" /> beeing thrown.
        /// </summary>
        /// <param name = "exception">Specifies the exception which should be thrown when the
        ///   behavior is invoked.</param>
        public void Throw(Exception exception)
        {
            _methodOptions.Throw(exception);
        }

        public void Callback(Action action)
        {
            _methodOptions.Callback(() =>
                                        {
                                            action();
                                            return true;
                                        });
        }

        #endregion
    }
}