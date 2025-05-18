using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseKeycardPickup = InventorySystem.Items.Keycards.KeycardPickup;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for the <see cref="BaseKeycardPickup"/> class.
/// </summary>
public class KeycardPickup : Pickup
{
    /// <summary>
    /// Contains all the cached keycard pickups, accessible through their <see cref="BaseKeycardPickup"/>.
    /// </summary>
    public new static Dictionary<BaseKeycardPickup, KeycardPickup> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="KeycardPickup"/>.
    /// </summary>
    public new static IReadOnlyCollection<KeycardPickup> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseKeycardPickup">The base <see cref="BaseKeycardPickup"/> object.</param>
    internal KeycardPickup(BaseKeycardPickup baseKeycardPickup)
        : base(baseKeycardPickup)
    {
        Base = baseKeycardPickup;

        if (CanCache)
            Dictionary.Add(baseKeycardPickup, this);
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
    /// The <see cref="BaseKeycardPickup"/> object.
    /// </summary>
    public new BaseKeycardPickup Base { get; }

    /// <summary>
    /// Gets the keycard pickup from the <see cref="Dictionary"/> or creates a new if it doesn't exist and the provided <see cref="BaseKeycardPickup"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="pickup">The <see cref="Base"/> if the pickup.</param>
    /// <returns>The requested pickup or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(pickup))]
    public static KeycardPickup? Get(BaseKeycardPickup? pickup)
    {
        if (pickup == null)
            return null;

        return Dictionary.TryGetValue(pickup, out KeycardPickup wrapper) ? wrapper : (KeycardPickup)CreateItemWrapper(pickup);
    }
}