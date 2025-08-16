using InventorySystem;
using InventorySystem.Items.MicroHID.Modules;
using LabApi.Features.Console;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseMicroHIDPickup = InventorySystem.Items.MicroHID.MicroHIDPickup;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for the <see cref="BaseMicroHIDPickup"/> class.
/// </summary>
public class MicroHIDPickup : Pickup
{
    /// <summary>
    /// Contains all the cached micro hid pickups, accessible through their <see cref="BaseMicroHIDPickup"/>.
    /// </summary>
    public static new Dictionary<BaseMicroHIDPickup, MicroHIDPickup> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="MicroHIDPickup"/>.
    /// </summary>
    public static new IReadOnlyCollection<MicroHIDPickup> List => Dictionary.Values;

    /// <summary>
    /// Gets the micro hid pickup from the <see cref="Dictionary"/> or creates a new if it doesn't exist and the provided <see cref="BaseMicroHIDPickup"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="pickup">The <see cref="Base"/> if the pickup.</param>
    /// <returns>The requested pickup or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(pickup))]
    public static MicroHIDPickup? Get(BaseMicroHIDPickup? pickup)
    {
        if (pickup == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(pickup, out MicroHIDPickup wrapper) ? wrapper : (MicroHIDPickup)CreateItemWrapper(pickup);
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseMicroHIDPickup">The base <see cref="BaseMicroHIDPickup"/> object.</param>
    internal MicroHIDPickup(BaseMicroHIDPickup baseMicroHIDPickup)
        : base(baseMicroHIDPickup)
    {
        Base = baseMicroHIDPickup;
        BaseCycleController = CycleSyncModule.GetCycleController(Serial);

        if (CanCache)
        {
            Dictionary.Add(baseMicroHIDPickup, this);
        }
    }

    /// <summary>
    /// The <see cref="BaseMicroHIDPickup"/> object.
    /// </summary>
    public new BaseMicroHIDPickup Base { get; }

    /// <summary>
    /// The base <see cref="CycleController"/> controller.
    /// </summary>
    public CycleController BaseCycleController { get; }

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
    /// Gets or sets the energy amount in this Micro-HID pickup.<para/>
    /// The energy value, automatically clamped, ranges from 0f to 1f.
    /// </summary>
    public float Energy
    {
        get
        {
            return EnergyManagerModule.GetEnergy(Serial);
        }
        set
        {
            if (!InventoryItemLoader.TryGetItem(ItemType.MicroHID, out InventorySystem.Items.MicroHID.MicroHIDItem item))
            {
                Logger.Error("Unable to get the base microhid item!");
                return;
            }

            item.EnergyManager.ServerSetEnergy(Serial, value);
        }
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
    /// A internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }
}
