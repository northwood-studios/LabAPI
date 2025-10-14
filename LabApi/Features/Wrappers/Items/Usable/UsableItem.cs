using InventorySystem.Items.Usables;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using BaseUsableItem = InventorySystem.Items.Usables.UsableItem;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseUsableItem"/>.
/// </summary>
public class UsableItem : Item
{
    /// <summary>
    /// Contains all the cached usable items, accessible through their <see cref="BaseUsableItem"/>.
    /// </summary>
    public static new Dictionary<BaseUsableItem, UsableItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="UsableItem"/>.
    /// </summary>
    public static new IReadOnlyCollection<UsableItem> List => Dictionary.Values;

    /// <summary>
    /// Gets the usable item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseUsableItem"/> was not null.
    /// </summary>
    /// <param name="baseUsableItem">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(baseUsableItem))]
    public static UsableItem? Get(BaseUsableItem? baseUsableItem)
    {
        if (baseUsableItem == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(baseUsableItem, out UsableItem item) ? item : (UsableItem)CreateItemWrapper(baseUsableItem);
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseUsableItem">The base <see cref="BaseUsableItem"/> object.</param>
    internal UsableItem(BaseUsableItem baseUsableItem)
        : base(baseUsableItem)
    {
        Base = baseUsableItem;

        if (CanCache)
        {
            Dictionary.Add(baseUsableItem, this);
        }
    }

    /// <summary>
    /// The base <see cref="BaseUsableItem"/> object.
    /// </summary>
    public new BaseUsableItem Base { get; }

    /// <summary>
    /// Gets or sets whether the item is in use.
    /// </summary>
    public bool IsUsing
    {
        get => Base.IsUsing;
        set => Base.IsUsing = value;
    }

    /// <summary>
    /// Gets or set the duration in seconds to use the item.
    /// </summary>
    /// <remarks>
    /// Does not effect the client side animation time.
    /// </remarks>
    public float UseDuration
    {
        get => Base.UseTime;
        set => Base.UseTime = value;
    }

    /// <summary>
    /// Gets or set the max duration in seconds after starting to use an item they are allowed to cancel.
    /// </summary>
    public float MaxCancellableDuration
    {
        get => Base.MaxCancellableTime;
        set => Base.MaxCancellableTime = value;
    }

    /// <summary>
    /// Gets or sets the duration in seconds for which the item of type <see cref="Item.Type"/> is on a cooldown for.
    /// </summary>
    public float GlobalCooldownDuration
    {
        get => UsableItemsController.GlobalItemCooldowns.TryGetValue(Serial, out float time) ? time - Time.timeSinceLevelLoad : 0;
        set => UsableItemsController.GlobalItemCooldowns[Serial] = Time.timeSinceLevelLoad + value;
    }

    /// <summary>
    /// Gets or sets the duration in seconds for which the item of type <see cref="Item.Type"/> is on a cooldown for the <see cref="Item.CurrentOwner"/>.
    /// </summary>
    public float PersonalCooldownDuration
    {
        get
        {
            if (CurrentOwner?.ReferenceHub == null || !UsableItemsController.GetHandler(CurrentOwner.ReferenceHub).PersonalCooldowns.TryGetValue(Type, out float time))
            {
                return 0;
            }

            return time - Time.timeSinceLevelLoad;
        }

        set
        {
            if (CurrentOwner?.ReferenceHub != null)
            {
                UsableItemsController.GetHandler(CurrentOwner.ReferenceHub).PersonalCooldowns[Type] = Time.timeSinceLevelLoad + value;
            }
        }
    }

    /// <summary>
    /// Gets whether the client is able to send use messages.
    /// </summary>
    /// <remarks>
    /// Not to be confused with whether the item can be used.
    /// </remarks>
    public bool CanClientStartUsing => Base.CanStartUsing;

    /// <summary>
    /// Apply the items effects to the <see cref="Item.CurrentOwner"/>.
    /// </summary>
    /// <remarks>
    /// Does not work on all items.
    /// </remarks>
    public void Use() => Base.ServerOnUsingCompleted();

    /// <summary>
    /// Tries to get the audible range in meters for the sound being emitted.
    /// </summary>
    /// <param name="range">The sounds range in meters.</param>
    /// <returns>Returns true if item is being used, otherwise false.</returns>
    public bool TryGetSoundEmissionRange(out float range) => Base.TryGetSoundEmissionRange(out range);

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }
}
