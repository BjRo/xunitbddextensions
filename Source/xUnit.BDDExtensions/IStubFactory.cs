namespace Xunit
{
    public interface IStubFactory
    {
        T CreateStub<T>() where T : class;
    }
}