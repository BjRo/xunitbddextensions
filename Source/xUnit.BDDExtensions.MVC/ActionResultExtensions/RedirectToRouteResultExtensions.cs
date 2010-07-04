// Copyright 2010 Albert Weinert - http://der-albert.com
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
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Xunit.Internal;

namespace Xunit
{
    public static class RedirectToRouteResultExtensions
    {
        public static void ShouldLinkTo<TController>(
            this Expression<Action<TController>> expected, 
            Expression<Action<TController>> controllerActionPointer) where TController : Controller
        {
            Guard.AgainstArgumentNull(expected, "expected");
            Guard.AgainstArgumentNull(controllerActionPointer, "controllerActionPointer");

            var expectedDictionary = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression(expected);
            var actionDictionary = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression(controllerActionPointer);
            actionDictionary.ShouldOnlyContain(expectedDictionary.ToArray());
        }

        public static void ShouldRouteTo<TController>(
            this ActionResult actionResult, 
            Expression<Action<TController>> controllerActionPointer) where TController : Controller
        {
            Guard.AgainstArgumentNull(actionResult, "actionResult");
            Guard.AgainstArgumentNull(controllerActionPointer, "controllerActionPointer");

            ShouldRouteTo((RedirectToRouteResult) actionResult, controllerActionPointer);
        }

        public static void ShouldRouteTo<TController>(
            this RedirectToRouteResult redirectToRouteResult,
            Expression<Action<TController>> controllerActionPointer) where TController : Controller
        {
            Guard.AgainstArgumentNull(redirectToRouteResult, "redirectToRouteResult");
            Guard.AgainstArgumentNull(controllerActionPointer, "controllerActionPointer");

            var actionDictionary = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression(controllerActionPointer);
            actionDictionary.ShouldOnlyContain(redirectToRouteResult.RouteValues.ToArray());
        }
    }
}