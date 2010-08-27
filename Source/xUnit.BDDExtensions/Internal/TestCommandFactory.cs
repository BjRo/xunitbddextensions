using System;
using System.Collections.Generic;
using Xunit.Sdk;

namespace Xunit.Internal
{
    internal class TestCommandFactory
    {
        public IEnumerable<ITestCommand> CreateTestCommands(IMethodInfo testMethod)
        {
            var skipReason = MethodUtility.GetSkipReason(testMethod);

            if (skipReason != null)
            {
                yield return new SkipCommand(testMethod, MethodUtility.GetDisplayName(testMethod), skipReason);
            }
            else
            {
                foreach (var testCommand in MethodUtility.GetTestCommands(testMethod))
                {
                    yield return new SkipIfNotImplementedCommand(testMethod, testCommand);
                }
            }
        }
    }
}