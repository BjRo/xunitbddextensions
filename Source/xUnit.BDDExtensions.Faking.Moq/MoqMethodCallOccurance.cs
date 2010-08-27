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
using System.Linq.Expressions;
using Moq;

namespace Xunit.Faking.Moq
{
    internal class MoqMethodCallOccurance<TDependency> : IMethodCallOccurance where TDependency : class
    {
        private readonly Expression<Action<TDependency>> _func;
        private readonly Mock<TDependency> _mock;

        public MoqMethodCallOccurance(Mock<TDependency> mock, Expression<Action<TDependency>> func)
        {
            _mock = mock;
            _func = func;

            _mock.Verify(func, global::Moq.Times.AtLeastOnce());
        }

        #region IMethodCallOccurance Members

        public void Times(int numberOfTimesTheMethodShouldHaveBeenCalled)
        {
            _mock.Verify(_func, global::Moq.Times.Exactly(numberOfTimesTheMethodShouldHaveBeenCalled));
        }

        public void OnlyOnce()
        {
            _mock.Verify(_func, global::Moq.Times.Once());
        }

        public void Twice()
        {
            Times(2);
        }

        #endregion
    }
}