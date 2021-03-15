using System;
using System.Collections.Generic;

namespace Util
{
    public static class DictionaryExtensions
    {
        public static TValue ComputeIfAbsent<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TKey, TValue> compute) 
        {
            if (dict.TryGetValue(key, out var value)) return value;
            value = compute.Invoke(key);
            dict.Add(key, value);
            return value;
        }
    }
}