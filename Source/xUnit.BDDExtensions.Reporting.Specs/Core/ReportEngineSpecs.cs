using System.Collections.Generic;
using Xunit;
using Xunit.Reporting.Core;
using Xunit.Reporting.Core.Configuration;
using Xunit.Reporting.Core.Generator;

namespace Xunit.Reporting.Specs.Core
{
    [Concern(typeof (ReportEngine))]
    public class When_running_the_reporting_engine : InstanceContextSpecification<ReportEngine>
    {
        private const string nameofTargetAssembly = "My.Lovely.Assembly";
        private IReportGenerator generator;
        private IModelBuilder modelBuilder;
        private IArguments arguments;
        private IReport reportModel;

        protected override void EstablishContext()
        {
            generator = The<IReportGenerator>();
            modelBuilder = The<IModelBuilder>();
            arguments = The<IArguments>();
            reportModel = An<IReport>();

            arguments
                .WhenToldTo(args => args.Get(ArgumentKeys.TargetAssemblies))
                .Return(new List<string>
                {
                    nameofTargetAssembly
                });

            modelBuilder
                .WhenToldTo(mb => mb.BuildModel(nameofTargetAssembly))
                .Return(reportModel);
        }

        protected override void Because()
        {
            Sut.Run();
        }

        [Observation]
        public void Should_get_the_target_assemblies_from_the_arguments()
        {
            arguments.WasToldTo(args => args.Get(ArgumentKeys.TargetAssemblies));
        }

        [Observation]
        public void Should_build_a_report_model_for_each_configured_assembly()
        {
            modelBuilder.WasToldTo(mb => mb.BuildModel(nameofTargetAssembly));
        }

        [Observation]
        public void Should_generate_a_report_based_on_each_report_model()
        {
            generator.WasToldTo(gen => gen.Generate(reportModel));
        }
    }
}