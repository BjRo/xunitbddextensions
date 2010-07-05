using System;
using Rhino.Mocks;

namespace Xunit
{
    public class RhinoMockingEngine : IMockingEngine
    {
        public object Stub(Type interfaceType)
        {
            var stub = MockRepository.GenerateStub(interfaceType);
            stub.Replay();
            return stub;
        }

        public T PartialMock<T>(params object[] args) where T : class
        {
            var mock = MockRepository.GenerateMock<T>(args);
            mock.Replay();
            return mock;
        }
    }
}