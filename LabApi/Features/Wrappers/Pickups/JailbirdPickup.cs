using InventorySystem.Items.Jailbird;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseJailbirdPickup = InventorySystem.Items.Jailbird.JailbirdPickup;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for the <see cref="BaseJailbirdPickup"/> class.
/// </summary>
public class JailbirdPickup : Pickup
{
    /// <summary>
    /// Contains all the cached ammo pickups, accessible through their <see cref="BaseJailbirdPickup"/>.
    /// </summary>
    public new static Dictionary<BaseJailbirdPickup, JailbirdPickup> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="JailbirdPickup"/>.
    /// </summary>
    public new static IReadOnlyCollection<JailbirdPickup> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseJailbirdPickup">The base <see cref="BaseJailbirdPickup"/> object.</param>
    internal JailbirdPickup(BaseJailbirdPickup baseJailbirdPickup)
        : base(baseJailbirdPickup)
    {
        Base = baseJailbirdPickup;

        if (CanCache)
            Dictionary.Add(baseJailbirdPickup, this);
    }

    /// <summary>
    /// A internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }

    /// <summary>
    /// The <see cref="BaseJailbirdPickup"/> object.
    /// </summary>
    public new BaseJailbirdPickup Base { get; }

    /// <summary>
    /// Gets the total melee damage dealt.
    /// </summary>
    public float TotalDamageDealt
    {
        get => Base.TotalMelee;
        set => Base.TotalMelee = value;
    }

    /// <summary>
    /// Gets the total charges performed so far.
    /// </summary>
    public int TotalChargesPerformed
    {
        get => Base.TotalCharges;
        set => Base.TotalCharges = value;
    }

    /// <summary>
    /// Gets or sets the visual wear state.
    /// </summary>
    public JailbirdWearState WearState
    {
        get => Base.Wear;
        set => Base.NetworkWear = value;
    }

    /// <summary>
    /// Gets the jailbird pickup from the <see cref="Dictionary"/> or creates a new if it doesn't exist and the provided <see cref="BaseJailbirdPickup"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="pickup">The <see cref="Base"/> if the pickup.</param>
    /// <returns>The requested pickup or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(pickup))]
    public static JailbirdPickup? Get(BaseJailbirdPickup? pickup)
    {
        if (pickup == null)
            return null;

        return Dictionary.TryGetValue(pickup, out JailbirdPickup wrapper) ? wrapper : (JailbirdPickup)CreateItemWrapper(pickup);
    }
}
