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
using System.Text;

namespace Xunit.Reporting.Internal.Generator
{
    /// <summary>
    /// A report generator which simply creates an ASCI text file.
    /// </summary>
    public class TextReportGenerator : IReportGenerator
    {
        private static readonly Pluralizer Pluralizer = new Pluralizer();
        private readonly IFileWriter _fileWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextReportGenerator"/> class.
        /// </summary>
        /// <param name="fileWriter">The file writer.</param>
        public TextReportGenerator(IFileWriter fileWriter)
        {
            _fileWriter = fileWriter;
        }

        #region IReportGenerator Members

        /// <summary>
        /// Generates a report filled with the content supplied by <paramref name="report"/>.
        /// </summary>
        /// <param name="report">Specifies the report model.</param>
        public void Generate(IReport report)
        {
            var contentBuilder = CreateContent(report);

            _fileWriter.Write(
                string.Concat(report.ReflectedAssembly, ".txt"),
                writer => writer.Write(contentBuilder.ToString()));
        }

        #endregion

        private static string CreateContent(IReport report)
        {
            var reportBuilder = new StringBuilder();

            WriteHeader(report, reportBuilder);

            foreach (var concern in report)
            {
                WriteConcern(concern, reportBuilder);
            }

            return reportBuilder.ToString();
        }

        private static void WriteConcern(Concern concern, StringBuilder reportBuilder)
        {
            reportBuilder.AppendFormat(
                "{0} specifications {1}",
                concern,
                new string(' ', 4));

            reportBuilder.AppendFormat(
                "({0} {1}, {2} {3})",
                concern.AmountOfContexts,
                Pluralizer.Pluralize("context", concern.AmountOfContexts),
                concern.AmountOfObservations,
                Pluralizer.Pluralize("observation", concern.AmountOfObservations));

            reportBuilder.AppendLine();
            reportBuilder.AppendLine(new string('-', 100));
            reportBuilder.AppendLine();

            foreach (var context in concern)
            {
                WriteContext(context, reportBuilder);
            }

            reportBuilder.AppendLine();
        }

        private static void WriteContext(Context context, StringBuilder reportBuilder)
        {
            var indentation = new string(' ', 4);

            reportBuilder.AppendFormat(
                "{0}{1}{2}",
                indentation,
                context,
                new string(' ', 4));

            reportBuilder.AppendFormat(
                "({0} {1})",
                context.AmountOfObservations,
                Pluralizer.Pluralize("observation", context.AmountOfObservations));

            reportBuilder.AppendLine();
            
            foreach (var observation in context)
            {
                WriteObservation(observation, reportBuilder);
            }

            reportBuilder.AppendLine();
        }

        private static void WriteObservation(Observation observation, StringBuilder reportBuilder)
        {
            var indentation = new string(' ', 8);

            reportBuilder.AppendFormat(
                "{0}- {1}{2}",
                indentation,
                observation,
                Environment.NewLine);
        }

        private static void WriteHeader(IReport report, StringBuilder reportBuilder)
        {
            reportBuilder.AppendFormat(
                "{0}{1}",
                report.ReflectedAssembly,
                new string(' ', 4));

            reportBuilder.AppendFormat(
                "{0} {1}, {2} {3}, {4} {5}",
                report.TotalAmountOfConcerns,
                Pluralizer.Pluralize("concern", report.TotalAmountOfConcerns),
                report.TotalAmountOfContexts,
                Pluralizer.Pluralize("context", report.TotalAmountOfContexts),
                report.TotalAmountOfObservations,
                Pluralizer.Pluralize("observation", report.TotalAmountOfObservations));

            reportBuilder.AppendLine();
            reportBuilder.AppendLine(new string('=', 100));
            reportBuilder.AppendLine();
        }
    }
}