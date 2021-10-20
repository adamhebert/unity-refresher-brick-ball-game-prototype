using System.Collections.Generic;

namespace Prelude
{
    public static class IDictionaryExtensions
    {
        /// <summary>
        /// Returns a possible element that matches the passed in key.
        /// </summary>
        /// <param name="key">The key to find in the Dictionary.</param>
        public static Option<TValue> GetValueIfPresent<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key) =>
            source.TryGetValue(key, out TValue value) ? Option.Some(value) : Option.None<TValue>();
    }
}
