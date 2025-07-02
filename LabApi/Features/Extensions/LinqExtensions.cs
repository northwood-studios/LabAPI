using System;
using System.Collections.Generic;
using System.Linq;

namespace LabApi.Features.Extensions;

/// <summary>
/// Extensions for Linq.
/// </summary>
public static class LinqExtensions
{
    /// <summary>
    /// Finds smallest value by a specified key. 
    /// If 2 or more occurances have the same minimum value then the first one in the collection is returned.
    /// </summary>
    /// <typeparam name="T">The type to compare and select.</typeparam>
    /// <typeparam name="K">The value to compare the keys.</typeparam>
    /// <param name="enumerable">The enumerable collection.</param>
    /// <param name="selectFunc">The selection function.</param>
    /// <returns>Minimum value or <see langword="null"/> if the collection is empty.</returns>
    public static T? MinBy<T, K>(this IEnumerable<T> enumerable, Func<T, K> selectFunc) where T : class where K : IComparable
    {
        if (!enumerable.Any())
            return null;

        T? currentMin = enumerable.First();
        foreach (T member in enumerable)
        {
            if (selectFunc(member).CompareTo(selectFunc(currentMin)) < 0)
                currentMin = member;
        }

        return currentMin;
    }

    /// <summary>
    /// Finds largest value by a specified key. 
    /// If 2 or more occurances have the same minimum value then the first one in the collection is returned.
    /// </summary>
    /// <typeparam name="T">The type to compare and select.</typeparam>
    /// <typeparam name="K">The value to compare the keys.</typeparam>
    /// <param name="enumerable">The enumerable collection.</param>
    /// <param name="selectFunc">The selection function.</param>
    /// <returns>Maximum value or <see langword="null"/> if the collection is empty.</returns>
    public static T? MaxBy<T, K>(this IEnumerable<T> enumerable, Func<T, K> selectFunc) where T : class where K : IComparable
    {
        if (!enumerable.Any())
            return null;

        T? currentMin = enumerable.First();
        foreach (T member in enumerable)
        {
            if (selectFunc(member).CompareTo(selectFunc(currentMin)) > 0)
                currentMin = member;
        }

        return currentMin;
    }
}
