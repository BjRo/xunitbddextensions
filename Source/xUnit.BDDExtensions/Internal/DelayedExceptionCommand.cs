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
using Xunit.Sdk;

namespace Xunit.Internal
{
    public class DelayedExceptionCommand : TestCommand
    {
        private readonly Exception _ex;

        public DelayedExceptionCommand(Exception ex, IMethodInfo method) : base(method, MethodUtility.GetDisplayName(method), 0)
        {
            _ex = ex;
        }

        public override MethodResult Execute(object testClass)
        {
            return new FailedResult(testMethod, _ex, DisplayName);
        }
    }
}