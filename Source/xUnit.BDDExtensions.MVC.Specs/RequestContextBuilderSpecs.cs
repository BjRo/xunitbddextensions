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
using Xunit.Internal;

namespace Xunit.Specs
{
    [Concern(typeof (RequestContextBuilder))]
    public abstract class ConcernOfRequestContextBuilder : InstanceContextSpecification<RequestContextBuilder>
    {
        protected override RequestContextBuilder CreateSut()
        {
            return new RequestContextBuilder(this);
        }
    }

    public class When_building_a_requestcontext : ConcernOfRequestContextBuilder
    {
        protected override void Because()
        {
        }

        [Observation]
        public void Should_the_request_and_response_cookiecollection_be_the_same()
        {
            Sut.Request.Cookies.ShouldBeTheSame(Sut.Response.Cookies);
        }

        [Observation]
        public void Should_request_headers_a_unique_collection()
        {
            var collection = Sut.Request.Headers;
            Sut.Request.Form.ShouldNotBeTheSame(collection);
            Sut.Request.QueryString.ShouldNotBeTheSame(collection);
        }

        [Observation]
        public void Should_request_form_a_unique_collection()
        {
            var collection = Sut.Request.Form;
            Sut.Request.Headers.ShouldNotBeTheSame(collection);
            Sut.Request.QueryString.ShouldNotBeTheSame(collection);
        }

        [Observation]
        public void Should_request_querystring_a_unique_collection()
        {
            var collection = Sut.Request.QueryString;
            Sut.Request.Headers.ShouldNotBeTheSame(collection);
            Sut.Request.Form.ShouldNotBeTheSame(collection);
        }

        [Observation]
        public void Should_request_not_null()
        {
            Sut.Request.ShouldNotBeNull();
        }

        [Observation]
        public void Should_server_not_null()
        {
            Sut.Server.ShouldNotBeNull();
        }

        [Observation]
        public void Should_response_not_null()
        {
            Sut.Response.ShouldNotBeNull();
        }

        [Observation]
        public void Should_context_not_null()
        {
            Sut.Context.ShouldNotBeNull();
        }

        [Observation]
        public void Should_the_httpcontext_delegations_the_same_as_in_context()
        {
            Sut.Response.ShouldBeTheSame(Sut.Context.Response);
            Sut.Request.ShouldBeTheSame(Sut.Context.Request);
            Sut.Server.ShouldBeTheSame(Sut.Server);
        }

        [Observation]
        public void Should_the_context_user_not_authenticated()
        {
            Sut.Context.User.Identity.IsAuthenticated.ShouldBeFalse();
        }

        [Observation]
        public void Should_the_httpmethod_is_get()
        {
            Sut.Request.HttpMethod.ShouldBeEqualTo("GET");
        }

        [Observation]
        public void Shoud_l_the_request_context_type_not_null()
        {
            Sut.Request.ContentType.ShouldNotBeNull();
        }
    }

    public class When_adding_the_role_administrator_to_request_context : ConcernOfRequestContextBuilder
    {
        protected override void Because()
        {
            Sut.Role("Administrator");
        }

        [Observation]
        public void Should_the_context_user_is_authenticated()
        {
            Sut.Context.User.Identity.IsAuthenticated.ShouldBeTrue();
        }

        [Observation]
        public void Should_the_contest_user_is_in_role_administrator()
        {
            Sut.Context.User.IsInRole("Administrator").ShouldBeTrue();
        }
    }

    public class When_changing_the_httpmethod_to_post : ConcernOfRequestContextBuilder
    {
        protected override void Because()
        {
            Sut.HttpMethod("POST");
        }


        [Observation]
        public void Should_the_httpmethod_in_request_is_post()
        {
            Sut.Request.HttpMethod.ShouldBeEqualTo("POST");
        }
    }

}