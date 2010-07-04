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

namespace Xunit
{
    public static class RedirectToRouteResultExtensions
    {
        public static void ShouldLinkTo<T>(this Expression<Action<T>> expected, Expression<Action<T>> action)
            where T : Controller
        {
            var expectedDictionary = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression(expected);
            var actionDictionary = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression(action);
            actionDictionary.ShouldOnlyContain(expectedDictionary.ToArray());
        }

        public static void ShouldRouteTo<T>(this ActionResult actionResult, Expression<Action<T>> action)
            where T : Controller
        {
            ShouldRouteTo((RedirectToRouteResult) actionResult, action);
        }

        public static void ShouldRouteTo<T>(this RedirectToRouteResult redirectToRouteResult,
                                            Expression<Action<T>> action)
            where T : Controller
        {
            var actionDictionary = Microsoft.Web.Mvc.Internal.ExpressionHelper.GetRouteValuesFromExpression(action);
            actionDictionary.ShouldOnlyContain(redirectToRouteResult.RouteValues.ToArray());
        }
    }
}