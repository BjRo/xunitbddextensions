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
using System.Diagnostics;
using Xunit.Internal;
using Xunit.Reporting.Internal.Configuration;
using Xunit.Reporting.Internal.Generator;

namespace Xunit.Reporting.Internal
{
    /// <summary>
    ///   The report engine which implements the main coordination part of this tool.
    /// </summary>
    public class ReportEngine : IReportEngine
    {
        private readonly IArguments _arguments;
        private readonly IModelBuilder _modelBuilder;
        private readonly IReportGenerator _reportGenerator;

        /// <summary>
        ///   Initializes a new instance of the <see cref = "ReportEngine" /> class.
        /// </summary>
        /// <param name = "arguments">
        ///   Specifies the arguments.
        /// </param>
        /// <param name = "modelBuilder">
        ///   Specifies the model builder for building a report model from an assembly.</param>
        /// <param name = "reportGenerator">
        ///   Specifies the report generator which generates the output.
        /// </param>
        /// <exception cref = "ArgumentNullException">
        ///   Thrown when one of the parameters is <c>null</c>.
        /// </exception>
        public ReportEngine(IArguments arguments, IModelBuilder modelBuilder, IReportGenerator reportGenerator)
        {
            Guard.AgainstArgumentNull(arguments, "arguments");
            Guard.AgainstArgumentNull(modelBuilder, "modelBuilder");
            Guard.AgainstArgumentNull(reportGenerator, "reportGenerator");

            _arguments = arguments;
            _modelBuilder = modelBuilder;
            _reportGenerator = reportGenerator;
        }

        #region IReportEngine Members

        /// <summary>
        ///   Runs the report generation.
        /// </summary>
        public void Run()
        {
            foreach (var targetAssembly in _arguments.Get(ArgumentKeys.TargetAssemblies))
            {
                var reportModel = _modelBuilder.BuildModel(targetAssembly);

                Debug.Assert(reportModel != null);

                _reportGenerator.Generate(reportModel);
            }
        }

        #endregion
    }
}