// Copyright 2009 Bj�rn Rochel - http://www.bjro.de/ 
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
    /// Specification that contains a hook for newing up the system under test after the context
    /// has been established.
    /// </summary>
    /// <typeparam name="TSystemUnderTest">
    /// Specifies the type of the system under test.
    /// </typeparam>
    public abstract class InstanceContextSpecification<TSystemUnderTest> : InstanceContextSpecificationBase<TSystemUnderTest> where TSystemUnderTest : class
    {
        /// <summary>
        /// Creates a new instance of the <see cref="InstanceContextSpecification{TSystemUnderTest}"/>.
        /// </summary>
        protected InstanceContextSpecification() : base(new RhinoMocksFactory())
        {
        }
    }
}