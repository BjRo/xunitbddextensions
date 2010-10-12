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
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;

namespace Xunit.Internal
{
    public class RhinoQueryOptions<TReturnValue> : IQueryOptions<TReturnValue>
    {
        private readonly IMethodOptions<TReturnValue> _methodOptions;

        public RhinoQueryOptions(IMethodOptions<TReturnValue> methodOptions)
        {
            Guard.AgainstArgumentNull(methodOptions, "methodOptions");

            _methodOptions = methodOptions;
        }

        #region void Members

        public void Return(TReturnValue returnValue)
        {
            _methodOptions.Return(returnValue);
        }

        public void Return(Func<TReturnValue> valueFunction)
        {
            RepeatAny(invocation => { invocation.ReturnValue = valueFunction(); });
        }

        public void Return<T>(Func<T, TReturnValue> valueFunction)
        {
            RepeatAny(invocation => { invocation.ReturnValue = valueFunction((T) invocation.Arguments[0]); });
        }

        public void Return<T1, T2>(Func<T1, T2, TReturnValue> valueFunction)
        {
            RepeatAny(invocation =>
            {
                invocation.ReturnValue = valueFunction(
                    (T1) invocation.Arguments[0],
                    (T2) invocation.Arguments[1]);
            });
        }

        public void Return<T1, T2, T3>(Func<T1, T2, T3, TReturnValue> valueFunction)
        {
            RepeatAny(invocation =>
            {
                invocation.ReturnValue = valueFunction(
                    (T1) invocation.Arguments[0],
                    (T2) invocation.Arguments[1],
                    (T3) invocation.Arguments[2]);
            });
        }

        public void Return<T1, T2, T3, T4>(Func<T1, T2, T3, T4, TReturnValue> valueFunction)
        {
            RepeatAny(invocation =>
            {
                invocation.ReturnValue = valueFunction(
                    (T1) invocation.Arguments[0],
                    (T2) invocation.Arguments[1],
                    (T3) invocation.Arguments[2],
                    (T4) invocation.Arguments[3]);
            });
        }

        public void Throw(Exception exception)
        {
            _methodOptions.Throw(exception);
        }

        #endregion

        private void RepeatAny(Action<MethodInvocation> invocationConfig)
        {
            _methodOptions.WhenCalled(invocationConfig).Return(default(TReturnValue)).Repeat.Any();
        }
    }
}