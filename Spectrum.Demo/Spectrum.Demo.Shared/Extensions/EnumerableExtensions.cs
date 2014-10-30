using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spectrum.Demo.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool None<T>(this IEnumerable<T> values)
        {
            return !values.Any();
        }

        public static bool None<T>(this IEnumerable<T> values, Func<T, bool> predicate)
        {
            return !values.Any(predicate);
        }

        public static async Task<IEnumerable<TResult>> SelectAsync<TSource, TResult>(this IEnumerable<TSource> values, Func<TSource, Task<TResult>> asyncSelector)
        {
            return await Task.WhenAll(values.Select(asyncSelector));
        }

        public static async Task WhenAllAsync<T>(this IEnumerable<T> values, Func<T, Task> asyncMethod)
        {
            await Task.WhenAll(values.Select(asyncMethod));
        }
    }
}
