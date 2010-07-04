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
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Rhino.Mocks;
using Xunit.Internal;

namespace Xunit
{
    public static class MockedRequestContextExtensions
    {
        private static readonly InstanceDictionary InstanceDictionary = new InstanceDictionary();

        public static IMockedRequestContext AntiForgeryToken(this IMockedRequestContext context)
        {
            HttpContext.Current = new HttpContext(new HttpRequest("/", "http://localhost/", ""),
                                                  new HttpResponse(TextWriter.Null));
            var viewContext = new ViewContext {HttpContext = context.Context};
            var htmlHelper = new HtmlHelper(viewContext, MockRepository.GenerateStub<IViewDataContainer>());
            var str = htmlHelper.AntiForgeryToken().ToHtmlString();
            var name = GetValue(str, "name");
            var value = GetValue(str, "value");
            context.Request.Form.Add(name, value);
            return context;
        }

        private static string GetValue(string xml, string name)
        {
            var element = XElement.Load(new StringReader(xml));
            return element.Attributes().Where(a => a.Name == name).Single().Value;
        }

        public static IMockedRequestContext SerializeModelToForm(this IMockedRequestContext context, object model,
                                                                 string parameterName)
        {
            var properties = model.GetType().GetProperties();
            foreach (var propertyInfo in properties)
            {
                var formName = propertyInfo.Name;
                if (!string.IsNullOrEmpty(parameterName))
                {
                    formName = parameterName + "." + formName;
                }
                var value = propertyInfo.GetValue(model, null) ?? "";
                context.Request.Form.Add(formName, value.ToString());
            }
            return context;
        }

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

        public static IMockedRequestContext HttpMethod(this IMockedRequestContext context, string httpMethod)
        {
            InstanceDictionary.Set(context, "HttpMethod", httpMethod);
            SetHttpMethodCallback(context);
            return context;
        }

        private static void SetHttpMethodCallback(IMockedRequestContext context)
        {
            if (context.Request.HttpMethod != null)
                return;

            context.Request.Stub(requestBase => requestBase.HttpMethod)
                .WhenCalled(
                    invocation => invocation.ReturnValue = InstanceDictionary.Get<string>(context, "HttpMethod")
                )
                .Return(null).Repeat.Any();
        }
    }
}