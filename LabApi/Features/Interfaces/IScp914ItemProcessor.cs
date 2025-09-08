using LabApi.Features.Wrappers;
using Scp914;
using UnityEngine;

namespace LabApi.Features.Interfaces;

/// <summary>
/// An interface for creating custom SCP-914 Item upgrade processors.
/// </summary>
public interface IScp914ItemProcessor
{
    /// <summary>
    /// The amount of world space position that needs to be added to move a pickup item from the input chamber to the output chamber.
    /// </summary>
    public static Vector3 MoveVector => Scp914Controller.MoveVector;

    /// <summary>
    /// Whether to use the <see cref="UpgradePickup"/> for inventory items and skip using <see cref="UpgradeItem"/>.
    /// </summary>
    public bool UsePickupMethodOnly { get; }

    /// <summary>
    /// Called for each players items in the intake chamber of SCP-914 if the <see cref="Wrappers.Scp914.Mode"/> allows so.
    /// </summary>
    /// <param name="setting">The <see cref="Scp914KnobSetting"/> used for this upgrade.</param>
    /// <param name="item">The <see cref="Item"/> to upgraded.</param>
    /// <returns>The upgrade result.</returns>
    /// <remarks>
    /// This is not called if <see cref="UsePickupMethodOnly"/> is true.
    /// Instead, items are converted to pickups and <see cref="UpgradePickup"/> is used, and then the pickups are converted back to items.
    /// </remarks>
    public Scp914Result UpgradeItem(Scp914KnobSetting setting, Item item);

    /// <summary>
    /// Called for each pickup in the intake chamber if the <see cref="Wrappers.Scp914.Mode"/> allows so.
    /// </summary>
    /// <remarks>
    /// Add <see cref="MoveVector"/> to the pickups world position to move pickup from the input chamber to the output.
    /// </remarks>
    /// <param name="setting">The <see cref="Scp914KnobSetting"/> used for this upgrade.</param>
    /// <param name="pickup">The <see cref="Pickup"/> to upgrade.</param>
    /// <returns>The upgrade result.</returns>
    public Scp914Result UpgradePickup(Scp914KnobSetting setting, Pickup pickup);
}
