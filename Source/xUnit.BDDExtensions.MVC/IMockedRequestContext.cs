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

using System.Web;
using System.Web.Routing;

namespace Xunit
{
    ///<summary>
    /// The mocked Request Context, used by <see cref="ControllerActionInvokerBuilder"/>
    ///</summary>
    public interface IMockedRequestContext
    {
        ///<summary>
        /// The mocked HttpContext
        ///</summary>
        HttpContextBase Context { get; }

        ///<summary>
        /// The mocked HttpRequest
        ///</summary>
        HttpRequestBase Request { get; }

        ///<summary>
        /// The mocked HttpServerUtility
        ///</summary>
        HttpServerUtilityBase Server { get; }

        ///<summary>
        /// The mocked HttpResponse
        ///</summary>
        HttpResponseBase Response { get; }

        ///<summary>
        /// The RouteData
        ///</summary>
        RouteData RouteData { get; }
    }
}