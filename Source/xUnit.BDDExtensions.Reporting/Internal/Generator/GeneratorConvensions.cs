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
using System;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using StructureMap.TypeRules;

namespace Xunit.Reporting.Internal.Generator
{
    /// <summary>
    /// Header a convention class for the StructureMap container 
    /// which defines the following convention:
    /// Any class in this assembly implementing <see cref="IReportGenerator"/>
    /// and suffixed with "ReportGenerator" while be automatically registered
    /// with its prefix in the container. For instance HtmlReportGenerator
    /// will be registered under the name "Html" because of this convention.
    /// </summary>
    public class GeneratorConvensions : IRegistrationConvention
    {
        #region ITypeScanner Members

        public void Process(Type type, Registry registry)
        {
            if (!type.IsConcreteAndAssignableTo(typeof (IReportGenerator)))
            {
                return;
            }

            if (!type.Name.EndsWith("ReportGenerator"))
            {
                return;
            }

            var generatorName = type.Name.Remove(type.Name.IndexOf("ReportGenerator"));

            registry.AddType(typeof (IReportGenerator), type, generatorName);
        }

        #endregion
    }
}
