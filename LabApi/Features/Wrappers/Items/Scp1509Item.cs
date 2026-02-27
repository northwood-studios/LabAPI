using InventorySystem.Items.Scp1509;
using Mirror;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BaseScp1509Item = InventorySystem.Items.Scp1509.Scp1509Item;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseScp1509Item"/>.
/// </summary>
public class Scp1509Item : Item
{
    /// <summary>
    /// Contains all the cached SCP-1509 items, accessible through their <see cref="BaseScp1509Item"/>.
    /// </summary>
    public static new Dictionary<BaseScp1509Item, Scp1509Item> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="Scp1509Item"/>.
    /// </summary>
    public static new IReadOnlyCollection<Scp1509Item> List => Dictionary.Values;

    /// <summary>
    /// Gets the SCP-1509 item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseScp1509Item"/> was not null.
    /// </summary>
    /// <param name="baseScp1509Item">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(baseScp1509Item))]
    public static Scp1509Item? Get(BaseScp1509Item? baseScp1509Item)
    {
        if (baseScp1509Item == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(baseScp1509Item, out Scp1509Item item) ? item : (Scp1509Item)CreateItemWrapper(baseScp1509Item);
    }

    /// <summary>
    /// Checks if a <paramref name="player"/> is eligible to be respawned.
    /// </summary>
    /// <param name="player">The player to check.</param>
    /// <returns><see langword="true"/> if can be respawned, otherwise <see langword="false"/>.</returns>
    public static bool IsEligible(Player player)
        => Scp1509RespawnEligibility.IsEligible(player.ReferenceHub);

    /// <summary>
    /// Sets whether a <paramref name="player"/> is eligible to be respawned by SCP-1509.
    /// </summary>
    /// <param name="player">The player to change the eligibility of.</param>
    /// <param name="isEligible">Whether to allow respawning.</param>
    public static void SetEligible(Player player, bool isEligible)
        => Scp1509RespawnEligibility.SetEligible(player.ReferenceHub, isEligible);

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseScp1509Item">The base <see cref="BaseScp1509Item"/> object.</param>
    internal Scp1509Item(BaseScp1509Item baseScp1509Item)
        : base(baseScp1509Item)
    {
        Base = baseScp1509Item;

        if (CanCache)
        {
            Dictionary.Add(baseScp1509Item, this);
        }
    }

    /// <summary>
    /// The base <see cref="BaseScp1509Item"/> object.
    /// </summary>
    public new BaseScp1509Item Base { get; }

    /// <summary>
    /// Gets or sets the rate at which shield regenerates (points per second) when SCP-1509 is held.
    /// </summary>
    public float ShieldRegenRate
    {
        get => Base.ShieldRegenRate;
        set => Base.ShieldRegenRate = value;
    }

    /// <summary>
    /// Gets or sets the rate at which shield is removed (points per second) when SCP-1509 is no longer held.
    /// </summary>
    public float ShieldDecayRate
    {
        get => Base.ShieldDecayRate;
        set => Base.ShieldDecayRate = value;
    }

    /// <summary>
    /// Gets or sets the number of seconds that have to elapse since the last time damage was taken.
    /// </summary>
    public float ShieldOnDamagePause
    {
        get => Base.ShieldOnDamagePause;
        set => Base.ShieldOnDamagePause = value;
    }

    /// <summary>
    /// Gets or sets the number of seconds that have to elapse since the item was last equipped, for the shield to begin decaying.
    /// </summary>
    public float UnequipDecayDelay
    {
        get => Base.UnequipDecayDelay;
        set => Base.UnequipDecayDelay = value;
    }

    /// <summary>
    /// Gets or sets the next time the item can revive someone.
    /// </summary>
    public double NextResurrectTime
    {
        get => Base.NextResurrectTime;
        set => Base.NextResurrectTime = NetworkTime.time + value;
    }

    /// <summary>
    /// Gets or sets the revive cooldown after resurrection.
    /// </summary>
    public double ReviveCooldown
    {
        get => Base.ReviveCooldown;
        set => Base.ReviveCooldown = value;
    }

    /// <summary>
    /// Gets or sets the <see cref="Item.CurrentOwner"/>'s Max HS value after resurrection.
    /// </summary>
    public float EquippedHS
    {
        get => Base.EquippedHS;
        set => Base.EquippedHS = value;
    }

    /// <summary>
    /// Gets or sets the duration of the Blurred effect applied to revived players.
    /// </summary>
    public float RevivedPlayerBlurTime
    {
        get => Base.RevivedPlayerBlurTime;
        set => Base.RevivedPlayerBlurTime = value;
    }

    /// <summary>
    /// Gets or sets the bonus AHP value for <see cref="RevivedPlayers"/> within <see cref="RevivedPlayerAOEBonusAHPDistance"/>.
    /// </summary>
    public float RevivedPlayerAOEBonusAHP
    {
        get => Base.RevivedPlayerAOEBonusAHP;
        set => Base.RevivedPlayerAOEBonusAHP = value;
    }

    /// <summary>
    /// Gets or sets the minimum distance of giving AHP bonus to <see cref="RevivedPlayers"/>.
    /// </summary>
    public float RevivedPlayerAOEBonusAHPDistance
    {
        get => Base.RevivedPlayerAOEBonusAHPDistance;
        set => Base.RevivedPlayerAOEBonusAHPDistance = value;
    }

    /// <summary>
    /// Gets whether SCP-1509 can resurrect a spectator.
    /// </summary>
    public bool CanResurrect
        => Base.CanResurrect;

    /// <summary>
    /// Gets the revived players.
    /// </summary>
    public IEnumerable<Player> RevivedPlayers
        => Base.RevivedPlayers.Select(Player.Get).Where(static player => player != null)!;

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }
}
