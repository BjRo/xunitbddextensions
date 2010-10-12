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
using Rhino.Mocks.Interfaces;

namespace Xunit.Internal
{
    public class RhinoCommandOptions : ICommandOptions
    {
        private readonly IMethodOptions<object> _methodOptions;

        public RhinoCommandOptions(IMethodOptions<object> methodOptions)
        {
            Guard.AgainstArgumentNull(methodOptions, "methodOptions");

            _methodOptions = methodOptions;
        }

        #region ICommandOptions Members

        public void Throw(Exception exception)
        {
            _methodOptions.Throw(exception);
        }

        public void Callback(Action callback)
        {
            _methodOptions.Callback(() =>
            {
                callback();
                return true;
            });
        }

        public void Callback<T>(Action<T> callback)
        {
            _methodOptions.Callback<T>(p =>
            {
                callback(p);
                return true;
            });
        }

        public void Callback<T1, T2>(Action<T1, T2> callback)
        {
            _methodOptions.Callback<T1, T2>((p1,p2) =>
            {
                callback(p1, p2);
                return true;
            });
        }

        public void Callback<T1, T2, T3>(Action<T1, T2, T3> callback)
        {
            _methodOptions.Callback<T1, T2, T3>((p1, p2, p3) =>
            {
                callback(p1, p2, p3);
                return true;
            });
        }

        public void Callback<T1, T2, T3, T4>(Action<T1, T2, T3, T4> callback)
        {
            _methodOptions.Callback<T1, T2, T3, T4>((p1, p2, p3, p4) =>
            {
                callback(p1, p2, p3, p4);
                return true;
            });
        }

        #endregion
    }
}
