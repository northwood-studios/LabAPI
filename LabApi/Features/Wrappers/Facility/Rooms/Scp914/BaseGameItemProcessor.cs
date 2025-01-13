using LabApi.Features.Interfaces;
using Scp914;
using Scp914.Processors;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Adapter for handling the base game <see cref="Scp914ItemProcessor"/>.
/// Used when <see cref="Scp914.GetItemProcessor(ItemType)"/> is used on a <see cref="ItemType"/> which is using a base game item processor.
/// </summary>
public class BaseGameItemProcessor : IScp914ItemProcessor
{
    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="processor">The base game <see cref="Scp914ItemProcessor"/>.</param>
    internal BaseGameItemProcessor(Scp914ItemProcessor processor)
    {
        Processor = processor;
    }

    /// <summary>
    /// Get base game <see cref="Scp914ItemProcessor"/> instance.
    /// </summary>
    public Scp914ItemProcessor Processor { get; internal set; }

    /// <inheritdoc/>
    public bool UsePickupMethodOnly => false;

    /// <inheritdoc/>
    public Scp914Result UpgradeItem(Scp914KnobSetting setting, Item item)
    {
        return Processor.UpgradeInventoryItem(setting, item.Base);
    }

    /// <inheritdoc/>
    public Scp914Result UpgradePickup(Scp914KnobSetting setting, Pickup pickup)
    {
        return Processor.UpgradePickup(setting, pickup.Base);
    }
}
