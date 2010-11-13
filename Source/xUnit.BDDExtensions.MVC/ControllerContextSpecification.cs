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
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Xunit
{
    /// <summary>
    /// A Specification Class you can use for you ControllerAction Tests.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ControllerContextSpecification<T> : InstanceContextSpecification<T>
        where T : Controller
    {
        protected ControllerContextSpecification()
        {
        }

        private ControllerActionInvokerBuilder _invokerBuilder;

        protected override void EstablishContext()
        {
            base.EstablishContext();

            _invokerBuilder = CreateControllerActionInvokerBuilder();
            PrepareRequestContext(_invokerBuilder.RequestContext);
        }

        /// <summary>
        /// Overide this if you need to modify your Mocked Request Context. You
        /// can add User-Roles for Authentication, can add the AntiForgeryToken, can change
        /// the HttpMethod and do whatever you need.
        /// </summary>
        /// <param name="requestContext">A mocked RequestContext</param>
        protected virtual void PrepareRequestContext(IMockedRequestContext requestContext)
        {
        }

        /// <summary>
        /// Creates a ControllerActionInvokerBuilder which use a <see cref="ControllerActionInvoker"/> specific
        /// for running specifications. You need only override this if you habe a custom ControllerActionInvoker
        /// </summary>
        /// <returns></returns>
        protected virtual ControllerActionInvokerBuilder CreateControllerActionInvokerBuilder()
        {
            return new ControllerActionInvokerBuilder(this, new SpecificationControllerActionInvoker());
        }

        protected override void AfterTheSpecification()
        {
            base.AfterTheSpecification();
            MockedRequestContextValueStore.RemoveDictionary(_invokerBuilder.RequestContext);
        }

        /// <summary>
        /// Invokes a Controller-Action with all ActionFilters applied to them.
        /// It uses the given HttpMethod - default is GET. Also is serialize given parameters to the FormCollection
        /// so that ModelBinding and Validation can executed
        /// </summary>
        /// <remarks>
        /// If you wan't the modified the RequestContext, you have to override the <see cref="PrepareRequestContext"/>
        /// to add the AntiForgeryToken or a UserRole for the Action.
        /// </remarks>
        /// <see cref="InvokePostAction"/>
        /// <typeparam name="TResult">Result Type of the Action</typeparam>
        /// <param name="controllerAction">The expression with of the Controller-Action</param>
        /// <returns>The result of the Action</returns>
        protected TResult InvokeAction<TResult>(Expression<Func<T, TResult>> controllerAction)
        {
            _invokerBuilder.SetControllerUnderTest(Sut).Action(controllerAction);
            return (TResult) _invokerBuilder.InvokeAction();
        }

        /// <summary>
        /// Invokes a Controller-Action with all ActionFilters applied to them.
        /// It uses the POST-HttpMethod also is serialize given parameters to the FormCollection
        /// so that ModelBinding and Validation can executed.
        /// </summary>
        /// <remarks>
        /// If you wan't the modified the RequestContext, you have to override the <see cref="PrepareRequestContext"/>
        /// to add the AntiForgeryToken or a UserRole for the Action.
        /// </remarks>
        /// <see cref="InvokeAction(Expression)" />
        /// <typeparam name="TResult">Result Type of the Action</typeparam>
        /// <param name="controllerAction">The expression with of the Controller-Action</param>
        /// <returns>The result of the Action</returns>
        protected TResult InvokePostAction<TResult>(Expression<Func<T, TResult>> controllerAction)
        {
            _invokerBuilder.SetControllerUnderTest(Sut).Action(controllerAction).RequestContext.HttpMethod("POST");
            return (TResult) _invokerBuilder.InvokeAction();
        }
    }
}
