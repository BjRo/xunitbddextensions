using System;

namespace Xunit
{
    public interface IAutoStubber<TTypeToStub> : IStubFactory
    {
        TTypeToStub BuildInstance();
        
        T GetSingleton<T>() where T : class;
        object GetSingleton(Type serviceType);
        
        void Inject(Type type, object instance);
    }
}