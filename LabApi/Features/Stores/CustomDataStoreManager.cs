using System;
using System.Collections.Generic;
using System.Reflection;
using LabApi.Features.Wrappers;

namespace LabApi.Features.Stores;

/// <summary>
/// Handles the registration and management of custom data stores.
/// </summary>
public static class CustomDataStoreManager
{
    private static readonly List<Type> RegisteredStores = new ();
    private static readonly Dictionary<Type, MethodInfo> GetOrAddMethods = new ();
    private static readonly Dictionary<Type, MethodInfo> DestroyMethods = new ();
    private static readonly Dictionary<Type, MethodInfo> DestroyAllMethods = new ();

    /// <summary>
    /// Registers a custom data store.
    /// </summary>
    /// <typeparam name="T">The type of the custom data store.</typeparam>
    /// <returns>Whether the store was successfully registered.</returns>
    public static bool RegisterStore<T>()
        where T : CustomDataStore
    {
        Type type = typeof(T);
        if (RegisteredStores.Contains(type)) return false;

        MethodInfo? getOrAddMethod = typeof(CustomDataStore).GetMethod(nameof(CustomDataStore.GetOrAdd), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        if (getOrAddMethod == null)
            return false;

        getOrAddMethod = getOrAddMethod.MakeGenericMethod(type);
        GetOrAddMethods.Add(type, getOrAddMethod);

        MethodInfo? destroyMethod = typeof(CustomDataStore).GetMethod(nameof(CustomDataStore.Destroy), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        if (destroyMethod == null)
            return false;

        destroyMethod = destroyMethod.MakeGenericMethod(type);
        DestroyMethods.Add(type, destroyMethod);

        MethodInfo? destroyAllMethod = typeof(CustomDataStore).GetMethod(nameof(CustomDataStore.DestroyAll), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        if (destroyAllMethod == null)
            return false;

        destroyAllMethod = destroyAllMethod.MakeGenericMethod(type);
        DestroyAllMethods.Add(type, destroyAllMethod);

        RegisteredStores.Add(type);

        return true;
    }

    /// <summary>
    /// Unregisters a custom data store.
    /// </summary>
    /// <typeparam name="T">The type of the custom data store.</typeparam>
    public static void UnregisterStore<T>()
        where T : CustomDataStore
    {
        Type type = typeof(T);
        
        if (DestroyAllMethods.TryGetValue(type, out MethodInfo? method))
            method.Invoke(null, null);

        DestroyAllMethods.Remove(type);
        RegisteredStores.Remove(type);
        GetOrAddMethods.Remove(type);
        DestroyMethods.Remove(type);
    }

    internal static void AddPlayer(Player player)
    {
        foreach (Type? storeType in RegisteredStores)
            GetOrAddMethods[storeType].Invoke(null, [player]);
    }

    internal  static void RemovePlayer(Player player)
    {
        foreach (Type? storeType in RegisteredStores)
            DestroyMethods[storeType].Invoke(null, [player]);
    }

    internal static bool IsRegistered(Type type) => RegisteredStores.Contains(type);

    internal static bool IsRegistered<T>() => IsRegistered(typeof(T));
}