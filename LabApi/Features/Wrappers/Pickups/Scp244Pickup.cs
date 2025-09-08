using InventorySystem.Items.Usables.Scp244;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for the <see cref="Scp244DeployablePickup"/>.
/// </summary>
public class Scp244Pickup : Pickup
{
    /// <summary>
    /// Contains all the cached SCP-244 pickups, accessible through their <see cref="Scp244DeployablePickup"/>.
    /// </summary>
    public static new Dictionary<Scp244DeployablePickup, Scp244Pickup> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="Scp244Pickup"/>.
    /// </summary>
    public static new IReadOnlyCollection<Scp244Pickup> List => Dictionary.Values;

    /// <summary>
    /// Gets the SCP-244 pickup from the <see cref="Dictionary"/> or creates a new if it doesn't exist and the provided <see cref="Scp244DeployablePickup"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="pickup">The <see cref="Base"/> if the pickup.</param>
    /// <returns>The requested pickup or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(pickup))]
    public static Scp244Pickup? Get(Scp244DeployablePickup? pickup)
    {
        if (pickup == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(pickup, out Scp244Pickup wrapper) ? wrapper : (Scp244Pickup)CreateItemWrapper(pickup);
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="scp244DeployablePickup">The base <see cref="Scp244DeployablePickup"/> object.</param>
    internal Scp244Pickup(Scp244DeployablePickup scp244DeployablePickup)
        : base(scp244DeployablePickup)
    {
        Base = scp244DeployablePickup;

        if (CanCache)
        {
            Dictionary.Add(scp244DeployablePickup, this);
        }
    }

    /// <summary>
    /// The <see cref="Scp244DeployablePickup"/> object.
    /// </summary>
    public new Scp244DeployablePickup Base { get; }

    /// <summary>
    /// Gets or sets the <see cref="Scp244State"/>.
    /// </summary>
    public Scp244State State
    {
        get => Base.State;
        set => Base.State = value;
    }

    /// <summary>
    /// Gets or set the size of the SCP-244 cloud.
    /// </summary>
    public float SizePercent => Base.CurrentSizePercent;

    /// <summary>
    /// A internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }
}
