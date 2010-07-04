using System.Collections.Generic;

namespace Xunit.Internal
{
    public class InstanceDictionary
    {
        private readonly Dictionary<object, Dictionary<string, object>> _dictionaries =  new Dictionary<object, Dictionary<string, object>>();

        public void SetValue<T>(object instance, string key, T value)
        {
            GetInstanceDictionary(instance)[key] = value;
        }

        public bool TryGetValue<T>(object instance, string key, out T value)
        {
            object dictValue;
            var instanceDictionary = GetInstanceDictionary(instance);
             if (instanceDictionary.TryGetValue(key, out dictValue))
             {
                 value = (T) dictValue;
                 return true;
             }
            value = default(T);
            return false;
        }

        public T GetValue<T>(object instance, string key)
        {
            T value;
            if (!TryGetValue(instance, key, out value))
            {
                throw new KeyNotFoundException();
            }
            return value;
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