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
using Rhino.Mocks;

namespace Xunit.Internal
{
    public class RhinoFakeEngine : IFakeEngine
    {
        #region IFakeEngine Members

        public object Stub(Type interfaceType)
        {
            var stub = MockRepository.GenerateStub(interfaceType);
            stub.Replay();
            return stub;
        }

        public T PartialMock<T>(params object[] args) where T : class
        {
            var mock = MockRepository.GenerateMock<T>(args);
            mock.Replay();
            return mock;
        }

        public IQueryOptions<TReturnValue> SetUpQueryBehaviorFor<TDependency, TReturnValue>(
            TDependency dependency,
            Expression<Func<TDependency, TReturnValue>> func) where TDependency : class
        {
            var compiledFunction = func.Compile();

            return new RhinoQueryOptions<TReturnValue>(dependency.Stub(f => compiledFunction(f)));
        }

        public ICommandOptions SetUpCommandBehaviorFor<TDependency>(
            TDependency fake,
            Expression<Action<TDependency>> func) where TDependency : class
        {
            var compiledFunction = func.Compile();

            return new RhinoCommandOptions(fake.Stub(compiledFunction));
        }

        public void VerifyBehaviorWasNotExecuted<TDependency>(
            TDependency fake,
            Expression<Action<TDependency>> func) where TDependency : class
        {
            var compiledFunction = func.Compile();

            fake.AssertWasNotCalled(compiledFunction);
        }

        public IMethodCallOccurance VerifyBehaviorWasExecuted<TDependency>(
            TDependency fake, 
            Expression<Action<TDependency>> func) where TDependency : class 
        {
            var compiledFunction = func.Compile();

            return new RhinoMethodCallOccurance<TDependency>(fake, compiledFunction);
        }

        #endregion
    }
}