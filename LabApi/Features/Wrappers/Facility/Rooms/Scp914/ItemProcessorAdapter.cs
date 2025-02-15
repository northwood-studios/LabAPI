using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using LabApi.Features.Interfaces;
using Scp914;
using Scp914.Processors;

namespace LabApi.Features.Wrappers;

/// <summary>
/// An internal adapter class to handle the conversion of the wrapper <see cref="IScp914ItemProcessor"/> interface to the base game <see cref="Scp914ItemProcessor"/>.
/// </summary>
internal class ItemProcessorAdapter : Scp914ItemProcessor
{
    /// <summary>
    /// The user supplied <see cref="IScp914ItemProcessor"/> implementation.
    /// </summary>
    public IScp914ItemProcessor Processor { get; internal set; } = null!;

    /// <summary>
    /// Used internally by the base game.
    /// </summary>
    public override Scp914Result UpgradeInventoryItem(Scp914KnobSetting setting, ItemBase item)
    {
        if (Processor.UsePickupMethodOnly)
            return base.UpgradeInventoryItem(setting, item);

        return Processor.UpgradeItem(setting, Item.Get(item));
    }

    /// <summary>
    /// Used internally by the base game.
    /// </summary>
    public override Scp914Result UpgradePickup(Scp914KnobSetting setting, ItemPickupBase pickup)
        => Processor.UpgradePickup(setting, Pickup.Get(pickup));
}
