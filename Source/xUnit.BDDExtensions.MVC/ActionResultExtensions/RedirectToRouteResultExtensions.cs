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
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Xunit.Internal;

namespace Xunit
{
    ///<summary>
    /// Several Extension Methods for Asserting RouteResult
    ///</summary>
    public static class RedirectToRouteResultExtensions
    {
        /// <summary>
        /// Assert that the Route 
        /// </summary>
        /// <typeparam name="TController"></typeparam>
        /// <param name="expected"></param>
        /// <param name="controllerAction"></param>
        public static void ShouldLinkTo<TController>(
            this Expression<Action<TController>> expected, 
            Expression<Action<TController>> controllerAction) where TController : Controller
        {
            Guard.AgainstArgumentNull(expected, "expected");
            Guard.AgainstArgumentNull(controllerAction, "controllerAction");

            var expectedDictionary = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression(expected);
            var actionDictionary = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression(controllerAction);
            actionDictionary.ShouldOnlyContain(expectedDictionary.ToArray());
        }

        /// <summary>
        /// Assert that the RedirectRouteResult goes to the expected Action
        /// </summary>
        /// <typeparam name="TController">Type of Controller</typeparam>
        /// <param name="actionResult">An ActionResult</param>
        /// <param name="controllerAction">Expreession which describes the Action</param>
        public static void ShouldRedirectTo<TController>(
            this ActionResult actionResult, 
            Expression<Action<TController>> controllerAction) where TController : Controller
        {
            Guard.AgainstArgumentNull(actionResult, "actionResult");
            Guard.AgainstArgumentNull(controllerAction, "controllerAction");

            ShouldRedirectTo((RedirectToRouteResult) actionResult, controllerAction);
        }

        /// <summary>
        /// Assert that the RedirectRouteResult goes to the expected Action
        /// </summary>
        /// <typeparam name="TController">Type of Controller</typeparam>
        /// <param name="redirectToRouteResult">A RedirectToRouteResult</param>
        /// <param name="controllerAction">Expreession which describes the Action</param>
        public static void ShouldRedirectTo<TController>(
            this RedirectToRouteResult redirectToRouteResult,
            Expression<Action<TController>> controllerAction) where TController : Controller
        {
            Guard.AgainstArgumentNull(redirectToRouteResult, "redirectToRouteResult");
            Guard.AgainstArgumentNull(controllerAction, "controllerAction");

            var actionDictionary = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression(controllerAction);
            actionDictionary.ShouldOnlyContain(redirectToRouteResult.RouteValues.ToArray());
        }
    }
}