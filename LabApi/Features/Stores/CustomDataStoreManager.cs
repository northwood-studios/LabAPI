using LabApi.Features.Wrappers;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace LabApi.Features.Stores;

/// <summary>
/// Handles the registration and management of custom data stores.
/// </summary>
public static class CustomDataStoreManager
{
    private static readonly List<Type> RegisteredStores = [];
    private static readonly Dictionary<Type, MethodInfo> GetOrAddMethods = [];
    private static readonly Dictionary<Type, MethodInfo> DestroyMethods = [];
    private static readonly Dictionary<Type, MethodInfo> DestroyAllMethods = [];

    /// <summary>
    /// Registers a custom data store.
    /// </summary>
    /// <typeparam name="T">The type of the custom data store.</typeparam>
    /// <returns>Whether the store was successfully registered.</returns>
    public static bool RegisterStore<T>()
        where T : CustomDataStore
    {
        Type type = typeof(T);
        if (RegisteredStores.Contains(type))
        {
            return false;
        }

        MethodInfo? getOrAddMethod = typeof(CustomDataStore).GetMethod(nameof(CustomDataStore.GetOrAdd), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        if (getOrAddMethod == null)
        {
            return false;
        }

        getOrAddMethod = getOrAddMethod.MakeGenericMethod(type);
        GetOrAddMethods.Add(type, getOrAddMethod);

        MethodInfo? destroyMethod = typeof(CustomDataStore).GetMethod(nameof(CustomDataStore.Destroy), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        if (destroyMethod == null)
        {
            return false;
        }

        destroyMethod = destroyMethod.MakeGenericMethod(type);
        DestroyMethods.Add(type, destroyMethod);

        MethodInfo? destroyAllMethod = typeof(CustomDataStore).GetMethod(nameof(CustomDataStore.DestroyAll), BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        if (destroyAllMethod == null)
        {
            return false;
        }

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
        {
            method.Invoke(null, null);
        }

        DestroyAllMethods.Remove(type);
        RegisteredStores.Remove(type);
        GetOrAddMethods.Remove(type);
        DestroyMethods.Remove(type);
    }

    /// <summary>
    /// Method used to initialize stores when a new player joins the server.
    /// </summary>
    /// <param name="player">The player added to the game.</param>
    internal static void AddPlayer(Player player)
    {
        foreach (Type? storeType in RegisteredStores)
        {
            GetOrAddMethods[storeType].Invoke(null, [player]);
        }
    }

    /// <summary>
    /// Method used to destroy stores when an existing player leaves the server.
    /// </summary>
    /// <param name="player">The player removed from the game.</param>
    internal static void RemovePlayer(Player player)
    {
        foreach (Type? storeType in RegisteredStores)
        {
            DestroyMethods[storeType].Invoke(null, [player]);
        }
    }

    /// <summary>
    /// Whether the store type had been registered.
    /// </summary>
    /// <param name="type">The <see cref="Type"/> of the store.</param>
    /// <returns><see langword="true"/> if registered; otherwise <see langword="false"/>.</returns>
    internal static bool IsRegistered(Type type) => RegisteredStores.Contains(type);

    /// <summary>
    /// Whether the store type had been registered.
    /// </summary>
    /// <typeparam name="T">The stores type.</typeparam>
    /// <returns><see langword="true"/> if registered; otherwise <see langword="false"/>.</returns>
    internal static bool IsRegistered<T>() => IsRegistered(typeof(T));
}