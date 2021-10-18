using System.Collections.Generic;
using Prelude;

public static class DictionaryExtensions
{
    public static Option<TValue> GetValueIfPresent<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key) =>
        source.TryGetValue(key, out TValue value) ? Option.Some(value) : Option.None<TValue>();
}
