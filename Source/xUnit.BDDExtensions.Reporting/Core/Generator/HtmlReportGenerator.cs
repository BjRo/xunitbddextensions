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
using System.Resources;
using Commons.Collections;
using NVelocity;
using NVelocity.App;
using NVelocity.Runtime.Resource;
using NVelocity.Runtime.Resource.Loader;
using System;

namespace Xunit.Reporting.Core.Generator
{
    /// <summary>
    /// An report generator wich generates an HTML file.
    /// </summary>
    public class HtmlReportGenerator : IReportGenerator
    {
        private readonly IFileWriter outputWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlReportGenerator"/> class.
        /// </summary>
        /// <param name="outputWriter">The output writer.</param>
        public HtmlReportGenerator(IFileWriter outputWriter)
        {
            this.outputWriter = outputWriter;
        }

        #region IReportGenerator Members

        /// <summary>
        /// Generates a report filled with the content supplied by <paramref name="report"/>.
        /// </summary>
        /// <param name="report">Specifies the report model.</param>
        public void Generate(IReport report)
        {
            var engine = new VelocityEngine();
            var properties = new ExtendedProperties();
            properties.AddProperty("resource.loader", "assembly");
            properties.AddProperty("resource.manager.class", typeof(ResourceManagerImpl).AssemblyQualifiedName);
            properties.AddProperty("directive.manager", EncodeExternalAssemblyQualifiedType(typeof(DirectiveManagerProxy)));
            properties.AddProperty("assembly.resource.loader.class", typeof(AssemblyResourceLoader).AssemblyQualifiedName);
            properties.AddProperty("assembly.resource.loader.assembly", GetType().Assembly.GetName().Name);
            engine.Init(properties);

            var htmlTemplate = engine.GetTemplate("Xunit.Reporting.Core.Generator.HtmlTemplate.vm");
            var generationContext = new VelocityContext();
            generationContext.Put("report", report);
            generationContext.Put("pluralizer", new Pluralizer());

            outputWriter.Write(
                string.Concat(report.ReflectedAssembly, ".html"),
                writer => htmlTemplate.Merge(generationContext, writer));
        }

        #endregion

        /// <remarks>
        /// NVelocity Hack: Encoding an assembly qualified name of a type residing in an assembly
        /// other than NVelocity. (Normally strips the assembly part away from the name which leads to 
        /// problems when the assemblies have not been merged together.) 
        /// </remarks>
        private static string EncodeExternalAssemblyQualifiedType(Type type)
        {
            return type.AssemblyQualifiedName.Replace(',', ';');
        }
    }
}