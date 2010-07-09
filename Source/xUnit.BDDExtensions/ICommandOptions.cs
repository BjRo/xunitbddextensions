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

namespace Xunit
{
    /// <summary>
    ///   Defines a mock framework independent fluent interface for setting up behavior
    ///   for methods returning void (commands)
    /// </summary>
    public interface ICommandOptions
    {
        /// <summary>
        ///   Configures that the invocation of the related behavior
        ///   results in the specified <see cref = "Exception" /> beeing thrown.
        /// </summary>
        /// <param name = "exception">
        ///   Specifies the exception which should be thrown when the 
        ///   behavior is invoked.
        /// </param>
        void Throw(Exception exception);

        /// <summary>
        /// Configures that the invocation of the related behavior
        /// results in a callback.
        /// </summary>
        /// <param name="action">
        /// Specifies the Action which should be called when the
        /// behavior is invoked
        /// </param>
        void Callback(Action action);

        /// <summary>
        /// Configures that the invocation of the related behavior
        /// results in a callback.
        /// </summary>
        /// <param name="action">
        /// Specifies the Action which should be called when the
        /// behavior is invoked
        /// </param>
        void Callback<T1>(Action<T1> action);

        /// <summary>
        /// Configures that the invocation of the related behavior
        /// results in a callback.
        /// </summary>
        /// <param name="action">
        /// Specifies the Action which should be called when the
        /// behavior is invoked
        /// </param>
        void Callback<T1, T2>(Action<T1, T2> action);

        /// <summary>
        /// Configures that the invocation of the related behavior
        /// results in a callback.
        /// </summary>
        /// <param name="action">
        /// Specifies the Action which should be called when the
        /// behavior is invoked
        /// </param>
        void Callback<T1, T2, T3>(Action<T1, T2, T3> action);

        /// <summary>
        /// Configures that the invocation of the related behavior
        /// results in a callback.
        /// </summary>
        /// <param name="action">
        /// Specifies the Action which should be called when the
        /// behavior is invoked
        /// </param>
        void Callback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action);
    }
}