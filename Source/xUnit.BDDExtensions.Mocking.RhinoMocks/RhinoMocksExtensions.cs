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
using Rhino.Mocks;

namespace Xunit
{
    /// <summary>
    ///   An extension class for RhinoMocks.
    /// </summary>
    public static class RhinoMocksExtensions
    {
        /// <summary>
        ///   Asserts that the action specified by <paramref name = "action" /> was called
        ///   on the object specified by <paramref name = "dependency" />.
        /// </summary>
        /// <typeparam name = "T">
        ///   Specifies the type of the dependency.
        /// </typeparam>
        /// <param name = "dependency">
        ///   Specifies the instance on which the action should have been called.
        /// </param>
        /// <param name = "action">
        ///   Specifies the action that should have been called on the dependency.
        /// </param>
        public static MethodCallOccurance<T> WasToldTo<T>(this T dependency, Action<T> action)
        {
            return new MethodCallOccurance<T>(dependency, action);
        }

        ///// <summary>
        ///// Configures the behavior of the dependency specified by <paramref name="dependency"/>.
        ///// </summary>
        ///// <typeparam name="T">
        ///// Specifies the type of the dependency.
        ///// </typeparam>
        ///// <typeparam name="R">
        ///// Specifies the type of the return type of the configured method
        ///// </typeparam>
        ///// <param name="dependency">
        ///// The dependency to configure behavior on.
        ///// </param>
        ///// <param name="func">
        ///// Configures the behavior.
        ///// </param>
        ///// <returns>
        ///// A <see cref="IMethodOptions{T}"/> for further configuration.
        ///// </returns>
        //public static IMethodOptions<R> WhenToldTo<T, R>(this T dependency, Function<T, R> func) where T : class
        //{
        //    return dependency.Stub(func).Repeat.Any();
        //}

        ///// <summary>
        ///// Configures the behavior of the dependency specified by <paramref name="dependency"/>.
        ///// </summary>
        ///// <typeparam name="T">
        ///// Specifies the type of the dependency.
        ///// </typeparam>
        ///// <param name="dependency">
        ///// The dependency to configure behavior on.
        ///// </param>
        ///// <param name="func">
        ///// Configures the behavior. This must be a void method.
        ///// </param>
        ///// <returns>
        ///// A <see cref="IMethodOptions{R}"/> for further configuration.
        ///// </returns>
        //public static IMethodOptions<object> WhenToldTo<T>(this T dependency, Action<T> func) where T : class
        //{
        //    return dependency.Stub(func).Repeat.Any();
        //}

        /// <summary>
        ///   Asserts that the action specified by <paramref name = "action" /> was not called on the object specified by <paramref name = "dependency" />.
        /// </summary>
        /// <typeparam name = "T">
        ///   Specifies the type of the dependency.
        /// </typeparam>
        /// <param name = "dependency">
        ///   Specifies the instance on which the action should not have been called.
        /// </param>
        /// <param name = "action">
        ///   Specifies the action that should not have been called on the dependency.
        /// </param>
        public static void WasNotToldTo<T>(this T dependency, Action<T> action)
        {
            dependency.AssertWasNotCalled(action);
        }
    }
}