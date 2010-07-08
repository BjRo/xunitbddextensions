namespace Xunit.Specs
{
    [Concern(typeof(MockingExtensions))]
    public class Given_that_the_return_value_of_a_configured_parameterless_method_is_evaluated_by_a_function_when_the_configure_method_is_invoked  : StaticContextSpecification
    {
        private IDependencyWithReturnValues _dependency;
        private object _recievedMethodResult;

        protected override void EstablishContext()
        {
            _dependency = An<IDependencyWithReturnValues>();
            
            _dependency
                .WhenToldTo(x => x.MethodWithoutParameter())
                .Return(() => "I want this result!");
        }

        protected override void Because()
        {
            _recievedMethodResult = _dependency.MethodWithoutParameter();
        }

        [Observation]
        public void Should_return_the_result_of_the_value_function()
        {
            _recievedMethodResult.ShouldBeEqualTo("I want this result!");
        }
    }

    [Concern(typeof(MockingExtensions))]
    public class Given_that_the_return_value_of_a_configured_method_with_a_single_parameter_is_evaluated_by_a_function_when_the_configured_method_is_finally_invoked : StaticContextSpecification
    {
        private IDependencyWithReturnValues _dependency;
        private object _recievedMethodResult;
        private string _parameter;
        private string _recievedParameter;

        protected override void EstablishContext()
        {
            _dependency = An<IDependencyWithReturnValues>();
            _parameter = "Some parameter";
            
            _dependency
                .WhenToldTo(x => x.MethodWithOneParameter(_parameter))
                .Return<string>(p =>
                {
                    _recievedParameter = p;
                    return "I want this result!";
                });
        }

        protected override void Because()
        {
            _recievedMethodResult = _dependency.MethodWithOneParameter(_parameter);
        }

        [Observation]
        public void Should_pass_the_recieved_parameter_into_the_configured_value_function()
        {
            _recievedParameter.ShouldBeEqualTo(_parameter);
        }

        [Observation]
        public void Should_return_the_result_of_the_value_function()
        {
            _recievedMethodResult.ShouldBeEqualTo("I want this result!");
        }
    }

    [Concern(typeof(MockingExtensions))]
    public class Given_that_the_return_value_of_a_configured_method_with_two_parameters_is_evaluated_by_a_function_when_the_configured_method_is_finally_invoked : StaticContextSpecification
    {
        private IDependencyWithReturnValues _dependency;
        private object _recievedMethodResult;
        private string _parameter1;
        private string _recievedParameter1;
        private string _parameter2;
        private string _recievedParameter2;

        protected override void EstablishContext()
        {
            _dependency = An<IDependencyWithReturnValues>();
            _parameter1 = "Some parameter";
            _parameter2 = "Some other parameter";

            _dependency
                .WhenToldTo(x => x.MethodWithTwoParameters(_parameter1, _parameter2))
                .Return<string, string>((p1, p2) =>
                {
                    _recievedParameter1 = p1;
                    _recievedParameter2 = p2;
                    return "I want this result!";
                });
        }

        protected override void Because()
        {
            _recievedMethodResult = _dependency.MethodWithTwoParameters(_parameter1, _parameter2);
        }

        [Observation]
        public void Should_pass_the_recieved_first_parameter_into_the_configured_value_function()
        {
            _recievedParameter1.ShouldBeEqualTo(_parameter1);
        }

        [Observation]
        public void Should_pass_the_recieved_second_parameter_into_the_configured_value_function()
        {
            _recievedParameter2.ShouldBeEqualTo(_parameter2);
        }

        [Observation]
        public void Should_return_the_result_of_the_value_function()
        {
            _recievedMethodResult.ShouldBeEqualTo("I want this result!");
        }
    }

    [Concern(typeof(MockingExtensions))]
    public class Given_that_the_return_value_of_a_configured_method_with_three_parameters_is_evaluated_by_a_function_when_the_configured_method_is_finally_invoked : StaticContextSpecification
    {
        private IDependencyWithReturnValues _dependency;
        private object _recievedMethodResult;
        private string _parameter1;
        private string _recievedParameter1;
        private string _parameter2;
        private string _recievedParameter2;
        private string _parameter3;
        private string _recievedParameter3;

        protected override void EstablishContext()
        {
            _dependency = An<IDependencyWithReturnValues>();
            _parameter1 = "Some parameter";
            _parameter2 = "Some other parameter";
            _parameter3 = "Some third parameter";

            _dependency
                .WhenToldTo(x => x.MethodWithThreeParameters(_parameter1, _parameter2, _parameter3))
                .Return<string, string, string>((p1, p2 ,p3) =>
                {
                    _recievedParameter1 = p1;
                    _recievedParameter2 = p2;
                    _recievedParameter3 = p3;
                    return "I want this result!";
                });
        }

        protected override void Because()
        {
            _recievedMethodResult = _dependency.MethodWithThreeParameters(_parameter1, _parameter2, _parameter3);
        }

        [Observation]
        public void Should_pass_the_recieved_first_parameter_into_the_configured_value_function()
        {
            _recievedParameter1.ShouldBeEqualTo(_parameter1);
        }

        [Observation]
        public void Should_pass_the_recieved_second_parameter_into_the_configured_value_function()
        {
            _recievedParameter2.ShouldBeEqualTo(_parameter2);
        }

        [Observation]
        public void Should_pass_the_recieved_third_parameter_into_the_configured_value_function()
        {
            _recievedParameter3.ShouldBeEqualTo(_parameter3);
        }

        [Observation]
        public void Should_return_the_result_of_the_value_function()
        {
            _recievedMethodResult.ShouldBeEqualTo("I want this result!");
        }
    }

    [Concern(typeof(MockingExtensions))]
    public class Given_that_the_return_value_of_a_configured_method_with_four_parameters_is_evaluated_by_a_function_when_the_configured_method_is_finally_invoked : StaticContextSpecification
    {
        private IDependencyWithReturnValues _dependency;
        private object _recievedMethodResult;
        private string _parameter1;
        private string _recievedParameter1;
        private string _parameter2;
        private string _recievedParameter2;
        private string _parameter3;
        private string _recievedParameter3;
        private string _parameter4;
        private string _recievedParameter4;

        protected override void EstablishContext()
        {
            _dependency = An<IDependencyWithReturnValues>();
            _parameter1 = "Some parameter";
            _parameter2 = "Some other parameter";
            _parameter3 = "Some third parameter";
            _parameter4 = "Some fourth parameter";

            _dependency
                .WhenToldTo(x => x.MethodWithFourParameters(_parameter1, _parameter2, _parameter3, _parameter4))
                .Return<string, string, string, string>((p1, p2, p3, p4) =>
                {
                    _recievedParameter1 = p1;
                    _recievedParameter2 = p2;
                    _recievedParameter3 = p3;
                    _recievedParameter4 = p4;
                    return "I want this result!";
                });
        }

        protected override void Because()
        {
            _recievedMethodResult = _dependency.MethodWithFourParameters(_parameter1, _parameter2, _parameter3, _parameter4);
        }

        [Observation]
        public void Should_pass_the_recieved_first_parameter_into_the_configured_value_function()
        {
            _recievedParameter1.ShouldBeEqualTo(_parameter1);
        }

        [Observation]
        public void Should_pass_the_recieved_second_parameter_into_the_configured_value_function()
        {
            _recievedParameter2.ShouldBeEqualTo(_parameter2);
        }

        [Observation]
        public void Should_pass_the_recieved_third_parameter_into_the_configured_value_function()
        {
            _recievedParameter3.ShouldBeEqualTo(_parameter3);
        }

        [Observation]
        public void Should_pass_the_recieved_fourth_parameter_into_the_configured_value_function()
        {
            _recievedParameter4.ShouldBeEqualTo(_parameter4);
        }

        [Observation]
        public void Should_return_the_result_of_the_value_function()
        {
            _recievedMethodResult.ShouldBeEqualTo("I want this result!");
        }
    }

    public interface IDependencyWithReturnValues
    {
        string MethodWithoutParameter();

        string MethodWithOneParameter(string parameter);

        string MethodWithTwoParameters(string parameter1, string parameter2);

        string MethodWithThreeParameters(string parameter1, string parameter2, string parameter3);

        string MethodWithFourParameters(string parameter1, string parameter2, string parameter3, string parameter4);
    }
}