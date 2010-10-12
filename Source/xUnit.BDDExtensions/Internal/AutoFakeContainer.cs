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
using StructureMap.AutoMocking;

namespace Xunit.Internal
{
    public sealed class AutoFakeContainer<TTargetClass> :
        AutoMocker<TTargetClass>,
        ServiceLocator,
        IFakeEngine where TTargetClass : class
    {
        private readonly IFakeEngine _fakeEngine;

        public AutoFakeContainer() : this(RunnerConfiguration.Instance)
        {
        }

        public AutoFakeContainer(IRunnerConfiguration runnerConfiguration)
        {
            _fakeEngine = runnerConfiguration.FakeEngine;
            _serviceLocator = this;
            _container = new AutoMockedContainer(this);
        }

        #region IFakeEngine Members

        public object Stub(Type interfaceType)
        {
            return _fakeEngine.Stub(interfaceType);
        }

        public IQueryOptions<TReturnValue> SetUpQueryBehaviorFor<TFake, TReturnValue>(
            TFake fake,
            Expression<Func<TFake, TReturnValue>> func) where TFake : class
        {
            return _fakeEngine.SetUpQueryBehaviorFor(fake, func);
        }

        public ICommandOptions SetUpCommandBehaviorFor<TFake>(
            TFake fake,
            Expression<Action<TFake>> func) where TFake : class
        {
            return _fakeEngine.SetUpCommandBehaviorFor(fake, func);
        }

        public void VerifyBehaviorWasNotExecuted<TFake>(
            TFake fake, 
            Expression<Action<TFake>> func) where TFake : class
        {
            _fakeEngine.VerifyBehaviorWasNotExecuted(fake, func);
        }

        public IMethodCallOccurance VerifyBehaviorWasExecuted<TFake>(
            TFake fake, 
            Expression<Action<TFake>> func) where TFake : class
        {
            return _fakeEngine.VerifyBehaviorWasExecuted(fake, func);
        }

        public IEventRaiser CreateEventRaiser<TFake>(TFake fake, Action<TFake> assignement) where TFake : class
        {
            return _fakeEngine.CreateEventRaiser(fake, assignement);
        }

        #endregion

        #region ServiceLocator Members

        public T Service<T>() where T : class
        {
            return (T) Service(typeof (T));
        }

        public object Service(Type serviceType)
        {
            return _fakeEngine.Stub(serviceType);
        }

        public T PartialMock<T>(params object[] args) where T : class
        {
            return _fakeEngine.PartialMock<T>(args);
        }

        #endregion

        public T Stub<T>() where T : class
        {
            return (T)Stub(typeof(T));
        }
    }
}