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
using System.Collections.Generic;
using Xunit.Reporting.Core;

namespace Xunit.Reporting.Specs.Core
{
    [Concern(typeof (ModelBuilder))]
    public class After_a_report_model_has_been_build_from_an_assembly : InstanceContextSpecification<ModelBuilder>
    {
        private string nameOfTargetAssembly;
        private IReport reportModel;

        protected override void EstablishContext()
        {
            nameOfTargetAssembly = "SomeAssembly";
            var assemblyLoader = The<IAssemblyLoader>();
            var assembly = An<IAssembly>();

            assemblyLoader
                .WhenToldTo(loader => loader.Load(nameOfTargetAssembly))
                .Return(assembly);

            assembly
                .WhenToldTo(a => a.AllTypesMatching(Context.Specification))
                .Return(new List<Type>
                {
                    typeof (After_this__fake__specification_has_been_executed)
                });

            assembly
                .WhenToldTo(a => a.Name)
                .Return(nameOfTargetAssembly);
        }

        protected override void Because()
        {
            reportModel = Sut.BuildModel(nameOfTargetAssembly);
        }

        [Observation]
        public void It_should_precisely_indicate_how_many__Context__specifications_have_been_found()
        {
            reportModel.TotalAmountOfContexts.ShouldBeEqualTo(1);
        }

        [Observation]
        public void It_should_precisely_indicate_how_many__Concerns__have_been_found()
        {
            reportModel.TotalAmountOfConcerns.ShouldBeEqualTo(1);
        }

        [Observation]
        public void It_should_precisely_indicate_how_many__Observations_have_been_found()
        {
            reportModel.TotalAmountOfObservations.ShouldBeEqualTo(1);
        }

        [Observation]
        public void it_should_reference_the_short_name_of_the_assembly_it_was_build_from()
        {
            reportModel.ReflectedAssembly.ShouldBeEqualTo(nameOfTargetAssembly);
        }
    }
}