using System;
using LabApi.Events.Arguments.Scp079Events;

namespace LabApi.Events.Handlers;

/// <inheritdoc />
public static partial class Scp079Events
{
    /// <summary>
    /// Invokes the <see cref="BlackingOutRoom"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp079BlackingOutRoomEventsArgs"/> of the event.</param>
    public static void OnBlackingOutRoom(Scp079BlackingOutRoomEventsArgs args) => BlackingOutRoom.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="BlackedOutRoom"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp079BlackedOutRoomEventArgs"/> of the event.</param>
    public static void OnBlackedOutRoom(Scp079BlackedOutRoomEventArgs args) => BlackedOutRoom.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="BlackingOutZone"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp079BlackingOutZoneEventArgs"/> of the event.</param>
    public static void OnBlackingOutZone(Scp079BlackingOutZoneEventArgs args) => BlackingOutZone.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="BlackedOutZone"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp079BlackedOutZoneEventArgs"/> of the event.</param>
    public static void OnBlackedOutZone(Scp079BlackedOutZoneEventArgs args) => BlackedOutZone.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ChangingCamera"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp079ChangingCameraEventArgs"/> of the event.</param>
    public static void OnChangingCamera(Scp079ChangingCameraEventArgs args) => ChangingCamera.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ChangedCamera"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp079ChangedCameraEventArgs"/> of the event.</param>
    public static void OnChangedCamera(Scp079ChangedCameraEventArgs args) => ChangedCamera.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="CancellingRoomLockdown"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp079CancellingRoomLockdownEventArgs"/> of the event.</param>
    public static void OnCancellingRoomLockdown(Scp079CancellingRoomLockdownEventArgs args) => CancellingRoomLockdown.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="CancelledRoomLockdown"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp079CancelledRoomLockdownEventArgs"/> of the event.</param>
    public static void OnCancelledRoomLockdown(Scp079CancelledRoomLockdownEventArgs args) => CancelledRoomLockdown.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="GainingExperience"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp079GainingExperienceEventArg"/> of the event.</param>
    public static void OnGainingExperience(Scp079GainingExperienceEventArgs args) => GainingExperience.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="GainedExperience"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp079GainedExperienceEventArgs"/> of the event.</param>
    public static void OnGainedExperience(Scp079GainedExperienceEventArgs args) => GainedExperience.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="LevelingUp"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp079LevelingUpEventArgs"/> of the event.</param>
    public static void OnLevelingUp(Scp079LevelingUpEventArgs args) => LevelingUp.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="LeveledUp"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp079LeveledUpEventArgs"/> of the event.</param>
    public static void OnLeveledUp(Scp079LeveledUpEventArgs args) => LeveledUp.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="LockingDoor"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp079LockingDoorEventArgs"/> of the event.</param>
    public static void OnLockingDoor(Scp079LockingDoorEventArgs args) => LockingDoor.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="LockedDoor"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp079LockedDoorEventArgs"/> of the event.</param>
    public static void OnLockedDoor(Scp079LockedDoorEventArgs args) => LockedDoor.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="LockingDownRoom"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp079LockingDownRoomEventArgs"/> of the event.</param>
    public static void OnLockingDownRoom(Scp079LockingDownRoomEventArgs args) => LockingDownRoom.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="LockedDownRoom"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp079LockedDownRoomEventArgs"/> of the event.</param>
    public static void OnLockedDownRoom(Scp079LockedDownRoomEventArgs args) => LockedDownRoom.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="UnlockingDoor"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp079UnlockingDoorEventArgs"/> of the event.</param>
    public static void OnUnlockingDoor(Scp079UnlockingDoorEventArgs args) => UnlockingDoor.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="UnlockedDoor"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp079UnlockedDoorEventArgs"/> of the event.</param>
    public static void OnUnlockedDoor(Scp079UnlockedDoorEventArgs args) => UnlockedDoor.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="UsingTesla"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp079UsingTeslaEventArgs"/> of the event.</param>
    public static void OnUsingTesla(Scp079UsingTeslaEventArgs args) => UsingTesla.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="UsedTesla"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp079UsedTeslaEventArgs"/> of the event.</param>
    public static void OnUsedTesla(Scp079UsedTeslaEventArgs args) => UsedTesla.InvokeEvent(args);
}
