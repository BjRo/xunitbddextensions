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
using System.Reflection;

namespace Xunit.Internal
{
    /// <summary>
    /// Analysis
    /// </summary>
    internal static class MvcExpressionHelper
    {
        public static string GetMemberName(Expression expression)
        {
            var memberExpression = GetMemberInfoFromExpression(GetLambdaBody(expression));
            return memberExpression.Name;
        }

        public static string[] GetParameterNames(Expression expression)
        {
            var methodCall = GetMethodCallExpression(expression);

            if (methodCall == null)
            {
                return new string[0];
            }

            return methodCall.Method.GetParameters().Select(p => p.Name).ToArray();
        }

        private static MemberInfo GetMemberInfoFromExpression(Expression expression)
        {
            if (expression is UnaryExpression)
            {
                return GetMemberInfoFromExpression(((UnaryExpression) expression).Operand);
            }

            if (expression is MemberExpression)
            {
                return ((MemberExpression) expression).Member;
            }

            if (expression is MethodCallExpression)
            {
                return ((MethodCallExpression) expression).Method;
            }

            throw new InvalidOperationException(string.Format("{0} not handled", expression.Type.Name));
        }

        private static MethodCallExpression GetMethodCallExpression(Expression expression)
        {
            if (expression is LambdaExpression)
            {
                expression = ((LambdaExpression) expression).Body;
            }

            if (expression is MethodCallExpression)
            {
                return ((MethodCallExpression) expression);
            }

            return null;
        }

        public static object GetParameterValue(Expression expression, string parameterName)
        {
            var methodCall = GetMethodCallExpression(expression);
            var names = GetParameterNames(methodCall);
            
            for (var i = 0; i < names.Length; i++)
            {
                var name = names[i];
            
                if (name == parameterName)
                {
                    return GetValue(methodCall, i);
                }
            }
            
            throw new InvalidOperationException("unknown parameter");
        }

        private static object GetValue(MethodCallExpression methodCall, int index)
        {
            var argument = methodCall.Arguments[index];
            
            if (argument is ConstantExpression)
            {
                return ((ConstantExpression)argument).Value;
            }
                
            return EvaluateExpression(argument);
        }

        private static object EvaluateExpression(Expression argument)
        {
            var lambda = Expression.Lambda(argument);
            return lambda.Compile().DynamicInvoke();
        }

        private static Expression GetLambdaBody(Expression expression)
        {
            return ((LambdaExpression) expression).Body;
        }
    }
}