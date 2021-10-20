using System.Collections.Generic;

namespace Prelude
{
    public static class IDictionaryExtensions
    {
        public static Option<TValue> GetValueIfPresent<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key) =>
            source.TryGetValue(key, out TValue value) ? Option.Some(value) : Option.None<TValue>();
    }
}
