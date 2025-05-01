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
    /// <typeparam name="TStore">The type of the custom data store.</typeparam>
    /// <returns>Whether the store was successfully registered.</returns>
    public static bool RegisterStore<TStore>()
        where TStore : CustomDataStore
    {
        Type type = typeof(TStore);
        if (Handlers.ContainsKey(type))
            return false;

        StoreHandler handler = new()
        {
            AddPlayer = player => CustomDataStore.GetOrAdd<TStore>(player),
            RemovePlayer = CustomDataStore.Destroy<TStore>,
            DestroyAll = CustomDataStore.DestroyAll<TStore>
        };

        Handlers.Add(type, handler);

        return true;
    }

    /// <summary>
    /// Unregisters a custom data store.
    /// </summary>
    /// <typeparam name="TStore">The type of the custom data store.</typeparam>
    public static void UnregisterStore<TStore>()
        where TStore : CustomDataStore
    {
        Type type = typeof(TStore);
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