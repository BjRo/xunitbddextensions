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
namespace Xunit
{
    /// <summary>
    ///   Interface used by the <see cref = "SpecificationCommand" /> to execute logic before
    ///   and after the execution of an xUnit.net test method.
    /// </summary>
    public interface ISpecification
    {
        /// <summary>
        ///   Initializes the specification class.
        /// </summary>
        void Initialize();

        /// <summary>
        ///   Cleans up the specification class.
        /// </summary>
        void Cleanup();
    }
}