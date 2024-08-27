using LabApi.Events.Arguments.Scp079Events;

namespace LabApi.Events.Handlers;

/// <summary>
/// Handles all events related to SCP-079.
/// </summary>
public static partial class Scp079Events
{
    /// <summary>
    /// Gets called when SCP-079 is blacking out a room.
    /// </summary>
    public static event LabEventHandler<Scp079BlackingOutRoomEventsArgs>? BlackingOutRoom;

    /// <summary>
    /// Gets called when SCP-079 has blacked out a room.
    /// </summary>
    public static event LabEventHandler<Scp079BlackedOutRoomEventArgs>? BlackedOutRoom;

    /// <summary>
    /// Gets called when SCP-079 is blacking out a zone.
    /// </summary>
    public static event LabEventHandler<Scp079BlackingOutZoneEventArgs>? BlackingOutZone;

    /// <summary>
    /// Gets called when SCP-079 has blacked out a zone.
    /// </summary>
    public static event LabEventHandler<Scp079BlackedOutZoneEventArgs>? BlackedOutZone;

    /// <summary>
    /// Gets called when SCP-079 is changing a camera.
    /// </summary>
    public static event LabEventHandler<Scp079ChangingCameraEventArgs>? ChangingCamera;

    /// <summary>
    /// Gets called when SCP-079 has changed a camera.
    /// </summary>
    public static event LabEventHandler<Scp079ChangedCameraEventArgs>? ChangedCamera;

    /// <summary>
    /// Gets called when SCP-079 is cancelling a room lockdown.
    /// </summary>
    public static event LabEventHandler<Scp079CancellingRoomLockdownEventArgs>? CancellingRoomLockdown;

    /// <summary>
    /// Gets called when SCP-079 has cancelled a room lockdown.
    /// </summary>
    public static event LabEventHandler<Scp079CancelledRoomLockdownEventArgs>? CancelledRoomLockdown;

    /// <summary>
    /// Gets called when SCP-079 is gaining experience.
    /// </summary>
    public static event LabEventHandler<Scp079GainingExperienceEventArgs>? GainingExperience;

    /// <summary>
    /// Gets called when SCP-079 has gained experience.
    /// </summary>
    public static event LabEventHandler<Scp079GainedExperienceEventArgs>? GainedExperience;

    /// <summary>
    /// Gets called when SCP-079 is leveling up.
    /// </summary>
    public static event LabEventHandler<Scp079LevelingUpEventArgs>? LevelingUp;

    /// <summary>
    /// Gets called when SCP-079 has leveled up.
    /// </summary>
    public static event LabEventHandler<Scp079LeveledUpEventArgs>? LeveledUp;

    /// <summary>
    /// Gets called when SCP-079 is locking a door.
    /// </summary>
    public static event LabEventHandler<Scp079LockingDoorEventArgs>? LockingDoor;

    /// <summary>
    /// Gets called when SCP-079 has locked a door.
    /// </summary>
    public static event LabEventHandler<Scp079LockedDoorEventArgs>? LockedDoor;

    /// <summary>
    /// Gets called when SCP-079 is locking down a room.
    /// </summary>
    public static event LabEventHandler<Scp079LockingDownRoomEventArgs>? LockingDownRoom;

    /// <summary>
    /// Gets called when SCP-079 has locked down a room.
    /// </summary>
    public static event LabEventHandler<Scp079LockedDownRoomEventArgs>? LockedDownRoom;

    /// <summary>
    /// Gets called when SCP-079 is unlocking a door.
    /// </summary>
    public static event LabEventHandler<Scp079UnlockingDoorEventArgs>? UnlockingDoor;

    /// <summary>
    /// Gets called when SCP-079 has unlocked a door.
    /// </summary>
    public static event LabEventHandler<Scp079UnlockedDoorEventArgs>? UnlockedDoor;

    /// <summary>
    /// Gets called when SCP-079 is using a tesla.
    /// </summary>
    public static event LabEventHandler<Scp079UsingTeslaEventArgs>? UsingTesla;

    /// <summary>
    /// Gets called when SCP-079 has used a tesla.
    /// </summary>
    public static event LabEventHandler<Scp079UsedTeslaEventArgs>? UsedTesla;
}