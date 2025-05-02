using System;
using System.Collections.Generic;
using System.Linq;
using LabApi.Features.Wrappers;
using NorthwoodLib.Pools;

namespace LabApi.Features.Stores;

/// <summary>
/// Represents a Custom Data Store that plugins can use to store data with a player.
/// </summary>
public abstract class CustomDataStore
{
    private static readonly Dictionary<Type, Dictionary<Player, CustomDataStore>> StoreInstances = new ();

    /// <summary>
    /// Gets the <see cref="Player"/> that this instance is associated with.
    /// </summary>
    public Player Owner { get; internal set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomDataStore"/> class.
    /// </summary>
    /// <param name="owner">The <see cref="Player"/> that this instance is associated with.</param>
    protected CustomDataStore(Player owner)
    {
        Owner = owner;
    }

    /// <summary>
    /// Gets the <see cref="CustomDataStore"/> for the specified <see cref="Player"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> to get the <see cref="CustomDataStore"/> for.</param>
    /// <typeparam name="TStore">The type of the <see cref="CustomDataStore"/>.</typeparam>
    /// <returns>The <see cref="CustomDataStore"/> for the specified <see cref="Player"/>.</returns>
    public static TStore GetOrAdd<TStore>(Player player)
        where TStore : CustomDataStore
    {
        Type type = typeof(TStore);

        if (!StoreInstances.TryGetValue(type, out Dictionary<Player, CustomDataStore>? playerStores))
        {
            playerStores = new Dictionary<Player, CustomDataStore>();
            StoreInstances[type] = playerStores;
        }

        if (playerStores.TryGetValue(player, out CustomDataStore? store))
            return (TStore)store;

        store = (TStore)Activator.CreateInstance(type, player);
        playerStores[player] = store;
        store.InternalOnInstanceCreated();

        return (TStore)store;
    }
    
    /// <summary>
    /// Checks if the <see cref="CustomDataStore"/> for the specified <see cref="Player"/> exists.
    /// </summary>
    /// <param name="player"> The <see cref="Player"/> to check the <see cref="CustomDataStore"/> for.</param>
    /// <typeparam name="TStore">The type of the <see cref="CustomDataStore"/></typeparam>
    /// <returns>True if the <see cref="CustomDataStore"/> exists for the specified <see cref="Player"/>, false if not.</returns>
    public static bool Exists<TStore>(Player player)
        where TStore : CustomDataStore
    {
        Type type = typeof(TStore);

        if (!StoreInstances.TryGetValue(type, out Dictionary<Player, CustomDataStore>? playerStores))
            return false;

        return playerStores.ContainsKey(player);
    }
    
    /// <summary>
    /// Gets all instances of the <see cref="CustomDataStore"/> for the specified type.
    /// </summary>
    /// <typeparam name="TStore">The type of the <see cref="CustomDataStore"/>.</typeparam>
    /// <returns>An <see cref="IEnumerable{TStore}"/> of all instances of the <see cref="CustomDataStore"/>.</returns>
    public static IEnumerable<(Player Player, TStore Store)> GetAll<TStore>()
        where TStore : CustomDataStore
    {
        if (!StoreInstances.TryGetValue(typeof(TStore), out Dictionary<Player, CustomDataStore>? playerStores))
            return Enumerable.Empty<(Player, TStore)>();

        return playerStores.Select(entry => (entry.Key, (TStore)entry.Value));
    }

    /// <summary>
    /// Called when a new instance of the <see cref="CustomDataStore"/> is created.
    /// </summary>
    protected virtual void OnInstanceCreated() { }

    /// <summary>
    /// Called when an instance of the <see cref="CustomDataStore"/> is going to be destroyed.
    /// </summary>
    protected virtual void OnInstanceDestroyed() { }

    /// <summary>
    /// Destroys the <see cref="CustomDataStore"/> for the specified <see cref="Player"/>.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> to destroy the <see cref="CustomDataStore"/> for.</param>
    /// <typeparam name="TStore">The type of the <see cref="CustomDataStore"/>.</typeparam>
    internal static void Destroy<TStore>(Player player)
        where TStore : CustomDataStore
    {
        if (!StoreInstances.TryGetValue(typeof(TStore), out Dictionary<Player, CustomDataStore>? playerStores))
            return;

        if (!playerStores.TryGetValue(player, out CustomDataStore? store))
            return;

        store.Destroy();
    }

    /// <summary>
    /// Destroys all instances of the <see cref="CustomDataStore"/> for the specified type.
    /// </summary>
    /// <typeparam name="TStore">The type of the <see cref="CustomDataStore"/>.</typeparam>
    internal static void DestroyAll<TStore>()
    {
        List<CustomDataStore>? storesToRemove = ListPool<CustomDataStore>.Shared.Rent(StoreInstances.SelectMany(entry =>
            entry.Value.Where(playerStore => playerStore.Value.GetType() == typeof(TStore)).Select(playerStore => playerStore.Value)));

        foreach (CustomDataStore? store in storesToRemove)
            store.Destroy();

        ListPool<CustomDataStore>.Shared.Return(storesToRemove);
    }

    /// <summary>
    /// Destroys this instance of the <see cref="CustomDataStore"/>.
    /// </summary>
    private void Destroy()
    {
        OnInstanceDestroyed();
        StoreInstances[this.GetType()].Remove(Owner);
    }

    private void InternalOnInstanceCreated() => OnInstanceCreated();
}

/// <summary>
/// Represents a Custom Data Store that plugins can use to store data with a player.
/// </summary>
/// <typeparam name="TStore">The type of the <see cref="CustomDataStore"/>.</typeparam>
public abstract class CustomDataStore<TStore> : CustomDataStore
    where TStore : CustomDataStore<TStore>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CustomDataStore"/> class.
    /// </summary>
    /// <param name="owner">The <see cref="Player"/> that this instance is associated with.</param>
    protected CustomDataStore(Player owner)
        : base(owner)
    {
    }

    /// <inheritdoc cref="CustomDataStore.GetOrAdd{TStore}"/>
    public static TStore Get(Player player) => GetOrAdd<TStore>(player);
    
    /// <inheritdoc cref="CustomDataStore.GetAll{TStore}"/>
    public static IEnumerable<(Player Player, TStore Store)> GetAll() => CustomDataStore.GetAll<TStore>();
    
    /// <inheritdoc cref="CustomDataStore.Exists{TStore}"/>
    public static bool Exists(Player player) => Exists<TStore>(player);
}