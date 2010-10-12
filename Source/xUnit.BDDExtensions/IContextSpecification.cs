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
using Xunit.Internal;

namespace Xunit
{
    /// <summary>
    ///   Interface used by the <see cref = "XbxRunner" /> to execute <see cref="StaticContextSpecification"/>
    ///   and <see cref="InstanceContextSpecification{T}"/> derivates.
    /// </summary>
    public interface IContextSpecification
    {
        /// <summary>
        /// Is used to intialize the context specification. This method is called once before
        /// all the methods marked with the observation methods are run.
        /// </summary>
        void InitializeContext();

        /// <summary>
        /// Is called after all observation methods are run.
        /// </summary>
        void CleanupSpecification();
    }
}