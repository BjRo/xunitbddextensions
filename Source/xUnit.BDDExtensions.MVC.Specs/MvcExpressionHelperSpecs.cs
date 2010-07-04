using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Xunit.Specs
{
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

    public class When_examine_the_index_method_with_a_result : ConcernOfMvcExpressionHelper
    {
        private string _result;

        protected override void Because()
        {
            _result = MvcExpressionHelper.GetMemberName(GetExpression<TestClass, ActionResult>(c => c.Index()));
        }

        [Observation]
        public void Should_the_membername_index()
        {
            _result.ShouldBeEqualTo("Index");
        }
    }

    public class When_examine_the_index_method_with_parameters_and_a_result : ConcernOfMvcExpressionHelper
    {
        private string _result;

        protected override void Because()
        {
            _result = MvcExpressionHelper.GetMemberName(GetExpression<TestClass, ActionResult>(c => c.Index(3, 2, 1)));
        }

        [Observation]
        public void Should_the_membername_index()
        {
            _result.ShouldBeEqualTo("Index");
        }
    }

    public class When_examine_the_create_method_without_a_result : ConcernOfMvcExpressionHelper
    {
        private string _result;

        protected override void Because()
        {
            _result = MvcExpressionHelper.GetMemberName(GetExpression<TestClass>(c => c.Create()));
        }

        [Observation]
        public void Should_the_membername_create()
        {
            _result.ShouldBeEqualTo("Create");
        }
    }

    public class When_examine_parameter_names : ConcernOfMvcExpressionHelper
    {
        private string[] _result;

        protected override void Because()
        {
            _result =
                MvcExpressionHelper.GetParameterNames(GetExpression<TestClass, ActionResult>(c => c.Index(3, 2, 1)));
        }

        [Observation]
        public void Should_the_names_a_b_c()
        {
            _result.ShouldOnlyContainInOrder("a", "b", "c");
        }
    }

    public class When_examine_parameter_names_on_a_method_without_parameters : ConcernOfMvcExpressionHelper
    {
        private string[] _result;

        protected override void Because()
        {
            _result = MvcExpressionHelper.GetParameterNames(GetExpression<TestClass, ActionResult>(c => c.Index()));
        }

        [Observation]
        public void Should_the_result_an_empty_array()
        {
            _result.ShouldBeEmpty();
        }
    }

    public class When_examine_parameters_with_the_constant_values_3_2_and_1 : ConcernOfMvcExpressionHelper
    {
        private object _result1;
        private object _result2;
        private object _result3;

        protected override void Because()
        {
            _result1 =
                MvcExpressionHelper.GetParameterValue(GetExpression<TestClass, ActionResult>(c => c.Index(3, 2, 1)), "a");
            _result2 =
                MvcExpressionHelper.GetParameterValue(GetExpression<TestClass, ActionResult>(c => c.Index(3, 2, 1)), "b");
            _result3 =
                MvcExpressionHelper.GetParameterValue(GetExpression<TestClass, ActionResult>(c => c.Index(3, 2, 1)), "c");
        }

        [Observation]
        public void Should_the_first_value_3()
        {
            _result1.ShouldBeEqualTo(3);
        }

        [Observation]
        public void Should_the_second_value_2()
        {
            _result2.ShouldBeEqualTo(2);
        }

        [Observation]
        public void Should_the_third_value_1()
        {
            _result3.ShouldBeEqualTo(1);
        }
    }

    public class When_examine_parameters_with_a_new_datetime_value : ConcernOfMvcExpressionHelper
    {
        private object _result1;

        protected override void Because()
        {
            _result1 =
                MvcExpressionHelper.GetParameterValue(
                    GetExpression<TestClass>(c => c.Date(new DateTime(2010, 07, 05))), "dateTime");
        }

        [Observation]
        public void Should_the_result_2010_07_05()
        {
            _result1.ShouldBeEqualTo(new DateTime(2010, 7, 5));
        }
    }

    public class When_examine_parameters_with_a_reference_values : ConcernOfMvcExpressionHelper
    {
        private object _result1;
        private object _result2;
        private TestClass _instance1;
        private TestClass _instnance2;

        protected override void EstablishContext()
        {
            _instance1 = new TestClass {Vorname = "Eins", Nachname = "Zwei"};
            _instnance2 = new TestClass {Vorname = "Drei", Nachname = "Vier"};
        }

        protected override void Because()
        {
            _result1 =
                MvcExpressionHelper.GetParameterValue(GetExpression<TestClass>(c => c.Person(_instance1, _instnance2)),
                                                      "param1");
            _result2 =
                MvcExpressionHelper.GetParameterValue(GetExpression<TestClass>(c => c.Person(_instance1, _instnance2)),
                                                      "param2");
        }

        [Observation]
        public void Should_the_first_parameter_value_same_as_instance1()
        {
            _result1.ShouldBeTheSame(_instance1);
        }

        [Observation]
        public void Should_the_second_parameter_value_same_as_instance2()
        {
            _result2.ShouldBeTheSame(_instnance2);
        }
    }
}