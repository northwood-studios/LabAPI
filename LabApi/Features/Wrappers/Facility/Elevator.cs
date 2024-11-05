using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;
using MapGeneration.Distributors;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;
using static Interactables.Interobjects.ElevatorManager;
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

        ElevatorChamber.OnAdded += (chamber) => _ = new Elevator(chamber);
        ElevatorChamber.OnRemoved += (chamber) => Dictionary.Remove(chamber);
    }

    /// <summary>
    /// Gets the current destination / location of the elevator.
    /// </summary>
    public ElevatorDoor CurrentDestination => Base.CurrentDestination;

    /// <summary>
    /// Gets the destination/current floor of the elevator.
    /// </summary>
    public int CurrentDestinationLevel => Base.CurrentLevel;

    /// <summary>
    /// Gets the destination this elevator will head towards once activated.
    /// </summary>
    public ElevatorDoor NextDestination => Base.NextDestination;

    /// <summary>
    /// Gets the destination floor index this elevator will head towards once activated.
    /// </summary>
    public int NextDestinationLevel => Base.NextLevel;

    /// <summary>
    /// Gets whether this elevator is ready to be activated.
    /// </summary>
    public bool IsReady => Base.IsReady;

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
    public ElevatorChamber.ElevatorSequence CurrentSequence => Base.CurrentSequence;

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

    /// <summary>
    /// Attempts to send the elevator to target destination.
    /// </summary>
    /// <param name="targetLevel">Target level index of the floor.</param>
    /// <param name="force">Whether the destination should be changed even that the elevator is not ready/is still moving.</param>
    /// <returns></returns>
    public bool TrySetDestination(int targetLevel, bool force = false) => Base.TrySetDestination(targetLevel, force);

    /// <summary>
    /// Attempts to send the elevator to the next available floor.
    /// </summary>
    /// <returns>Whether the elevator was sent. Returns false if elevator is still moving or not yet ready.</returns>
    public bool TrySendToNextFloor() => TrySetDestination(NextDestinationLevel, false);

    /// <summary>
    /// Attempts to get elevator door at specified floor.
    /// </summary>
    /// <param name="targetLevel">The target elevator floor.</param>
    /// <param name="door">The elevator door at floor.</param>
    /// <returns>Bool whether the elevator door was found.</returns>
    public bool TryGetDoorAtLevel(int targetLevel, [NotNullWhen(true)] out ElevatorDoor door) => Base.TryGetLevelDoor(targetLevel, out door);

    /// <summary>
    /// Sets the lock reason of all elevator doors to the specified state.
    /// </summary>
    /// <param name="reason">The reason for door lock.</param>
    /// <param name="state">Whether the lock is active due to the specified reason.</param>
    public void LockAllDoors(DoorLockReason reason, bool state) => Base.LockAllDoors(reason, state);

    /// <summary>
    /// Unlocks all elevator doors assigned to this <see cref="Group"/>.
    /// </summary>
    public void UnlockAllDoors()
    {
        if (ElevatorDoor.AllElevatorDoors.TryGetValue(Group, out List<ElevatorDoor> doors))
        {
            foreach (ElevatorDoor door in doors)
            {
                door.ActiveLocks = 0;
            }
        }
    }

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
    public static IEnumerable<Elevator>? GetByGroup(ElevatorGroup group) => List.Where(n => n.Group == group);
}