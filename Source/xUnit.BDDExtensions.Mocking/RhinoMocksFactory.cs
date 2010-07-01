using System;
using Rhino.Mocks;

namespace Xunit
{
    public class RhinoMocksFactory : IMockFactory
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