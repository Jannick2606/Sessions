using Newtonsoft.Json;

namespace Sessions
{
    /// <summary>
    /// Class with 2 methods that gets and sets an object as json
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// Sets a value in the session
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetObjectAsJson(this ISession session, string key,
        object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        /// <summary>
        /// Gets an object from the session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T? GetObjectFromJson<T>(this ISession session, string key)
        {
           
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
