using Generators;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using Mirror;
using PlayerRoles.PlayableScps.Scp106;
using RelativePositioning;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper over the <see cref="Scp106PocketItemManager.PocketItem"/> base object.
/// </summary>
/// <remarks>
/// Contains the item pickup and its associated pocket dimension properties.
/// </remarks>
public class PocketItem
{
    /// <summary>
    /// Contains all the cached <see cref="PocketItem"/> instances, accessible through their <see cref="ItemBase"/>.
    /// </summary>
    private static readonly Dictionary<ItemPickupBase, PocketItem> Dictionary = [];

    /// <summary>
    /// A reference to all <see cref="PocketItem"/> instances currently in the game.
    /// </summary>
    public static IReadOnlyCollection<PocketItem> List => Dictionary.Values;

    /// <summary>
    /// Tries to get the <see cref="PocketItem"/> associated with the <see cref="Wrappers.Pickup"/>.
    /// </summary>
    /// <param name="pickup">The <see cref="Wrappers.Pickup"/> inside the pocket dimension to get the <see cref="PocketItem"/> from.</param>
    /// <param name="pocketItem">The <see cref="PocketItem"/> associated with <see cref="Wrappers.Pickup"/> or null if it doesn't exists.</param>
    /// <returns>Whether the <see cref="PocketItem"/> was successfully retrieved.</returns>
    public static bool TryGet(Pickup pickup, [NotNullWhen(true)] out PocketItem? pocketItem)
        => Dictionary.TryGetValue(pickup.Base, out pocketItem);

    /// <summary>
    /// Gets the <see cref="PocketItem"/> associated with the <see cref="Wrappers.Pickup"/>.
    /// </summary>
    /// <param name="pickup">The <see cref="Wrappers.Pickup"/> inside the pocket dimension to get the <see cref="PocketItem"/> from.</param>
    /// <returns>The associated <see cref="PocketItem"/> for the <see cref="Wrappers.Pickup"/> or null if it doesn't exist.</returns>
    public static PocketItem? Get(Pickup pickup) => TryGet(pickup, out PocketItem? pocketItem) ? pocketItem : null;

    /// <summary>
    /// Gets or adds a <see cref="PocketItem"/>.
    /// </summary>
    /// <param name="pickup">The <see cref="Wrappers.Pickup"/> to get or add to the pocket dimension.</param>
    /// <returns>The <see cref="PocketItem"/> instance.</returns>
    /// <remarks>
    /// If the pickup is not in the pocket dimension it is teleported there on creation of the <see cref="PocketItem"/>.
    /// </remarks>
    public static PocketItem GetOrAdd(Pickup pickup)
    {
        if (Dictionary.TryGetValue(pickup.Base, out PocketItem? pocketItem))
        {
            return pocketItem;
        }

        pickup.Position = PocketDimension.Instance!.Position + Vector3.up;
        Scp106PocketItemManager.AddItem(pickup.Base);
        return Get(pickup)!;
    }

    /// <summary>
    /// Initializes the PocketItem wrapper by subscribing to the PocketDimensionTeleport events.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        Scp106PocketItemManager.OnPocketItemAdded += (itemPickupBase, pocketItem) => _ = new PocketItem(itemPickupBase, pocketItem);
        Scp106PocketItemManager.OnPocketItemRemoved += (itemPickupBase) => Dictionary.Remove(itemPickupBase);
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="pickup">The item pickup in the pocket dimension.</param>
    /// <param name="pocketItem">The base <see cref="Scp106PocketItemManager.PocketItem"/> object.</param>
    internal PocketItem(ItemPickupBase pickup, Scp106PocketItemManager.PocketItem pocketItem)
    {
        Dictionary.Add(pickup, this);
        Pickup = Pickup.Get(pickup);
        Base = pocketItem;
    }

    /// <summary>
    /// The item in the pocket dimension.
    /// </summary>
    public Pickup Pickup { get; }

    /// <summary>
    /// The base <see cref="Scp106PocketItemManager.PocketItem"/> object.
    /// </summary>
    public Scp106PocketItemManager.PocketItem Base { get; }

    /// <summary>
    /// Gets or sets the delay before the item pickup drops out or is destroyed from the pocket dimension.
    /// </summary>
    public double TriggerDelay
    {
        get => Base.TriggerTime - NetworkTime.time;
        set => Base.TriggerTime = NetworkTime.time + value;
    }

    /// <summary>
    /// Gets or sets whether the item pickup is destroyed after the <see cref="TriggerDelay"/>.
    /// </summary>
    public bool WillBeDestroyed
    {
        get => Base.Remove;
        set => Base.Remove = value;
    }

    /// <summary>
    /// The position to drop the item pickup if <see cref="WillBeDestroyed"/> is set to false.
    /// </summary>
    public Vector3 DropPosition
    {
        get => Base.DropPosition.Position;
        set => Base.DropPosition = new RelativePosition(value);
    }

    /// <summary>
    /// Gets whether a warning cue was sent to the players about a dropping item pickup.
    /// </summary>
    public bool IsWarningSent => Base.WarningSent;
}
