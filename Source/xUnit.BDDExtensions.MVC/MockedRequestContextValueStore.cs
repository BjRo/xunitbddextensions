using System.Collections.Generic;

namespace Xunit
{
    /// <summary>
    /// A helper class which enables you the use it in ExtensionMethods for IMockedRequestContext if
    /// you need to save some values for the test outside the RequestContext
    /// </summary>
    public static class MockedRequestContextValueStore
    {
        private static readonly Dictionary<IMockedRequestContext, Dictionary<string, object>> Dictionaries =
            new Dictionary<IMockedRequestContext, Dictionary<string, object>>();

        private static readonly object DictionariesLock = new object();

        /// <summary>
        /// Set a value for 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetValue<T>(this IMockedRequestContext context, string key, T value)
        {
            GetContextDictionary(context)[key] = value;
        }

        public static bool TryGetValue<T>(this IMockedRequestContext context, string key, out T value)
        {
            object dictValue;
            var instanceDictionary = GetContextDictionary(context);
            if (instanceDictionary.TryGetValue(key, out dictValue))
            {
                value = (T) dictValue;
                return true;
            }
            value = default(T);
            return false;
        }

        /// <summary>
        /// Get the value for a given key
        /// </summary>
        /// <remarks>
        /// throws <see cref="KeyNotFoundException()"/> if the key doesn't exists
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetValue<T>(this IMockedRequestContext context, string key)
        {
            T value;
            if (!context.TryGetValue(key, out value))
            {
                throw new KeyNotFoundException();
            }
            return value;
        }

        private static Dictionary<string, object> GetContextDictionary(IMockedRequestContext context)
        {
            Dictionary<string, object> instanceDictionary;

            lock (DictionariesLock)
            {
                if (!Dictionaries.TryGetValue(context, out instanceDictionary))
                {
                    instanceDictionary = new Dictionary<string, object>();
                    Dictionaries[context] = instanceDictionary;
                }
            }

            return instanceDictionary;
        }

        internal static void RemoveDictionary(IMockedRequestContext requestContext)
        {
            lock (DictionariesLock)
            {
                Dictionaries.Remove(requestContext);
            }
        }
    }
}