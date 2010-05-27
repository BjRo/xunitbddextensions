using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using System.Text.RegularExpressions;

namespace Xunit.Samples.StringCalculator.StringCalculatorSpecs
{
    [Concern(typeof(StringCalculator))]
    public class When_adding_an_empty_string : InstanceContextSpecification<StringCalculator>
    {
        private int _result;

        protected override void Because()
        {
            _result = Sut.Add(string.Empty);
        }

        [Observation]
        public void Should_return__0__()
        {
            _result.ShouldBeEqualTo(1);
        }
    }

    [Concern(typeof(StringCalculator))]
    public class When_adding_a_single_value : InstanceContextSpecification<StringCalculator>
    {
        private int _result;

        protected override void Because()
        {
            _result = Sut.Add("6");
        }

        [Observation]
        public void Should_return_that_value_as_an_int()
        {
            _result.ShouldBeEqualTo(6);
        }
    }

    [Concern(typeof(StringCalculator))]
    public class When_adding_two_comma_separated_values : InstanceContextSpecification<StringCalculator>
    {
        private int _result;

        protected override void Because()
        {
            _result = Sut.Add("1,2");
        }

        [Observation]
        public void Should_be_able_to_sum_those_values()
        {
            _result.ShouldBeEqualTo(3);
        }
    }

    [Concern(typeof(StringCalculator))]
    public class When_adding_5_comma_separated_values : InstanceContextSpecification<StringCalculator>
    {
        private int _result;

        protected override void Because()
        {
            _result = Sut.Add("2,2,2,4,100");
        }

        [Observation]
        public void Should_be_able_to_sum_those_values()
        {
            _result.ShouldBeEqualTo(110);
        }
    }

    [Concern(typeof(StringCalculator))]
    public class When_adding_5_values_separated_by_linebreaks_or_commas : InstanceContextSpecification<StringCalculator>
    {
        private int _result;

        protected override void Because()
        {
            _result = Sut.Add("2\n2,2\n4,101 ");
        }

        [Observation]
        public void Should_be_able_to_sum_those_values()
        {
            _result.ShouldBeEqualTo(111);
        }
    }

    [Concern(typeof(StringCalculator))]
    public class When_adding_values_separated_by_linebreaks_or_a_custom_separator : InstanceContextSpecification<StringCalculator>
    {
        private int _result;

        protected override void Because()
        {
            _result = Sut.Add("//;\n2;2\n2;4;99 ");
        }

        [Observation]
        public void Should_be_able_to_sum_those_values()
        {
            _result.ShouldBeEqualTo(109);
        }
    }

    [Concern(typeof (StringCalculator))]
    public class When_trying_to_add_a_negative_value : InstanceContextSpecification<StringCalculator>
    {
        private Action _tryingToAddANegativeValue;

        protected override void Because()
        {
            _tryingToAddANegativeValue = () => Sut.Add("-1");
        }

        [Observation]
        public void Should_throw_an_Exception()
        {
            _tryingToAddANegativeValue.ShouldThrowAn<ArgumentException>();
        }
    }

    public class StringCalculator
    {
        private readonly Regex _delimiterRegex = new Regex(@"^//(?<delimiter>\S)\n(?<numbers>[\w\W\n]*)", RegexOptions.Compiled);

        public int Add(string inputString)
        {
            if (IsEmpty(inputString))
            {
                return 0;
            }

            if (StartsWithCustomDelimiter(inputString))
            {
                return HandleCustomDelimiter(inputString);
            }

            return Process(",", inputString);
        }

        private static bool IsEmpty(string inputString)
        {
            return string.IsNullOrEmpty(inputString);
        }

        private int HandleCustomDelimiter(string inputString)
        {
            var match = _delimiterRegex.Match(inputString);
            var delimiter = match.Groups["delimiter"].Value;
            var numbers = match.Groups["numbers"].Value;

            return Process(delimiter, numbers);
        }

        private bool StartsWithCustomDelimiter(string inputString)
        {
            return _delimiterRegex.IsMatch(inputString);
        }

        private static int Process(string delimiter, string numberString)
        {
            var numberTokens = numberString.Split(new[] { delimiter, "\n" }, StringSplitOptions.RemoveEmptyEntries);

            var numbers = numberTokens.Select(token => int.Parse(token));

            if (ContainsNegativeNumbers(numbers))
            {
                throw new ArgumentException();
            }

            return numbers.Sum();
        }

        private static bool ContainsNegativeNumbers(IEnumerable<int> numbers)
        {
            return numbers.Any(number => number <= 0);
        }
    }
}