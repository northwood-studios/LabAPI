using Interactables.Interobjects.DoorUtils;
using InventorySystem.Items.Pickups;
using NorthwoodLib.Pools;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;
using BaseLockerChamber = MapGeneration.Distributors.LockerChamber;
using BaseLocker = MapGeneration.Distributors.Locker;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper over the <see cref="BaseLockerChamber"/> object.
/// </summary>
public class LockerChamber
{
    /// <summary>
    /// Contains all the cached locker chambers, accessible through their <see cref="BaseLockerChamber"/>.
    /// </summary>
    public static Dictionary<BaseLockerChamber, LockerChamber> Dictionary { get; } = [];

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseLockerChamber">The base <see cref="BaseLockerChamber"/> object.</param>
    /// <param name="locker">The <see cref="Wrappers.Locker"/> that has this chamber.</param>
    /// <param name="id">The id of the chamber inside the locker.</param>
    internal LockerChamber(BaseLockerChamber baseLockerChamber, Locker locker, byte id)
    {
        Dictionary.Add(baseLockerChamber, this);
        Base = baseLockerChamber;
        Locker = locker;
        Id = id;
    }

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal void OnRemove()
    {
        Dictionary.Remove(Base);
    }

    /// <summary>
    /// The base <see cref="BaseLockerChamber"/> object.
    /// </summary>
    public BaseLockerChamber Base { get; }

    /// <summary>
    /// The <see cref="Wrappers.Locker"/> that contains this chamber.
    /// </summary>
    public Locker Locker { get; }

    /// <summary>
    /// The Id of the chamber inside the <see cref="Locker"/>.
    /// </summary>
    public byte Id { get; }

    /// <summary>
    /// Gets or sets whether the chamber door is open.
    /// </summary>
    public bool IsOpen
    {
        get => Base.IsOpen;
        set
        {
            Base.SetDoor(value, null);
            Locker.Base.RefreshOpenedSyncvar();
        }
    }

    /// <summary>
    /// Gets whether  the chamber can be interacted by a <see cref="Player"/>.
    /// </summary>
    public bool CanInteract => Base.CanInteract;

    /// <summary>
    /// Gets whether the chamber contains no items.
    /// </summary>
    public bool IsEmpty => Base.Content.All(x => x == null);

    /// <summary>
    /// Gets or sets the <see cref="KeycardPermissions"/> required by the <see cref="Player"/> to open/close the chamber.
    /// </summary>
    public KeycardPermissions RequiredPermissions
    {
        get => Base.RequiredPermissions;
        set => Base.RequiredPermissions = value;
    }

    /// <summary>
    /// Gets or sets the latest cooldown duration.
    /// </summary>
    /// <remarks>
    /// Cooldown for door open/close and denied interactions.
    /// </remarks>
    public float TargetCooldown
    {
        get => Base.TargetCooldown;
        set => Base.TargetCooldown = value;
    }

    /// <summary>
    /// Gets whether <see cref="Pickup"/> instances are spawned on the client only when the chamber is first opened.
    /// </summary>
    public bool SpawnOnFirstOpening => Base.SpawnOnFirstChamberOpening;

    /// <summary>
    /// Gets or sets the array of acceptable <see cref="ItemType">item types</see> that can spawn when filling the chamber with loot.
    /// </summary>
    /// <remarks>
    /// Only used when filling the chamber see <see cref="Locker.Loot"/> and <see cref="Locker.FillChambers"/>.
    /// </remarks>
    public ItemType[] AcceptableItems
    {
        get => Base.AcceptableItems;
        set => Base.AcceptableItems = value;
    }

    /// <summary>
    /// Fill chamber with random loot from <see cref="Locker.Loot"/> filtered by the <see cref="AcceptableItems"/>.
    /// </summary>
    public void Fill()
    {
        Locker.Base.FillChamber(Base);
        foreach (ItemPickupBase pickupBase in Base.Content)
        {
            if (!pickupBase.TryGetComponent(out Rigidbody rigidbody))
                continue;

            rigidbody.isKinematic = false;
        }
    }

    /// <summary>
    /// Gets all <see cref="Pickup"/> instances currently in the chamber.
    /// </summary>
    /// <returns>The result set of all <see cref="Pickup"/> instances.</returns>
    public HashSet<Pickup> GetAllItems()
    {
        HashSet<Pickup> items = HashSetPool<Pickup>.Shared.Rent();
        foreach (ItemPickupBase pickupBase in Base.Content.ToArray())
        {
            if (pickupBase == null)
            {
                Base.Content.Remove(pickupBase);
                continue;
            }

            items.Add(Pickup.Get(pickupBase));
        }

        return items;
    }

    /// <summary>
    /// Removes all <see cref="Pickup"/> instances from the chamber.
    /// </summary>
    public void RemoveAllItems()
    {
        foreach (ItemPickupBase pickupBase in Base.Content)
            pickupBase.DestroySelf();

        Base.Content.Clear();
        Base.ToBeSpawned.Clear();
    }

    /// <summary>
    /// Removes the specified <see cref="Pickup"/> from the chamber.
    /// </summary>
    /// <param name="pickup">The <see cref="Pickup"/> instance to remove.</param>
    public void RemoveItem(Pickup pickup)
    {
        Base.Content.Remove(pickup.Base);
        Base.ToBeSpawned.Remove(pickup.Base);
        pickup.Destroy();
    }

    /// <summary>
    /// Creates a new <see cref="Pickup"/> of the specified <see cref="ItemType"/> to add to the chamber.
    /// </summary>
    /// <param name="type">The <see cref="ItemType"/> of the new pickup.</param>
    /// <returns>The created <see cref="Pickup"/>.</returns>
    public Pickup AddItem(ItemType type)
    {
        Pickup? pickup = Pickup.Create(type, Base.Spawnpoint.position, Base.Spawnpoint.rotation);
        if (pickup == null)
            throw new ArgumentNullException(nameof(pickup));
        pickup.Transform.SetParent(Base.Spawnpoint);
        Base.Content.Add(pickup.Base);
        (pickup.Base as IPickupDistributorTrigger)?.OnDistributed();
        if (!IsOpen)
        {
            pickup.IsLocked = true;
            Base.WasEverOpened = false;
        }

        if (Base.SpawnOnFirstChamberOpening && !IsOpen)
            Base.ToBeSpawned.Add(pickup.Base);
        else
            pickup.Spawn();

        return pickup;
    }

    /// <summary>
    /// Interact with the chamber.
    /// <remarks>
    /// Will uses the currently held item in the interaction.
    /// </remarks>
    /// </summary>
    /// <param name="player">The <see cref="Player"/> that trigged the interaction.</param>
    public void Interact(Player player)
    {
        if (player == null)
            throw new ArgumentNullException(nameof(player));

        Locker.Base.ServerInteract(player.ReferenceHub, Id);
    }

    /// <summary>
    /// Plays the Access Denied sound for this chamber.
    /// </summary>
    public void PlayDeniedSound() => Locker.Base.RpcPlayDenied(Id);

    /// <summary>
    /// Gets the locker chamber wrapper from the <see cref="Dictionary"/> or creates a new if it doesn't exist and the provided <see cref="BaseLockerChamber"/> was not null.
    /// </summary>
    /// <param name="baseLockerChamber">The <see cref="BaseLockerChamber"/> object.</param>
    /// <returns>The requested locker chamber wrapper or null.</returns>
    [return: NotNullIfNotNull(nameof(baseLockerChamber))]
    public static LockerChamber? Get(BaseLockerChamber? baseLockerChamber)
    {
        if (baseLockerChamber == null)
            return null;

        return Dictionary.TryGetValue(baseLockerChamber, out LockerChamber lockerChamber) ? lockerChamber : CreateLockerChamberWrapper(baseLockerChamber);
    }

    private static LockerChamber CreateLockerChamberWrapper(BaseLockerChamber baseLockerChamber)
    {
        BaseLocker locker = baseLockerChamber.GetComponentInParent<BaseLocker>();
        return new LockerChamber(baseLockerChamber, (Locker)Structure.Get(locker), (byte)locker.Chambers.IndexOf(baseLockerChamber));
    }
}