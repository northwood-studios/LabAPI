using System.Linq;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.Scp049Events;
using LabApi.Events.Arguments.Scp079Events;
using LabApi.Events.Arguments.Scp096Events;
using LabApi.Events.Arguments.Scp106Events;
using LabApi.Events.Arguments.Scp173Events;
using LabApi.Events.Arguments.Scp914Events;
using LabApi.Events.Arguments.Scp939Events;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Events.Arguments.WarheadEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Console;

namespace LogPlugin;

internal class MyCustomEventsHandler : CustomEventsHandler
{
    public override void OnPlayerActivatingGenerator(PlayerActivatingGeneratorEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerActivatingGenerator)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerActivatedGenerator(PlayerActivatedGeneratorEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerActivatedGenerator)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerAimingWeapon(PlayerAimingWeaponEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerAimingWeapon)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerAimedWeapon(PlayerAimedWeaponEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerAimedWeapon)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerBanning(PlayerBanningEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerBanning)} triggered by {ev.Player?.UserId ?? "Unknown"}");
    }

    public override void OnPlayerBanned(PlayerBannedEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerBanned)} triggered by {ev.Player?.UserId ?? "Unknown"}");
    }

    public override void OnPlayerCancellingUsingItem(PlayerCancellingUsingItemEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerCancellingUsingItem)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerCancelledUsingItem(PlayerCancelledUsingItemEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerCancelledUsingItem)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerChangingItem(PlayerChangingItemEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerChangingItem)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerChangedItem(PlayerChangedItemEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerChangedItem)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerChangingRadioRange(PlayerChangingRadioRangeEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerChangingRadioRange)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerChangedRadioRange(PlayerChangedRadioRangeEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerChangedRadioRange)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerChangingRole(PlayerChangingRoleEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerChangingRole)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerChangedRole(PlayerChangedRoleEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerChangedRole)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerChangedSpectator(PlayerChangedSpectatorEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerChangedSpectator)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerClosingGenerator(PlayerClosingGeneratorEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerClosingGenerator)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerClosedGenerator(PlayerClosedGeneratorEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerClosedGenerator)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerCuffing(PlayerCuffingEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerCuffing)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerCuffed(PlayerCuffedEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerCuffed)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerDamagingShootingTarget(PlayerDamagingShootingTargetEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerDamagingShootingTarget)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerDamagedShootingTarget(PlayerDamagedShootingTargetEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerDamagedShootingTarget)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerDamagingWindow(PlayerDamagingWindowEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerDamagingWindow)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerDamagedWindow(PlayerDamagedWindowEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerDamagedWindow)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerDeactivatingGenerator(PlayerDeactivatingGeneratorEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerDeactivatingGenerator)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerDeactivatedGenerator(PlayerDeactivatedGeneratorEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerDeactivatedGenerator)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerDying(PlayerDyingEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerDying)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerDeath(PlayerDeathEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerDeath)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerDroppingAmmo(PlayerDroppingAmmoEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerDroppingAmmo)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerDroppedAmmo(PlayerDroppedAmmoEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerDroppedAmmo)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerDroppingItem(PlayerDroppingItemEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerDroppingItem)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerDroppedItem(PlayerDroppedItemEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerDroppedItem)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerDryFiringWeapon(PlayerDryFiringWeaponEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerDryFiringWeapon)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerDryFiredWeapon(PlayerDryFiredWeaponEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerDryFiredWeapon)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerEnteringPocketDimension(PlayerEnteringPocketDimensionEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerEnteringPocketDimension)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerEnteredPocketDimension(PlayerEnteredPocketDimensionEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerEnteredPocketDimension)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerEscaping(PlayerEscapingEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerEscaping)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerEscaped(PlayerEscapedEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerEscaped)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerFlippingCoin(PlayerFlippingCoinEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerFlippingCoin)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerFlippedCoin(PlayerFlippedCoinEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerFlippedCoin)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerGroupChanged(PlayerGroupChangedEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerGroupChanged)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerHurting(PlayerHurtingEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerHurting)} triggered by {ev.Player?.UserId ?? "Unknown"}");
    }

    public override void OnPlayerHurt(PlayerHurtEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerHurt)} triggered by {ev.Player?.UserId ?? "Unknown"}");
    }

    public override void OnPlayerInteractingDoor(PlayerInteractingDoorEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerInteractingDoor)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerInteractedDoor(PlayerInteractedDoorEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerInteractedDoor)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerInteractingElevator(PlayerInteractingElevatorEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerInteractingElevator)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerInteractedElevator(PlayerInteractedElevatorEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerInteractedElevator)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerInteractingGenerator(PlayerInteractingGeneratorEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerInteractingGenerator)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerInteractedGenerator(PlayerInteractedGeneratorEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerInteractedGenerator)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerInteractingLocker(PlayerInteractingLockerEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerInteractingLocker)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerInteractedLocker(PlayerInteractedLockerEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerInteractedLocker)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerInteractingScp330(PlayerInteractingScp330EventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerInteractingScp330)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerInteractedScp330(PlayerInteractedScp330EventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerInteractedScp330)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerInteractingShootingTarget(PlayerInteractingShootingTargetEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerInteractingShootingTarget)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerInteractedShootingTarget(PlayerInteractedShootingTargetEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerInteractedShootingTarget)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerJoined(PlayerJoinedEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerJoined)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerLeft(PlayerLeftEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerLeft)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerKicking(PlayerKickingEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerKicking)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerKicked(PlayerKickedEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerKicked)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerLeavingPocketDimension(PlayerLeavingPocketDimensionEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerLeavingPocketDimension)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerLeftPocketDimension(PlayerLeftPocketDimensionEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerLeftPocketDimension)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerMakingNoise(PlayerMakingNoiseEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerMakingNoise)} triggered by {ev.Player?.UserId ?? "Unknown"}");
    }

    public override void OnPlayerMadeNoise(PlayerMadeNoiseEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerMadeNoise)} triggered by {ev.Player?.UserId ?? "Unknown"}");
    }

    public override void OnPlayerMuting(PlayerMutingEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerMuting)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerMuted(PlayerMutedEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerMuted)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerOpeningGenerator(PlayerOpeningGeneratorEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerOpeningGenerator)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerOpenedGenerator(PlayerOpenedGeneratorEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerOpenedGenerator)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerPickingUpAmmo(PlayerPickingUpAmmoEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerPickingUpAmmo)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerPickedUpAmmo(PlayerPickedUpAmmoEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerPickedUpAmmo)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerPickingUpArmor(PlayerPickingUpArmorEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerPickingUpArmor)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerPickedUpArmor(PlayerPickedUpArmorEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerPickedUpArmor)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerPickingUpItem(PlayerPickingUpItemEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerPickingUpItem)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerPickedUpItem(PlayerPickedUpItemEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerPickedUpItem)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerPickingUpScp330(PlayerPickingUpScp330EventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerPickingUpScp330)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerPickedUpScp330(PlayerPickedUpScp330EventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerPickedUpScp330)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerPlacingBlood(PlayerPlacingBloodEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerPlacingBlood)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerPlacedBlood(PlayerPlacedBloodEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerPlacedBlood)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerPlacingBulletHole(PlayerPlacingBulletHoleEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerPlacingBulletHole)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerPlacedBulletHole(PlayerPlacedBulletHoleEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerPlacedBulletHole)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerPreAuthenticating(PlayerPreAuthenticatingEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerPreAuthenticating)} triggered by {ev.UserId}");
    }

    public override void OnPlayerPreAuthenticated(PlayerPreAuthenticatedEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerPreAuthenticated)} triggered by {ev.UserId}");
    }

    public override void OnPlayerUpdatingEffect(PlayerEffectUpdatingEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerUpdatingEffect)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerUpdatedEffect(PlayerEffectUpdatedEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerUpdatedEffect)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerReceivingVoiceMessage(PlayerReceivingVoiceMessageEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerReceivingVoiceMessage)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerReloadingWeapon(PlayerReloadingWeaponEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerReloadingWeapon)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerReloadedWeapon(PlayerReloadedWeaponEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerReloadedWeapon)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerReportingCheater(PlayerReportingCheaterEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerReportingCheater)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerReportedCheater(PlayerReportedCheaterEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerReportedCheater)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerReportingPlayer(PlayerReportingPlayerEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerReportingPlayer)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerReportedPlayer(PlayerReportedPlayerEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerReportedPlayer)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerReceivingLoadout(PlayerReceivingLoadoutEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerReceivingLoadout)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerReceivedLoadout(PlayerReceivedLoadoutEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerReceivedLoadout)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerSearchingAmmo(PlayerSearchingAmmoEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerSearchingAmmo)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerSearchedAmmo(PlayerSearchedAmmoEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerSearchedAmmo)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerSearchingArmor(PlayerSearchingArmorEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerSearchingArmor)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerSearchedArmor(PlayerSearchedArmorEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerSearchedArmor)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerSearchingPickup(PlayerSearchingPickupEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerSearchingPickup)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerSearchedPickup(PlayerSearchedPickupEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerSearchedPickup)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerSendingVoiceMessage(PlayerSendingVoiceMessageEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerSendingVoiceMessage)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerShootingWeapon(PlayerShootingWeaponEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerShootingWeapon)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerShotWeapon(PlayerShotWeaponEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerShotWeapon)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerSpawning(PlayerSpawningEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerSpawning)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerSpawned(PlayerSpawnedEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerSpawned)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerSpawningRagdoll(PlayerSpawningRagdollEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerSpawningRagdoll)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerSpawnedRagdoll(PlayerSpawnedRagdollEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerSpawnedRagdoll)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerThrowingItem(PlayerThrowingItemEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerThrowingItem)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerThrewItem(PlayerThrewItemEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerThrewItem)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerThrowingProjectile(PlayerThrowingProjectileEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerThrowingProjectile)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerThrewProjectile(PlayerThrewProjectileEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerThrewProjectile)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerTogglingFlashlight(PlayerTogglingFlashlightEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerTogglingFlashlight)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerToggledFlashlight(PlayerToggledFlashlightEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerToggledFlashlight)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerTogglingWeaponFlashlight(PlayerTogglingWeaponFlashlightEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerTogglingWeaponFlashlight)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerToggledWeaponFlashlight(PlayerToggledWeaponFlashlightEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerToggledWeaponFlashlight)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerTogglingRadio(PlayerTogglingRadioEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerTogglingRadio)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerToggledRadio(PlayerToggledRadioEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerToggledRadio)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerUncuffing(PlayerUncuffingEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerUncuffing)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerUncuffed(PlayerUncuffedEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerUncuffed)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerUnloadingWeapon(PlayerUnloadingWeaponEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerUnloadingWeapon)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerUnloadedWeapon(PlayerUnloadedWeaponEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerUnloadedWeapon)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerUnmuting(PlayerUnmutingEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerUnmuting)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerUnmuted(PlayerUnmutedEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerUnmuted)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerUsingIntercom(PlayerUsingIntercomEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerUsingIntercom)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerUsedIntercom(PlayerUsedIntercomEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerUsedIntercom)} triggered by {ev.Player?.UserId ?? "Unknown"}");
    }

    public override void OnPlayerUsingItem(PlayerUsingItemEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerUsingItem)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerUsedItem(PlayerUsedItemEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerUsedItem)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerTogglingNoclip(PlayerTogglingNoclipEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerTogglingNoclip)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerToggledNoclip(PlayerToggledNoclipEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerToggledNoclip)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerEnteringHazard(PlayerEnteringHazardEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerEnteringHazard)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerEnteredHazard(PlayerEnteredHazardEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerEnteredHazard)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerStayingInHazard(PlayersStayingInHazardEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerStayingInHazard)} triggered by {string.Join(", ", ev.AffectedPlayers.Select(x => x.UserId))}");
    }

    public override void OnPlayerLeavingHazard(PlayerLeavingHazardEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerLeavingHazard)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerLeftHazard(PlayerLeftHazardEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerLeftHazard)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp049StartingResurrection(Scp049StartingResurrectionEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp049StartingResurrection)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp049ResurrectingBody(Scp049ResurrectingBodyEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp049ResurrectingBody)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp049ResurrectedBody(Scp049ResurrectedBodyEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp049ResurrectedBody)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp049UsingDoctorsCall(Scp049UsingDoctorsCallEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp049UsingDoctorsCall)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp049UsedDoctorsCall(Scp049UsedDoctorsCallEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp049UsedDoctorsCall)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp049UsingSense(Scp049UsingSenseEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp049UsingSense)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp049UsedSense(Scp049UsedSenseEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp049UsedSense)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp079BlackingOutRoom(Scp079BlackingOutRoomEventsArgs ev)
    {
        Logger.Info($"{nameof(OnScp079BlackingOutRoom)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp079BlackedOutRoom(Scp079BlackedOutRoomEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp079BlackedOutRoom)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp079BlackingOutZone(Scp079BlackingOutZoneEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp079BlackingOutZone)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp079BlackedOutZone(Scp079BlackedOutZoneEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp079BlackedOutZone)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp079ChangingCamera(Scp079ChangingCameraEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp079ChangingCamera)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp079ChangedCamera(Scp079ChangedCameraEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp079ChangedCamera)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp079CancellingRoomLockdown(Scp079CancellingRoomLockdownEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp079CancellingRoomLockdown)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp079CancelledRoomLockdown(Scp079CancelledRoomLockdownEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp079CancelledRoomLockdown)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp079GainingExperience(Scp079GainingExperienceEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp079GainingExperience)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp079GainedExperience(Scp079GainedExperienceEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp079GainedExperience)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp079LevelingUp(Scp079LevelingUpEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp079LevelingUp)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp079LeveledUp(Scp079LeveledUpEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp079LeveledUp)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp079LockingDoor(Scp079LockingDoorEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp079LockingDoor)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp079LockedDoor(Scp079LockedDoorEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp079LockedDoor)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp079LockingDownRoom(Scp079LockingDownRoomEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp079LockingDownRoom)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp079LockedDownRoom(Scp079LockedDownRoomEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp079LockedDownRoom)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp079UnlockingDoor(Scp079UnlockingDoorEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp079UnlockingDoor)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp079UnlockedDoor(Scp079UnlockedDoorEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp079UnlockedDoor)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp079UsingTesla(Scp079UsingTeslaEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp079UsingTesla)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp079UsedTesla(Scp079UsedTeslaEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp079UsedTesla)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp096AddingTarget(Scp096AddingTargetEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp096AddingTarget)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp096AddedTarget(Scp096AddedTargetEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp096AddedTarget)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp096ChangingState(Scp096ChangingStateEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp096ChangingState)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp096ChangedState(Scp096ChangedStateEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp096ChangedState)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp096Charging(Scp096ChargingEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp096Charging)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp096Charged(Scp096ChargedEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp096Charged)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp096Enraging(Scp096EnragingEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp096Enraging)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp096Enraged(Scp096EnragedEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp096Enraged)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp096PryingGate(Scp096PryingGateEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp096PryingGate)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp096PriedGate(Scp096PriedGateEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp096PriedGate)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp096StartCrying(Scp096StartCryingEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp096StartCrying)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp096StartedCrying(Scp096StartedCryingEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp096StartedCrying)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp096TryingNotToCry(Scp096TryingNotToCryEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp096TryingNotToCry)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp096TriedNotToCry(Scp096TriedNotToCryEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp096TriedNotToCry)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp106ChangingStalkMode(Scp106ChangingStalkModeEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp106ChangingStalkMode)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp106ChangedStalkMode(Scp106ChangedStalkModeEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp106ChangedStalkMode)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp106ChangingVigor(Scp106ChangingVigorEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp106ChangingVigor)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp106ChangedVigor(Scp106ChangedVigorEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp106ChangedVigor)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp106UsedHunterAtlas(Scp106UsedHunterAtlasEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp106UsedHunterAtlas)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp106UsingHunterAtlas(Scp106UsingHunterAtlasEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp106UsingHunterAtlas)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp106ChangingSubmersionStatus(Scp106ChangingSubmersionStatusEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp106ChangingSubmersionStatus)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp106ChangedSubmersionStatus(Scp106ChangedSubmersionStatusEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp106ChangedSubmersionStatus)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp173BreakneckSpeedChanging(Scp173BreakneckSpeedChangingEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp173BreakneckSpeedChanging)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp173BreakneckSpeedChanged(Scp173BreakneckSpeedChangedEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp173BreakneckSpeedChanged)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp173AddingObserver(Scp173AddingObserverEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp173AddingObserver)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp173AddedObserver(Scp173AddedObserverEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp173AddedObserver)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp173RemovingObserver(Scp173RemovingObserverEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp173RemovingObserver)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp173RemovedObserver(Scp173RemovedObserverEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp173RemovedObserver)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp173CreatingTantrum(Scp173CreatingTantrumEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp173CreatingTantrum)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp173CreatedTantrum(Scp173CreatedTantrumEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp173CreatedTantrum)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp173PlayingSound(Scp173PlayingSoundEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp173PlayingSound)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp173PlayedSound(Scp173PlayedSoundEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp173PlayedSound)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp914Activating(Scp914ActivatingEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp914Activating)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp914Activated(Scp914ActivatedEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp914Activated)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp914KnobChanging(Scp914KnobChangingEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp914KnobChanging)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp914KnobChanged(Scp914KnobChangedEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp914KnobChanged)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp914ProcessingPickup(Scp914ProcessingPickupEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp914ProcessingPickup)} triggered");
    }

    public override void OnScp914ProcessedPickup(Scp914ProcessedPickupEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp914ProcessedPickup)} triggered");
    }

    public override void OnScp914ProcessingPlayer(Scp914ProcessingPlayerEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp914ProcessingPlayer)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp914ProcessedPlayer(Scp914ProcessedPlayerEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp914ProcessedPlayer)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp914ProcessingInventoryItem(Scp914ProcessingInventoryItemEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp914ProcessingInventoryItem)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp914ProcessedInventoryItem(Scp914ProcessedInventoryItemEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp914ProcessedInventoryItem)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp939Attacking(Scp939AttackingEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp939Attacking)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp939Attacked(Scp939AttackedEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp939Attacked)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp939CreatingAmnesticCloud(Scp939CreatingAmnesticCloudEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp939CreatingAmnesticCloud)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp939CreatedAmnesticCloud(Scp939CreatedAmnesticCloudEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp939CreatedAmnesticCloud)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp939Lunging(Scp939LungingEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp939Lunging)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp939Lunged(Scp939LungedEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp939Lunged)} triggered by {ev.Player.UserId}");
    }

    public override void OnServerWaitingForPlayers()
    {
        Logger.Info($"{nameof(OnServerWaitingForPlayers)} triggered");
    }

    public override void OnServerRoundRestarted()
    {
        Logger.Info($"{nameof(OnServerRoundRestarted)} triggered");
    }

    public override void OnServerRoundEnding(RoundEndingEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerRoundEnding)} triggered");
    }

    public override void OnServerRoundEnded(RoundEndedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerRoundEnded)} triggered");
    }

    public override void OnServerRoundStarting(RoundStartingEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerRoundStarting)} triggered");
    }

    public override void OnServerRoundStarted()
    {
        Logger.Info($"{nameof(OnServerRoundStarted)} triggered");
    }

    public override void OnServerBanIssuing(BanIssuingEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerBanIssuing)} triggered");
    }

    public override void OnServerBanIssued(BanIssuedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerBanIssued)} triggered");
    }

    public override void OnServerBanRevoking(BanRevokingEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerBanRevoking)} triggered");
    }

    public override void OnServerBanRevoked(BanRevokedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerBanRevoked)} triggered");
    }

    public override void OnServerBanUpdating(BanUpdatingEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerBanUpdating)} triggered");
    }

    public override void OnServerBanUpdated(BanUpdatedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerBanUpdated)} triggered");
    }

    public override void OnServerCommandExecuting(CommandExecutingEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerCommandExecuting)} triggered");
    }

    public override void OnServerCommandExecuted(CommandExecutedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerCommandExecuted)} triggered");
    }

    public override void OnServerWaveRespawning(WaveRespawningEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerWaveRespawning)} triggered");
    }

    public override void OnServerWaveRespawned(WaveRespawnedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerWaveRespawned)} triggered");
    }

    public override void OnServerWaveTeamSelecting(WaveTeamSelectingEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerWaveTeamSelecting)} triggered");
    }

    public override void OnServerWaveTeamSelected(WaveTeamSelectedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerWaveTeamSelected)} triggered");
    }

    public override void OnServerLczDecontaminationAnnounced(LczDecontaminationAnnouncedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerLczDecontaminationAnnounced)} triggered");
    }

    public override void OnServerLczDecontaminationStarting(LczDecontaminationStartingEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerLczDecontaminationStarting)} triggered");
    }

    public override void OnServerLczDecontaminationStarted()
    {
        Logger.Info($"{nameof(OnServerLczDecontaminationStarted)} triggered");
    }

    public override void OnServerMapGenerating(MapGeneratingEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerMapGenerating)} triggered");
    }

    public override void OnServerMapGenerated(MapGeneratedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerMapGenerated)} triggered");
    }

    public override void OnServerItemSpawning(ItemSpawningEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerItemSpawning)} triggered");
    }

    public override void OnServerItemSpawned(ItemSpawnedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerItemSpawned)} triggered");
    }

    public override void OnServerCassieAnnouncing(CassieAnnouncingEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerCassieAnnouncing)} triggered");
    }

    public override void OnServerCassieAnnounced(CassieAnnouncedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerCassieAnnounced)} triggered");
    }

    public override void OnServerGrenadeExploding(GrenadeExplodingEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerGrenadeExploding)} triggered by {ev.Player.UserId}");
    }

    public override void OnServerGrenadeExploded(GrenadeExplodedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerGrenadeExploded)} triggered by {ev.Player.UserId}");
    }

    public override void OnServerGeneratorActivating(GeneratorActivatingEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerGeneratorActivating)} triggered");
    }

    public override void OnServerGeneratorActivated(GeneratorActivatedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerGeneratorActivated)} triggered");
    }

    public override void OnWarheadStarting(WarheadStartingEventArgs ev)
    {
        Logger.Info($"{nameof(OnWarheadStarting)} triggered by {ev.Player.UserId}");
    }

    public override void OnWarheadStarted(WarheadStartedEventArgs ev)
    {
        Logger.Info($"{nameof(OnWarheadStarted)} triggered by {ev.Player.UserId}");
    }

    public override void OnWarheadStopping(WarheadStoppingEventArgs ev)
    {
        Logger.Info($"{nameof(OnWarheadStopping)} triggered by {ev.Player.UserId}");
    }

    public override void OnWarheadStopped(WarheadStoppedEventArgs ev)
    {
        Logger.Info($"{nameof(OnWarheadStopped)} triggered by {ev.Player.UserId}");
    }

    public override void OnWarheadDetonating(WarheadDetonatingEventArgs ev)
    {
        Logger.Info($"{nameof(OnWarheadDetonating)} triggered by {ev.Player.UserId}");
    }

    public override void OnWarheadDetonated(WarheadDetonatedEventArgs ev)
    {
        Logger.Info($"{nameof(OnWarheadDetonated)} triggered by {ev.Player.UserId}");
    }

    #region Excluded Events

    // The following events spam the console and are therefore excluded from this example:

    // public override void OnPlayerUsingRadio(PlayerUsingRadioEventArgs ev)
    // {
    //     Logger.Info($"{nameof(OnPlayerUsingRadio)} triggered by {ev.Player.UserId}");
    // }
    //
    // public override void OnPlayerUsedRadio(PlayerUsedRadioEventArgs ev)
    // {
    //     Logger.Info($"{nameof(OnPlayerUsedRadio)} triggered by {ev.Player.UserId}");
    // }

    #endregion

}