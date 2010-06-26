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
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Xunit
{
    public class ControllerInvokerBuilder
    {
        private Expression actionExpression;
        private Controller controller;
        private readonly RequestContextBuilder contextBuilder;
        private readonly ITestActionInvoker actionInvoker;

        public ControllerInvokerBuilder(IDependencyAccessor dependencyAccessor, ITestActionInvoker actionInvoker)
        {
            contextBuilder=new RequestContextBuilder(dependencyAccessor);
            this.actionInvoker = actionInvoker;
        }

        public IMockedRequestContext RequestContext
        {
            get { return contextBuilder; }
        }

        public ControllerInvokerBuilder Action<T,TResult>(Expression<Func<T, TResult>> expression) where T:Controller
        {
            actionExpression = expression;
            return this;
        }

        public static implicit operator ActionResult(ControllerInvokerBuilder builder)
        {
            return (ActionResult) builder.Execute();
        }

        public object Execute()
        {         
            ConvertParameterToFormCollection();

            var controllerContext = new ControllerContext(contextBuilder, controller);
            controller.ControllerContext = controllerContext;
            
            var actionName = MvcExpressionHelper.GetMemberName(actionExpression);

            contextBuilder.RouteData.Values["action"] = actionName;
            contextBuilder.RouteData.Values["controller"] = GetControllerName();

            actionInvoker.InvokeAction(controllerContext, actionName);
            return actionInvoker.Result;
        }

        private string GetControllerName()
        {
            var name = controller.GetType().Name;
            return name.Substring(0, name.Length - "Controller".Length);
        }

        private void ConvertParameterToFormCollection()
        {
            var parameterNames = MvcExpressionHelper.GetParameterNames(actionExpression);

            foreach (var parameterName in parameterNames)
            {
                contextBuilder.Model(MvcExpressionHelper.GetParameterValue(actionExpression, parameterName), parameterName);
            }
        }

        public ControllerInvokerBuilder Role(string role)
        {
            contextBuilder.Role(role);
            return this;
        }

        public ControllerInvokerBuilder Controller(Controller controllerUnderTest)
        {
            controller = controllerUnderTest;
            return this;
        }
    }
}