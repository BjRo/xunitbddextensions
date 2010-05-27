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
using System.Transactions;

namespace Xunit
{
    /// <summary>
    /// A scoped instance specification. Creates a <see cref="TransactionScope"/> before each 
    /// observation which is rolled back after each observation.
    /// </summary>
    /// <typeparam name="SystemUnderTest">
    /// Specififies type of the system under test.
    /// </typeparam>
    public abstract class ScopedContextSpecification<SystemUnderTest> : InstanceContextSpecification<SystemUnderTest> where SystemUnderTest : class
    {
        private TransactionScope scope;

        /// <summary>
        /// Establishes the context for the specification. In AAA terms this
        /// method implements the Arange part.
        /// </summary>
        protected override void EstablishContext()
        {
            scope = new TransactionScope();
        }

        /// <summary>
        /// Is called after each observation. Can be used for cleanup.
        /// </summary>
        protected override void AfterEachObservation()
        {
            base.AfterEachObservation();
            scope.Dispose();
        }
    }
}