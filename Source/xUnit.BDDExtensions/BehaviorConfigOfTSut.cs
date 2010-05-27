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
namespace Xunit
{
    /// <summary>
    /// A base class for behavior configurations with a focus on
    /// SUT preparations.
    /// </summary>
    /// <typeparam name="TSut">
    /// Specifies the type of the sut.
    /// </typeparam>
    public abstract class BehaviorConfig<TSut> : IBehaviorConfig
    {
        /// <summary>
        /// Uses the accessor specified by <paramref name="accessor"/> in order to configure some behavior on dependencies
        /// of the system under test. This is called before the system under test has been created.
        /// </summary>
        /// <param name="accessor">
        /// Specifies the accessor for accessing dependencies of the SUT.
        /// </param>
        public virtual void EstablishContext(IDependencyAccessor accessor)
        {
        }

        /// <summary>
        /// Does some preparation of the sut itself (e.g. adding some elements to container like structures)
        /// </summary>
        /// <param name="sut">
        /// Specifies the sut.
        /// </param>
        public void PrepareSut(object sut)
        {
            OnPrepareSut((TSut) sut);
        }

        /// <summary>
        /// Is called in order to prepare some state in the SUT.
        /// </summary>
        /// <param name="sut">
        /// Specifies the system under test.
        /// </param>
        protected virtual void OnPrepareSut(TSut sut)
        {            
        }
    }
}