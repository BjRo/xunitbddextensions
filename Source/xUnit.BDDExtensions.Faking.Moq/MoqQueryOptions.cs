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
    /// <typeparam name = "TReturnValue">Specifies the type of the return value.</typeparam>
    internal class MoqQueryOptions<TTarget, TReturnValue> : IQueryOptions<TReturnValue> where TTarget : class
    {
        private readonly ISetup<TTarget, TReturnValue> _methodOptions;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "MoqQueryOptions{TTarget,TReturnValue}" /> class.
        /// </summary>
        /// <param name = "methodOptions">The method options.</param>
        public MoqQueryOptions(ISetup<TTarget, TReturnValue> methodOptions)
        {
            Guard.AgainstArgumentNull(methodOptions, "methodOptions");

            _methodOptions = methodOptions;
        }

        #region IQueryOptions<TReturnValue> Members

        /// <summary>
        ///   Sets up the return value of a behavior.
        /// </summary>
        /// <param name = "returnValue">
        ///   Specifies the return value.
        /// </param>
        /// <returns>
        ///   A <see cref = "IQueryOptions{TReturn}" /> for further configuration.
        /// </returns>
        public IQueryOptions<TReturnValue> Return(TReturnValue returnValue)
        {
            _methodOptions.Returns(returnValue);
            return this;
        }

        /// <summary>
        ///   Configures that the function supplied by <paramref name = "valueFunction" />
        ///   will be used to evaluate the result value of a behavior.
        /// </summary>
        /// <param name = "valueFunction">
        ///   Specifies the function which is called when the method is called.
        /// </param>
        /// <returns>
        ///   A <see cref = "IQueryOptions{TReturn}" /> for further configuration.
        /// </returns>
        /// <remarks>
        ///   Use this for configuring parameterless methods.
        /// </remarks>
        public IQueryOptions<TReturnValue> Return(Func<TReturnValue> valueFunction)
        {
            _methodOptions.Returns(valueFunction);
            return this;
        }

        /// <summary>
        ///   Configures that the function supplied by <paramref name = "valueFunction" />
        ///   will be used to evaluate the result value of a behavior.
        /// </summary>
        /// <param name = "valueFunction">
        ///   Specifies the function which is called when the method is called.
        /// </param>
        /// <returns>
        ///   A <see cref = "IQueryOptions{TReturn}" /> for further configuration.
        /// </returns>
        /// <remarks>
        ///   Use this for configuring methods with a single parameter.
        /// </remarks>
        public IQueryOptions<TReturnValue> Return<T>(Func<T, TReturnValue> valueFunction)
        {
            _methodOptions.Returns(valueFunction);
            return this;
        }

        /// <summary>
        ///   Configures that the function supplied by <paramref name = "valueFunction" />
        ///   will be used to evaluate the result value of a behavior.
        /// </summary>
        /// <param name = "valueFunction">
        ///   Specifies the function which is called when the method is called.
        /// </param>
        /// <returns>
        ///   A <see cref = "IQueryOptions{TReturn}" /> for further configuration.
        /// </returns>
        /// <remarks>
        ///   Use this for configuring methods with two parameters.
        /// </remarks>
        public IQueryOptions<TReturnValue> Return<T1, T2>(Func<T1, T2, TReturnValue> valueFunction)
        {
            _methodOptions.Returns(valueFunction);
            return this;
        }

        /// <summary>
        ///   Configures that the function supplied by <paramref name = "valueFunction" />
        ///   will be used to evaluate the result value of a behavior.
        /// </summary>
        /// <param name = "valueFunction">
        ///   Specifies the function which is called when the method is called.
        /// </param>
        /// <returns>
        ///   A <see cref = "IQueryOptions{TReturn}" /> for further configuration.
        /// </returns>
        /// <remarks>
        ///   Use this for configuring methods with three parameters.
        /// </remarks>
        public IQueryOptions<TReturnValue> Return<T1, T2, T3>(Func<T1, T2, T3, TReturnValue> valueFunction)
        {
            _methodOptions.Returns(valueFunction);
            return this;
        }

        /// <summary>
        ///   Configures that the function supplied by <paramref name = "valueFunction" />
        ///   will be used to evaluate the result value of a behavior.
        /// </summary>
        /// <param name = "valueFunction">
        ///   Specifies the function which is called when the method is called.
        /// </param>
        /// <returns>
        ///   A <see cref = "IQueryOptions{TReturn}" /> for further configuration.
        /// </returns>
        /// <remarks>
        ///   Use this for configuring methods with four parameters.
        /// </remarks>
        public IQueryOptions<TReturnValue> Return<T1, T2, T3, T4>(Func<T1, T2, T3, T4, TReturnValue> valueFunction)
        {
            _methodOptions.Returns(valueFunction);
            return this;
        }

        /// <summary>
        ///   Configures that the invocation of the related behavior
        ///   results in the specified <see cref = "Exception" /> beeing thrown.
        /// </summary>
        /// <param name = "exception">
        ///   Specifies the exception which should be thrown when the 
        ///   behavior is invoked.
        /// </param>
        /// <returns>
        ///   A <see cref = "IQueryOptions{TReturn}" /> for further configuration.
        /// </returns>
        public IQueryOptions<TReturnValue> Throw(Exception exception)
        {
            _methodOptions.Throws(exception);
            return this;
        }

        #endregion
    }
}