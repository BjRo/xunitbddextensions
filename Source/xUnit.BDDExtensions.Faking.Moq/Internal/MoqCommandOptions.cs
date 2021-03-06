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
using Moq.Language.Flow;
using Xunit.Internal;

namespace Xunit.Faking.Moq.Internal
{
    public class MoqCommandOptions<TFake> : ICommandOptions where TFake : class
    {
        private readonly ISetup<TFake> _methodOptions;

        public MoqCommandOptions(ISetup<TFake> methodOptions)
        {
            Guard.AgainstArgumentNull(methodOptions, "methodOptions");

            _methodOptions = methodOptions;
        }

        #region ICommandOptions Members

        public void Throw(Exception exception)
        {
            _methodOptions.Throws(exception);
        }

        public void Callback(Action callback)
        {
            _methodOptions.Callback(callback);
        }

        public void Callback<T>(Action<T> callback)
        {
            _methodOptions.Callback(callback);
        }

        public void Callback<T1, T2>(Action<T1, T2> callback)
        {
            _methodOptions.Callback(callback);
        }

        public void Callback<T1, T2, T3>(Action<T1, T2, T3> callback)
        {
            _methodOptions.Callback(callback);
        }

        public void Callback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> callback)
        {
            _methodOptions.Callback(callback);
        }

        #endregion
    }
}