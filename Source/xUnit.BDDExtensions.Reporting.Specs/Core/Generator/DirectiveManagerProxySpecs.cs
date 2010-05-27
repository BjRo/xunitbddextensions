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
using NVelocity.Runtime.Directive;
using Xunit.Reporting.Core;
using Xunit.Reporting.Core.Generator;

namespace Xunit.Reporting.Specs.Core.Generator
{
    [Concern(typeof (DirectiveManagerProxy))]
    public class When_a_directive_is_registered_inside_NVelocity_and_NVelocity_has_been_merged_into_BDDExtensions :
        InstanceContextSpecification<DirectiveManagerProxy>
    {
        private string assemblyQualifiedDirectiveName;
        private IDirectiveManager directiveManager;

        protected override void EstablishContext()
        {
            directiveManager = The<IDirectiveManager>();
            assemblyQualifiedDirectiveName = "NVelocity.Some.Class,NVelocity";

            The<IAssembly>().WhenToldTo(assembly => assembly.Name).Return("ReportGenerator");
        }

        protected override void Because()
        {
            Sut.Register(assemblyQualifiedDirectiveName);
        }

        [Observation]
        public void Should_replace_the_assembly_part_of_the_assembly_qualified_name_with_the_name_of_BDDExtensions()
        {
            var nameOfTheReportAssembly = typeof (IReportGenerator).Assembly.GetName().Name;
            var updatedName = string.Concat("NVelocity.Some.Class,", nameOfTheReportAssembly);

            directiveManager.WasToldTo(dm => dm.Register(updatedName));
        }
    }

    [Concern(typeof (DirectiveManagerProxy))]
    public class When_a_directive_is_registered_inside_NVelocity_and_NVelocity_was_not_merged_into_BDDExtensions :
        InstanceContextSpecification<DirectiveManagerProxy>
    {
        private IDirectiveManager directiveManager;
        private string velocityTypeName;

        protected override void EstablishContext()
        {
            directiveManager = The<IDirectiveManager>();
            velocityTypeName = "NVelocity.Some.Class,NVelocity";

            The<IAssembly>().WhenToldTo(assembly => assembly.Name).Return("NVelocity");
        }

        protected override void Because()
        {
            Sut.Register(velocityTypeName);
        }

        [Observation]
        public void Should_not_update_the_directives_assembly_qualified_name()
        {
            directiveManager.WasToldTo(dm => dm.Register(velocityTypeName));
        }
    }
}