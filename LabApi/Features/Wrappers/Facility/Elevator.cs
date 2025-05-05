using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;
using MapGeneration.Distributors;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Generators;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="ElevatorChamber">elevators</see>, the in-game elevators.
/// </summary>
public class Elevator
{
    /// <summary>
    /// Contains all the cached <see cref="ElevatorChamber">generators</see> in the game, accessible through their <see cref="Scp079Generator"/>.
    /// </summary>
    public static Dictionary<ElevatorChamber, Elevator> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="Elevator"/> instances currently in the game.
    /// </summary>
    public static IReadOnlyCollection<Elevator> List => Dictionary.Values;

    /// <summary>
    /// A private constructor to prevent external instantiation.
    /// </summary>
    /// <param name="elevator">The <see cref="ElevatorChamber"/> of the elevator.</param>
    private Elevator(ElevatorChamber elevator)
    {
        Dictionary.Add(elevator, this);
        Base = elevator;
    }

    /// <summary>
    /// The base object.
    /// </summary>
    public ElevatorChamber Base { get; }

    /// <summary>
    /// Initializes the <see cref="Elevator"/> class to subscribe to <see cref="ElevatorChamber"/> events.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        Dictionary.Clear();

        ElevatorChamber.OnElevatorSpawned += (chamber) => _ = new Elevator(chamber);
        ElevatorChamber.OnElevatorRemoved += (chamber) => Dictionary.Remove(chamber);
    }

    /// <summary>
    /// Gets the current destination / location of the elevator.
    /// </summary>
    public ElevatorDoor CurrentDestination => ElevatorDoor.Get(Base.DestinationDoor);

    /// <summary>
    /// Gets the destination/current floor of the elevator.
    /// </summary>
    public int CurrentDestinationLevel => Base.DestinationLevel;

    /// <summary>
    /// Gets the destination this elevator will head towards once activated.
    /// </summary>
    public ElevatorDoor NextDestination => ElevatorDoor.Get(Base.NextDestinationDoor);

    /// <summary>
    /// Gets the destination floor index this elevator will head towards once activated.
    /// </summary>
    public int NextDestinationLevel => Base.NextLevel;

    /// <summary>
    /// Gets whether this elevator is ready to be activated.
    /// </summary>
    public bool IsReady => Base.IsReady;

    /// <summary>
    /// Gets whether the level of this elevator is increasing.
    /// </summary>
    public bool GoingUp => Base.GoingUp;

    /// <summary>
    /// Gets or sets the <see cref="ElevatorGroup"/> this elevator belongs to.
    /// </summary>
    public ElevatorGroup Group
    {
        get => Base.AssignedGroup;
        set => Base.AssignedGroup = value;
    }

    /// <summary>
    /// Gets the current <see cref="ElevatorChamber.ElevatorSequence"/>.
    /// </summary>
    public ElevatorChamber.ElevatorSequence CurrentSequence => Base.CurSequence;

    /// <summary>
    /// Gets the current world space bounds of this elevator.
    /// <para>World space bounds are cached and recalculated if not valid after elevator movement.</para>
    /// </summary>
    public Bounds WorldSpaceBounds => Base.WorldspaceBounds;

    /// <summary>
    /// Gets the reason why is ANY elevator door locked.
    /// </summary>
    public DoorLockReason AnyDoorLockedReason => Base.ActiveLocksAnyDoors;

    /// <summary>
    /// Gets the reason why is EVERY elevator door locked.
    /// </summary>
    public DoorLockReason AllDoorsLockedReason => Base.ActiveLocksAllDoors;

    /// <summary>
    /// Indicates whether dynamic admin lock is enabled.
    /// <para>Dynamic Admin Lock is a mode where only doors on the floor with elevator are unlocked.</para>
    /// </summary>
    public bool DynamicAdminLock
    {
        get => Base.DynamicAdminLock;
        set => Base.DynamicAdminLock = value;
    }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"[Elevator: Group={Group}, IsReady={IsReady}, GoingUp={GoingUp}, CurrentSequence={CurrentSequence}]";
    }

    /// <summary>
    /// Locks every door of every elevator on map.
    /// </summary>
    public static void LockAll()
    {
        foreach (Elevator el in List)
            el.LockAllDoors();
    }

    /// <summary>
    /// Unlocks every door of every elevator on map.
    /// </summary>
    public static void UnlockAll()
    {
        foreach (Elevator el in List)
            el.UnlockAllDoors();
    }

    /// <summary>
    /// Attempts to send the elevator to target destination.
    /// </summary>
    /// <param name="targetLevel">Target level index of the floor.</param>
    /// <param name="force">Whether the destination should be changed even that the elevator is not ready/is still moving.</param>
    public void SetDestination(int targetLevel, bool force = false) => Base.ServerSetDestination(targetLevel, force);

    /// <summary>
    /// Simulates interaction of specified <see cref="Player"/> on this elevator.
    /// </summary>
    /// <param name="player">The player who is interacting with this elevator.</param>
    public void Interact(Player player) => Base.ServerInteract(player.ReferenceHub, 0);

    /// <summary>
    /// Attempts to send the elevator to the next available floor.
    /// </summary>
    public void SendToNextFloor() => SetDestination(NextDestinationLevel, false);

    /// <summary>
    /// Sets the lock reason of all elevator doors to the specified state.
    /// </summary>
    public void LockAllDoors() => Base.ServerLockAllDoors(DoorLockReason.AdminCommand, true);

    /// <summary>
    /// Unlocks all elevator doors assigned to this chamber.
    /// </summary>
    public void UnlockAllDoors() => Base.ServerLockAllDoors(DoorLockReason.AdminCommand, false);

    /// <summary>
    /// Gets the elevator wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="elevatorChamber">The <see cref="ElevatorChamber"/> of the elevator.</param>
    /// <returns>The requested elevator.</returns>
    public static Elevator Get(ElevatorChamber elevatorChamber) =>
        Dictionary.TryGetValue(elevatorChamber, out Elevator generator) ? generator : new Elevator(elevatorChamber);

    /// <summary>
    /// Gets the enumerable of elevators that are assigned to the specific group.
    /// </summary>
    /// <param name="group">The specified elevator group.</param>
    /// <returns>Enumerable where the group is equal to the one specified.</returns>
    public static IEnumerable<Elevator> GetByGroup(ElevatorGroup group) => List.Where(n => n.Group == group);
}