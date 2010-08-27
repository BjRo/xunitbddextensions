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
using System.Collections.Generic;
using System.Linq;
using Xunit.Sdk;

namespace Xunit.Internal
{
    public class XbxRunner : ITestClassCommand
    {
        private readonly TestCommandFactory _commandFactory = new TestCommandFactory();
        private IContextSpecification _contextSpec;
        private Exception _initializationException;
        private IEnumerable<IMethodInfo> _observationMethods;
        private Random _randomizer = new Random();
        private ITypeInfo _typeUnderTest;

        public Random Randomizer
        {
            get { return _randomizer; }
            set { _randomizer = value; }
        }

        #region ITestClassCommand Members

        public int ChooseNextTest(ICollection<IMethodInfo> testsLeftToRun)
        {
            return _randomizer.Next(testsLeftToRun.Count);
        }

        public Exception ClassStart()
        {
            try
            {
                _observationMethods = EnumerateTestMethods();

                Bootstrap();

                _contextSpec = (IContextSpecification)Activator.CreateInstance(_typeUnderTest.Type);
                _contextSpec.InitializeContext();
            }
            catch (Exception exception)
            {
                _initializationException = exception;
            }

            return null;
        }

        public Exception ClassFinish()
        {
            try
            {
                if (_contextSpec != null)
                {
                    _contextSpec.CleanupSpecification();
                }

                return null;
            }
            catch (Exception exception)
            {
                return exception;
            }
        }

        public IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo testMethod)
        {
            if (_initializationException != null)
            {
                yield return new DelayedExceptionCommand(_initializationException, testMethod);
                yield break;
            }

            foreach (var cmd in _commandFactory.CreateTestCommands(testMethod))
            {
                yield return cmd;
            }
        }

        public IEnumerable<IMethodInfo> EnumerateTestMethods()
        {
            return _observationMethods ?? (_observationMethods = TypeUtility.GetTestMethods(TypeUnderTest));
        }

        public bool IsTestMethod(IMethodInfo testMethod)
        {
            return MethodUtility.IsTest(testMethod);
        }

        public object ObjectUnderTest
        {
            get { return _contextSpec; }
        }

        public ITypeInfo TypeUnderTest
        {
            get { return _typeUnderTest; }
            set { _typeUnderTest = value; }
        }

        #endregion

        private void Bootstrap()
        {
            var currentSpecType = TypeUnderTest.Type;
            var currentSpecAssembly = currentSpecType.Assembly;

            currentSpecType
                .GetCustomAttributes(typeof(RunnerConfigurationAttribute), false)
                .AlternativeIfNullOrEmpty(() => currentSpecAssembly.GetCustomAttributes(typeof(RunnerConfigurationAttribute), true))
                .Cast<RunnerConfigurationAttribute>()
                .FirstOrCustomDefaultValue(RunnerConfigurationAttribute.DefaultConfiguration)
                .Configure(RunnerConfiguration.Instance);
        }
    }
}