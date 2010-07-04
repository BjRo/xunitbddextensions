using System;

namespace Xunit.Specs
{
    [Concern(typeof (RequestContextBuilder))]
    public class When_building_a_requestconext : InstanceContextSpecification<RequestContextBuilder>
    {
        protected override void Because()
        {
        }

        protected override RequestContextBuilder CreateSut()
        {
            return new RequestContextBuilder(this);
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
    }
}