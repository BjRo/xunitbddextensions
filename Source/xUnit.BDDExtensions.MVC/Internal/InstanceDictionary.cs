using System.Collections.Generic;

namespace Xunit.Internal
{
    public class InstanceDictionary
    {
        private readonly Dictionary<object, Dictionary<string, object>> _dictionaries =  new Dictionary<object, Dictionary<string, object>>();

        public void Set<T>(object instance, string key, T value)
        {
            GetInstanceDictionary(instance)[key] = value;
        }

        public T Get<T>(object instance, string key)
        {
            var instanceDictionary = GetInstanceDictionary(instance);

            object value;

            instanceDictionary.TryGetValue(key, out value);

            return value != null ? (T) value : default(T);
        }

        private Dictionary<string, object> GetInstanceDictionary(object instance)
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