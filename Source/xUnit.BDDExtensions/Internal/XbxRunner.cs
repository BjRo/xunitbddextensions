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
    /// <summary>
    ///   A specialized runner which executes the context first, then all methods
    ///   marked with the observation attribute and the cleans the context.
    /// </summary>
    public class XbxRunner : ITestClassCommand
    {
        private IContextSpecification _contextSpec;
        private Exception _initializationException;
        private IEnumerable<IMethodInfo> _observationMethods;
        private Random _randomizer = new Random();
        private ITypeInfo _typeUnderTest;

        /// <summary>
        ///   Gets or sets the randomizer.
        /// </summary>
        public Random Randomizer
        {
            get { return _randomizer; }
            set { _randomizer = value; }
        }

        #region ITestClassCommand Members

        /// <summary>
        ///   Allows the test class command to choose the next test to be run from the list of
        ///   tests that have not yet been run, thereby allowing it to choose the run order.
        /// </summary>
        /// <param name = "testsLeftToRun">The tests remaining to be run</param>
        /// <returns>The index of the test that should be run</returns>
        public int ChooseNextTest(ICollection<IMethodInfo> testsLeftToRun)
        {
            return _randomizer.Next(testsLeftToRun.Count);
        }

        /// <summary>
        ///   Execute actions to be run before any of the test methods of this test class are run.
        /// </summary>
        /// <returns>
        ///   Returns the <see cref = "T:System.Exception" /> thrown during execution, if any; null, otherwise
        /// </returns>
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

        /// <summary>
        ///   Execute actions to be run after all the test methods of this test class are run.
        /// </summary>
        /// <returns>
        ///   Returns the <see cref = "T:System.Exception" /> thrown during execution, if any; null, otherwise
        /// </returns>
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

        /// <summary>
        ///   Enumerates the test commands for a given test method in this test class.
        /// </summary>
        /// <param name = "testMethod">The method under test</param>
        /// <returns>
        ///   The test commands for the given test method
        /// </returns>
        public IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo testMethod)
        {
            if (_initializationException != null)
            {
                yield return new ExceptionCommand(_initializationException, testMethod);
            }

            //TODO: Implement Skip on complete spec.

            var skipReason = MethodUtility.GetSkipReason(testMethod);

            if (skipReason != null)
            {
                yield return new SkipCommand(testMethod, MethodUtility.GetDisplayName(testMethod), skipReason);
            }
            else
            {
                foreach (var testCommand in MethodUtility.GetTestCommands(testMethod))
                {
                    yield return testCommand;
                }
            }
        }

        /// <summary>
        ///   Enumerates the methods which are test methods in this test class.
        /// </summary>
        /// <returns>The test methods</returns>
        public IEnumerable<IMethodInfo> EnumerateTestMethods()
        {
            return _observationMethods ?? (_observationMethods = TypeUtility.GetTestMethods(TypeUnderTest));
        }

        /// <summary>
        ///   Determines if a given <see cref = "T:Xunit.Sdk.IMethodInfo" /> refers to a test method.
        /// </summary>
        /// <param name = "testMethod">The test method to validate</param>
        /// <returns>
        ///   True if the method is a test method; false, otherwise
        /// </returns>
        public bool IsTestMethod(IMethodInfo testMethod)
        {
            return MethodUtility.IsTest(testMethod);
        }

        /// <summary>
        ///   Gets the object instance that is under test. May return null if you wish
        ///   the test framework to create a new object instance for each test method.
        /// </summary>
        public object ObjectUnderTest
        {
            get { return _contextSpec; }
        }

        /// <summary>
        ///   Gets or sets the type that is being tested
        /// </summary>
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
                .GetCustomAttributes(typeof(XbxRunnerConfigurationAttribute), false)
                .AlternativeIfNullOrEmpty(() => currentSpecAssembly.GetCustomAttributes(typeof(XbxRunnerConfigurationAttribute), true))
                .Cast<XbxRunnerConfigurationAttribute>()
                .FirstOrCustomDefaultValue(XbxRunnerConfigurationAttribute.DefaultConfiguration)
                .Configure(RunnerConfiguration.Instance);
        }
    }
}