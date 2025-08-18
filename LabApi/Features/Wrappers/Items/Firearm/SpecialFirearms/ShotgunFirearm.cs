using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Modules;
using LabApi.Features.Console;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for shotgun firearm.
/// </summary>
public class ShotgunFirearm : FirearmItem
{
    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="firearm">The base <see cref="Firearm"/> object.</param>
    internal ShotgunFirearm(Firearm firearm)
        : base(firearm)
    {
    }

    /// <inheritdoc/>
    public override bool OpenBolt => false;

    /// <summary>
    /// Gets whether any of the hammers is cocked.
    /// Sets the cocked status of ALL hammers.
    /// </summary>
    public override bool Cocked
    {
        get
        {
            if (ActionModule is PumpActionModule actionModule)
            {
                return actionModule.SyncCocked == actionModule.Barrels;
            }

            return false;
        }

        set
        {
            if (ActionModule is not PumpActionModule actionModule)
            {
                Logger.Error($"Unable to set {nameof(Cocked)} as it is invalid.");
                return;
            }

            actionModule.SyncCocked = actionModule.Barrels;
        }
    }

    /// <summary>
    /// Gets or sets the amount of currently cocked chambers.
    /// </summary>
    public int CockedChambers
    {
        get
        {
            if (ActionModule is PumpActionModule actionModule)
            {
                return actionModule.SyncCocked;
            }

            return 0;
        }

        set
        {
            if (ActionModule is not PumpActionModule actionModule)
            {
                Logger.Error($"Unable to set {nameof(CockedChambers)} as it is invalid.");
                return;
            }

            actionModule.SyncCocked = value;
        }
    }

    /// <summary>
    /// Gets or sets the amount of barrels this shotgun has.
    /// Useful when creating custom weapons.<br/>
    /// It is recommended to set this value before the shotgun has been pumped by the player, otherwise you might need to set <see cref="CockedChambers"/> and <see cref="ChamberedAmmo"/> to the correct value.
    /// </summary>
    public override int ChamberMax
    {
        get
        {
            if (ActionModule is PumpActionModule actionModule)
            {
                return actionModule.Barrels;
            }

            return 0;
        }

        set
        {
            if (ActionModule is not PumpActionModule actionModule)
            {
                Logger.Error($"Unable to set {nameof(ChamberMax)} as it is invalid.");
                return;
            }

            actionModule.Barrels = value;
        }
    }

    /// <summary>
    /// Gets or sets the current ammo in the barrels.
    /// </summary>
    public override int ChamberedAmmo
    {
        get
        {
            if (ActionModule is PumpActionModule actionModule)
            {
                return actionModule.AmmoStored;
            }

            return 0;
        }

        set
        {
            if (ActionModule is not PumpActionModule actionModule)
            {
                Logger.Error($"Unable to set {nameof(ChamberedAmmo)} as it is null.");
                return;
            }

            actionModule.AmmoStored = value;
        }
    }

    /// <summary>
    /// Schedules pumping for this firearm.<br/>
    /// Value of 0 pumps the firearm instantly. Any value above 0 delays the pump by <paramref name="shotsFired"/> * 0.5 second.
    /// </summary>
    /// <param name="shotsFired">The amount of shots that has been fired. Pumping is delayed by <paramref name="shotsFired"/> * 0.5 second.</param>
    public void Pump(int shotsFired = 0)
    {
        if (ActionModule is not PumpActionModule actionModule)
        {
            Logger.Error($"Unable to pump {nameof(PumpActionModule)} as it is null.");
            return;
        }

        actionModule.SchedulePumping(shotsFired);
    }
}
