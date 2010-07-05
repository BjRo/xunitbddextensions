using System.Collections.Generic;
using Xunit;
using Xunit.Reporting.Internal;
using Xunit.Reporting.Internal.Configuration;
using Xunit.Reporting.Internal.Generator;

namespace Xunit.Reporting.Specs.Internal
{
    [Concern(typeof (ReportEngine))]
    public class When_running_the_reporting_engine : InstanceContextSpecification<ReportEngine>
    {
        private const string NameofTargetAssembly = "My.Lovely.Assembly";
        private IReportGenerator _generator;
        private IModelBuilder _modelBuilder;
        private IArguments _arguments;
        private IReport _reportModel;

        protected override void EstablishContext()
        {
            _generator = The<IReportGenerator>();
            _modelBuilder = The<IModelBuilder>();
            _arguments = The<IArguments>();
            _reportModel = An<IReport>();

            _arguments
                .WhenToldTo(args => args.Get(ArgumentKeys.TargetAssemblies))
                .Return(new List<string>
                {
                    NameofTargetAssembly
                });

            _modelBuilder
                .WhenToldTo(mb => mb.BuildModel(NameofTargetAssembly))
                .Return(_reportModel);
        }

        protected override void Because()
        {
            Sut.Run();
        }

        [Observation]
        public void Should_get_the_target_assemblies_from_the_arguments()
        {
            _arguments.WasToldTo(args => args.Get(ArgumentKeys.TargetAssemblies));
        }

        [Observation]
        public void Should_build_a_report_model_for_each_configured_assembly()
        {
            _modelBuilder.WasToldTo(mb => mb.BuildModel(NameofTargetAssembly));
        }

        [Observation]
        public void Should_generate_a_report_based_on_each_report_model()
        {
            _generator.WasToldTo(gen => gen.Generate(_reportModel));
        }
    }
}