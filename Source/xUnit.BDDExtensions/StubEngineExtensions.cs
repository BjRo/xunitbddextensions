using System.Collections.Generic;
using System.Linq;

namespace Xunit
{
    public static class StubEngineExtensions
    {
        public static IList<TInterfaceType> CreateStubCollectionOf<TInterfaceType>(this IStubFactory stubFactory) where TInterfaceType : class 
        {
           return Enumerable.Range(0, 3).Select(x => stubFactory.CreateStub<TInterfaceType>()).ToList();
        }
    }
}