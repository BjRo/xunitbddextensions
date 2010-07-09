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

using System.Collections.Specialized;
using System.Web;
using System.Web.Routing;
using Xunit.Internal;

namespace Xunit
{
    /// <summary>
    /// Builds a mocked Request Context which
    /// there are several extensions method with daily needs of
    /// an ASP.NET MVC Developer.
    /// <see cref="IMockedRequestContext"/>
    /// </summary>
    public class RequestContextBuilder : IMockedRequestContext
    {
        private readonly IDependencyAccessor _dependencyAccessor;

        private HttpContextBase _context;

        public HttpContextBase Context
        {
            get
            {
                if (_context == null)
                {
                    EnsureMockedContext();
                }

                return _context;
            }
        }

        public HttpRequestBase Request
        {
            get { return Context.Request; }
        }

        public HttpServerUtilityBase Server
        {
            get { return Context.Server; }
        }

        public HttpResponseBase Response
        {
            get { return Context.Response; }
        }

        public RouteData RouteData { get; private set; }

        private T An<T>() where T : class
        {
            return _dependencyAccessor.An<T>();
        }

        private T The<T>() where T : class
        {
            return _dependencyAccessor.The<T>();
        }

        public RequestContextBuilder(IDependencyAccessor dependencyAccessor)
        {
            _dependencyAccessor = dependencyAccessor;
            EnsureMockedContext();
        }

        private void EnsureMockedContext()
        {
            if (_context != null)
            {
                return;
            }
            CreateHttpContext();
            CreateCollectionsForContext();
            CreateRouteData();
            this.Role("");
            this.HttpMethod("GET");
        }

        private void CreateHttpContext()
        {
            _context = The<HttpContextBase>();

            _context.WhenToldTo(s => s.Request).Return(The<HttpRequestBase>());
            _context.WhenToldTo(s => s.Server).Return(The<FakeHttpServerUtility>());
            _context.WhenToldTo(s => s.Response).Return(The<HttpResponseBase>());

            Request.WhenToldTo(r => r.Files).Return(The<HttpFileCollectionBase>());

            Response.WhenToldTo(responseBase => responseBase.Cache).Return(The<HttpCachePolicyBase>());
            Request.WhenToldTo(requestbase => requestbase.ApplicationPath).Return("/");
        }

        private void CreateCollectionsForContext()
        {
            Request.WhenToldTo(requestBase => requestBase.Headers).Return(new NameValueCollection());
            Request.WhenToldTo(requestBase => requestBase.Form).Return(new NameValueCollection());
            Request.WhenToldTo(requestBase => requestBase.QueryString).Return(new NameValueCollection());

            var cookiesCollection = new HttpCookieCollection();
            Request.WhenToldTo(requestBase => requestBase.Cookies).Return(cookiesCollection);
            Response.WhenToldTo(responseBase => responseBase.Cookies).Return(cookiesCollection);
        }

        public static implicit operator RequestContext(RequestContextBuilder builder)
        {
            return new RequestContext(builder.Context, builder.RouteData);
        }

        private void CreateRouteData()
        {
            RouteData = new RouteData();
            RouteData.Values["action"] = "a";
            RouteData.Values["controller"] = "a";
        }
    }
}
