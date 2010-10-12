using System.Reflection;
using Xunit;
using Xunit.Faking.Moq;

[assembly: AssemblyTitle("xUnit.BDDExtensions.Mocking.Moq.Specs")]
[assembly: RunnerConfiguration(FakeEngineType = typeof(MoqFakeEngine))]