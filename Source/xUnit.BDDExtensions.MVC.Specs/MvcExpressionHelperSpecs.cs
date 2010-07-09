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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Xunit.Internal;

namespace Xunit.Specs
{
    public class Given_an_expression_which_points_to_a_controller_action_returning_an_action_result_when_trying_to_extract_the_name : ConcernOfMvcExpressionHelper
    {
        private string _result;

        protected override void Because()
        {
            _result = MvcExpressionHelper.GetMemberName(GetExpression<TestClass, ActionResult>(c => c.Index()));
        }

        [Observation]
        public void Should_be_able_to_extract_the_name_of_the_controller_action()
        {
            _result.ShouldBeEqualTo("Index");
        }
    }

    public class Given_an_expression_which_points_to_a_parameterized_controller_action_returning_an_action_result_when_trying_to_extract_the_name : ConcernOfMvcExpressionHelper
    {
        private string _result;

        protected override void Because()
        {
            _result = MvcExpressionHelper.GetMemberName(GetExpression<TestClass, ActionResult>(c => c.Index(3, 2, 1)));
        }

        [Observation]
        public void Should_be_able_to_extract_the_name_of_the_controller_action()
        {
            _result.ShouldBeEqualTo("Index");
        }
    }

    public class Given_an_expression_which_points_to_a_controller_comand_when_trying_to_extract_the_name : ConcernOfMvcExpressionHelper
    {
        private string _result;

        protected override void Because()
        {
            _result = MvcExpressionHelper.GetMemberName(GetExpression<TestClass>(c => c.Create()));
        }

        [Observation]
        public void Should_be_able_to_extract_the_name_of_the_controller_action()
        {
            _result.ShouldBeEqualTo("Create");
        }
    }

    public class Given_an_expression_which_points_to_parameterized_controller_action_when_trying_to_extract_the_parameter_names : ConcernOfMvcExpressionHelper
    {
        private string[] _result;

        protected override void Because()
        {
            _result = MvcExpressionHelper.GetParameterNames(GetExpression<TestClass, ActionResult>(c => c.Index(3, 2, 1)));
        }

        [Observation]
        public void Should_be_able_to_extract_the_parameter_names_in_the_correct_order()
        {
            _result.ShouldOnlyContainInOrder("a", "b", "c");
        }
    }

    public class Given_an_expression_which_points_to_a_controller_command_when_trying_to_extract_the_parameter_names : ConcernOfMvcExpressionHelper
    {
        private string[] _result;

        protected override void Because()
        {
            _result = MvcExpressionHelper.GetParameterNames(GetExpression<TestClass, ActionResult>(c => c.Index()));
        }

        [Observation]
        public void Should_return_an_empty_array()
        {
            _result.ShouldBeEmpty();
        }
    }

    public class Given_an_expression_which_points_to_parameterized_controller_action_when_trying_to_extract_the_parameter_values : ConcernOfMvcExpressionHelper
    {
        private string[] _parameterNames;
        private IEnumerable<object> _parameterValues;

        protected override void EstablishContext()
        {
            _parameterNames = new[] { "a", "b", "c" };
        }

        protected override void Because()
        {
            _parameterValues = _parameterNames.Select(parameterName =>
                                        MvcExpressionHelper.GetParameterValue(GetExpression<TestClass, ActionResult>(c => c.Index(3, 2, 1)), 
                                        parameterName));
        }

        [Observation]
        public void Should_be_able_to_extract_each_parameter_value()
        {
            _parameterValues.ShouldOnlyContainInOrder(3,2,1);
        }
    }

    public class Given_an_expression_which_points_to_controller_action_with_a_date_time_parameter_when_trying_to_extract_the_parameter_value : ConcernOfMvcExpressionHelper
    {
        private object _result1;

        protected override void Because()
        {
            _result1 = MvcExpressionHelper.GetParameterValue(
                    GetExpression<TestClass>(c => c.Date(new DateTime(2010, 07, 05))), 
                    "dateTime");
        }

        [Observation]
        public void Should_be_able_to_extract_the_correct_date()
        {
            _result1.ShouldBeEqualTo(new DateTime(2010, 7, 5));
        }
    }

    public class Given_an_expression_which_points_to_controller_action_with_parameters_of_a_reference_type_when_trying_to_extract_the_parameter_values : ConcernOfMvcExpressionHelper
    {
        private string[] _parameterNames;
        private IEnumerable<object> _extractedParameters;
        private TestClass _instance1;
        private TestClass _instance2;

        protected override void EstablishContext()
        {
            _instance1 = new TestClass {Vorname = "Eins", Nachname = "Zwei"};
            _instance2 = new TestClass {Vorname = "Drei", Nachname = "Vier"};
       
            _parameterNames = new[]
            {
                "param1",
                "param2"
            };
        }

        protected override void Because()
        {
            _extractedParameters = _parameterNames.Select(parameterName =>
                MvcExpressionHelper.GetParameterValue(
                GetExpression<TestClass>(c => c.Person(_instance1, _instance2)),
                parameterName));
        }

        [Observation]
        public void Should_able_to_extract_the_parameter_values()
        {
            _extractedParameters.ShouldOnlyContain(_instance1, _instance2);
        }

    }

    [Concern(typeof (MvcExpressionHelper))]
    public abstract class ConcernOfMvcExpressionHelper : StaticContextSpecification
    {
        protected static Expression GetExpression<T, TResult>(Expression<Func<T, TResult>> expression)
        {
            return expression;
        }

        protected static Expression GetExpression<T>(Expression<Action<T>> expression)
        {
            return expression;
        }
    }

    internal class TestClass
    {
        public ActionResult Index()
        {
            throw new NotImplementedException();
        }

        public ActionResult Index(int a, int b, int c)
        {
            throw new NotImplementedException();
        }

        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Date(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public void Person(TestClass param1, TestClass param2)
        {
            throw new NotImplementedException();
        }

        public string Vorname { get; set; }
        public string Nachname { get; set; }
    }
}