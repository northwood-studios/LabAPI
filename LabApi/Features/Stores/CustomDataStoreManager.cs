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
    private static readonly Dictionary<Type, StoreHandler> Handlers = new();

    /// <summary>
    /// Registers a custom data store.
    /// </summary>
    /// <typeparam name="T">The type of the custom data store.</typeparam>
    /// <returns>Whether the store was successfully registered.</returns>
    public static bool RegisterStore<T>()
        where T : CustomDataStore
    {
        Type type = typeof(T);
        if (Handlers.ContainsKey(type))
            return false;

        StoreHandler handler = new()
        {
            AddPlayer = player => CustomDataStore.GetOrAdd<T>(player),
            RemovePlayer = CustomDataStore.Destroy<T>,
            DestroyAll = CustomDataStore.DestroyAll<T>
        };

        Handlers.Add(type, handler);

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
        if (Handlers.TryGetValue(type, out StoreHandler? handler))
            handler.DestroyAll();

        Handlers.Remove(type);
    }

    internal static void AddPlayer(Player player)
    {
        foreach (StoreHandler? handler in Handlers.Values)
            handler.AddPlayer(player);
    }

    internal static void RemovePlayer(Player player)
    {
        foreach (StoreHandler? handler in Handlers.Values)
            handler.RemovePlayer(player);
    }
}