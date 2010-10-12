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
    /// A custom exception which can be used in observation methods
    /// to mark them as currently not implemented. Those methods
    /// will automatically be skipped and ignored by the fx.
    /// </summary>
    public class ObservationNotImplementedException : ApplicationException
    {
    }
}