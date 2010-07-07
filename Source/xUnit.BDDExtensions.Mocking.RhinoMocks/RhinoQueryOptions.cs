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
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;
using Xunit.Internal;

namespace Xunit
{
    /// <summary>
    /// A <see cref="IQueryOptions{TReturn}"/> implementation for the Rhino.Mocks framework.
    /// </summary>
    /// <typeparam name="TReturnValue">
    /// Specifies the type of the return value.
    /// </typeparam>
    internal class RhinoQueryOptions<TReturnValue> : IQueryOptions<TReturnValue>
    {
        private readonly IMethodOptions<TReturnValue> _methodOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="RhinoQueryOptions{TReturnValue}"/> class.
        /// </summary>
        /// <param name="methodOptions">The method options.</param>
        public RhinoQueryOptions(IMethodOptions<TReturnValue> methodOptions)
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
        /// A <see cref="IQueryOptions{TReturn}"/> for further configuration.
        /// </returns>
        public IQueryOptions<TReturnValue> Return(TReturnValue returnValue)
        {
            _methodOptions.Return(returnValue);
            return this;
        }

        public IQueryOptions<TReturnValue> Return(Func<TReturnValue> valueFunction)
        {
            return RepeatAny(invocation =>
            {
                invocation.ReturnValue = valueFunction();
            });
        }

        public IQueryOptions<TReturnValue> Return<T>(Func<T, TReturnValue> valueFunction)
        {
            return RepeatAny(invocation =>
            {
                invocation.ReturnValue = valueFunction((T)invocation.Arguments[0]);
            });
        }

        public IQueryOptions<TReturnValue> Return<T1, T2>(Func<T1, T2, TReturnValue> valueFunction)
        {
            return RepeatAny(invocation =>
            {
                invocation.ReturnValue = valueFunction(
                    (T1)invocation.Arguments[0],
                    (T2)invocation.Arguments[1]);
            });
        }

        public IQueryOptions<TReturnValue> Return<T1, T2, T3>(Func<T1, T2, T3, TReturnValue> valueFunction)
        {
            return RepeatAny(invocation =>
            {
                invocation.ReturnValue = valueFunction(
                    (T1) invocation.Arguments[0],
                    (T2) invocation.Arguments[1],
                    (T3) invocation.Arguments[2]);
            });
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
        /// A <see cref="IQueryOptions{TReturn}"/> for further configuration.
        /// </returns>
        public IQueryOptions<TReturnValue> Throw(Exception exception)
        {
            _methodOptions.Throw(exception);
            return this;
        }

        private IQueryOptions<TReturnValue> RepeatAny(Action<MethodInvocation> invocationConfig)
        {
            _methodOptions.WhenCalled(invocationConfig).Return(default(TReturnValue)).Repeat.Any();

            return this;
        }
    }
}