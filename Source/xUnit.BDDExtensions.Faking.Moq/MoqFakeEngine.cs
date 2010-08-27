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
using System.Linq.Expressions;
using Moq;

namespace Xunit.Faking.Moq
{
    /// <summary>
    ///   An implementation of <see cref = "IFakeEngine" />
    ///   using the Moq framework.
    /// </summary>
    public class MoqFakeEngine : IFakeEngine
    {
        #region IFakeEngine Members

        public object Stub(Type interfaceType)
        {
            var closedMockType = typeof (Mock<>).MakeGenericType(interfaceType);
            var objectProperty = closedMockType.GetProperty("Object", closedMockType);
            var instance = Activator.CreateInstance(closedMockType);
            return objectProperty.GetValue(instance, null);
        }

        public T PartialMock<T>(params object[] args) where T : class
        {
            var closedMockType = typeof (Mock<>).MakeGenericType(typeof (T));
            var callBaseProperty = closedMockType.GetProperty("CallBase", typeof (bool));
            var objectProperty = closedMockType.GetProperty("Object", typeof (T));
            var constructor = closedMockType.GetConstructor(new[]
            {
                typeof (object[])
            });
            var instance = constructor.Invoke(new[]
            {
                args
            });
            callBaseProperty.SetValue(instance, true, null);
            return objectProperty.GetValue(instance, null) as T;
        }

        public IQueryOptions<TReturnValue> SetUpQueryBehaviorFor<TDependency, TReturnValue>(
            TDependency dependency,
            Expression<Func<TDependency, TReturnValue>> func) where TDependency : class
        {
            var mock = Mock.Get(dependency);

            return new MoqQueryOptions<TDependency, TReturnValue>(mock.Setup(func));
        }

        public ICommandOptions SetUpCommandBehaviorFor<TDependency>(
            TDependency fake,
            Expression<Action<TDependency>> func) where TDependency : class
        {
            var mock = Mock.Get(fake);

            return new MoqCommandOptions<TDependency>(mock.Setup(func));
        }

        public void VerifyBehaviorWasNotExecuted<TDependency>(
            TDependency fake, 
            Expression<Action<TDependency>> func) where TDependency : class
        {
            var mock = Mock.Get(fake);

            mock.Verify(func, Times.Never());
        }

        public IMethodCallOccurance VerifyBehaviorWasExecuted<TDependency>(
            TDependency fake, 
            Expression<Action<TDependency>> func) where TDependency : class
        {
            var mock = Mock.Get(fake);

            return new MoqMethodCallOccurance<TDependency>(mock, func);
        }

        #endregion
    }
}