// Copyright 2010 Björn Rochel - http://www.bjro.de/ 
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

using System.IO;
using System.Text;

namespace Xunit
{
    /// <summary>
    /// A specialized <see cref="Sdk.AssertException"/> which filters out all unnecessary StackTrace information from
    /// the original exception. This is done in order to strip the StackTrace parts from the message which shows methods
    /// inside the <see cref="BDDExtensions"/> class.
    /// </summary>
    public class AssertException : Sdk.AssertException
    {
        /// <summary>
        /// Creates an instance of <see cref="AssertException"/>.
        /// </summary>
        /// <param name="original">
        /// The exception thrown by the xUnit core.
        /// </param>
        public AssertException(Sdk.AssertException original) : base(original.Message)
        {
        }

        /// <summary>
        /// Gets the stack trace of the exception
        /// </summary>
        public override string StackTrace
        {
            get
            {
                return Filter(base.StackTrace);
            }
        }

        /// <summary>
        /// Removes all lines that occur inside <see cref="BDDExtensions"/> class and which do not start with a should.
        /// </summary>
        private static string Filter(string stackTrace)
        {
            var sb = new StringBuilder();
            var sr = new StringReader(stackTrace);

            return stackTrace;
        }
    }
}