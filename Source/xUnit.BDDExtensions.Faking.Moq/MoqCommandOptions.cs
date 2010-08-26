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
using Moq.Language.Flow;
using Xunit.Internal;

namespace Xunit
{
    /// <summary>
    ///   A <see cref = "IQueryOptions{TReturn}" /> implementation for the Moq framework.
    /// </summary>
    /// <typeparam name = "TTarget">The type of the target.</typeparam>
    internal class MoqCommandOptions<TTarget> : ICommandOptions where TTarget : class
    {
        private readonly ISetup<TTarget> _methodOptions;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "MoqCommandOptions{TTarget}" /> class.
        /// </summary>
        /// <param name = "methodOptions">The method options.</param>
        public MoqCommandOptions(ISetup<TTarget> methodOptions)
        {
            Guard.AgainstArgumentNull(methodOptions, "methodOptions");

            _methodOptions = methodOptions;
        }

        #region ICommandOptions Members

        /// <summary>
        ///   Configures that the invocation of the related behavior
        ///   results in the specified <see cref = "Exception" /> beeing thrown.
        /// </summary>
        /// <param name = "exception">
        ///   Specifies the exception which should be thrown when the 
        ///   behavior is invoked.
        /// </param>
        public void Throw(Exception exception)
        {
            _methodOptions.Throws(exception);
        }

        /// <summary>
        ///   Configures that the function supplied by <paramref name = "callback" />
        ///   will be called when the method under configuration is called.
        /// </summary>
        /// <param name = "callback">
        ///   Specifies the function which is called when the method under configuration is called.
        /// </param>
        /// <remarks>
        ///   Use this overload when you're not interested in the parameters.
        /// </remarks>
        public void Callback(Action callback)
        {
            _methodOptions.Callback(callback);
        }

        /// <summary>
        ///   Configures that the function supplied by <paramref name = "callback" />
        ///   will be called when the method under configuration is called.
        /// </summary>
        /// <param name = "callback">
        ///   Specifies the function which is called when the method under configuration is called.
        /// </param>
        /// <remarks>
        ///   Use this for callbacks on methods with a single parameter.
        /// </remarks>
        public void Callback<T>(Action<T> callback)
        {
            _methodOptions.Callback(callback);
        }

        /// <summary>
        ///   Configures that the function supplied by <paramref name = "callback" />
        ///   will be called when the method under configuration is called.
        /// </summary>
        /// <param name = "callback">
        ///   Specifies the function which is called when the method under configuration is called.
        /// </param>
        /// <remarks>
        ///   Use this for callbacks on methods with two parameters.
        /// </remarks>
        public void Callback<T1, T2>(Action<T1, T2> callback)
        {
            _methodOptions.Callback(callback);
        }

        /// <summary>
        ///   Configures that the function supplied by <paramref name = "callback" />
        ///   will be called when the method under configuration is called.
        /// </summary>
        /// <param name = "callback">
        ///   Specifies the function which is called when the method under configuration is called.
        /// </param>
        /// <remarks>
        ///   Use this for callbacks on methods with three parameters.
        /// </remarks>
        public void Callback<T1, T2, T3>(Action<T1, T2, T3> callback)
        {
            _methodOptions.Callback(callback);
        }

        /// <summary>
        ///   Configures that the function supplied by <paramref name = "callback" />
        ///   will be called when the method under configuration is called.
        /// </summary>
        /// <param name = "callback">
        ///   Specifies the function which is called when the method under configuration is called.
        /// </param>
        /// <remarks>
        ///   Use this for callbacks on methods with four parameters.
        /// </remarks>
        public void Callback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> callback)
        {
            _methodOptions.Callback(callback);
        }

        #endregion
    }
}