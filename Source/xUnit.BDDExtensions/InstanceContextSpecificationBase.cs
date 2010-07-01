// Copyright 2010 Björn Rochel - http://www.bjro.de/ 
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
using System.Collections.Generic;
using Xunit.Internal;

namespace Xunit
{
    /// <summary>
    /// Specification that contains a hook for newing up the system under test after the context
    /// has been established.
    /// </summary>
    /// <typeparam name="TSystemUnderTest">
    /// Specifies the type of the system under test.
    /// </typeparam>
    public abstract class InstanceContextSpecificationBase<TSystemUnderTest> : ISpecification, IDependencyAccessor where TSystemUnderTest : class
    {
        private readonly AutoMockingContainer<TSystemUnderTest> _autoMockingContainer;
        private readonly List<IBehaviorConfig> _behaviors = new List<IBehaviorConfig>();

        /// <summary>
        /// Creats a new instance of the <see cref="InstanceContextSpecificationBase{TSystemUnderTest}"/>.
        /// </summary>
        /// <param name="mockFactory">
        /// The auto stub container.
        /// </param>
        protected InstanceContextSpecificationBase(IMockFactory mockFactory)
        {
            Guard.AgainstArgumentNull(mockFactory, "mockFactory");

            _autoMockingContainer = new AutoMockingContainer<TSystemUnderTest>(mockFactory);
        }

        /// <summary>
        /// Gets the system under test. This is the actual class under test.
        /// </summary>
        protected TSystemUnderTest Sut { get; private set; }

        /// <summary>
        /// Creates a dependency of the type specified by <typeparamref name="TInterfaceType"/>.
        /// This method reuses existing instances. If an instance of <typeparamref name="TInterfaceType"/>
        /// was already requested it's returned here. (You can say this is kind of a singleton behavior)
        /// 
        /// Besides that, you can obtain a reference to automatically injected stubs with this 
        /// method.
        /// </summary>
        /// <typeparam name="TInterfaceType">
        /// The type to create a dependency for. (Should be an interface)
        /// </typeparam>
        /// <returns>
        /// An instance implementing <see cref="TInterfaceType"/>.
        /// </returns>
        public TInterfaceType The<TInterfaceType>() where TInterfaceType : class
        {
            return _autoMockingContainer.Service<TInterfaceType>();
        }

        /// <summary>
        /// Creates a dependency of the type specified by <typeparamref name="TInterfaceType"/>.
        /// </summary>
        /// <typeparam name="TInterfaceType">
        /// The type to create a dependency for. (Should be an interface)
        /// </typeparam>
        /// <returns>
        /// An newly created instance implementing <typeparamref name="TInterfaceType"/>.
        /// </returns>
        public TInterfaceType An<TInterfaceType>() where TInterfaceType : class
        {
            return _autoMockingContainer.Stub<TInterfaceType>();
        }

        /// <summary>
        /// Creates a list of dependencies of the type specified by <typeparamref name="TInterfaceType"/>.
        /// </summary>
        /// <typeparam name="TInterfaceType">
        /// Specifies the dependency type. (Should be an interface).
        /// </typeparam>
        /// <returns>
        /// An newly created instance implementing <typeparamref name="TInterfaceType"/>.
        /// </returns>
        public IList<TInterfaceType> Some<TInterfaceType>() where TInterfaceType : class
        {
            return _autoMockingContainer.CreateStubCollectionOf<TInterfaceType>();
        }

        /// <summary>
        /// Uses the instance supplied by <paramref name="instance"/> during the 
        /// creation of the sut. The specified instance will be injected into the constructor.
        /// </summary>
        /// <typeparam name="TInterfaceType">
        /// Specifies the interface type.
        /// </typeparam>
        /// <param name="instance">
        /// Specifies the instance to be used for the specification.
        /// </param>
        public void Use<TInterfaceType>(TInterfaceType instance) where TInterfaceType : class
        {
            _autoMockingContainer.Inject(typeof(TInterfaceType), instance);
        }

        /// <summary>
        /// Initializes the specification class.
        /// </summary>
        void ISpecification.Initialize()
        {
            EstablishContext();
            _behaviors.ForEach(x => x.EstablishContext(this));
            Sut = CreateSut();
            _behaviors.ForEach(x => x.PrepareSut(Sut));
            PrepareSut();
            Because();
        }

        /// <summary>
        /// Cleans up the specification class.
        /// </summary>
        void ISpecification.Cleanup()
        {
            _behaviors.Clear();
            AfterEachObservation();
        }

        /// <summary>
        /// Establishes the context for the specification. In AAA terms this 
        /// method implements the Arange part.
        /// </summary>
        protected virtual void EstablishContext()
        {
        }

        /// <summary>
        /// Creates the system under test.
        /// </summary>
        /// <returns>
        /// A new instance of the system under test.
        /// </returns>
        protected virtual TSystemUnderTest CreateSut()
        {
            return _autoMockingContainer.ClassUnderTest;
        }

        /// <summary>
        /// Can be overriden in order to do further configuration of the SUT after the SUT
        /// has been created. This method is called after <see cref="CreateSut"/>
        /// and before <see cref="Because"/>.
        /// </summary>
        protected virtual void PrepareSut()
        {
        }

        /// <summary>
        /// Performs the actual action which should be observed by the specification. 
        /// In AAA terms this method implements the Act part.
        /// </summary>
        protected abstract void Because();

        /// <summary>
        /// Is called after each observation. Can be used for cleanup.
        /// </summary>
        protected virtual void AfterEachObservation()
        {
        }

        /// <summary>
        /// Configures the specification to execute the <see cref="IBehaviorConfig"/> specified
        /// by <typeparamref name="TBehaviorConfig"/> before the action on the sut is executed (<see cref="Because"/>).
        /// </summary>
        /// <typeparam name="TBehaviorConfig">
        /// Specifies the type of the config to be executed.
        /// </typeparam>
        protected void With<TBehaviorConfig>() where TBehaviorConfig : IBehaviorConfig, new()
        {
            With(new TBehaviorConfig());
        }

        /// <summary>
        /// Configures the specification to execute the <see cref="IBehaviorConfig"/> specified
        /// by <paramref name="behaviorConfig"/> before the action on the sut is executed (<see cref="Because"/>).
        /// </summary>
        /// <param name="behaviorConfig">
        /// Specifies the behavior config to be executed.
        /// </param>
        protected void With(IBehaviorConfig behaviorConfig)
        {
            Guard.AgainstArgumentNull(behaviorConfig, "behaviorConfig");

            _behaviors.Add(behaviorConfig);
        }
    }
}