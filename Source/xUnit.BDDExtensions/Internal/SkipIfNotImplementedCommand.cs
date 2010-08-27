//  Copyright 2010 xUnit.BDDExtensions
//    
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License. 
//  You may obtain a copy of the License at
//    
//        http://www.apache.org/licenses/LICENSE-2.0
//    
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
//  implied. See the License for the specific language governing permissions and
//  limitations under the License.  
//  
using System;
using Xunit.Sdk;

namespace Xunit.Internal
{
    public class SkipIfNotImplementedCommand : TestCommand
    {
        private readonly ITestCommand _innerCommand;

        public SkipIfNotImplementedCommand(IMethodInfo method, ITestCommand innerCommand)
            : base(method, MethodUtility.GetDisplayName(method), MethodUtility.GetTimeoutParameter(method))
        {
            _innerCommand = innerCommand;
        }

        public override MethodResult Execute(object testClass)
        {
            try
            {
                return _innerCommand.Execute(testClass);
            }
            catch (ObservationNotImplementedException)
            {
                return new SkipResult(testMethod, DisplayName, "Not implemented yet");
            }
        }
    }
}