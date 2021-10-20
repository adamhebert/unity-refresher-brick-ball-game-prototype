using System;
using System.Collections.Generic;

namespace Prelude
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Performs a specific action on every IEnumerable element.
        /// </summary>
        /// <param name="action">Called on all elements of the IEnumerable</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            // Maybe do this via IEnumerator instead...examine performance.
            foreach (var e in enumerable)
            {
                action(e);
            }
        }
    }
}
