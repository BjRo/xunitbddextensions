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

namespace Xunit
{
    /// <summary>
    /// Interface for detail configuration used by <see cref="FakeApi.WasToldTo{T}"/>.
    /// </summary>
    public interface IMethodCallOccurance
    {
        /// <summary>
        ///   Specifies that the behavior on the fake should be executed several times. <paramref name = "numberOfTimesTheMethodShouldHaveBeenCalled" />
        ///   specifies exactly how often.
        /// </summary>
        /// <param name = "numberOfTimesTheMethodShouldHaveBeenCalled">
        ///   The number of times the behavior should have been executed.
        /// </param>
        void Times(int numberOfTimesTheMethodShouldHaveBeenCalled);

        /// <summary>
        ///   Specifies that the behavior on the fake should only be executed once.
        /// </summary>
        void OnlyOnce();

        /// <summary>
        ///   Specifies that the behavior on the fake should be called twice.
        /// </summary>
        void Twice();
    }
}