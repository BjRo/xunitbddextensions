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
using System.Collections.Generic;

namespace Xunit.Internal
{
    /// <summary>
    ///   A null implementation of the <see cref = "ICore" /> interface.
    /// </summary>
    internal class NulloCore : ICore
    {
        #region ICore Members

        public IMockingEngine MockingEngine { get; set; }

        public IEnumerable<IBuilder> Builders { get; set; }

        public IEnumerable<IConfigurationRule> ConfigurationRules { get; set; }

        public void EnsureConfigured()
        {
            throw new InvalidOperationException("xUnit.BDDExtensions was not configured !!!");
        }

        #endregion
    }
}