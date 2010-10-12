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
using Xunit.Internal;

namespace Xunit
{
    /// <summary>
    ///   This attribute is the configuration endpoint to xUnit.BDDExtensions.
    ///   It can be used on assembly level (for all specifications in an assembly) or on class level (for each spec).
    ///   Combinations of both are possible, too. In that case the class level attribute takes precedence.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = false)]
    public class RunnerConfigurationAttribute : Attribute
    {
        /// <summary>
        ///   Gets or sets the type of the fake engine which is used.
        /// </summary>
        public Type FakeEngineType { get; set; }

        /// <summary>
        /// Returns a new instance representing a default configuration.
        /// </summary>
        public static RunnerConfigurationAttribute DefaultConfiguration
        {
            get
            {
                return new RunnerConfigurationAttribute
                {
                    FakeEngineType = typeof (RhinoFakeEngine)
                };
            }
        }

        /// <summary>
        /// Is called by the runner to configure the underlying framework.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public void Configure(IRunnerConfiguration configuration)
        {
            Guard.AgainstArgumentNull(configuration, "configuration");

            OnConfigure(configuration);

            if (RunnerConfiguration.FakeEngine == null)
            {
                throw new InvalidOperationException("No FakeEngine configured");
            }
        }

        /// <summary>
        /// Hook method which can be overridded to apply custom configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        protected virtual void OnConfigure(IRunnerConfiguration configuration)
        {
            configuration.FakeEngineIs(FakeEngineType);
        }
    }
}