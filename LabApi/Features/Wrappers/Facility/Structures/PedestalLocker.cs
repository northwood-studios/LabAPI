using Interactables.Interobjects.DoorUtils;
using MapGeneration.Distributors;
using System.Collections.Generic;
using System.Linq;
namespace LabApi.Features.Wrappers;

/// <summary>
/// Represents the PedestalLocker prefab instances.
/// </summary>
public class PedestalLocker : Locker
{
    /// <summary>
    /// Contains all the cached pedestal lockers, accessible through their <see cref="PedestalScpLocker"/>.
    /// </summary>
    public new static Dictionary<PedestalScpLocker, PedestalLocker> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="PedestalLocker"/> instances.
    /// </summary>
    public new static IReadOnlyCollection<PedestalLocker> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="pedestalScpLocker">The base <see cref="PedestalScpLocker"/> object.</param>
    internal PedestalLocker(PedestalScpLocker pedestalScpLocker)
        :base(pedestalScpLocker)
    {
        Base = pedestalScpLocker;
        Dictionary.Add(pedestalScpLocker, this);
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
    /// The base <see cref="PedestalScpLocker"/> object.
    /// </summary>
    public new PedestalScpLocker Base { get; }

    /// <summary>
    /// The pedestal's chamber.
    /// </summary>
    public LockerChamber Chamber => Chambers.First();

    /// <summary>
    /// Gets or sets whether or not the pedestal is open.
    /// </summary>
    public bool IsOpen
    {
        get => Chamber.IsOpen;
        set => Chamber.IsOpen = value;
    }

    /// <summary>
    /// Gets whether or not the pedestal can be interacted with by a <see cref="Player"/>.
    /// </summary>
    public bool CanInteract => Chamber.CanInteract;

    /// <summary>
    /// Gets or sets the <see cref="KeycardPermissions"/> required by the <see cref="Player"/> to open/close the pedestal.
    /// </summary>
    public KeycardPermissions RequiredPermissions
    {
        get => Chamber.RequiredPermissions;
        set => Chamber.RequiredPermissions = value;
    }

    /// <summary>
    /// Gets or sets the latest cooldown duration on the pedestal.
    /// </summary>
    /// <remarks>
    /// Cooldown for door open/close and denied interactions.
    /// </remarks>
    public float TargetCooldown
    {
        get => Chamber.TargetCooldown;
        set => Chamber.TargetCooldown = value;
    }

    /// <summary>
    /// Gets or sets the array of acceptable <see cref="ItemType">item types</see> that can spawn when filling the pedestal with loot.
    /// </summary>
    public ItemType[] AcceptableItems
    {
        get => Chamber.AcceptableItems;
        set => Chamber.AcceptableItems = value;
    }

    /// <summary>
    /// Fill the pedestal with random loot from <see cref="Locker.Loot"/> filtered by the <see cref="AcceptableItems"/>.
    /// </summary>
    public void Fill() => Chamber.Fill();

    /// <summary>
    /// Gets all <see cref="Pickup"/> instances currently in the pedestal.
    /// </summary>
    /// <returns>The result set of all <see cref="Pickup"/> instances.</returns>
    public HashSet<Pickup> GetAllItems() => Chamber.GetAllItems();

    /// <summary>
    /// Removes all <see cref="Pickup"/> instances from the pedestal.
    /// </summary>
    public void RemoveAllItems() => Chamber.RemoveAllItems();

    /// <summary>
    /// Removes the specified <see cref="Pickup"/> from the pedestal.
    /// </summary>
    /// <param name="pickup">The <see cref="Pickup"/> instance to remove.</param>
    public void RemoveItem(Pickup pickup) => Chamber.RemoveItem(pickup);

    /// <summary>
    /// Creates a new <see cref="Pickup"/> of the specified <see cref="ItemType"/> to add to the pedestal.
    /// </summary>
    /// <param name="type">The <see cref="ItemType"/> of the new pickup.</param>
    /// <returns>The created <see cref="Pickup"/>.</returns>
    public Pickup AddItem(ItemType type) => Chamber.AddItem(type);

    /// <summary>
    /// Interact with the pedestal.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> that trigged the interaction.</param>
    public void Interact(Player player) => Chamber.Interact(player);

    /// <summary>
    /// Plays the Access Denied sound for the pedestal.
    /// </summary>
    public void PlayDeniedSound() => Chamber.PlayDeniedSound();
}
