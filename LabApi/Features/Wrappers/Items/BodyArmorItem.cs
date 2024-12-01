using InventorySystem.Items.Armor;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BodyArmor"/>.
/// </summary>
public class BodyArmorItem : Item
{
    /// <summary>
    /// Contains all the cached body armor items, accessible through their <see cref="BodyArmor"/>.
    /// </summary>
    public new static Dictionary<BodyArmor, BodyArmorItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="BodyArmorItem"/>.
    /// </summary>
    public new static IReadOnlyCollection<BodyArmorItem> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="bodyArmor">The base <see cref="BodyArmor"/> object.</param>
    internal BodyArmorItem(BodyArmor bodyArmor)
        : base(bodyArmor)
    {
        Dictionary.Add(bodyArmor, this);
        Base = bodyArmor;
    }

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }

    /// <summary>
    /// The base <see cref="BodyArmor"/> object.
    /// </summary>
    public new BodyArmor Base { get; }

    /// <summary>
    /// Gets the body armor item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BodyArmor"/> was not null.
    /// </summary>
    /// <param name="bodyArmor">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(bodyArmor))]
    public static BodyArmorItem? Get(BodyArmor? bodyArmor)
    {
        if (bodyArmor == null)
            return null;

        return Dictionary.TryGetValue(bodyArmor, out BodyArmorItem item) ? item : (BodyArmorItem)CreateItemWrapper(bodyArmor);
    }
}
