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

using System.Web.Mvc;

namespace Xunit
{
    /// <summary>
    /// Interface for SpecificationActionInvoker for Running test. Create
    /// your own if you have a custom ControllerActionInvoker.
    ///  </summary>
    /// <remarks>
    /// Derive from your ControllerActionInvoker, implement this interface
    /// and override InvokeActionResult(), within that you only safe the
    /// ActionResult and don't invoke the ActionResult.
    /// </remarks>
    public interface ISpecificationActionInvoker : IActionInvoker
    {
        ///<summary>
        /// The ActionResult which is return by the invoked action
        ///</summary>
        ActionResult Result { get; }
    }
}