using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Modules;
using LabApi.Features.Console;
using System.Collections.Generic;
using static InventorySystem.Items.Firearms.Modules.CylinderAmmoModule;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for revolver firearm.
/// </summary>
public class RevolverFirearm : FirearmItem
{
    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="firearm">The base <see cref="Firearm"/> object.</param>
    internal RevolverFirearm(Firearm firearm) : base(firearm)
    {
    }

    /// <inheritdoc/>
    public override bool OpenBolt => false;

    /// <summary>
    /// Gets collection of all active chambers. The first chamber is the one aligned with the barrel.<br/>
    /// Each subsequent index corresponds to the next chambers that will become aligned with the barrel when the cylinder rotates in its intended direction.<br/>
    /// <b>Note that double-action revolvers rotate the cylinder right before firing, which means the 0th element isn't necessarily the next round to be fired (unless the revolver is already cocked).</b><para/>
    /// </summary>
    public IEnumerable<Chamber> Chambers
    {
        get
        {
            if (_ammoContainerModule is CylinderAmmoModule)
                return GetChambersArrayForSerial(Serial, MaxAmmo);

            return null;
        }
    }

    /// <inheritdoc/>
    public override bool Cocked
    {
        get
        {
            if (_actionModule is DoubleActionModule actionModule)
                return actionModule.Cocked;

            return false;
        }
        set
        {
            if (_actionModule is not DoubleActionModule actionModule)
            {
                Logger.Error($"Unable to set {nameof(Cocked)} as the {nameof(DoubleActionModule)} is null.");
                return;
            }

            actionModule.Cocked = value;
        }
    }

    /// <summary>
    /// Gets or sets the stored ammo in a <b>ammo container</b> for this firearm.
    /// </summary>
    /// <remarks>
    /// Stored ammo in revolver firearm cannot exceed its <see cref="FirearmItem.MaxAmmo"/> and any additional values are ignored.
    /// </remarks>
    public override int StoredAmmo
    {
        get
        {
            if (_ammoContainerModule is CylinderAmmoModule ammoModule)
                return ammoModule.AmmoStored;

            return 0;
        }
        set
        {
            if (_ammoContainerModule is not CylinderAmmoModule ammoModule)
            {
                Logger.Error($"Unable to set {nameof(StoredAmmo)} as the {nameof(CylinderAmmoModule)} is null.");
                return;
            }

            foreach (Chamber chamber in Chambers)
            {
                chamber.ServerSyncState = ChamberState.Empty;
            }

            ammoModule.ServerModifyAmmo(value);
        }
    }

    /// <summary>
    /// Gets or sets the current ammo in the chamber.<para/>
    /// Revolver's <see cref="ChamberedAmmo"/> only accounts for the currently aligned chamber with the barrel. Therefore the maximum of chambered ammo is 1 (live round) or 0 (empty / discharged).
    /// Any value greater than 1 is counted as live round.
    /// </summary>
    public override int ChamberedAmmo
    {
        get
        {
            return GetChambersArrayForSerial(Serial, MaxAmmo)[0].ServerSyncState == ChamberState.Live ? 1 : 0;
        }
        set
        {
            if (_actionModule is not DoubleActionModule actionModule)
            {
                Logger.Error($"Unable to set {nameof(ChamberedAmmo)} as the {nameof(DoubleActionModule)} is null.");
                return;
            }

            SetChamberStatus(0, value == 0 ? ChamberState.Empty : ChamberState.Live);
        }
    }

    private RevolverRouletteModule _rouletteModule;

    /// <summary>
    /// Sets the status of the chamber at <paramref name="index"/>. 0th element is the one currently aligned with the barrel.<para/>
    /// <b>Indexes of each round are clockwise.</b>
    /// </summary>
    /// <param name="index">Index of the chamber clockwise. Starting at barrel aligned chamber.</param>
    /// <param name="state">Target state of the chamber.</param>
    public void SetChamberStatus(int index, ChamberState state)
    {
        if (_ammoContainerModule is not CylinderAmmoModule ammoModule)
        {
            Logger.Error($"Unable to set chamber status, this firearm doesn't have {nameof(CylinderAmmoModule)}!");
            return;
        }

        Chamber[] chambers = GetChambersArrayForSerial(Serial, MaxAmmo);
        chambers[index].ServerSyncState = state;

        ammoModule.ServerResync();
    }

    /// <summary>
    /// Rotates the chamber counter-clockwise (positive numbers) or clockwise (negative numbers) <paramref name="amount"/> times.
    /// No animation is played client-side.
    /// </summary>
    /// <param name="amount">The amount of times to rotate this cylinder.</param>
    public void Rotate(int amount)
    {
        if (_ammoContainerModule is not CylinderAmmoModule ammoModule)
        {
            Logger.Error($"Unable to rotate this cylinder, this firearm doesn't have {nameof(CylinderAmmoModule)}!");
            return;
        }

        ammoModule.RotateCylinder(amount);
    }

    /// <summary>
    /// Attempts to spin the revolver if the firearm is't busy doing something else.
    /// </summary>
    /// <returns>Whether the spin request was successful.</returns>
    public bool TrySpin()
    {
        if (_rouletteModule is not RevolverRouletteModule ammoModule)
        {
            Logger.Error($"Unable to spin this cylinder, this firearm doesn't have {nameof(RevolverRouletteModule)}!");
            return false;
        }

        return _rouletteModule.ServerTrySpin();
    }

    /// <inheritdoc/>
    protected override void CacheModules()
    {
        base.CacheModules();

        foreach (ModuleBase module in Modules)
        {
            if (module is RevolverRouletteModule rouletteModule)
            {
                _rouletteModule = rouletteModule;
                break;
            }
        }
    }
}
