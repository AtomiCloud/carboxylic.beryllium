namespace CarboxylicBeryllium;

public static class DictionaryUtility
{
    /// <summary>
    /// Convert Dictionary's value to nullable
    /// </summary>
    /// <param name="od">Dictionary</param>
    /// <typeparam name="TK">Key Type</typeparam>
    /// <typeparam name="TV">Value Type</typeparam>
    /// <returns></returns>
    public static Dictionary<TK, TV?> ToNullable<TK, TV>(this IDictionary<TK, TV> od)
        where TK : notnull
    {
        return od.ToDictionary(kvp => kvp.Key, TV? (kvp) => kvp.Value);
    }

    /// <summary>
    /// Get Reference to Value in Dictionary in null-safe way (returns nullable)
    /// </summary>
    /// <param name="d">Dictionary</param>
    /// <param name="key">Key to get</param>
    /// <typeparam name="TK">Key Type</typeparam>
    /// <typeparam name="TV">Value Type</typeparam>
    /// <returns></returns>
    public static TV? GetRef<TK, TV>(this Dictionary<TK, TV> d, TK key)
        where TV : class
        where TK : notnull
    {
        return d.ToNullable().GetValueOrDefault(key, null);
    }

    /// <summary>
    /// Get Value in Dictionary in null-safe way (returns nullable)
    /// </summary>
    /// <param name="d">Dictionary</param>
    /// <param name="key">Key to get</param>
    /// <typeparam name="TK">Key Type</typeparam>
    /// <typeparam name="TV">Value Type</typeparam>
    /// <returns></returns>
    public static TV? GetVal<TK, TV>(this Dictionary<TK, TV> d, TK key)
        where TV : struct, IComparable
        where TK : notnull
    {
        return d.ToNullable().GetValueOrDefault(key, default);
    }

    /// <summary>
    /// Maps the values of a dictionary to a new type using the provided mapping function.
    /// </summary>
    /// <typeparam name="K">The type of the keys in the dictionary, which cannot be null.</typeparam>
    /// <typeparam name="V">The type of the original values in the dictionary.</typeparam>
    /// <typeparam name="NV">The type of the new values in the resulting dictionary, which cannot be null.</typeparam>
    /// <param name="d">The source dictionary whose values will be mapped.</param>
    /// <param name="map">A function that defines how to map each value from the original to a new value.</param>
    /// <returns>A new dictionary with the same keys but new values mapped from the original values.</returns>
    public static Dictionary<K, NV> MapVal<K, V, NV>(this Dictionary<K, V> d, Func<V, NV> map)
        where K : notnull
        where NV : notnull
    {
        return d.ToDictionary(pair => pair.Key, pair => map(pair.Value));
    }

    /// <summary>
    /// Maps the keys of a dictionary to a new type using the provided mapping function, taking both key and value as input.
    /// </summary>
    /// <typeparam name="K">The type of the original keys in the dictionary, which cannot be null.</typeparam>
    /// <typeparam name="V">The type of the values in the dictionary.</typeparam>
    /// <typeparam name="NK">The type of the new keys in the resulting dictionary, which cannot be null.</typeparam>
    /// <param name="d">The source dictionary whose keys will be mapped.</param>
    /// <param name="map">A function that defines how to map each key using both the original key and its associated value.</param>
    /// <returns>A new dictionary with new keys mapped from the original keys and the same values.</returns>
    public static Dictionary<NK, V> MapKey<K, V, NK>(this Dictionary<K, V> d, Func<K, V, NK> map)
        where K : notnull
        where NK : notnull
    {
        return d.ToDictionary(pair => map(pair.Key, pair.Value), pair => pair.Value);
    }

    /// <summary>
    /// Maps the keys of a dictionary to a new type using the provided mapping function, taking only the key as input.
    /// </summary>
    /// <typeparam name="TK">The type of the original keys in the dictionary, which cannot be null.</typeparam>
    /// <typeparam name="TV">The type of the values in the dictionary.</typeparam>
    /// <typeparam name="TNk">The type of the new keys in the resulting dictionary, which cannot be null.</typeparam>
    /// <param name="d">The source dictionary whose keys will be mapped.</param>
    /// <param name="map">A function that defines how to map each key from the original to a new key.</param>
    /// <returns>A new dictionary with new keys mapped from the original keys and the same values.</returns>
    public static Dictionary<TNk, TV> MapKey<TK, TV, TNk>(
        this Dictionary<TK, TV> d,
        Func<TK, TNk> map
    )
        where TK : notnull
        where TNk : notnull
    {
        return d.ToDictionary(pair => map(pair.Key), pair => pair.Value);
    }

    /// <summary>
    /// Creates a dictionary from a collection by using the elements themselves as keys and mapping them to values using the provided function.
    /// </summary>
    /// <typeparam name="TK">The type of the values in the resulting dictionary, which cannot be null.</typeparam>
    /// <typeparam name="T">The type of the elements in the source collection, which cannot be null.</typeparam>
    /// <param name="c">The source collection of elements.</param>
    /// <param name="valFunc">A function that defines how to map each element to a value in the dictionary.</param>
    /// <returns>A dictionary with elements from the collection as keys and mapped values.</returns>
    public static Dictionary<T, TK> ChooseVal<TK, T>(this IEnumerable<T> c, Func<T, TK> valFunc)
        where TK : notnull
        where T : notnull
    {
        return c.ToDictionary(e => e, valFunc);
    }

    /// <summary>
    /// Creates a dictionary from a collection by mapping each element to a key using the provided function and using the elements themselves as values.
    /// </summary>
    /// <typeparam name="TK">The type of the keys in the resulting dictionary, which cannot be null.</typeparam>
    /// <typeparam name="T">The type of the elements in the source collection.</typeparam>
    /// <param name="c">The source collection of elements.</param>
    /// <param name="keyFunc">A function that defines how to map each element to a key in the dictionary.</param>
    /// <returns>A dictionary with mapped keys and elements from the collection as values.</returns>
    public static Dictionary<TK, T> ChooseKey<TK, T>(this IEnumerable<T> c, Func<T, TK> keyFunc)
        where TK : notnull
    {
        return c.ToDictionary(keyFunc, e => e);
    }

    /// <summary>
    /// Inverts a dictionary by swapping each key with its corresponding value.
    /// </summary>
    /// <typeparam name="TK">The type of the keys in the original dictionary, which cannot be null.</typeparam>
    /// <typeparam name="TV">The type of the values in the original dictionary, which cannot be null.</typeparam>
    /// <param name="o">The source dictionary to be inverted.</param>
    /// <returns>A new dictionary where each value of the original dictionary becomes a key, and each key becomes a value.</returns>
    /// <remarks>
    /// If the original dictionary contains duplicate values, an exception will be thrown because keys must be unique.
    /// </remarks>
    public static Dictionary<TV, TK> Invert<TK, TV>(this Dictionary<TK, TV> o)
        where TV : notnull
        where TK : notnull
    {
        return o.ToDictionary(pair => pair.Value, pair => pair.Key);
    }

    /// <summary>
    /// Merges elements of a collection into a dictionary under a common key, with values grouped as collections.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <typeparam name="K">The type of the keys in the resulting dictionary, which cannot be null.</typeparam>
    /// <typeparam name="V">The type of the values in the value collections of the dictionary.</typeparam>
    /// <param name="collection">The source collection of elements to group into a dictionary.</param>
    /// <param name="keyFunc">A function to map each element to a key in the dictionary.</param>
    /// <param name="valFunc">A function to map each element to a value in the value collections of the dictionary.</param>
    /// <returns>A dictionary where each key maps to a collection of values produced by the elements that share that key.</returns>
    public static Dictionary<K, IEnumerable<V>> GroupByKeys<T, K, V>(
        this IEnumerable<T> collection,
        Func<T, K> keyFunc,
        Func<T, V> valFunc
    )
        where K : notnull
    {
        var ret = new Dictionary<K, IEnumerable<V>>();

        foreach (var element in collection)
        {
            var (k, v) = (keyFunc(element), valFunc(element));
            if (!ret.ContainsKey(k))
                ret[k] = (List<V>)[];
            (ret[k] as List<V>)?.Add(v);
        }

        return ret;
    }
}
