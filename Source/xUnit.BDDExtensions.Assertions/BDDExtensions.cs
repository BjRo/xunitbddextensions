// Copyright 2009 Björn Rochel - http://www.bjro.de/ 
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
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Sdk;

namespace Xunit
{
    /// <summary>
    /// A class containing extension methods for left to right assertions.
    /// </summary>
    public static class BDDExtensions
    {
        /// <summary>
        /// Asserts that the object supplied by <paramref name="item"/> should be null.
        /// </summary>
        /// <param name="item">
        /// The item to check.
        /// </param>
        /// <exception cref="NullException">
        /// Thrown when <paramref name="item"/> is not <c>null</c>.
        /// </exception>
        public static void ShouldBeNull(this object item)
        {
            Assert.Null(item);
        }

        /// <summary>
        /// Asserts that the object supplied by <paramref name="item"/> should not be null.
        /// </summary>
        /// <param name="item">
        /// The item to check.
        /// </param>
        /// <exception cref="NotNullException">
        /// Thrown when <paramref name="item"/> is <c>null</c>.
        /// </exception>
        public static void ShouldNotBeNull(this object item)
        {
            Assert.NotNull(item);
        }

        /// <summary>
        /// Asserts that the operation supplied by <paramref name="workToPerform"/> should
        /// throw an exception of the type specified by <typeparamref name="ExceptionType"/>.
        /// </summary>
        /// <typeparam name="ExceptionType">
        /// Specifies the type of the exception that is expected to occur.
        /// </typeparam>
        /// <param name="workToPerform">
        /// Specifies the action that is expected to raise the exception.
        /// </param>
        /// <returns>
        /// The occured exception.
        /// </returns>
        /// <exception cref="ThrowsException">
        /// Thrown when <paramref name="workToPerform"/> does not throw the exception specified by
        /// <typeparamref name="ExceptionType"/>.
        /// </exception>
        public static ExceptionType ShouldThrowAn<ExceptionType>(this Action workToPerform)
            where ExceptionType : Exception
        {
            Exception exceptionCaught = null;

            try
            {
                workToPerform();
            }
            catch (Exception ex)
            {
                exceptionCaught = ex;
            }

            if (exceptionCaught == null)
            {
                throw new ThrowsException(typeof (ExceptionType));
            }

            if (!typeof (ExceptionType).IsAssignableFrom(exceptionCaught.GetType()))
            {
                throw new ThrowsException(typeof (ExceptionType), exceptionCaught);
            }

            return (ExceptionType) exceptionCaught;
        }

        /// <summary>
        /// Asserts that the item supplied by <paramref name="item"/> is equal to the
        /// item supplied by <paramref name="other"/>.
        /// </summary>
        /// <param name="item">
        /// The instance to check.
        /// </param>
        /// <param name="other">
        /// The instance to check against.
        /// </param>
        /// <exception cref="EqualException">
        /// Thrown when the the supplied objects are not equal.
        /// </exception>
        public static void ShouldBeEqualTo(this object item, object other)
        {
            Assert.Equal(other, item);
        }

        /// <summary>
        /// Asserts that the item supplied by <paramref name="item"/> is equal to the
        /// item supplied by <paramref name="other"/>.
        /// </summary>
        /// <param name="item">
        /// The instance to check.
        /// </param>
        /// <param name="other">
        /// The instance to check against.
        /// </param>
        /// <typeparam name="T">
        /// Specifies the concrete type of items to be compared.
        /// </typeparam>
        /// <exception cref="EqualException">
        /// Thrown when the the supplied objects are not equal.
        /// </exception>
        public static void ShouldBeEqualTo<T>(this T item, T other)
        {
            Assert.Equal(other, item);
        }

        /// <summary>
        /// Asserts that the item supplied by <paramref name="item"/> is not equal to the
        /// item supplied by <paramref name="other"/>.
        /// </summary>
        /// <param name="item">
        /// The instance to check.
        /// </param>
        /// <param name="other">
        /// The instance to check against.
        /// </param>
        /// <exception cref="NotEqualException">
        /// Thrown when the the supplied objects are equal.
        /// </exception>
        public static void ShouldNotBeEqualTo<T>(this T item, T other)
        {
            Assert.NotEqual(other, item);
        }

        /// <summary>
        /// Asserts that the item supplied by <paramref name="item"/> is the same item as the
        /// item supplied by <paramref name="other"/>.
        /// </summary>
        /// <param name="item">
        /// The instance to check.
        /// </param>
        /// <param name="other">
        /// The instance to check against.
        /// </param>
        /// <exception cref="SameException">
        /// Thrown when the the supplied objects are not the same.
        /// </exception>
        public static void ShouldBeTheSame<T>(this T item, T other)
        {
            Assert.Same(other, item);
        }

        /// <summary>
        /// Asserts that the item supplied by <paramref name="item"/> is not the same item as the
        /// item supplied by <paramref name="other"/>.
        /// </summary>
        /// <param name="item">
        /// The instance to check.
        /// </param>
        /// <param name="other">
        /// The instance to check against.
        /// </param>
        /// <exception cref="NotSameException">
        /// Thrown when the the supplied objects are the same.
        /// </exception>
        public static void ShouldNotBeTheSame<T>(this T item, T other)
        {
            Assert.NotSame(other, item);
        }

        /// <summary>
        /// Asserts that  the item supplied by <paramref name="item"/> should be contained 
        /// in the enumerable supplied by <paramref name="items"/>.
        /// </summary>
        /// <typeparam name="T">
        /// Specifies the type of the item.
        /// </typeparam>
        /// <param name="items">
        /// Specifies the enumerable that should contain the item.
        /// </param>
        /// <param name="item">
        /// Specifies the item which should be contained.
        /// </param>
        /// <exception cref="ContainsException">
        /// Thrown when <paramref name="item"/> is not contained in <paramref name="items"/>.
        /// </exception>
        public static void ShouldContain<T>(this IEnumerable<T> items, T item)
        {
            Assert.Contains(item, items);
        }

        /// <summary>
        /// Asserts that  the string supplied by <paramref name="substring"/> should be contained 
        /// in the string supplied by <paramref name="actualString"/>.
        /// </summary>
        /// <param name="actualString">
        /// Specifies the parent string.
        /// </param>
        /// <param name="substring">
        /// Specifies the string which should be contained.
        /// </param>
        /// <exception cref="ContainsException">
        /// Thrown when <paramref name="substring"/> is not contained in <paramref name="actualString"/>.
        /// </exception>
        public static void ShouldContain(this string actualString, string substring)
        {
            Assert.Contains(substring, actualString);
        }

        /// <summary>
        /// Asserts that the string supplied by <paramref name="substring"/> should not be contained 
        /// in the string supplied by <paramref name="actualString"/>.
        /// </summary>
        /// <param name="actualString">
        /// Specifies the parent string.
        /// </param>
        /// <param name="substring">
        /// Specifies the string which should not be contained.
        /// </param>
        /// <exception cref="DoesNotContainException">
        /// Thrown when <paramref name="substring"/> is not contained in <paramref name="actualString"/>.
        /// </exception>
        public static void ShouldNotContain(this string actualString, string substring)
        {
            Assert.DoesNotContain(substring, actualString);
        }

        /// <summary>
        /// Asserts that  the item supplied by <paramref name="item"/> should not be contained 
        /// in the enumerable supplied by <paramref name="items"/>.
        /// </summary>
        /// <typeparam name="T">
        /// Specifies the type of the item.
        /// </typeparam>
        /// <param name="items">
        /// Specifies the enumerable that should contain the item.
        /// </param>
        /// <param name="item">
        /// Specifies the item which should not be contained.
        /// </param>
        /// <exception cref="DoesNotContainException">
        /// Thrown when <paramref name="item"/> is contained in <paramref name="items"/>.
        /// </exception>
        public static void ShouldNotContain<T>(this IEnumerable<T> items, T item)
        {
            Assert.DoesNotContain(item, items);
        }

        /// <summary>
        /// Asserts that the enumerable supplied by <paramref name="items"/> only contains the
        /// items supplied by <paramref name="itemsToFind"/>;
        /// </summary>
        /// <typeparam name="T">
        /// Specifies the item type.
        /// </typeparam>
        /// <param name="items">
        /// Specifies the enumerable in which the items should be.
        /// </param>
        /// <param name="itemsToFind">
        /// Specifies the items to be contained in the enumerable.
        /// </param>
        /// <exception cref="AssertException">
        /// Thrown when the enumerable contains more items, less items, or different items
        /// than the ones specified by <paramref name="itemsToFind"/>.
        /// </exception>
        public static void ShouldOnlyContain<T>(this IEnumerable<T> items, params T[] itemsToFind)
        {
            var results = new List<T>(items);
            itemsToFind.Length.ShouldBeEqualTo(items.Count());
            foreach (var itemToFind in itemsToFind)
            {
                results.Contains(itemToFind).ShouldBeTrue();
            }
        }

        /// <summary>
        /// Asserts that the enumerable supplied by <paramref name="items"/> only contains the
        /// items supplied by <paramref name="itemsToFind"/> in the specifies order;
        /// </summary>
        /// <typeparam name="T">
        /// Specifies the item type.
        /// </typeparam>
        /// <param name="items">
        /// Specifies the enumerable in which the items should be.
        /// </param>
        /// <param name="itemsToFind">
        /// Specifies the items to be contained in the enumerable.
        /// </param>
        /// <exception cref="AssertException">
        /// Thrown when the enumerable contains more items, less items, different items, or the correct items
        /// but in a different order than the one specified by <paramref name="itemsToFind"/>.
        /// </exception>
        public static void ShouldOnlyContainInOrder<T>(this IEnumerable<T> items, params T[] itemsToFind)
        {
            var results = new List<T>(items);
            itemsToFind.Length.ShouldBeEqualTo(items.Count());
            for (var i = 0; i < itemsToFind.Count(); i++)
            {
                itemsToFind[i].ShouldBeEqualTo(results[i]);
            }
        }

        /// <summary>
        /// Asserts that the extended boolean value is <c>true</c>.
        /// </summary>
        /// <param name="item">
        /// A boolean value.
        /// </param>
        /// <exception cref="TrueException">
        /// Thrown when <paramref name="item"/> is <c>false</c>.
        /// </exception>
        public static void ShouldBeTrue(this bool item)
        {
            Assert.True(item);
        }

        /// <summary>
        /// Asserts that the extended boolean value is <c>false</c>.
        /// </summary>
        /// <param name="item">
        /// A boolean value.
        /// </param>
        /// <exception cref="FalseException">
        /// Thrown when <paramref name="item"/> is <c>true</c>.
        /// </exception>
        public static void ShouldBeFalse(this bool item)
        {
            Assert.False(item);
        }

        /// <summary>
        /// Asserts that the collection supplied by <paramref name="collection"/> is empty.
        /// </summary>
        /// <typeparam name="T">
        /// Specifies the item type of the collection.
        /// </typeparam>
        /// <param name="collection">
        /// Specifies the collection.
        /// </param>
        /// <exception cref="EmptyException">
        /// Thrown when <paramref name="collection"/> is not empty.
        /// </exception>
        public static void ShouldBeEmpty<T>(this IEnumerable<T> collection)
        {
            Assert.Empty(collection);
        }

        /// <summary>
        /// Asserts that the collection supplied by <paramref name="collection"/> is not empty.
        /// </summary>
        /// <typeparam name="T">
        /// Specifies the item type of the collection.
        /// </typeparam>
        /// <param name="collection">
        /// Specifies the collection.
        /// </param>
        /// <exception cref="NotEmptyException">
        /// Thrown when <paramref name="collection"/> is empty.
        /// </exception>
        public static void ShouldNotBeEmpty<T>(this IEnumerable<T> collection)
        {
            Assert.NotEmpty(collection);
        }

        /// <summary>
        /// Asserts that the item supplied by <paramref name="item"/> is greated that
        /// the item <paramref name="other"/>.
        /// </summary>
        /// <typeparam name="T">
        /// Specifies the type of the items to compare.
        /// </typeparam>
        /// <param name="item">
        /// Specifies the first item.
        /// </param>
        /// <param name="other">
        /// Specifies the second item.
        /// </param>
        /// <exception cref="AssertException">
        /// Thrown when <paramref name="item"/> is smaller or equal to <paramref name="other"/>.
        /// </exception>
        public static void ShouldBeGreaterThan<T>(this T item, T other) where T : IComparable<T>
        {
            (item.CompareTo(other) > 0).ShouldBeTrue();
        }

        /// <summary>
        /// Asserts that an instance is of the type specified by <typeparamref name="Type"/>.
        /// </summary>
        /// <typeparam name="Type">
        /// Specifies the type.
        /// </typeparam>
        /// <param name="item">
        /// Specifies the item to check.
        /// </param>
        /// <exception cref="IsAssignableFromException">
        /// Thrown when the instance is not the type or not assignable to the type
        /// specified by <typeparamref name="Type"/>.
        /// </exception>
        public static void ShouldBeAnInstanceOf<Type>(this object item)
        {
            Assert.IsAssignableFrom<Type>(item);
        }

        /// <summary>
        /// Asserts that the string supplied by <paramref name="item"/> is 
        /// equal (ignoring casing) to the string supplied by <paramref name="other"/>.
        /// </summary>
        /// <param name="item">
        /// Specifies the first string.
        /// </param>
        /// <param name="other">
        /// Specifies the second string to check against.
        /// </param>
        /// <exception cref="EqualException">
        /// Thrown when the strings are not equal.
        /// </exception>
        public static void ShouldBeEqualIgnoringCase(this string item, string other)
        {
            Assert.Equal(other, item, (IEqualityComparer<string>)StringComparer.InvariantCultureIgnoreCase );
        }

        /// <summary>
        /// Forces the traversal on an enumerable.
        /// </summary>
        /// <typeparam name="T">
        /// Specifies the type of the enumerable.
        /// </typeparam>
        /// <param name="items">
        /// An enumerable to be iterated.
        /// </param>
        public static void ForceTraversal<T>(this IEnumerable<T> items)
        {
            items.Count();
        }

        /// <summary>
        /// Asserts that no exceptions should be thrown.
        /// </summary>
        /// <param name="workToPerform">
        /// The action that could possibly raise an exception.
        /// </param>
        public static void ShouldNotThrowAnyExceptions(this Action workToPerform)
        {
            workToPerform();
        }
    }
}