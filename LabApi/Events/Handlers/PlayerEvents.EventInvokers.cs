using LabApi.Events.Arguments.PlayerEvents;

namespace LabApi.Events.Handlers;

/// <inheritdoc />
public static partial class PlayerEvents
{
    /// <summary>
    /// Invokes the <see cref="ActivatingGenerator"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerActivatingGeneratorEventArgs"/> of the event.</param>
    public static void OnActivatingGenerator(PlayerActivatingGeneratorEventArgs args) => ActivatingGenerator.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ActivatedGenerator"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerActivatedGeneratorEventArgs"/> of the event.</param>
    public static void OnActivatedGenerator(PlayerActivatedGeneratorEventArgs args) => ActivatedGenerator.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="AimingWeapon"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerAimingWeaponEventArgs"/> of the event.</param>
    public static void OnAimingWeapon(PlayerAimingWeaponEventArgs args) => AimingWeapon.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="AimedWeapon"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerAimedWeaponEventArgs"/> of the event.</param>
    public static void OnAimedWeapon(PlayerAimedWeaponEventArgs args) => AimedWeapon.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Banning"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerBanningEventArgs"/> of the event.</param>
    public static void OnBanning(PlayerBanningEventArgs args) => Banning.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Banned"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerBannedEventArgs"/> of the event.</param>
    public static void OnBanned(PlayerBannedEventArgs args) => Banned.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="CancellingUsingItem"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerCancellingUsingItemEventArgs"/> of the event.</param>
    public static void OnCancelingUsingItem(PlayerCancellingUsingItemEventArgs args) => CancellingUsingItem.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="CancelledUsingItem"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerCancelledUsingItemEventArgs"/> of the event.</param>
    public static void OnCanceledUsingItem(PlayerCancelledUsingItemEventArgs args) => CancelledUsingItem.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ChangingItem"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerChangingItemEventArgs"/> of the event.</param>
    public static void OnChangingItem(PlayerChangingItemEventArgs args) => ChangingItem.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ChangedItem"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerChangedItemEventArgs"/> of the event.</param>
    public static void OnChangedItem(PlayerChangedItemEventArgs args) => ChangedItem.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ChangingRadioRange"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerChangingRadioRangeEventArgs"/> of the event.</param>
    public static void OnChangingRadioRange(PlayerChangingRadioRangeEventArgs args) => ChangingRadioRange.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ChangedRadioRange"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerChangedRadioRangeEventArgs"/> of the event.</param>
    public static void OnChangedRadioRange(PlayerChangedRadioRangeEventArgs args) => ChangedRadioRange.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ChangingRole"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerChangingRoleEventArgs"/> of the event.</param>
    public static void OnChangingRole(PlayerChangingRoleEventArgs args) => ChangingRole.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ChangedRole"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerChangedRoleEventArgs"/> of the event.</param>
    public static void OnChangedRole(PlayerChangedRoleEventArgs args) => ChangedRole.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ChangedSpectator"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerChangedSpectatorEventArgs"/> of the event.</param>
    public static void OnChangedSpectator(PlayerChangedSpectatorEventArgs args) => ChangedSpectator.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ClosingGenerator"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerClosingGeneratorEventArgs"/> of the event.</param>
    public static void OnClosingGenerator(PlayerClosingGeneratorEventArgs args) => ClosingGenerator.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ClosedGenerator"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerClosedGeneratorEventArgs"/> of the event.</param>
    public static void OnClosedGenerator(PlayerClosedGeneratorEventArgs args) => ClosedGenerator.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Cuffing"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerCuffingEventArgs"/> of the event.</param>
    public static void OnCuffing(PlayerCuffingEventArgs args) => Cuffing.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Cuffed"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerCuffedEventArgs"/> of the event.</param>
    public static void OnCuffed(PlayerCuffedEventArgs args) => Cuffed.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="DamagingShootingTarget"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerDamagingShootingTargetEventArgs"/> of the event.</param>
    public static void OnDamagingShootingTarget(PlayerDamagingShootingTargetEventArgs args) => DamagingShootingTarget.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="DamagedShootingTarget"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerDamagedShootingTargetEventArgs"/> of the event.</param>
    public static void OnDamagedShootingTarget(PlayerDamagedShootingTargetEventArgs args) => DamagedShootingTarget.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="DamagingWindow"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerDamagingWindowEventArgs"/> of the event.</param>
    public static void OnDamagingWindow(PlayerDamagingWindowEventArgs args) => DamagingWindow.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="DamagedWindow"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerDamagedWindowEventArgs"/> of the event.</param>
    public static void OnDamagedWindow(PlayerDamagedWindowEventArgs args) => DamagedWindow.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="DeactivatingGenerator"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerDeactivatingGeneratorEventArgs"/> of the event.</param>
    public static void OnDeactivatingGenerator(PlayerDeactivatingGeneratorEventArgs args) => DeactivatingGenerator.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="DeactivatedGenerator"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerDeactivatedGeneratorEventArgs"/> of the event.</param>
    public static void OnDeactivatedGenerator(PlayerDeactivatedGeneratorEventArgs args) => DeactivatedGenerator.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Dying"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerDyingEventArgs"/> of the event.</param>
    public static void OnDying(PlayerDyingEventArgs args) => Dying.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Death"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerDeathEventArgs"/> of the event.</param>
    public static void OnDeath(PlayerDeathEventArgs args) => Death.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="DroppingAmmo"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerDroppingAmmoEventArgs"/> of the event.</param>
    public static void OnDroppingAmmo(PlayerDroppingAmmoEventArgs args) => DroppingAmmo.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="DroppedAmmo"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerDroppedAmmoEventArgs"/> of the event.</param>
    public static void OnDroppedAmmo(PlayerDroppedAmmoEventArgs args) => DroppedAmmo.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="DroppingItem"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerDroppingItemEventArgs"/> of the event.</param>
    public static void OnDroppingItem(PlayerDroppingItemEventArgs args) => DroppingItem.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="DroppedItem"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerDroppedItemEventArgs"/> of the event.</param>
    public static void OnDroppedItem(PlayerDroppedItemEventArgs args) => DroppedItem.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="DryFiringWeapon"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerDryFiringWeaponEventArgs"/> of the event.</param>
    public static void OnDryFiringWeapon(PlayerDryFiringWeaponEventArgs args) => DryFiringWeapon.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="DryFiredWeapon"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerDryFiredWeaponEventArgs"/> of the event.</param>
    public static void OnDryFiredWeapon(PlayerDryFiredWeaponEventArgs args) => DryFiredWeapon.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="EnteringPocketDimension"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerEnteringPocketDimensionEventArgs"/> of the event.</param>
    public static void OnEnteringPocketDimension(PlayerEnteringPocketDimensionEventArgs args) => EnteringPocketDimension.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="EnteredPocketDimension"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerEnteredPocketDimensionEventArgs"/> of the event.</param>
    public static void OnEnteredPocketDimension(PlayerEnteredPocketDimensionEventArgs args) => EnteredPocketDimension.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Escaping"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerEscapingEventArgs"/> of the event.</param>
    public static void OnEscaping(PlayerEscapingEventArgs args) => Escaping.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Escaped"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerEscapedEventArgs"/> of the event.</param>
    public static void OnEscaped(PlayerEscapedEventArgs args) => Escaped.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="FlippingCoin"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerFlippingCoinEventArgs"/> of the event.</param>
    public static void OnFlippingCoin(PlayerFlippingCoinEventArgs args) => FlippingCoin.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="FlippedCoin"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerFlippedCoinEventArgs"/> of the event.</param>
    public static void OnFlippedCoin(PlayerFlippedCoinEventArgs args) => FlippedCoin.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="GetGroup"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerGetGroupEventArgs"/> of the event.</param>
    public static void OnGetGroup(PlayerGetGroupEventArgs args) => GetGroup.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Hurting"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerHurtingEventArgs"/> of the event.</param>
    public static void OnHurting(PlayerHurtingEventArgs args) => Hurting.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Hurt"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerHurtEventArgs"/> of the event.</param>
    public static void OnHurt(PlayerHurtEventArgs args) => Hurt.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="InteractingDoor"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerInteractingDoorEventArgs"/> of the event.</param>
    public static void OnInteractingDoor(PlayerInteractingDoorEventArgs args) => InteractingDoor.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="InteractedDoor"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerInteractedDoorEventArgs"/> of the event.</param>
    public static void OnInteractedDoor(PlayerInteractedDoorEventArgs args) => InteractedDoor.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="InteractingElevator"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerInteractingElevatorEventArgs"/> of the event.</param>
    public static void OnInteractingElevator(PlayerInteractingElevatorEventArgs args) => InteractingElevator.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="InteractedElevator"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerInteractedElevatorEventArgs"/> of the event.</param>
    public static void OnInteractedElevator(PlayerInteractedElevatorEventArgs args) => InteractedElevator.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="InteractingGenerator"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerInteractingGeneratorEventArgs"/> of the event.</param>
    public static void OnInteractingGenerator(PlayerInteractingGeneratorEventArgs args) => InteractingGenerator.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="InteractedGenerator"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerInteractedGeneratorEventArgs"/> of the event.</param>
    public static void OnInteractedGenerator(PlayerInteractedGeneratorEventArgs args) => InteractedGenerator.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="InteractingLocker"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerInteractingLockerEventArgs"/> of the event.</param>
    public static void OnInteractingLocker(PlayerInteractingLockerEventArgs args) => InteractingLocker.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="InteractedLocker"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerInteractedLockerEventArgs"/> of the event.</param>
    public static void OnInteractedLocker(PlayerInteractedLockerEventArgs args) => InteractedLocker.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="InteractingScp330"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerInteractingScp330EventArgs"/> of the event.</param>
    public static void OnInteractingScp330(PlayerInteractingScp330EventArgs args) => InteractingScp330.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="InteractedScp330"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerInteractedScp330EventArgs"/> of the event.</param>
    public static void OnInteractedScp330(PlayerInteractedScp330EventArgs args) => InteractedScp330.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="InteractingShootingTarget"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerInteractingShootingTargetEventArgs"/> of the event.</param>
    public static void OnInteractingShootingTarget(PlayerInteractingShootingTargetEventArgs args) => InteractingShootingTarget.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="InteractedShootingTarget"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerInteractedShootingTargetEventArgs"/> of the event.</param>
    public static void OnInteractedShootingTarget(PlayerInteractedShootingTargetEventArgs args) => InteractedShootingTarget.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Joined"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerJoinedEventArgs"/> of the event.</param>
    public static void OnJoined(PlayerJoinedEventArgs args) => Joined.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Left"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerLeftEventArgs"/> of the event.</param>
    public static void OnLeft(PlayerLeftEventArgs args) => Left.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Kicking"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerKickingEventArgs"/> of the event.</param>
    public static void OnKicking(PlayerKickingEventArgs args) => Kicking.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Kicked"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerKickedEventArgs"/> of the event.</param>
    public static void OnKicked(PlayerKickedEventArgs args) => Kicked.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="LeavingPocketDimension"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerLeavingPocketDimensionEventArgs"/> of the event.</param>
    public static void OnLeavingPocketDimension(PlayerLeavingPocketDimensionEventArgs args) => LeavingPocketDimension.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="LeftPocketDimension"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerLeftPocketDimensionEventArgs"/> of the event.</param>
    public static void OnLeftPocketDimension(PlayerLeftPocketDimensionEventArgs args) => LeftPocketDimension.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="MakingNoise"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerMakingNoiseEventArgs"/> of the event.</param>
    public static void OnMakingNoise(PlayerMakingNoiseEventArgs args) => MakingNoise.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="MadeNoise"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerMadeNoiseEventArgs"/> of the event.</param>
    public static void OnMadeNoise(PlayerMadeNoiseEventArgs args) => MadeNoise.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Muting"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerMutingEventArgs"/> of the event.</param>
    public static void OnMuting(PlayerMutingEventArgs args) => Muting.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Muted"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerMutedEventArgs"/> of the event.</param>
    public static void OnMuted(PlayerMutedEventArgs args) => Muted.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="OpeningGenerator"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerOpeningGeneratorEventArgs"/> of the event.</param>
    public static void OnOpeningGenerator(PlayerOpeningGeneratorEventArgs args) => OpeningGenerator.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="OpenedGenerator"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerOpenedGeneratorEventArgs"/> of the event.</param>
    public static void OnOpenedGenerator(PlayerOpenedGeneratorEventArgs args) => OpenedGenerator.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="PickingUpAmmo"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerPickingUpAmmoEventArgs"/> of the event.</param>
    public static void OnPickingUpAmmo(PlayerPickingUpAmmoEventArgs args) => PickingUpAmmo.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="PickedUpAmmo"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerPickedUpAmmoEventArgs"/> of the event.</param>
    public static void OnPickedUpAmmo(PlayerPickedUpAmmoEventArgs args) => PickedUpAmmo.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="PickingUpArmor"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerPickingUpArmorEventArgs"/> of the event.</param>
    public static void OnPickingUpArmor(PlayerPickingUpArmorEventArgs args) => PickingUpArmor.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="PickedUpArmor"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerPickedUpArmorEventArgs"/> of the event.</param>
    public static void OnPickedUpArmor(PlayerPickedUpArmorEventArgs args) => PickedUpArmor.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="PickingUpItem"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerPickingUpItemEventArgs"/> of the event.</param>
    public static void OnPickingUpItem(PlayerPickingUpItemEventArgs args) => PickingUpItem.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="PickedUpItem"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerPickedUpItemEventArgs"/> of the event.</param>
    public static void OnPickedUpItem(PlayerPickedUpItemEventArgs args) => PickedUpItem.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="PickingUpScp330"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerPickingUpScp330EventArgs"/> of the event.</param>
    public static void OnPickingUpScp330(PlayerPickingUpScp330EventArgs args) => PickingUpScp330.InvokeEvent(args);

    public static void OnPickedUpScp330(PlayerPickedUpScp330EventArgs args) => PickedUpScp330.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="PlacingBlood"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerPlacingBloodEventArgs"/> of the event.</param>
    public static void OnPlacingBlood(PlayerPlacingBloodEventArgs args) => PlacingBlood.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="PlacedBlood"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerPlacedBloodEventArgs"/> of the event.</param>
    public static void OnPlacedBlood(PlayerPlacedBloodEventArgs args) => PlacedBlood.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="PlacingBulletHole"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerPlacingBulletHoleEventArgs"/> of the event.</param>
    public static void OnPlacingBulletHole(PlayerPlacingBulletHoleEventArgs args) => PlacingBulletHole.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="PlacedBulletHole"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerPlacedBulletHoleEventArgs"/> of the event.</param>
    public static void OnPlacedBulletHole(PlayerPlacedBulletHoleEventArgs args) => PlacedBulletHole.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="PreAuthenticating"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerPreAuthenticatingEventArgs"/> of the event.</param>
    public static void OnPreAuthenticating(PlayerPreAuthenticatingEventArgs args) => PreAuthenticating.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="PreAuthenticated"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerPreAuthenticatedEventArgs"/> of the event.</param>
    public static void OnPreAuthenticated(PlayerPreAuthenticatedEventArgs args) => PreAuthenticated.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ReceivingEffect"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerReceivingEffectEventArgs"/> of the event.</param>
    public static void OnReceivingEffect(PlayerReceivingEffectEventArgs args) => ReceivingEffect.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ReceivedEffect"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerReceivedEffectEventArgs"/> of the event.</param>
    public static void OnReceivedEffect(PlayerReceivedEffectEventArgs args) => ReceivedEffect.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ReloadingWeapon"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerReloadingWeaponEventArgs"/> of the event.</param>
    public static void OnReloadingWeapon(PlayerReloadingWeaponEventArgs args) => ReloadingWeapon.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ReloadedWeapon"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerReloadedWeaponEventArgs"/> of the event.</param>
    public static void OnReloadedWeapon(PlayerReloadedWeaponEventArgs args) => ReloadedWeapon.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ReportingCheater"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerReportingCheaterEventArgs"/> of the event.</param>
    public static void OnReportingCheater(PlayerReportingCheaterEventArgs args) => ReportingCheater.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ReportedCheater"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerReportedCheaterEventArgs"/> of the event.</param>
    public static void OnReportedCheater(PlayerReportedCheaterEventArgs args) => ReportedCheater.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ReportingPlayer"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerReportingPlayerEventArgs"/> of the event.</param>
    public static void OnReportingPlayer(PlayerReportingPlayerEventArgs args) => ReportingPlayer.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ReportedPlayer"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerReportedPlayerEventArgs"/> of the event.</param>
    public static void OnReportedPlayer(PlayerReportedPlayerEventArgs args) => ReportedPlayer.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ReceivingLoadout"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerReceivingLoadoutEventArgs"/> of the event.</param>
    public static void OnReceivingLoadout(PlayerReceivingLoadoutEventArgs args) => ReceivingLoadout.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ReceivedLoadout"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerReceivedLoadoutEventArgs"/> of the event.</param>
    public static void OnReceivedLoadout(PlayerReceivedLoadoutEventArgs args) => ReceivedLoadout.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="SearchingAmmo"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerSearchingAmmoEventArgs"/> of the event.</param>
    public static void OnSearchingAmmo(PlayerSearchingAmmoEventArgs args) => SearchingAmmo.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="SearchedAmmo"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerSearchedAmmoEventArgs"/> of the event.</param>
    public static void OnSearchedAmmo(PlayerSearchedAmmoEventArgs args) => SearchedAmmo.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="SearchingArmor"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerSearchingArmorEventArgs"/> of the event.</param>
    public static void OnSearchingArmor(PlayerSearchingArmorEventArgs args) => SearchingArmor.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="SearchedArmor"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerSearchedArmorEventArgs"/> of the event.</param>
    public static void OnSearchedArmor(PlayerSearchedArmorEventArgs args) => SearchedArmor.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="SearchingPickup"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerSearchingPickupEventArgs"/> of the event.</param>
    public static void OnSearchingPickup(PlayerSearchingPickupEventArgs args) => SearchingPickup.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="SearchedPickup"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerSearchedPickupEventArgs"/> of the event.</param>
    public static void OnSearchedPickup(PlayerSearchedPickupEventArgs args) => SearchedPickup.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ShootingWeapon"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerShootingWeaponEventArgs"/> of the event.</param>
    public static void OnShootingWeapon(PlayerShootingWeaponEventArgs args) => ShootingWeapon.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ShotWeapon"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerShotWeaponEventArgs"/> of the event.</param>
    public static void OnShotWeapon(PlayerShotWeaponEventArgs args) => ShotWeapon.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Spawning"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerSpawningEventArgs"/> of the event.</param>
    public static void OnSpawning(PlayerSpawningEventArgs args) => Spawning.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Spawned"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerSpawnedEventArgs"/> of the event.</param>
    public static void OnSpawned(PlayerSpawnedEventArgs args) => Spawned.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="SpawningRagdoll"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerSpawningRagdollEventArgs"/> of the event.</param>
    public static void OnSpawningRagdoll(PlayerSpawningRagdollEventArgs args) => SpawningRagdoll.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="SpawnedRagdoll"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerSpawnedRagdollEventArgs"/> of the event.</param>
    public static void OnSpawnedRagdoll(PlayerSpawnedRagdollEventArgs args) => SpawnedRagdoll.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ThrowingItem"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerThrowingItemEventArgs"/> of the event.</param>
    public static void OnThrowingItem(PlayerThrowingItemEventArgs args) => ThrowingItem.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ThrewItem"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerThrewItemEventArgs"/> of the event.</param>
    public static void OnThrewItem(PlayerThrewItemEventArgs args) => ThrewItem.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ThrowingProjectile"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerThrowingProjectileEventArgs"/> of the event.</param>
    public static void OnThrowingProjectile(PlayerThrowingProjectileEventArgs args) => ThrowingProjectile.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ThrewProjectile"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerThrewProjectileEventArgs"/> of the event.</param>
    public static void OnThrewProjectile(PlayerThrewProjectileEventArgs args) => ThrewProjectile.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="TogglingFlashlight"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerTogglingFlashlightEventArgs"/> of the event.</param>
    public static void OnTogglingFlashlight(PlayerTogglingFlashlightEventArgs args) => TogglingFlashlight.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ToggledFlashlight"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerToggledFlashlightEventArgs"/> of the event.</param>
    public static void OnToggledFlashlight(PlayerToggledFlashlightEventArgs args) => ToggledFlashlight.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="TogglingWeaponFlashlight"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerTogglingWeaponFlashlightEventArgs"/> of the event.</param>
    public static void OnTogglingWeaponFlashlight(PlayerTogglingWeaponFlashlightEventArgs args) => TogglingWeaponFlashlight.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ToggledWeaponFlashlight"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerToggledWeaponFlashlightEventArgs"/> of the event.</param>
    public static void OnToggledWeaponFlashlight(PlayerToggledWeaponFlashlightEventArgs args) => ToggledWeaponFlashlight.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="TogglingRadio"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerTogglingRadioEventArgs"/> of the event.</param>
    public static void OnTogglingRadio(PlayerTogglingRadioEventArgs args) => TogglingRadio.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ToggledRadio"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerToggledRadioEventArgs"/> of the event.</param>
    public static void OnToggledRadio(PlayerToggledRadioEventArgs args) => ToggledRadio.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Uncuffing"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerUncuffingEventArgs"/> of the event.</param>
    public static void OnUncuffing(PlayerUncuffingEventArgs args) => Uncuffing.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Uncuffed"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerUncuffedEventArgs"/> of the event.</param>
    public static void OnUncuffed(PlayerUncuffedEventArgs args) => Uncuffed.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="UnloadingWeapon"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerUnloadingWeaponEventArgs"/> of the event.</param>
    public static void OnUnloadingWeapon(PlayerUnloadingWeaponEventArgs args) => UnloadingWeapon.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="UnloadedWeapon"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerUnloadedWeaponEventArgs"/> of the event.</param>
    public static void OnUnloadedWeapon(PlayerUnloadedWeaponEventArgs args) => UnloadedWeapon.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Unmuting"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerUnmutingEventArgs"/> of the event.</param>
    public static void OnUnmuting(PlayerUnmutingEventArgs args) => Unmuting.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="Unmuted"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerUnmutedEventArgs"/> of the event.</param>
    public static void OnUnmuted(PlayerUnmutedEventArgs args) => Unmuted.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="UsingIntercom"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerUsingIntercomEventArgs"/> of the event.</param>
    public static void OnUsingIntercom(PlayerUsingIntercomEventArgs args) => UsingIntercom.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="UsedIntercom"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerUsedIntercomEventArgs"/> of the event.</param>
    public static void OnUsedIntercom(PlayerUsedIntercomEventArgs args) => UsedIntercom.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="UsingItem"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerUsingItemEventArgs"/> of the event.</param>
    public static void OnUsingItem(PlayerUsingItemEventArgs args) => UsingItem.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="UsedItem"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerUsedItemEventArgs"/> of the event.</param>
    public static void OnUsedItem(PlayerUsedItemEventArgs args) => UsedItem.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="UsingRadio"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerUsingRadioEventArgs"/> of the event.</param>
    public static void OnUsingRadio(PlayerUsingRadioEventArgs args) => UsingRadio.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="UsedRadio"/> event.
    /// </summary>
    /// <param name="args">The <see cref="PlayerUsedRadioEventArgs"/> of the event.</param>
    public static void OnUsedRadio(PlayerUsedRadioEventArgs args) => UsedRadio.InvokeEvent(args);
}