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
    /// This abstraction represents a single event on a fake.
    /// </summary>
    public interface IEventRaiser
    {
        /// <summary>
        /// Raises the represented event with the specified arguments.
        /// </summary>
        /// <param name="sender">
        /// Specifies the sender.
        /// </param>
        /// <param name="e">
        /// Specifies event arguments.
        /// </param>
        void Raise(object sender, EventArgs e);

        /// <summary>
        /// Raises the represented event with the specified arguments.
        /// </summary>
        /// <param name="args">
        /// Specifies the arguments as an untyped object array.
        /// </param>
        void Raise(params object[] args);
    }
}