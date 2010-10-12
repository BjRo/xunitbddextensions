// Copyright 2010 xUnit.BDDExtensions
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
using System.Linq.Expressions;

namespace Xunit
{
    /// <summary>
    /// Interface to a fake framework. 
    /// </summary>
    public interface IFakeEngine
    {
        /// <summary>
        /// Creates a fake of the type specified via <paramref name="interfaceType"/>.
        /// </summary>
        /// <param name="interfaceType">
        /// Specifies the interface type to create a fake for.
        /// </param>
        /// <returns>
        /// The created fake instance.
        /// </returns>
        object Stub(Type interfaceType);

        /// <summary>
        /// Creates a partial mock.
        /// </summary>
        /// <typeparam name="T">
        /// Specifies the type of the partial mock. This needs to be 
        /// an abstract base class.
        /// </typeparam>
        /// <param name="args">
        /// Specifies the constructor parameters.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        T PartialMock<T>(params  object[] args) where T : class;

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
        ///   Expression to set up the behavior.
        /// </param>
        /// <returns>
        ///   A <see cref = "IQueryOptions{TReturn}" /> for further configuration.
        /// </returns>
        IQueryOptions<TReturnValue> SetUpQueryBehaviorFor<TFake, TReturnValue>(
            TFake fake, 
            Expression<Func<TFake, TReturnValue>> func) where TFake : class;

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
        ICommandOptions SetUpCommandBehaviorFor<TFake>(
            TFake fake,
            Expression<Action<TFake>> func) where TFake : class;

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
        void VerifyBehaviorWasNotExecuted<TFake>(
            TFake fake,
            Expression<Action<TFake>> func) where TFake : class;

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
        IMethodCallOccurance VerifyBehaviorWasExecuted<TFake>(
            TFake fake, 
            Expression<Action<TFake>> func) where TFake : class ;

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
        IEventRaiser CreateEventRaiser<TFake>(
            TFake fake, 
            Action<TFake> assignement) where TFake : class;
    }
}