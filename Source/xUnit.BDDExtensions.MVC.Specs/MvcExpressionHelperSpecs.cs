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

    public class When_examining_a_method_with_a_result : ConcernOfMvcExpressionHelper
    {
        private string result;

        protected override void Because()
        {
            result = MvcExpressionHelper.GetMemberName(GetExpression<TestClass, ActionResult>(c => c.Index()));
        }

        [Observation]
        public void Should_the_member_name_index()
        {
            result.ShouldBeEqualTo("Index");
        }
    }

    public class When_examining_a_method_with_parameters_with_a_result : ConcernOfMvcExpressionHelper
    {
        private string result;

        protected override void Because()
        {
            result = MvcExpressionHelper.GetMemberName(GetExpression<TestClass, ActionResult>(c => c.Index(3, 2, 1)));
        }

        [Observation]
        public void Should_the_member_name_index()
        {
            result.ShouldBeEqualTo("Index");
        }
    }

    public class When_examining_a_method_without_a_result : ConcernOfMvcExpressionHelper
    {
        private string result;

        protected override void Because()
        {
            result = MvcExpressionHelper.GetMemberName(GetExpression<TestClass>(c => c.Create()));
        }

        [Observation]
        public void Should_the_member_name_index()
        {
            result.ShouldBeEqualTo("Create");
        }
    }

    public class When_examining_parameters_names : ConcernOfMvcExpressionHelper
    {
        private string[] result;

        protected override void Because()
        {
            result = MvcExpressionHelper.GetParameterNames(GetExpression<TestClass, ActionResult>(c => c.Index(3, 2, 1)));
        }

        [Observation]
        public void Should_the_names_a_b_c()
        {
            result.ShouldOnlyContainInOrder("a", "b", "c");
        }
    }

    public class When_examining_parameters_names_on_a_method_without_parameters : ConcernOfMvcExpressionHelper
    {
        private string[] result;

        protected override void Because()
        {
            result = MvcExpressionHelper.GetParameterNames(GetExpression<TestClass, ActionResult>(c => c.Index()));
        }

        [Observation]
        public void Should_the_result_an_empty_array()
        {
            result.ShouldBeEmpty();
        }
    }

    public class When_examining_parameters_with_constant_values : ConcernOfMvcExpressionHelper
    {
        private object result1;
        private object result2;
        private object result3;

        protected override void Because()
        {
            result1 =
                MvcExpressionHelper.GetParameterValue(GetExpression<TestClass, ActionResult>(c => c.Index(3, 2, 1)), "a");
            result2 =
                MvcExpressionHelper.GetParameterValue(GetExpression<TestClass, ActionResult>(c => c.Index(3, 2, 1)), "b");
            result3 =
                MvcExpressionHelper.GetParameterValue(GetExpression<TestClass, ActionResult>(c => c.Index(3, 2, 1)), "c");
        }

        [Observation]
        public void Should_the_first_value_3()
        {
            result1.ShouldBeEqualTo(3);
        }

        [Observation]
        public void Should_the_second_value_2()
        {
            result2.ShouldBeEqualTo(2);
        }

        [Observation]
        public void Should_the_third_value_1()
        {
            result3.ShouldBeEqualTo(1);
        }
    }

    public class When_examining_parameters_with_a_date_value : ConcernOfMvcExpressionHelper
    {
        private object result1;

        protected override void Because()
        {
            result1 =
                MvcExpressionHelper.GetParameterValue(GetExpression<TestClass>(c => c.Date(new DateTime(2010,07,05))), "dateTime");
        }

        [Observation]
        public void Should_the_value_the_2010_07_05()
        {
            result1.ShouldBeEqualTo(new DateTime(2010,7,5));
        }
    }

    public class When_examining_parameters_with_a_object_values : ConcernOfMvcExpressionHelper
    {
        private object result1;
        private object result2;
        private TestClass param1;
        private TestClass param2;

        protected override void EstablishContext()
        {
            param1 = new TestClass {Vorname = "Eins", Nachname = "Zwei"};
            param2 = new TestClass { Vorname = "Drei", Nachname = "Vier" };
        }
        protected override void Because()
        {
            result1 =
                MvcExpressionHelper.GetParameterValue(GetExpression<TestClass>(c => c.Person(param1,param2)), "param1");
            result2 =
                MvcExpressionHelper.GetParameterValue(GetExpression<TestClass>(c => c.Person(param1, param2)), "param2");
        }

        [Observation]
        public void Should_the_first_parameter_same_as_instance1()
        {
            result1.ShouldBeTheSame(param1);
        }

        [Observation]
        public void Should_the_second_parameter_same_as_instance2()
        {
            result2.ShouldBeTheSame(param2);
        }
    }

}