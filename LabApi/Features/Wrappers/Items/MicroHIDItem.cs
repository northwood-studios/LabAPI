using InventorySystem.Items.MicroHID.Modules;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseMicroHIDItem = InventorySystem.Items.MicroHID.MicroHIDItem;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseMicroHIDItem"/>.
/// </summary>
public class MicroHIDItem : Item
{
    /// <summary>
    /// Contains all the cached micro hid items, accessible through their <see cref="BaseMicroHIDItem"/>.
    /// </summary>
    public static new Dictionary<BaseMicroHIDItem, MicroHIDItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="MicroHIDItem"/>.
    /// </summary>
    public static new IReadOnlyCollection<MicroHIDItem> List => Dictionary.Values;

    /// <summary>
    /// Gets the micro hid item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseMicroHIDItem"/> was not null.
    /// </summary>
    /// <param name="baseMicroHIDItem">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(baseMicroHIDItem))]
    public static MicroHIDItem? Get(BaseMicroHIDItem? baseMicroHIDItem)
    {
        if (baseMicroHIDItem == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(baseMicroHIDItem, out MicroHIDItem item) ? item : (MicroHIDItem)CreateItemWrapper(baseMicroHIDItem);
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseMicroHIDItem">The base <see cref="BaseMicroHIDItem"/> object.</param>
    internal MicroHIDItem(BaseMicroHIDItem baseMicroHIDItem)
        : base(baseMicroHIDItem)
    {
        Base = baseMicroHIDItem;

        if (CanCache)
        {
            Dictionary.Add(baseMicroHIDItem, this);
        }
    }

    /// <summary>
    /// The base <see cref="BaseMicroHIDItem"/> object.
    /// </summary>
    public new BaseMicroHIDItem Base { get; }

    /// <summary>
    /// The base <see cref="EnergyManagerModule"/> module.
    /// </summary>
    public EnergyManagerModule BaseEnergyManager => Base.EnergyManager;

    /// <summary>
    /// The base <see cref="InputSyncModule"/> module.
    /// </summary>
    public InputSyncModule BaseInputSyncModule => Base.InputSync;

    /// <summary>
    /// The base <see cref="BrokenSyncModule"/> module.
    /// </summary>
    public BrokenSyncModule BaseBrokenSyncModule => Base.BrokenSync;

    /// <summary>
    /// The base <see cref="CycleController"/> controller.
    /// </summary>
    public CycleController BaseCycleController => Base.CycleController;

    /// <summary>
    /// Gets or sets the remaining energy left in the micro.
    /// 0.0 = empty, 1.0 = full.
    /// </summary>
    public float Energy
    {
        get => BaseEnergyManager.Energy;
        set => BaseEnergyManager.ServerSetEnergy(Serial, value);
    }

    /// <summary>
    /// Gets or sets whether the micro is considered broken.
    /// </summary>
    public bool IsBroken
    {
        get => BaseBrokenSyncModule.Broken;
        set => BaseBrokenSyncModule.ServerSetBroken(Serial, value);
    }

    /// <summary>
    /// Gets or sets the current <see cref="MicroHidPhase"/> of the micro.
    /// </summary>
    public MicroHidPhase Phase
    {
        get => BaseCycleController.Phase;
        set => BaseCycleController.Phase = value;
    }

    /// <summary>
    /// Gets or sets the last known firing mode of the micro.
    /// </summary>
    public MicroHidFiringMode FiringMode
    {
        get => BaseCycleController.LastFiringMode;
        set => BaseCycleController.LastFiringMode = value;
    }

    /// <summary>
    /// The progress from 0 to 1 for how ready the micro is to fire.
    /// Goes up when winding up, and down when winding down.
    /// </summary>
    public float WindUpProgress => BaseCycleController.ServerWindUpProgress;

    /// <summary>
    /// Time in seconds that the current phase has been active.
    /// </summary>
    public float PhaseElapsed => BaseCycleController.CurrentPhaseElapsed;

    /// <summary>
    /// Gets whether the primary fire is being held by the <see cref="Item.CurrentOwner"/>.
    /// </summary>
    public bool IsPrimaryHeld => BaseInputSyncModule.Primary;

    /// <summary>
    /// Gets whether the secondary fire is being held by the <see cref="Item.CurrentOwner"/>.
    /// </summary>
    public bool IsSecondaryHeld => BaseInputSyncModule.Secondary;

    /// <summary>
    /// Tries to get the audible range in meters for the sound being emitted.
    /// </summary>
    /// <param name="range">The sounds range in meters.</param>
    /// <returns>Returns true if the micro is emitting sound, otherwise false.</returns>
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
