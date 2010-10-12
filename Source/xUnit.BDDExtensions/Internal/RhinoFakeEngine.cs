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
            TDependency fake,
            Expression<Func<TDependency, TReturnValue>> func) where TDependency : class
        {
            var compiledFunction = func.Compile();

            return new RhinoQueryOptions<TReturnValue>(fake.Stub(f => compiledFunction(f)));
        }

        public ICommandOptions SetUpCommandBehaviorFor<TFake>(
            TFake fake,
            Expression<Action<TFake>> func) where TFake : class
        {
            var compiledFunction = func.Compile();

            return new RhinoCommandOptions(fake.Stub(compiledFunction));
        }

        public void VerifyBehaviorWasNotExecuted<TFake>(
            TFake fake,
            Expression<Action<TFake>> func) where TFake : class
        {
            var compiledFunction = func.Compile();

            fake.AssertWasNotCalled(compiledFunction);
        }

        public IMethodCallOccurance VerifyBehaviorWasExecuted<TFake>(
            TFake fake, 
            Expression<Action<TFake>> func) where TFake : class 
        {
            var compiledFunction = func.Compile();

            return new RhinoMethodCallOccurance<TFake>(fake, compiledFunction);
        }

        public IEventRaiser CreateEventRaiser<TFake>(
            TFake fake, 
            Action<TFake> assignement) where TFake : class
        {
            var eventRaiser = fake.Stub(assignement).IgnoreArguments().GetEventRaiser();

            return new RhinoEventRaiser(eventRaiser);
        }

        #endregion
    }
}