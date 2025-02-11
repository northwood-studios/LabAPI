using CustomPlayerEffects;
using InventorySystem;
using InventorySystem.Items;
using MapGeneration;
using PlayerRoles.PlayableScps.Scp106;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper over the <see cref="RoomIdentifier"/> that represents the pocket dimension.
/// </summary>
public class PocketDimension : Room
{
    /// <summary>
    /// Gets the current <see cref="PocketDimension"/> instance.
    /// </summary>
    /// <remarks>
    /// May be null if the map has not been generated yet or was previously destroyed.
    /// </remarks>
    public static PocketDimension? Instance { get; private set; }

    /// <summary>
    /// A reference to all <see cref="PocketTeleport"/> instances currently in the game.
    /// </summary>
    public static IReadOnlyCollection<PocketTeleport> PocketTeleports => PocketTeleport.List;

    /// <summary>
    /// Gets all items pickup in the pocket dimension by their associated <see cref="PocketItem"/> instances.
    /// </summary>
    public static IEnumerable<PocketItem> PocketItems => PocketItem.List;

    /// <summary>
    /// Gets an array of the recycle chances.
    /// </summary>
    /// <remarks>
    /// Indexing the array by the rarity of the item see <see cref="GetRarity"/> gives the chance for the item to be dropped from 0.0 to 1.0.
    /// </remarks>
    public static float[] RecycleChances => Scp106PocketItemManager.RecycleChances;

    /// <summary>
    /// Gets or sets the minimum time that an item can remain in the pocket dimension.
    /// </summary>
    public static float MinPocketItemTriggerDelay
    {
        get => Scp106PocketItemManager.TimerRange.x;
        set => Scp106PocketItemManager.TimerRange = Scp106PocketItemManager.TimerRange with
        {
            x = value
        };
    }

    /// <summary>
    /// Gets or sets the maximum time that an item can remain in the pocket dimension.
    /// </summary>
    public static float MaxPocketItemTriggerDelay
    {
        get => Scp106PocketItemManager.TimerRange.y;
        set => Scp106PocketItemManager.TimerRange = Scp106PocketItemManager.TimerRange with
        {
            y = value
        };
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="room">The room identifier for the pocket dimension.</param>
    internal PocketDimension(RoomIdentifier room) 
        : base(room) 
    {
        Instance = this;
    }

    /// <summary>
    /// An internal method to set the instance to null when the base object is destroyed.
    /// </summary>
    internal override void OnRemoved()
    {
        base.OnRemoved();
        Instance = null;
    }

    /// <summary>
    /// Force a <see cref="Player"/> inside the pocket dimension.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> to send.</param>
    public static void ForceInside(Player player) => player.EnableEffect<PocketCorroding>();

    /// <summary>
    /// Gets whether a <see cref="Player"/> is considered inside the pocket dimension.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> to check.</param>
    /// <returns>True if inside otherwise false.</returns>
    public static bool IsPlayerInside(Player player) => player.HasEffect<PocketCorroding>();

    /// <summary>
    /// Force a player to exit the pocket dimension.
    /// </summary>
    /// <param name="player">The player inside the pocket dimension.</param>
    /// <remarks>
    /// Player must be inside the pocket dimension, see <see cref="IsPlayerInside(Player)"/>.
    /// Triggers pocket dimension leaving/left events.
    /// </remarks>
    public static void ForceExit(Player player)
    {
        PocketDimensionTeleport.Exit(null, player.ReferenceHub);
    }

    /// <summary>
    /// Force a player to be killed by the pocket dimension.
    /// </summary>
    /// <param name="player">The player to kill.</param>
    /// <remarks>
    /// Instantly pocket decays the player.
    /// Triggers pocket dimension leaving/left events.
    /// </remarks>
    public static void ForceKill(Player player)
    {
        PocketDimensionTeleport.Kill(null, player.ReferenceHub);
    }

    /// <summary>
    /// Gets whether a <see cref="Pickup"/> is inside the pocket dimension.
    /// </summary>
    /// <param name="pickup">The <see cref="Pickup"/> to check.</param>
    /// <returns>True if inside otherwise false.</returns>
    public static bool IsPickupInside(Pickup pickup)
        => Scp106PocketItemManager.TrackedItems.ContainsKey(pickup.Base);

    /// <summary>
    /// Randomizes which pocket dimension's teleports are exits. 
    /// </summary>
    public static void RandomizeExits() => PocketDimensionGenerator.RandomizeTeleports();

    /// <summary>
    /// Gets the poses used for exits for the pocket dimension.
    /// </summary>
    /// <param name="zone">The zone that the exits are associated with.</param>
    /// <returns>A collection of exit <see cref="Pose"/> instances.</returns>
    public static IReadOnlyCollection<Pose> GetExitPosesForZone(FacilityZone zone)
        => Scp106PocketExitFinder.GetPosesForZone(zone);

    /// <summary>
    /// Adds the specified <see cref="Pose">poses</see> to be used as exits for the pocket dimension.
    /// </summary>
    /// <param name="zone">The zone the exits should apply too.</param>
    /// <param name="poses">The <see cref="Pose">poses</see> to add.</param>
    public static void AddExitPosesForZone(FacilityZone zone, IEnumerable<Pose> poses)
    {
        // Attempts to generate the pose array as it could be empty.
        if (!Scp106PocketExitFinder.PosesForZoneCache.ContainsKey(zone))
            Scp106PocketExitFinder.GetPosesForZone(zone);

        Scp106PocketExitFinder.PosesForZoneCache[zone] = Scp106PocketExitFinder.PosesForZoneCache[zone].Concat(poses).ToArray();
    }

    /// <summary>
    /// Adds the specified <see cref="Pose"/> to be used as exits for the pocket dimension.
    /// </summary>
    /// <param name="zone">The zone the exits should apply too.</param>
    /// <param name="pose">The <see cref="Pose"/> to add.</param>
    public static void AddExitPoseForZone(FacilityZone zone, Pose pose) => AddExitPosesForZone(zone, [pose]);

    /// <summary>
    /// Removes all poses used as exits for the pocket dimension for the specified zone.
    /// </summary>
    /// <param name="zone">The zone to remove exits from.</param>
    public static void RemoveAllExitPosesForZone(FacilityZone zone)
    {
        Scp106PocketExitFinder.PosesForZoneCache[zone] = [];
    }
    
    /// <summary>
    /// Removes the specified <see cref="Pose">poses</see> from use as exits for the pocket dimension.
    /// </summary>
    /// <param name="zone">The zone to remove exits from.</param>
    /// <param name="poses">the <see cref="Pose">poses</see> to remove.</param>
    public static void RemoveExitPosesForZone(FacilityZone zone, IEnumerable<Pose> poses)
    {
        // Attempts to generate the pose array as it could be empty.
        if (!Scp106PocketExitFinder.PosesForZoneCache.ContainsKey(zone))
            Scp106PocketExitFinder.GetPosesForZone(zone);

        Scp106PocketExitFinder.PosesForZoneCache[zone] = Scp106PocketExitFinder.PosesForZoneCache[zone].Except(poses).ToArray();
    }

    /// <summary>
    /// Removes the specified <see cref="Pose"/> from use as exits for the pocket dimension.
    /// </summary>
    /// <param name="zone">The zone to remove exits from.</param>
    /// <param name="pose">the <see cref="Pose"/> to remove.</param>
    public static void RemoveExitPoseForZone(FacilityZone zone, Pose pose) => RemoveExitPosesForZone(zone, [pose]);

    /// <summary>
    /// Gets the rarity of the item using its <see cref="Pickup"/> wrapper see <see cref="RecycleChances"/>.
    /// </summary>
    /// <param name="pickup">The <see cref="Pickup"/> to get the rarity from.</param>
    /// <returns>The rarity of the item.</returns>
    public static int GetRarity(Pickup pickup)
        => GetRarity(InventoryItemLoader.AvailableItems[pickup.Type]);

    /// <summary>
    /// Gets the rarity of the item using its <see cref="Item"/> wrapper see <see cref="RecycleChances"/>.
    /// </summary>
    /// <param name="item">The <see cref="Item"/> to get the rarity from.</param>
    /// <returns>The rarity of the item.</returns>
    public static int GetRarity(Item item) => GetRarity(item.Base);

    /// <summary>
    /// Gets the rarity of the item using its <see cref="ItemBase"/> base object see <see cref="RecycleChances"/>.
    /// </summary>
    /// <param name="item">The <see cref="ItemBase"/> to get the rarity from.</param>
    /// <returns>The rarity of the item.</returns>
    public static int GetRarity(ItemBase item)
        => Scp106PocketItemManager.GetRarity(item);
}
