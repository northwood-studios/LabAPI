using System.Linq;
using LabApi.Events.Arguments.ObjectiveEvents;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.Scp0492Events;
using LabApi.Events.Arguments.Scp049Events;
using LabApi.Events.Arguments.Scp079Events;
using LabApi.Events.Arguments.Scp096Events;
using LabApi.Events.Arguments.Scp106Events;
using LabApi.Events.Arguments.Scp127Events;
using LabApi.Events.Arguments.Scp173Events;
using LabApi.Events.Arguments.Scp3114Events;
using LabApi.Events.Arguments.Scp914Events;
using LabApi.Events.Arguments.Scp939Events;
using LabApi.Events.Arguments.ScpEvents;
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

    public override void OnPlayerUnlockingGenerator(PlayerUnlockingGeneratorEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerUnlockingGenerator)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerUnlockedGenerator(PlayerUnlockedGeneratorEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerUnlockedGenerator)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerUnlockingWarheadButton(PlayerUnlockingWarheadButtonEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerUnlockingWarheadButton)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerUnlockedWarheadButton(PlayerUnlockedWarheadButtonEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerUnlockedWarheadButton)} triggered by {ev.Player.UserId}");
    }

    //public override void OnPlayerAimingWeapon(PlayerAimingWeaponEventArgs ev)
    //{
    //    Logger.Info($"{nameof(OnPlayerAimingWeapon)} triggered by {ev.Player.UserId}");
    //}

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

    public override void OnPlayerChangingNickname(PlayerChangingNicknameEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerChangingNickname)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerChangedNickname(PlayerChangedNicknameEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerChangedNickname)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerChangingBadgeVisibility(PlayerChangingBadgeVisibilityEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerChangingBadgeVisibility)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerChangedBadgeVisibility(PlayerChangedBadgeVisibilityEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerChangedBadgeVisibility)} triggered by {ev.Player.UserId}");
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
        Logger.Info($"{nameof(OnPlayerFlippingCoin)} triggered by {ev.Player.UserId}. Item Serial {ev.Item.Serial}");
    }

    public override void OnPlayerFlippedCoin(PlayerFlippedCoinEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerFlippedCoin)} triggered by {ev.Player.UserId}. Item Serial {ev.Item.Serial}");
    }

    public override void OnPlayerGroupChanging(PlayerGroupChangingEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerGroupChanging)} triggered by {ev.Player.UserId}");
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

    public override void OnPlayerIdlingTesla(PlayerIdlingTeslaEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerIdlingTesla)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerIdledTesla(PlayerIdledTeslaEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerIdledTesla)} triggered by {ev.Player.UserId}");
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

    public override void OnPlayerInteractedToy(PlayerInteractedToyEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerInteractedToy)} triggered by {ev.Player.UserId}");
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

    public override void OnPlayerProcessingJailbirdMessage(PlayerProcessingJailbirdMessageEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerProcessingJailbirdMessage)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerProcessedJailbirdMessage(PlayerProcessedJailbirdMessageEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerProcessedJailbirdMessage)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerUpdatingEffect(PlayerEffectUpdatingEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerUpdatingEffect)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerUpdatedEffect(PlayerEffectUpdatedEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerUpdatedEffect)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerRaPlayerListAddedPlayer(PlayerRaPlayerListAddedPlayerEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerRaPlayerListAddedPlayer)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerRaPlayerListAddingPlayer(PlayerRaPlayerListAddingPlayerEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerRaPlayerListAddingPlayer)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerReceivedAchievement(PlayerReceivedAchievementEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerReceivedAchievement)} triggered by {ev.Player?.UserId ?? "unknown"}");
    }

    public override void OnPlayerRequestedCustomRaInfo(PlayerRequestedCustomRaInfoEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerRequestedCustomRaInfo)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerRequestedRaPlayerInfo(PlayerRequestedRaPlayerInfoEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerRequestedRaPlayerInfo)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerRequestedRaPlayerList(PlayerRequestedRaPlayerListEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerRequestedRaPlayerList)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerRequestedRaPlayersInfo(PlayerRequestedRaPlayersInfoEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerRequestedRaPlayersInfo)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerRequestingRaPlayerInfo(PlayerRequestingRaPlayerInfoEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerRequestingRaPlayerInfo)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerRequestingRaPlayerList(PlayerRequestingRaPlayerListEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerRequestingRaPlayerList)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerRequestingRaPlayersInfo(PlayerRequestingRaPlayersInfoEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerRequestingRaPlayersInfo)} triggered by {ev.Player.UserId}");
    }

    //public override void OnPlayerReceivingVoiceMessage(PlayerReceivingVoiceMessageEventArgs ev)
    //{
    //    Logger.Info($"{nameof(OnPlayerReceivingVoiceMessage)} triggered by {ev.Player.UserId}");
    //}

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

    public override void OnPlayerSearchingToy(PlayerSearchingToyEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerSearchingToy)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerSearchedToy(PlayerSearchedToyEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerSearchedToy)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerSearchToyAborted(PlayerSearchToyAbortedEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerSearchToyAborted)} triggered by {ev.Player.UserId}");
    }

    //public override void OnPlayerSendingVoiceMessage(PlayerSendingVoiceMessageEventArgs ev)
    //{
    //    Logger.Info($"{nameof(OnPlayerSendingVoiceMessage)} triggered by {ev.Player.UserId}");
    //}

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

    public override void OnPlayerTriggeringTesla(PlayerTriggeringTeslaEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerTriggeringTesla)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerTriggeredTesla(PlayerTriggeredTeslaEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerTriggeredTesla)} triggered by {ev.Player.UserId}");
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

    public override void OnPlayerItemUsageEffectsApplying(PlayerItemUsageEffectsApplyingEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerItemUsageEffectsApplying)} triggered by {ev.Player.UserId}");
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

    public override void OnScp049Attacking(Scp049AttackingEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp049Attacking)} triggered by {ev.Player} {ev.Target} {ev.InstantKill} {ev.IsSenseTarget} {ev.CooldownTime}");
    }

    public override void OnScp049Attacked(Scp049AttackedEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp049Attacked)} triggered by {ev.Player} {ev.Target} {ev.InstantKill} {ev.IsSenseTarget}");
    }

    public override void OnScp049SenseKilledTarget(Scp049SenseKilledTargetEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp049SenseKilledTarget)} triggered by {ev.Player} {ev.Target}");
    }

    public override void OnScp049SenseLostTarget(Scp049SenseLostTargetEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp049SenseLostTarget)} triggered by {ev.Player} {ev.Target}");
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

    public override void OnScp0492StartingConsumingCorpse(Scp0492StartingConsumingCorpseEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp0492StartingConsumingCorpse)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp0492StartedConsumingCorpse(Scp0492StartedConsumingCorpseEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp0492StartedConsumingCorpse)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp0492ConsumingCorpse(Scp0492ConsumingCorpseEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp0492ConsumingCorpse)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp0492ConsumedCorpse(Scp0492ConsumedCorpseEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp0492ConsumedCorpse)} triggered by {ev.Player.UserId}");
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

    public override void OnScp079Recontaining(Scp079RecontainingEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp079Recontaining)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp079Recontained(Scp079RecontainedEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp079Recontained)} triggered by {ev.Player.UserId}");
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

    public override void OnScp106TeleportingPlayer(Scp106TeleportingPlayerEvent ev)
    {
        Logger.Info($"{nameof(OnScp106TeleportingPlayer)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp106TeleportedPlayer(Scp106TeleportedPlayerEvent ev)
    {
        Logger.Info($"{nameof(OnScp106TeleportedPlayer)} triggered by {ev.Player.UserId}");
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

    public override void OnScp173Snapped(Scp173SnappedEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp173Snapped)} triggered by {ev.Player.UserId} targeting {ev.Target.UserId}");
    }

    public override void OnScp173Snapping(Scp173SnappingEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp173Snapping)} triggered by {ev.Player.UserId} targeting {ev.Target.UserId}");
    }

    public override void OnScp3114StrangleAborting(Scp3114StrangleAbortingEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp3114StrangleAborting)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp3114StrangleAborted(Scp3114StrangleAbortedEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp3114StrangleAborted)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp3114StrangleStarting(Scp3114StrangleStartingEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp3114StrangleStarting)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp3114StrangleStarted(Scp3114StrangleStartedEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp3114StrangleStarted)} triggered by {ev.Player.UserId}");
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

    public override void OnServerPickupCreated(PickupCreatedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerPickupCreated)} triggered");
    }

    public override void OnServerPickupDestroyed(PickupDestroyedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerPickupDestroyed)} triggered");
    }

    public override void OnServerSendingAdminChat(SendingAdminChatEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerSendingAdminChat)} triggered by {ev.Sender.LogName}");
    }

    public override void OnServerSentAdminChat(SentAdminChatEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerSentAdminChat)} triggered by {ev.Sender.LogName}");
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

    public override void OnServerCassieQueuingScpTermination(CassieQueuingScpTerminationEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerCassieQueuingScpTermination)} triggered");
    }

    public override void OnServerCassieQueuedScpTermination(CassieQueuedScpTerminationEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerCassieQueuedScpTermination)} triggered");
    }

    public override void OnServerProjectileExploding(ProjectileExplodingEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerProjectileExploding)} triggered");
    }

    public override void OnServerProjectileExploded(ProjectileExplodedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerProjectileExploded)} triggered");
    }

    public override void OnServerExplosionSpawning(ExplosionSpawningEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerExplosionSpawning)} triggered");
    }

    public override void OnServerExplosionSpawned(ExplosionSpawnedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerExplosionSpawned)} triggered");
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

    public override void OnObjectiveCompleting(ObjectiveCompletingBaseEventArgs ev)
    {
        Logger.Info($"{nameof(OnObjectiveCompleting)} triggered by {ev.Player.UserId}");
    }

    public override void OnObjectiveCompleted(ObjectiveCompletedBaseEventArgs ev)
    {
        Logger.Info($"{nameof(OnObjectiveCompleted)} triggered by {ev.Player.UserId}");
    }

    public override void OnObjectiveActivatedGeneratorCompleted(GeneratorActivatedObjectiveEventArgs ev)
    {
        Logger.Info($"{nameof(OnObjectiveActivatedGeneratorCompleted)} triggered by {ev.Player.UserId}");
    }

    public override void OnObjectiveActivatingGeneratorCompleting(GeneratorActivatingObjectiveEventArgs ev)
    {
        Logger.Info($"{nameof(OnObjectiveActivatingGeneratorCompleting)} triggered by {ev.Player.UserId}");
    }

    public override void OnObjectiveDamagedScpCompleted(ScpDamagedObjectiveEventArgs ev)
    {
        Logger.Info($"{nameof(OnObjectiveDamagedScpCompleted)} triggered by {ev.Player.UserId}");
    }

    public override void OnObjectiveDamagingScpCompleting(ScpDamagingObjectiveEventArgs ev)
    {
        Logger.Info($"{nameof(OnObjectiveDamagingScpCompleting)} triggered by {ev.Player.UserId}");
    }

    public override void OnObjectiveEscapingCompleting(EscapingObjectiveEventArgs ev)
    {
        Logger.Info($"{nameof(OnObjectiveEscapingCompleting)} triggered by {ev.Player.UserId}");
    }

    public override void OnObjectiveEscapedCompleted(EscapedObjectiveEventArgs ev)
    {
        Logger.Info($"{nameof(OnObjectiveEscapedCompleted)} triggered by {ev.Player.UserId}");
    }

    public override void OnObjectiveKillingEnemyCompleting(EnemyKillingObjectiveEventArgs ev)
    {
        Logger.Info($"{nameof(OnObjectiveKillingEnemyCompleting)} triggered by {ev.Player.UserId}");
    }

    public override void OnObjectiveKilledEnemyCompleted(EnemyKilledObjectiveEventArgs ev)
    {
        Logger.Info($"{nameof(OnObjectiveKilledEnemyCompleted)} triggered by {ev.Player.UserId}");
    }

    public override void OnObjectivePickingScpItemCompleting(ScpItemPickingObjectiveEventArgs ev)
    {
        Logger.Info($"{nameof(OnObjectivePickingScpItemCompleting)} triggered by {ev.Player.UserId}");
    }

    public override void OnObjectivePickedScpItemCompleted(ScpItemPickedObjectiveEventArgs ev)
    {
        Logger.Info($"{nameof(OnObjectivePickedScpItemCompleted)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerJumped(PlayerJumpedEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerJumped)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerMovementStateChanged(PlayerMovementStateChangedEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerMovementStateChanged)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerChangingAttachments(PlayerChangingAttachmentsEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerChangingAttachments)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerChangedAttachments(PlayerChangedAttachmentsEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerChangedAttachments)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerSendingAttachmentsPrefs(PlayerSendingAttachmentsPrefsEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerSendingAttachmentsPrefs)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerSentAttachmentsPrefs(PlayerSentAttachmentsPrefsEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerSentAttachmentsPrefs)} triggered by {ev.Player.UserId}");
    }

    public override void OnServerElevatorSequenceChanged(ElevatorSequenceChangedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerElevatorSequenceChanged)} triggered");
    }

    public override void OnPlayerInteractingWarheadLever(PlayerInteractingWarheadLeverEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerInteractingWarheadLever)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerInteractedWarheadLever(PlayerInteractedWarheadLeverEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerInteractedWarheadLever)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp3114Disguising(Scp3114DisguisingEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp3114Disguising)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp3114Disguised(Scp3114DisguisedEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp3114Disguised)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp3114Revealing(Scp3114RevealingEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp3114Revealing)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp3114Revealed(Scp3114RevealedEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp3114Revealed)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp3114StartDancing(Scp3114StartingDanceEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp3114StartDancing)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp3114Dance(Scp3114StartedDanceEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp3114Dance)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerToggledDisruptorFiringMode(PlayerToggledDisruptorFiringModeEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerToggledDisruptorFiringMode)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerSpinningRevolver(PlayerSpinningRevolverEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerSpinningRevolver)} triggered by {ev.Player.UserId}");
    }

    public override void OnPlayerSpinnedRevolver(PlayerSpinnedRevolverEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerSpinnedRevolver)} triggered by {ev.Player.UserId}");
    }

    public override void OnScp127GainingExperience(Scp127GainingExperienceEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp127GainingExperience)} triggered by {ev.Scp127Item.CurrentOwner}");
    }

    public override void OnScp127GainExperience(Scp127GainExperienceEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp127GainExperience)} triggered by {ev.Scp127Item.CurrentOwner}");
    }

    public override void OnScp127LevellingUp(Scp127LevellingUpEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp127LevellingUp)} triggered by {ev.Scp127Item.CurrentOwner}");
    }

    public override void OnScp127LevelUp(Scp127LevelUpEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp127LevelUp)} triggered by {ev.Scp127Item.CurrentOwner}");
    }

    public override void OnScp127Talking(Scp127TalkingEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp127Talking)} triggered by {ev.Scp127Item.CurrentOwner}");
    }

    public override void OnScp127Talked(Scp127TalkedEventArgs ev)
    {
        Logger.Info($"{nameof(OnScp127Talked)} triggered by {ev.Scp127Item.CurrentOwner}");
    }
    
    public override void OnPlayerCheckedHitmarker(PlayerCheckedHitmarkerEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerCheckedHitmarker)} triggered by {ev.Player} {ev.Victim} {ev.Result}");
    }

    public override void OnPlayerSentHitmarker(PlayerSentHitmarkerEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerSentHitmarker)} triggered by {ev.Player} {ev.Size} {ev.PlayedAudio}");
    }

    public override void OnPlayerSendingHitmarker(PlayerSendingHitmarkerEventArgs ev)
    {
        Logger.Info($"{nameof(OnPlayerSendingHitmarker)} triggered by {ev.Player} {ev.Size} {ev.PlayAudio}");
    }

    public override void OnScpHumeShieldBroken(ScpHumeShieldBrokenEventArgs ev)
    {
        Logger.Info($"{nameof(OnScpHumeShieldBroken)} triggered by {ev.Player}");
    }

    public override void OnServerAchievedMilestone(AchievedMilestoneEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerAchievedMilestone)} triggered by {ev.Faction} {ev.Threshold} {ev.MilestoneIndex}");
    }

    public override void OnServerAchievingMilestone(AchievingMilestoneEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerAchievingMilestone)} triggered by {ev.Faction} {ev.Threshold} {ev.MilestoneIndex}");
    }

    public override void OnServerModifiedFactionInfluence(ModifiedFactionInfluenceEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerModifiedFactionInfluence)} triggered by {ev.Faction} {ev.Influence}");
    }

    public override void OnServerModifyingFactionInfluence(ModifyingFactionInfluenceEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerModifyingFactionInfluence)} triggered by {ev.Faction} {ev.Influence}");
    }

    public override void OnServerBlastDoorChanged(BlastDoorChangedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerBlastDoorChanged)} triggered by {ev.BlastDoor} {ev.NewState}");
    }

    public override void OnServerBlastDoorChanging(BlastDoorChangingEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerBlastDoorChanging)} triggered by {ev.BlastDoor} {ev.NewState}");
    }

    public override void OnServerDoorDamaged(DoorDamagedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerDoorDamaged)} triggered by {ev.Door} {ev.Damage} {ev.DamageType}");
    }

    public override void OnServerDoorDamaging(DoorDamagingEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerDoorDamaging)} triggered by {ev.Door} {ev.Damage} {ev.DamageType}");
    }

    public override void OnServerDoorLockChanged(DoorLockChangedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerDoorLockChanged)} triggered by {ev.Door} {ev.PrevLockReason} => {ev.LockReason}");
    }

    public override void OnServerDoorRepaired(DoorRepairedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerDoorRepaired)} triggered by {ev.Door} {ev.RemainingHealth}");
    }

    public override void OnServerDoorRepairing(DoorRepairingEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerDoorRepairing)} triggered by {ev.Door} {ev.RemainingHealth}");
    }

    public override void OnServerCheckpointDoorSequenceChanged(CheckpointDoorSequenceChangedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerCheckpointDoorSequenceChanged)} triggered by {ev.CheckpointDoor} {ev.CurrentSequence}");
    }

    public override void OnServerCheckpointDoorSequenceChanging(CheckpointDoorSequenceChangingEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerCheckpointDoorSequenceChanging)} triggered by {ev.CheckpointDoor} {ev.CurrentSequence} {ev.NewSequence}");
    }

    public override void OnServerRoomColorChanged(RoomColorChangedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerRoomColorChanged)} triggered by {ev.Room} {ev.NewState}");
    }

    public override void OnServerRoomLightChanged(RoomLightChangedEventArgs ev)
    {
        Logger.Info($"{nameof(OnServerRoomLightChanged)} triggered by {ev.Room} {ev.NewState}");
    }

    #region Excluded Events

    // The following events spam the console and are therefore excluded from this example:

    //public override void OnPlayerValidatedVisibility(PlayerValidatedVisibilityEventArgs ev)
    //{
    //    Logger.Info($"{nameof(OnPlayerValidatedVisibility)} triggered by {ev.Player.UserId}");
    //}
    //
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