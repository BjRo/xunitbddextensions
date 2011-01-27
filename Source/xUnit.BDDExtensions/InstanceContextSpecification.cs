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
using System.Collections.Generic;
using Xunit.Internal;

namespace Xunit
{
    /// <summary>
    ///   Context specification type which uses a combination of inversion-of-control and a fake framework
    ///   to automatically create the system under test using constructor injection. All fakable ctor dependencies
    ///   will be created by the fake framework.
    /// </summary>
    /// <typeparam name = "TSystemUnderTest">Specifies the type of the system under test.</typeparam>
    [RunWith(typeof (XbxRunner))]
    public abstract class InstanceContextSpecification<TSystemUnderTest> :
        IContextSpecification,
        IFakeAccessor
        where TSystemUnderTest : class
    {
        private readonly AutoFakeContainer<TSystemUnderTest> _autoFakeContainer =
            new AutoFakeContainer<TSystemUnderTest>();

        private readonly List<IBehaviorConfig> _behaviors = new List<IBehaviorConfig>();

        /// <summary>
        ///   Gets the system under test. This is the actual class under test.
        /// </summary>
        protected static TSystemUnderTest Sut { get; private set; }

        #region IContextSpecification Members

        /// <summary>
        ///   Initializes the specification class.
        /// </summary>
        void IContextSpecification.InitializeContext()
        {
            EstablishContext();
            Sut = CreateSut();
            _behaviors.ForEach(x => x.PrepareSut(Sut));
            PrepareSut();
            Because();
        }

        /// <summary>
        ///   Cleans up the specification class.
        /// </summary>
        void IContextSpecification.CleanupSpecification()
        {
            _behaviors.ForEach(x => x.Cleanup(Sut));
            _behaviors.Clear();
            AfterTheSpecification();
        }

        #endregion

        #region IFakeAccessor Members

        /// <summary>
        ///   Creates a fake of the type specified by <typeparamref name = "TInterfaceType" />.
        ///   This method reuses existing instances. If an instance of <typeparamref name = "TInterfaceType" />
        ///   was already requested it's returned here. (You can say this is kind of a singleton behavior)
        ///   Besides that, you can obtain a reference to automatically injected fakes with this
        ///   method.
        /// </summary>
        /// <typeparam name = "TInterfaceType">The type to create a fake for. (Should be an interface or an abstract class)</typeparam>
        /// <returns>
        ///   An instance implementing <see cref = "TInterfaceType" />.
        /// </returns>
        public TInterfaceType The<TInterfaceType>() where TInterfaceType : class
        {
            return _autoFakeContainer.Get<TInterfaceType>();
        }


        /// <summary>
        ///   Creates a fake of the type specified by <typeparamref name = "TInterfaceType" />.
        /// </summary>
        /// <typeparam name = "TInterfaceType">The type to create a fake for. (Should be an interface or an abstract class)</typeparam>
        /// <returns>
        ///   An newly created fake implementing <typeparamref name = "TInterfaceType" />.
        /// </returns>
        public TInterfaceType An<TInterfaceType>() where TInterfaceType : class
        {
            return _autoFakeContainer.Stub<TInterfaceType>();
        }

        /// <summary>
        ///   Creates a list containing 3 fake instances of the type specified
        ///   via <typeparamref name = "TInterfaceType" />.
        /// </summary>
        /// <typeparam name = "TInterfaceType">Specifies the item type of the list. This should be an interface or an abstract class.</typeparam>
        /// <returns>An <see cref = "IList{T}" />.</returns>
        public IList<TInterfaceType> Some<TInterfaceType>() where TInterfaceType : class
        {
            return _autoFakeContainer.CreateFakeCollectionOf<TInterfaceType>();
        }

        /// <summary>
        ///   Uses the instance supplied by <paramref name = "instance" /> during the
        ///   creation of the sut. The specified instance will be injected into the constructor.
        /// </summary>
        /// <typeparam name = "TInterfaceType">Specifies the interface type.</typeparam>
        /// <param name = "instance">Specifies the instance to be used for the specification.</param>
        public void Use<TInterfaceType>(TInterfaceType instance) where TInterfaceType : class
        {
            _autoFakeContainer.Inject(typeof (TInterfaceType), instance);
        }

        #endregion

        /// <summary>
        ///   Establishes the context for the specification. In AAA terms this 
        ///   method implements the Arange part.
        /// </summary>
        protected virtual void EstablishContext()
        {
        }

        /// <summary>
        ///   Creates the system under test.
        /// </summary>
        /// <returns>
        ///   A new instance of the system under test.
        /// </returns>
        protected virtual TSystemUnderTest CreateSut()
        {
            return _autoFakeContainer.CreateTarget();
        }

        /// <summary>
        ///   Can be overriden in order to do further configuration of the SUT after the SUT
        ///   has been created. This method is called after <see cref = "CreateSut" />
        ///   and before <see cref = "Because" />.
        /// </summary>
        protected virtual void PrepareSut()
        {
        }

        /// <summary>
        ///   Performs the actual action which should be observed by the specification. 
        ///   In AAA terms this method implements the Act part.
        /// </summary>
        protected abstract void Because();

        [Obsolete("EstablishContext and Because are now executed once for all observations which renders this method useless. Use AfterTheSpecification instead.")]
        protected virtual void AfterEachObservation()
        {
        }

        /// <summary>
        ///   Is called after each observation. Can be used for cleanup.
        /// </summary>
        protected virtual void AfterTheSpecification()
        {
        }

        /// <summary>
        ///   Configures the specification to execute the <see cref = "IBehaviorConfig" /> specified
        ///   by <typeparamref name = "TBehaviorConfig" /> before the action on the sut is executed (<see cref = "Because" />).
        /// </summary>
        /// <typeparam name = "TBehaviorConfig">
        ///   Specifies the type of the config to be executed.
        /// </typeparam>
        protected TBehaviorConfig With<TBehaviorConfig>() where TBehaviorConfig : IBehaviorConfig, new()
        {
            var behaviorConfig = new TBehaviorConfig();
            With(behaviorConfig);
            return behaviorConfig;
        }

        /// <summary>
        ///   Configures the specification to execute the <see cref = "IBehaviorConfig" /> specified
        ///   by <paramref name = "behaviorConfig" /> before the action on the sut is executed (<see cref = "Because" />).
        /// </summary>
        /// <param name = "behaviorConfig">
        ///   Specifies the behavior config to be executed.
        /// </param>
        protected void With(IBehaviorConfig behaviorConfig)
        {
            Guard.AgainstArgumentNull(behaviorConfig, "behaviorConfig");

            _behaviors.Add(behaviorConfig);

            behaviorConfig.EstablishContext(this);
        }
    }
}
