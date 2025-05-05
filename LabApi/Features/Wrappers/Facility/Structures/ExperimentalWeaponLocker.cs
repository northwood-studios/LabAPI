using Interactables.Interobjects.DoorUtils;
using MapGeneration.Distributors;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security;
using BaseExperimentalWeaponLocker = MapGeneration.Distributors.ExperimentalWeaponLocker;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Represents the ExperimentalWeaponLocker prefab instance.
/// </summary>
public class ExperimentalWeaponLocker : Locker
{
    /// <summary>
    /// Contains all the cached experimental weapon lockers, accessible through their <see cref="BaseExperimentalWeaponLocler"/>.
    /// </summary>
    public new static Dictionary<BaseExperimentalWeaponLocker, ExperimentalWeaponLocker> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="ExperimentalWeaponLocker"/> instances.
    /// </summary>
    public new static IReadOnlyCollection<ExperimentalWeaponLocker> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseExperimentalWeaponLocker">The base <see cref="BaseExperimentalWeaponLocker"/> object.</param>
    internal ExperimentalWeaponLocker(BaseExperimentalWeaponLocker baseExperimentalWeaponLocker)
        : base(baseExperimentalWeaponLocker)
    {
        Base = baseExperimentalWeaponLocker;
        Dictionary.Add(baseExperimentalWeaponLocker, this);
    }

    /// <summary>
    /// An internal method to remove itself from the cache when the abase object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }

    /// <summary>
    /// The base <see cref="BaseExperimentalWeaponLocker"/> object.
    /// </summary>
    public new BaseExperimentalWeaponLocker Base { get; }

    /// <summary>
    /// The experimental weapon's chamber.
    /// </summary>
    public LockerChamber Chamber => Chambers.First();

    /// <summary>
    /// Gets or sets whether the experimental weapon locker is open.
    /// </summary>
    public bool IsOpen
    {
        get => Chamber.IsOpen;
        set => Chamber.IsOpen = value;
    }

    /// <summary>
    /// Gets whether the experimental weapon locker can be interacted with by a <see cref="Player"/>.
    /// </summary>
    public bool CanInteract => Chamber.CanInteract;

    /// <summary>
    /// Gets or sets the <see cref="DoorPermissionFlags"/> required by the <see cref="Player"/> to open/close the experimental weapon locker.
    /// </summary>
    public DoorPermissionFlags RequiredPermissions
    {
        get => Chamber.RequiredPermissions;
        set => Chamber.RequiredPermissions = value;
    }

    /// <summary>
    /// Gets or sets the latest cooldown duration on the experimental weapon locker.
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
    /// Gets or sets the array of acceptable <see cref="ItemType">item types</see> that can spawn when filling the experimental weapon locker with loot.
    /// </summary>
    public ItemType[] AcceptableItems
    {
        get => Chamber.AcceptableItems;
        set => Chamber.AcceptableItems = value;
    }

    /// <summary>
    /// Fill the experimental weapon locker with random loot from <see cref="Locker.Loot"/> filtered by the <see cref="AcceptableItems"/>.
    /// </summary>
    public void Fill() => Chamber.Fill();

    /// <summary>
    /// Gets all <see cref="Pickup"/> instances currently in the experimental weapon locker.
    /// </summary>
    /// <returns>The result set of all <see cref="Pickup"/> instances.</returns>
    public HashSet<Pickup> GetAllItems() => Chamber.GetAllItems();

    /// <summary>
    /// Removes all <see cref="Pickup"/> instances from the experimental weapon locker.
    /// </summary>
    public void RemoveAllItems() => Chamber.RemoveAllItems();

    /// <summary>
    /// Removes the specified <see cref="Pickup"/> from the experimental weapon locker.
    /// </summary>
    /// <param name="pickup">The <see cref="Pickup"/> instance to remove.</param>
    public void RemoveItem(Pickup pickup) => Chamber.RemoveItem(pickup);

    /// <summary>
    /// Creates a new <see cref="Pickup"/> of the specified <see cref="ItemType"/> to add to the experimental weapon locker.
    /// </summary>
    /// <param name="type">The <see cref="ItemType"/> of the new pickup.</param>
    /// <returns>The created <see cref="Pickup"/>.</returns>
    public Pickup AddItem(ItemType type) => Chamber.AddItem(type);

    /// <summary>
    /// Interact with the experimental weapon locker.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> that trigged the interaction.</param>
    public void Interact(Player player) => Chamber.Interact(player);

    /// <summary>
    /// Plays the Access Denied sound for the experimental weapon locker.
    /// </summary>
    /// <param name="flags">The <see cref="DoorPermissionFlags"/> that will be shown on the keycard reader.</param>
    public void PlayDeniedSound(DoorPermissionFlags flags = DoorPermissionFlags.None) => Chamber.PlayDeniedSound(flags);

    /// <summary>
    /// Gets the experimental weapon locker wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist and the provided <see cref="BaseExperimentalWeaponLocker"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="baseLocker">The <see cref="Base"/> of the experimental weapon locker.</param>
    /// <returns>The requested wrapper or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(baseLocker))]
    public static ExperimentalWeaponLocker? Get(BaseExperimentalWeaponLocker? baseLocker)
    {
        if (baseLocker == null)
            return null;

        return Dictionary.TryGetValue(baseLocker, out ExperimentalWeaponLocker found) ? found : (ExperimentalWeaponLocker)CreateStructureWrapper(baseLocker);
    }
}
