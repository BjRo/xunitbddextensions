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
using Moq.Language.Flow;
using Xunit.Internal;

namespace Xunit.Faking.Moq.Internal
{
    public class MoqQueryOptions<TFake, TReturnValue> : IQueryOptions<TReturnValue> where TFake : class
    {
        private readonly ISetup<TFake, TReturnValue> _methodOptions;

        public MoqQueryOptions(ISetup<TFake, TReturnValue> methodOptions)
        {
            Guard.AgainstArgumentNull(methodOptions, "methodOptions");

            _methodOptions = methodOptions;
        }

        public void Return(TReturnValue returnValue)
        {
            _methodOptions.Returns(returnValue);
        }

        public void Return(Func<TReturnValue> valueFunction)
        {
            _methodOptions.Returns(valueFunction);
        }

        public void Return<T>(Func<T, TReturnValue> valueFunction)
        {
            _methodOptions.Returns(valueFunction);
        }

        public void Return<T1, T2>(Func<T1, T2, TReturnValue> valueFunction)
        {
            _methodOptions.Returns(valueFunction);
        }

        public void Return<T1, T2, T3>(Func<T1, T2, T3, TReturnValue> valueFunction)
        {
            _methodOptions.Returns(valueFunction);
        }

        public void Return<T1, T2, T3, T4>(Func<T1, T2, T3, T4, TReturnValue> valueFunction)
        {
            _methodOptions.Returns(valueFunction);
        }

        public void Throw(Exception exception)
        {
            _methodOptions.Throws(exception);
        }
    }
}