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
using System.Linq.Expressions;
using Xunit.Internal;

namespace Xunit
{
    /// <summary>
    ///   A set of extension methods for setting up behavior on fakes in a fashion independant 
    ///   to a particular fake framework.
    /// </summary>
    public static class FakeApi
    {
        /// <summary>
        ///   Configures the behavior of the fake specified by <paramref name = "fake" />.
        /// </summary>
        /// <typeparam name = "TFake">
        ///   Specifies the type of the fake.
        /// </typeparam>
        /// <typeparam name = "TReturnValue">
        ///   Specifies the type of the return value.
        /// </typeparam>
        /// <param name = "fake">
        ///   The fake to configure behavior on.
        /// </param>
        /// <param name = "func">
        ///   Configures the behavior. This must be a void method.
        /// </param>
        /// <returns>
        ///   A <see cref = "IQueryOptions{TReturn}" /> for further configuration.
        /// </returns>
        public static IQueryOptions<TReturnValue> WhenToldTo<TFake, TReturnValue>(
            this TFake fake,
            Expression<Func<TFake, TReturnValue>> func) where TFake : class
        {
            Guard.AgainstArgumentNull(fake, "fake");
            Guard.AgainstArgumentNull(func, "func");

            return RunnerConfiguration.FakeEngine.SetUpQueryBehaviorFor(fake, func);
        }

        /// <summary>
        ///   Configures the behavior of the fake specified by <paramref name = "fake" />.
        /// </summary>
        /// <typeparam name = "TFake">
        ///   Specifies the type of the fake.
        /// </typeparam>
        /// <param name = "fake">
        ///   The fake to configure behavior on.
        /// </param>
        /// <param name = "func">
        ///   Configures the behavior. This must be a void method.
        /// </param>
        /// <returns>
        ///   A <see cref = "ICommandOptions" /> for further configuration.
        /// </returns>
        /// <remarks>
        ///   This method is used for command, e.g. methods returning void.
        /// </remarks>
        public static ICommandOptions WhenToldTo<TFake>(
            this TFake fake,
            Expression<Action<TFake>> func) where TFake : class
        {
            Guard.AgainstArgumentNull(fake, "fake");
            Guard.AgainstArgumentNull(func, "func");

            return RunnerConfiguration.FakeEngine.SetUpCommandBehaviorFor(fake, func);
        }

        /// <summary>
        /// Verifies that the behavior specified by <paramref name="func"/>
        /// was not executed on the fake specified by <paramref name="fake"/>.
        /// </summary>
        /// <typeparam name="TFake">
        /// Specifies the type of the fake.
        /// </typeparam>
        /// <param name="fake">
        /// Specifies the fake instance.
        /// </param>
        /// <param name="func">
        /// Specifies the behavior that was not supposed to happen.
        /// </param>
        public static void WasNotToldTo<TFake>(
            this TFake fake,
            Expression<Action<TFake>> func) where TFake : class 
        {
            Guard.AgainstArgumentNull(fake, "fake");
            Guard.AgainstArgumentNull(func, "func");

            RunnerConfiguration.FakeEngine.VerifyBehaviorWasNotExecuted(fake, func);
        }

        /// <summary>
        /// Verifies that the behavior specified by <paramref name="func"/>
        /// was executed on the fake specified by <paramref name="fake"/>.
        /// </summary>
        /// <typeparam name="TFake">
        /// Specifies the type of the fake.
        /// </typeparam>
        /// <param name="fake">
        /// Specifies the fake instance.
        /// </param>
        /// <param name="func">
        /// Specifies the behavior that was supposed to happen.
        /// </param>
        /// <returns>
        /// A <see cref="IMethodCallOccurance"/> which can be used
        /// to narrow down the expectations to a particular amount of times.
        /// </returns>
        public static IMethodCallOccurance WasToldTo<TFake>(
            this TFake fake, 
            Expression<Action<TFake>> func) where TFake : class
        {
            Guard.AgainstArgumentNull(fake, "fake");
            Guard.AgainstArgumentNull(func, "func");

            return RunnerConfiguration.FakeEngine.VerifyBehaviorWasExecuted(fake, func);
        }

        /// <summary>
        /// Gets an <see cref="IEventRaiser"/> which can be used to raise an event
        /// on the fake specified via <typeparamref name="TFake"/>.
        /// </summary>
        /// <typeparam name="TFake">
        /// Specifies the type of the fake.
        /// </typeparam>
        /// <param name="fake">
        /// Specifies the fake instance.
        /// </param>
        /// <param name="assignement">
        /// A function specifying the event assignement.
        /// </param>
        /// <returns>
        /// A <see cref="IEventRaiser"/>.
        /// </returns>
        /// <remarks>
        ///     Get a refrence to an <see cref="IEventRaiser"/> like this:
        ///     <code>
        ///         var eventRaiser = fake.Event(x => x.PropertyChanged +=null);
        ///     </code>
        ///     Use it to raise the event.
        ///     <code>
        ///         fake.Event(x => x.PropertyChanged += null)
        ///             .Raise(this, new PropertyChangedArgs("NoProp")); 
        ///     </code>
        /// </remarks>
        public static IEventRaiser Event<TFake>(
            this TFake fake, 
            Action<TFake> assignement) where TFake : class 
        {
            Guard.AgainstArgumentNull(fake, "fake");
            Guard.AgainstArgumentNull(assignement, "assignement");

            return RunnerConfiguration.FakeEngine.CreateEventRaiser(fake, assignement);
        }
    }
}