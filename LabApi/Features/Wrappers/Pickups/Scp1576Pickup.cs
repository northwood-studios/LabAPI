using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseScp1576Pickup = InventorySystem.Items.Usables.Scp1576.Scp1576Pickup;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for the <see cref="BaseScp1576Pickup"/> class.
/// </summary>
public class Scp1576Pickup : Pickup
{
    /// <summary>
    /// Contains all the cached SCP-1576 pickups, accessible through their <see cref="BaseScp1576Pickup"/>.
    /// </summary>
    public new static Dictionary<BaseScp1576Pickup, Scp1576Pickup> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="Scp1576Pickup"/>.
    /// </summary>
    public new static IReadOnlyCollection<Scp1576Pickup> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseScp1576Pickup">The base <see cref="BaseScp1576Pickup"/> object.</param>
    internal Scp1576Pickup(BaseScp1576Pickup baseScp1576Pickup)
        : base(baseScp1576Pickup)
    {
        Base = baseScp1576Pickup;

        if (CanCache)
            Dictionary.Add(baseScp1576Pickup, this);
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
    /// The <see cref="BaseScp1576Pickup"/> object.
    /// </summary>
    public new BaseScp1576Pickup Base { get; }

    /// <summary>
    /// Gets or set the horn position from 0.0 to 1.0.
    /// </summary>
    public float HornPosition
    {
        get => Base.HornPos;
        set => Base.HornPos = value;
    }

    /// <summary>
    /// Gets the SCP-1576 pickup from the <see cref="Dictionary"/> or creates a new if it doesn't exist and the provided <see cref="BaseScp1576Pickup"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="pickup">The <see cref="Base"/> if the pickup.</param>
    /// <returns>The requested pickup or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(pickup))]
    public static Scp1576Pickup? Get(BaseScp1576Pickup? pickup)
    {
        if (pickup == null)
            return null;

        return Dictionary.TryGetValue(pickup, out Scp1576Pickup wrapper) ? wrapper : (Scp1576Pickup)CreateItemWrapper(pickup);
    }

}