using InventorySystem.Items.Usables.Scp330;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseScp330Pickup = InventorySystem.Items.Usables.Scp330.Scp330Pickup;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for the <see cref="BaseScp330Pickup"/>.
/// </summary>
public class Scp330Pickup : Pickup
{
    /// <summary>
    /// Contains all the cached SCP-330 pickups, accessible through their <see cref="BaseScp330Pickup"/>.
    /// </summary>
    public static new Dictionary<BaseScp330Pickup, Scp330Pickup> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="Scp330Pickup"/>.
    /// </summary>
    public static new IReadOnlyCollection<Scp330Pickup> List => Dictionary.Values;

    /// <summary>
    /// Gets the SCP-330 pickup from the <see cref="Dictionary"/> or creates a new if it doesn't exist and the provided <see cref="BaseScp330Pickup"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="pickup">The <see cref="Base"/> if the pickup.</param>
    /// <returns>The requested pickup or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(pickup))]
    public static Scp330Pickup? Get(BaseScp330Pickup? pickup)
    {
        if (pickup == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(pickup, out Scp330Pickup wrapper) ? wrapper : (Scp330Pickup)CreateItemWrapper(pickup);
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseScp330Pickup">The base <see cref="BaseScp330Pickup"/> object.</param>
    internal Scp330Pickup(BaseScp330Pickup baseScp330Pickup)
        : base(baseScp330Pickup)
    {
        Base = baseScp330Pickup;

        if (CanCache)
        {
            Dictionary.Add(baseScp330Pickup, this);
        }
    }

    /// <summary>
    /// The <see cref="BaseScp330Pickup"/> object.
    /// </summary>
    public new BaseScp330Pickup Base { get; }

    /// <summary>
    /// The list of candies stored in the bag.
    /// </summary>
    public List<CandyKindID> Candies => Base.StoredCandies;

    /// <summary>
    /// Gets or sets the visual candy model used.
    /// Typically only used if theres one candy stored in <see cref="Candies"/>.
    /// </summary>
    public CandyKindID ExposedCandy
    {
        get => Base.ExposedCandy;
        set => Base.NetworkExposedCandy = value;
    }

    /// <summary>
    /// A internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }
}
