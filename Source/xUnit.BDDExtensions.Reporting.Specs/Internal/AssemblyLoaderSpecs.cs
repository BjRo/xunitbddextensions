// Copyright 2009 Bj�rn Rochel - http://www.bjro.de/ 
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
using Xunit.Reporting.Internal;

namespace Xunit.Reporting.Specs.Internal
{
    [Concern(typeof (AssemblyLoader))]
    public class When_trying_to_load_an_assembly_by_the_assembly_name : InstanceContextSpecification<AssemblyLoader>
    {
        private IAssembly _loadedAssembly;
        private string _nameOfTheCurrentAssembly;

        protected override void EstablishContext()
        {
            _nameOfTheCurrentAssembly = GetType().Assembly.GetName().Name;
        }

        protected override void Because()
        {
            _loadedAssembly = Sut.Load(_nameOfTheCurrentAssembly);
        }

        [Observation]
        public void Should_be_able_to_load_the_assembly()
        {
            _loadedAssembly.ShouldNotBeNull();
        }
    }

    [Concern(typeof (AssemblyLoader))]
    public class When_trying_to_load_an_assembly_by_its_file_name : InstanceContextSpecification<AssemblyLoader>
    {
        private IAssembly _loadedAssembly;
        private string _locationOfMscorlib;

        protected override void EstablishContext()
        {
            _locationOfMscorlib = typeof(string).Assembly.Location;
        }

        protected override void Because()
        {
            _loadedAssembly = Sut.Load(_locationOfMscorlib);
        }

        [Observation]
        public void Should_be_able_to_load_the_assembly()
        {
            _loadedAssembly.ShouldNotBeNull();
        }
    }
}