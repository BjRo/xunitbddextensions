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

using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Xunit.Internal;

namespace Xunit
{
    public static class MockedRequestContextExtensions
    {
        private class FakeViewDataContainer : IViewDataContainer
        {
            public FakeViewDataContainer()
            {
                ViewData = new ViewDataDictionary();
            }

            public ViewDataDictionary ViewData { get; set; }
        }

        /// <summary>
        /// Creates the AntiForgeryToken in the MockedRequestContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns>IMockedRequestContext for chaining</returns>
        public static IMockedRequestContext AntiForgeryToken(this IMockedRequestContext context)
        {
            var httpRequest = new HttpRequest("/default.aspx", "http://localhost/default.aspx", "?a=1");
            httpRequest.Browser=new HttpBrowserCapabilities();
            HttpContext.Current = new HttpContext(httpRequest,new HttpResponse(TextWriter.Null));
            var viewContext = new ViewContext {HttpContext = context.Context};
            var htmlHelper = new HtmlHelper(viewContext, Core.MockingEngine.Stub<IViewDataContainer>());
            var str = htmlHelper.AntiForgeryToken().ToHtmlString();
            var name = GetXmlAttributeValue(str, "name");
            var value = GetXmlAttributeValue(str, "value");
            context.Request.Form.Add(name, value);
            return context;
        }

        private static string GetXmlAttributeValue(string xml, string name)
        {
            var element = XElement.Load(new StringReader(xml));

            return element.Attributes().Where(a => a.Name == name).Single().Value;
        }

        /// <summary>
        /// Serialize all public properties of a model instance to the FormCollection
        /// of the IMockedRequestContext.Request
        /// </summary>
        /// <param name="context"></param>
        /// <param name="instance"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static IMockedRequestContext SerializeModelToForm(
            this IMockedRequestContext context, object instance,
            string parameterName)
        {
            var properties = instance.GetType().GetProperties();

            foreach (var propertyInfo in properties)
            {
                var elementName = propertyInfo.Name;

                if (!string.IsNullOrEmpty(parameterName))
                {
                    elementName = parameterName + "." + elementName;
                }

                var value = propertyInfo.GetValue(instance, null) ?? "";
                context.Request.Form.Add(elementName, value.ToString());
            }

            return context;
        }

        /// <summary>
        /// Creates a IPrincipal which is in the given role. The created user ist
        /// stored in IMockedRequestContext.Context.User
        /// </summary>
        /// <param name="context"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public static IMockedRequestContext Role(this IMockedRequestContext context, string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                context.Context.User = new GenericPrincipal(new GenericIdentity(""), null);
            }
            else
            {
                context.Context.User = new GenericPrincipal(new GenericIdentity("AnUser"), new[] {role});
            }

            return context;
        }

        /// <summary>
        /// Set HttpMode in IMockedRequestContext.Request.HttpMode. This is used from the
        /// ControllerActionInvoker to selecte the Action which is invoked.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        public static IMockedRequestContext HttpMethod(this IMockedRequestContext context, string httpMethod)
        {
            context.SetValue("HttpMethod", httpMethod);
            SetHttpMethodCallback(context);
            return context;
        }

        private static void SetHttpMethodCallback(IMockedRequestContext context)
        {
            if (context.Request.HttpMethod != null)
            {
                return;
            }

            context.Request
                .WhenToldTo(requestBase => requestBase.HttpMethod)
                .Return(() => context.GetValue<string>("HttpMethod"));
        }
    }
}
