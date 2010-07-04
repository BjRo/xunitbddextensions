using System.Collections.Generic;

namespace Xunit.Internal
{
    public class InstanceDictionary
    {
        private readonly Dictionary<object, Dictionary<string, object>> _dictionaries =
            new Dictionary<object, Dictionary<string, object>>();

        public void Set<T>(object instance, string key, T value)
        {
            GetCollection(instance)[key] = value;
        }

        public T Get<T>(object instance, string key)
        {
            return (T) GetCollection(instance)[key];
        }

        private Dictionary<string, object> GetCollection(object instance)
        {
            Dictionary<string, object> instanceDictionary;
            if (!_dictionaries.TryGetValue(instance, out instanceDictionary))
            {
                instanceDictionary = new Dictionary<string, object>();
                _dictionaries[instance] = instanceDictionary;
            }
            return instanceDictionary;
        }
    }
}