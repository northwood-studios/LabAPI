using LabApi.Events.Arguments.PlayerEvents;

namespace LabApi.Events.Handlers;

/// <summary>
/// Handles all the events related to the player.
/// </summary>
public static partial class PlayerEvents
{
    #region Connection & VC

    /// <summary>
    /// Gets called when the player has joined.
    /// </summary>
    public static event LabEventHandler<PlayerJoinedEventArgs>? Joined;

    /// <summary>
    /// Gets called when the player has left.
    /// </summary>
    public static event LabEventHandler<PlayerLeftEventArgs>? Left;

    /// <summary>
    /// Gets called when the player is receiving a voice message.
    /// </summary>
    public static event LabEventHandler<PlayerReceivingVoiceMessageEventArgs>? ReceivingVoiceMessage;

    /// <summary>
    /// Gets called when the player is sending a voice message.
    /// </summary>
    public static event LabEventHandler<PlayerSendingVoiceMessageEventArgs>? SendingVoiceMessage;

    /// <summary>
    /// Gets called when the player is pre-authenticating.
    /// </summary>
    public static event LabEventHandler<PlayerPreAuthenticatingEventArgs>? PreAuthenticating;

    /// <summary>
    /// Gets called when the player has pre-authenticated.
    /// </summary>
    public static event LabEventHandler<PlayerPreAuthenticatedEventArgs>? PreAuthenticated;

    /// <summary>
    /// Gets called when the player is using the intercom.
    /// </summary>
    public static event LabEventHandler<PlayerUsingIntercomEventArgs>? UsingIntercom;

    /// <summary>
    /// Gets called when the player has used the intercom.
    /// </summary>
    public static event LabEventHandler<PlayerUsedIntercomEventArgs>? UsedIntercom;

    #endregion

    #region Moderation

    /// <summary>
    /// Gets called when the player is being banned.
    /// </summary>
    public static event LabEventHandler<PlayerBanningEventArgs>? Banning;

    /// <summary>
    /// Gets called when the player is banned.
    /// </summary>
    public static event LabEventHandler<PlayerBannedEventArgs>? Banned;

    /// <summary>
    /// Gets called when the player is being kicked.
    /// </summary>
    public static event LabEventHandler<PlayerKickingEventArgs>? Kicking;

    /// <summary>
    /// Gets called when the player has been kicked.
    /// </summary>
    public static event LabEventHandler<PlayerKickedEventArgs>? Kicked;

    /// <summary>
    /// Gets called when the player is being muted.
    /// </summary>
    public static event LabEventHandler<PlayerMutingEventArgs>? Muting;

    /// <summary>
    /// Gets called when the player has been muted.
    /// </summary>
    public static event LabEventHandler<PlayerMutedEventArgs>? Muted;

    /// <summary>
    /// Gets called when the player is being unmuted.
    /// </summary>
    public static event LabEventHandler<PlayerUnmutingEventArgs>? Unmuting;

    /// <summary>
    /// Gets called when the player has been unmuted.
    /// </summary>
    public static event LabEventHandler<PlayerUnmutedEventArgs>? Unmuted;

    /// <summary>
    /// Gets called when the player is reporting a cheater.
    /// </summary>
    public static event LabEventHandler<PlayerReportingCheaterEventArgs>? ReportingCheater;

    /// <summary>
    /// Gets called when the player has reported a cheater.
    /// </summary>
    public static event LabEventHandler<PlayerReportedCheaterEventArgs>? ReportedCheater;

    /// <summary>
    /// Gets called when the player is reporting another player.
    /// </summary>
    public static event LabEventHandler<PlayerReportingPlayerEventArgs>? ReportingPlayer;

    /// <summary>
    /// Gets called when the player has reported another player.
    /// </summary>
    public static event LabEventHandler<PlayerReportedPlayerEventArgs>? ReportedPlayer;

    /// <summary>
    /// Gets called when the player is attempting to toggle noclip (pressed alt).
    /// </summary>
    public static event LabEventHandler<PlayerTogglingNoclipEventArgs>? TogglingNoclip;

    /// <summary>
    /// Gets called when the player has toggled the noclip.
    /// </summary>
    public static event LabEventHandler<PlayerToggledNoclipEventArgs>? ToggledNoclip;

    /// <summary>
    /// Gets called when the player is requesting the remote admin player list.
    /// </summary>
    public static event LabEventHandler<PlayerRequestingRaPlayerListEventArgs>? RequestingRaPlayerList;

    /// <summary>
    /// Gets called when the player had requested the remote admin player list.
    /// </summary>
    public static event LabEventHandler<PlayerRequestedRaPlayerListEventArgs>? RequestedRaPlayerList;

    /// <summary>
    /// Gets called when adding a target player to the remote admin player list while processing the request for the player.
    /// </summary>
    public static event LabEventHandler<PlayerRaPlayerListAddingPlayerEventArgs>? RaPlayerListAddingPlayer;

    /// <summary>
    /// Gets called when a target player was added to the remote admin player list while processing the request for the player.
    /// </summary>
    public static event LabEventHandler<PlayerRaPlayerListAddedPlayerEventArgs>? RaPlayerListAddedPlayer;

    /// <summary>
    /// Gets called when a player requested info for an unknown target in the remote admin.
    /// </summary>
    public static event LabEventHandler<PlayerRequestedCustomRaInfoEventArgs>? RequestedCustomRaInfo;

    /// <summary>
    /// Gets called when a player is requesting info for multiple players in the remote admin.
    /// </summary>
    public static event LabEventHandler<PlayerRequestingRaPlayersInfoEventArgs>? RequestingRaPlayersInfo;

    /// <summary>
    /// Gets called when a player had requested info for multiple players in the remote admin.
    /// </summary>
    public static event LabEventHandler<PlayerRequestedRaPlayersInfoEventArgs>? RequestedRaPlayersInfo;

    /// <summary>
    /// Gets called when a player is requesting info for a target player in the remote admin.
    /// </summary>
    public static event LabEventHandler<PlayerRequestingRaPlayerInfoEventArgs>? RequestingRaPlayerInfo;

    /// <summary>
    /// Gets called when a player had requested info for a target player in the remote admin.
    /// </summary>
    public static event LabEventHandler<PlayerRequestedRaPlayerInfoEventArgs>? RequestedRaPlayerInfo;

    #endregion

    #region Badges

    /// <summary>
    /// Gets called when the player is changing their global or local badge visibility.
    /// </summary>
    public static event LabEventHandler<PlayerChangingBadgeVisibilityEventArgs>? ChangingBadgeVisibility;

    /// <summary>
    /// Gets called when the player has changed their global or local badge visibility.
    /// </summary>
    public static event LabEventHandler<PlayerChangedBadgeVisibilityEventArgs>? ChangedBadgeVisibility;

    #endregion

    #region Player properties, inventory and effects

    /// <summary>
    /// Gets called when a players' nickname is changing.
    /// </summary>
    public static event LabEventHandler<PlayerChangingNicknameEventArgs>? ChangingNickname;

    /// <summary>
    /// Gets called when a players' nickname has changed.
    /// </summary>
    public static event LabEventHandler<PlayerChangedNicknameEventArgs>? ChangedNickname;

    /// <summary>
    /// Gets called when the player's group is changing.
    /// </summary>
    public static event LabEventHandler<PlayerGroupChangingEventArgs>? GroupChanging;

    /// <summary>
    /// Gets called when the player's group has changed.
    /// </summary>
    public static event LabEventHandler<PlayerGroupChangedEventArgs>? GroupChanged;

    /// <summary>
    /// Gets called when the player is receiving an effect.
    /// </summary>
    public static event LabEventHandler<PlayerEffectUpdatingEventArgs>? UpdatingEffect;

    /// <summary>
    /// Gets called when the player has received an effect.
    /// </summary>
    public static event LabEventHandler<PlayerEffectUpdatedEventArgs>? UpdatedEffect;

    /// <summary>
    /// Gets called when the player is dying.
    /// </summary>
    public static event LabEventHandler<PlayerDyingEventArgs>? Dying;

    /// <summary>
    /// Gets called when the player has died.
    /// </summary>
    public static event LabEventHandler<PlayerDeathEventArgs>? Death;

    /// <summary>
    /// Gets called when the player is getting hurt.
    /// </summary>
    public static event LabEventHandler<PlayerHurtingEventArgs>? Hurting;

    /// <summary>
    /// Gets called when the player has been hurt.
    /// </summary>
    public static event LabEventHandler<PlayerHurtEventArgs>? Hurt;

    /// <summary>
    /// Gets called when the player's role is changing.
    /// </summary>
    public static event LabEventHandler<PlayerChangingRoleEventArgs>? ChangingRole;

    /// <summary>
    /// Gets called when the player's role has changed.
    /// </summary>
    public static event LabEventHandler<PlayerChangedRoleEventArgs>? ChangedRole;

    /// <summary>
    /// Gets called when the player is being disarmed by another.
    /// </summary>
    public static event LabEventHandler<PlayerCuffingEventArgs>? Cuffing;

    /// <summary>
    /// Gets called when the player has been disarmed by another.
    /// </summary>
    public static event LabEventHandler<PlayerCuffedEventArgs>? Cuffed;

    /// <summary>
    /// Gets called when the player is being undisarmed.
    /// </summary>
    public static event LabEventHandler<PlayerUncuffingEventArgs>? Uncuffing;

    /// <summary>
    /// Gets called when the player has been undisarmed.
    /// </summary>
    public static event LabEventHandler<PlayerUncuffedEventArgs>? Uncuffed;

    /// <summary>
    /// Gets called when the player is receiving a loadout.
    /// </summary>
    public static event LabEventHandler<PlayerReceivingLoadoutEventArgs>? ReceivingLoadout;

    /// <summary>
    /// Gets called when the player has received a loadout.
    /// </summary>
    public static event LabEventHandler<PlayerReceivedLoadoutEventArgs>? ReceivedLoadout;

    /// <summary>
    /// Gets called when the player is spawning.
    /// </summary>
    public static event LabEventHandler<PlayerSpawningEventArgs>? Spawning;

    /// <summary>
    /// Gets called when the player has spawned.
    /// </summary>
    public static event LabEventHandler<PlayerSpawnedEventArgs>? Spawned;

    #endregion

    #region Items

    /// <summary>
    /// Gets called when the player is changing their held item.
    /// </summary>
    public static event LabEventHandler<PlayerChangingItemEventArgs>? ChangingItem;

    /// <summary>
    /// Gets called when the player has changed their held item.
    /// </summary>
    public static event LabEventHandler<PlayerChangedItemEventArgs>? ChangedItem;

    /// <summary>
    /// Gets called when the player is dropping ammo.
    /// </summary>
    public static event LabEventHandler<PlayerDroppingAmmoEventArgs>? DroppingAmmo;

    /// <summary>
    /// Gets called when the player has dropped ammo.
    /// </summary>
    public static event LabEventHandler<PlayerDroppedAmmoEventArgs>? DroppedAmmo;

    /// <summary>
    /// Gets called when the player is dropping an item.
    /// </summary>
    public static event LabEventHandler<PlayerDroppingItemEventArgs>? DroppingItem;

    /// <summary>
    /// Gets called when the player has dropped an item.
    /// </summary>
    public static event LabEventHandler<PlayerDroppedItemEventArgs>? DroppedItem;

    /// <summary>
    /// Gets called when the player is picking up ammo.
    /// </summary>
    public static event LabEventHandler<PlayerPickingUpAmmoEventArgs>? PickingUpAmmo;

    /// <summary>
    /// Gets called when the player has picked up ammo.
    /// </summary>
    public static event LabEventHandler<PlayerPickedUpAmmoEventArgs>? PickedUpAmmo;

    /// <summary>
    /// Gets called when the player is picking up armor.
    /// </summary>
    public static event LabEventHandler<PlayerPickingUpArmorEventArgs>? PickingUpArmor;

    /// <summary>
    /// Gets called when the player has picked up armor.
    /// </summary>
    public static event LabEventHandler<PlayerPickedUpArmorEventArgs>? PickedUpArmor;

    /// <summary>
    /// Gets called when the player is picking up an item.
    /// </summary>
    public static event LabEventHandler<PlayerPickingUpItemEventArgs>? PickingUpItem;

    /// <summary>
    /// Gets called when the player has picked up an item.
    /// </summary>
    public static event LabEventHandler<PlayerPickedUpItemEventArgs>? PickedUpItem;

    /// <summary>
    /// Gets called when the player is picking up SCP-330.
    /// </summary>
    public static event LabEventHandler<PlayerPickingUpScp330EventArgs>? PickingUpScp330;

    /// <summary>
    /// Gets called when the player has picked up SCP-330.
    /// </summary>
    public static event LabEventHandler<PlayerPickedUpScp330EventArgs>? PickedUpScp330;

    /// <summary>
    /// Gets called when the player has searched for ammo.
    /// </summary>
    public static event LabEventHandler<PlayerSearchedAmmoEventArgs>? SearchedAmmo;

    /// <summary>
    /// Gets called when the player is searching for armor.
    /// </summary>
    public static event LabEventHandler<PlayerSearchingArmorEventArgs>? SearchingArmor;

    /// <summary>
    /// Gets called when the player has searched for armor.
    /// </summary>
    public static event LabEventHandler<PlayerSearchedArmorEventArgs>? SearchedArmor;

    /// <summary>
    /// Gets called when the player is searching a pickup.
    /// </summary>
    public static event LabEventHandler<PlayerSearchingPickupEventArgs>? SearchingPickup;

    /// <summary>
    /// Gets called when the player has interacted with an invisible interactable toy.
    /// </summary>
    public static event LabEventHandler<PlayerInteractedToyEventArgs>? InteractedToy;

    /// <summary>
    /// Gets called when the player has joined.
    /// </summary>
    public static event LabEventHandler<PlayerSearchedPickupEventArgs>? SearchedPickup;

    /// <summary>
    /// Gets called when the player is searching for ammo.
    /// </summary>
    public static event LabEventHandler<PlayerSearchingAmmoEventArgs>? SearchingAmmo;

    /// <summary>
    /// Gets called when the player is throwing an item.
    /// </summary>
    public static event LabEventHandler<PlayerThrowingItemEventArgs>? ThrowingItem;

    /// <summary>
    /// Gets called when the player has thrown an item.
    /// </summary>
    public static event LabEventHandler<PlayerThrewItemEventArgs>? ThrewItem;

    /// <summary>
    /// Gets called when the player is throwing a projectile.
    /// </summary>
    public static event LabEventHandler<PlayerThrowingProjectileEventArgs>? ThrowingProjectile;

    /// <summary>
    /// Gets called when the player has thrown a projectile.
    /// </summary>
    public static event LabEventHandler<PlayerThrewProjectileEventArgs>? ThrewProjectile;

    /// <summary>
    /// Gets called when the player wants to inspect any keycard item.
    /// </summary>
    public static event LabEventHandler<PlayerInspectingKeycardEventArgs>? InspectingKeycard;

    /// <summary>
    /// Gets called when the player inspected keycard item.
    /// </summary>
    public static event LabEventHandler<PlayerInspectedKeycardEventArgs>? InspectedKeycard;

    /// <summary>
    /// Gets called when the player requests to spin the revolver.
    /// </summary>
    public static event LabEventHandler<PlayerSpinningRevolverEventArgs>? SpinningRevolver;

    /// <summary>
    /// Gets called when the player spinned the revolver.
    /// </summary>
    public static event LabEventHandler<PlayerSpinnedRevolverEventArgs>? SpinnedRevolver;

    /// <summary>
    /// Gets called when the player toggled disruptor firing mode.
    /// </summary>
    public static event LabEventHandler<PlayerToggledDisruptorFiringModeEventArgs>? ToggledDisruptorFiringMode;

    #endregion

    #region Item Actions and Interactions

    /// <summary>
    /// Gets called when the player is using an item.
    /// </summary>
    public static event LabEventHandler<PlayerUsingItemEventArgs>? UsingItem;

    /// <summary>
    /// Gets called when the player has used an item.
    /// </summary>
    public static event LabEventHandler<PlayerUsedItemEventArgs>? UsedItem;

    /// <summary>
    /// Gets called when the player is about to complete using an item.
    /// </summary>
    public static event LabEventHandler<PlayerItemUsageEffectsApplyingEventArgs>? ItemUsageEffectsApplying;

    /// <summary>
    /// Gets called when the player is using the radio.
    /// </summary>
    public static event LabEventHandler<PlayerUsingRadioEventArgs>? UsingRadio;

    /// <summary>
    /// Gets called when the player has used the radio.
    /// </summary>
    public static event LabEventHandler<PlayerUsedRadioEventArgs>? UsedRadio;

    ///// <summary>
    ///// Gets called when the player is aiming the weapon.
    ///// </summary>
    // public static event LabEventHandler<PlayerAimingWeaponEventArgs>? AimingWeapon;

    /// <summary>
    /// Gets called when the player aimed the weapon.
    /// </summary>
    public static event LabEventHandler<PlayerAimedWeaponEventArgs>? AimedWeapon;

    /// <summary>
    /// Gets called when the player is dry-firing a weapon.
    /// </summary>
    public static event LabEventHandler<PlayerDryFiringWeaponEventArgs>? DryFiringWeapon;

    /// <summary>
    /// Gets called when the player has dry-fired a weapon.
    /// </summary>
    public static event LabEventHandler<PlayerDryFiredWeaponEventArgs>? DryFiredWeapon;

    /// <summary>
    /// Gets called when the player is unloading a weapon.
    /// </summary>
    public static event LabEventHandler<PlayerUnloadingWeaponEventArgs>? UnloadingWeapon;

    /// <summary>
    /// Gets called when the player has unloaded a weapon.
    /// </summary>
    public static event LabEventHandler<PlayerUnloadedWeaponEventArgs>? UnloadedWeapon;

    /// <summary>
    /// Gets called when the player is reloading a weapon.
    /// </summary>
    public static event LabEventHandler<PlayerReloadingWeaponEventArgs>? ReloadingWeapon;

    /// <summary>
    /// Gets called when the player has reloaded a weapon.
    /// </summary>
    public static event LabEventHandler<PlayerReloadedWeaponEventArgs>? ReloadedWeapon;

    /// <summary>
    /// Gets called when the player is shooting from a weapon.
    /// </summary>
    public static event LabEventHandler<PlayerShootingWeaponEventArgs>? ShootingWeapon;

    /// <summary>
    /// Gets called when the player has shot from a weapon.
    /// </summary>
    public static event LabEventHandler<PlayerShotWeaponEventArgs>? ShotWeapon;

    /// <summary>
    /// Gets called when player is changing weapon attachments.
    /// </summary>
    public static event LabEventHandler<PlayerChangingAttachmentsEventArgs>? ChangingAttachments;

    /// <summary>
    /// Gets called when player has changed weapon attachments.
    /// </summary>
    public static event LabEventHandler<PlayerChangedAttachmentsEventArgs>? ChangedAttachments;

    /// <summary>
    /// Gets called when player is changing weapon attachments preferences.
    /// </summary>
    public static event LabEventHandler<PlayerSendingAttachmentsPrefsEventArgs>? SendingAttachmentsPrefs;

    /// <summary>
    /// Gets called when player has changed weapon attachments preferences.
    /// </summary>
    public static event LabEventHandler<PlayerSentAttachmentsPrefsEventArgs>? SentAttachmentsPrefs;

    /// <summary>
    /// Gets called when the player is cancelling the use of an item.
    /// </summary>
    public static event LabEventHandler<PlayerCancellingUsingItemEventArgs>? CancellingUsingItem;

    /// <summary>
    /// Gets called when the player has cancelled the use of an item.
    /// </summary>
    public static event LabEventHandler<PlayerCancelledUsingItemEventArgs>? CancelledUsingItem;

    /// <summary>
    /// Gets called when the player is changing range of their radio.
    /// </summary>
    public static event LabEventHandler<PlayerChangingRadioRangeEventArgs>? ChangingRadioRange;

    /// <summary>
    /// Gets called when the player has changed range of their radio.
    /// </summary>
    public static event LabEventHandler<PlayerChangedRadioRangeEventArgs>? ChangedRadioRange;

    /// <summary>
    /// Gets called when processing a player's interaction with the jailbird item.
    /// </summary>
    public static event LabEventHandler<PlayerProcessingJailbirdMessageEventArgs>? ProcessingJailbirdMessage;

    /// <summary>
    /// Gets called when processed the player's interaction with the jailbird item.
    /// </summary>
    public static event LabEventHandler<PlayerProcessedJailbirdMessageEventArgs>? ProcessedJailbirdMessage;

    /// <summary>
    /// Gets called when the player is toggling a flashlight.
    /// </summary>
    public static event LabEventHandler<PlayerTogglingFlashlightEventArgs>? TogglingFlashlight;

    /// <summary>
    /// Gets called when the player has toggled a flashlight.
    /// </summary>
    public static event LabEventHandler<PlayerToggledFlashlightEventArgs>? ToggledFlashlight;

    /// <summary>
    /// Gets called when the player is toggling a weapon flashlight.
    /// </summary>
    public static event LabEventHandler<PlayerTogglingWeaponFlashlightEventArgs>? TogglingWeaponFlashlight;

    /// <summary>
    /// Gets called when the player has toggled a weapon flashlight.
    /// </summary>
    public static event LabEventHandler<PlayerToggledWeaponFlashlightEventArgs>? ToggledWeaponFlashlight;

    /// <summary>
    /// Gets called when the player is toggling a radio.
    /// </summary>
    public static event LabEventHandler<PlayerTogglingRadioEventArgs>? TogglingRadio;

    /// <summary>
    /// Gets called when the player has toggled a radio.
    /// </summary>
    public static event LabEventHandler<PlayerToggledRadioEventArgs>? ToggledRadio;

    /// <summary>
    /// Gets called when player successfully jumps. Not called when jumping is prevented by status effects.
    /// </summary>
    public static event LabEventHandler<PlayerJumpedEventArgs>? Jumped;

    /// <summary>
    /// Gets called when player's movement state change. Such as from walking to running, sneaking and opposite way around.
    /// </summary>
    public static event LabEventHandler<PlayerMovementStateChangedEventArgs>? MovementStateChanged;

    #endregion

    #region World Interaction

    /// <summary>
    /// Gets called when the player is damaging a shooting target toy.
    /// </summary>
    public static event LabEventHandler<PlayerDamagingShootingTargetEventArgs>? DamagingShootingTarget;

    /// <summary>
    /// Gets called when the player has damaged a shooting target toy.
    /// </summary>
    public static event LabEventHandler<PlayerDamagedShootingTargetEventArgs>? DamagedShootingTarget;

    /// <summary>
    /// Gets called when the player is damaging a window.
    /// </summary>
    public static event LabEventHandler<PlayerDamagingWindowEventArgs>? DamagingWindow;

    /// <summary>
    /// Gets called when the player has damaged a window.
    /// </summary>
    public static event LabEventHandler<PlayerDamagedWindowEventArgs>? DamagedWindow;

    /// <summary>
    /// Gets called when the player is entering the pocket dimension.
    /// </summary>
    public static event LabEventHandler<PlayerEnteringPocketDimensionEventArgs>? EnteringPocketDimension;

    /// <summary>
    /// Gets called when the player has entered the pocket dimension.
    /// </summary>
    public static event LabEventHandler<PlayerEnteredPocketDimensionEventArgs>? EnteredPocketDimension;

    /// <summary>
    /// Gets called when the player is leaving the pocket dimension.
    /// </summary>
    public static event LabEventHandler<PlayerLeavingPocketDimensionEventArgs>? LeavingPocketDimension;

    /// <summary>
    /// Gets called when the player has left the pocket dimension.
    /// </summary>
    public static event LabEventHandler<PlayerLeftPocketDimensionEventArgs>? LeftPocketDimension;

    /// <summary>
    /// Gets called when the player is triggering a <see cref="TeslaGate"/>.
    /// </summary>
    public static event LabEventHandler<PlayerTriggeringTeslaEventArgs>? TriggeringTesla;

    /// <summary>
    /// Gets called when the player has triggered a <see cref="TeslaGate"/>.
    /// </summary>
    public static event LabEventHandler<PlayerTriggeredTeslaEventArgs>? TriggeredTesla;

    /// <summary>
    /// Gets called when the player is escaping.
    /// </summary>
    public static event LabEventHandler<PlayerEscapingEventArgs>? Escaping;

    /// <summary>
    /// Gets called when the player has escaped.
    /// </summary>
    public static event LabEventHandler<PlayerEscapedEventArgs>? Escaped;

    /// <summary>
    /// Gets called when the player is flipping a coin.
    /// </summary>
    public static event LabEventHandler<PlayerFlippingCoinEventArgs>? FlippingCoin;

    /// <summary>
    /// Gets called when the player has flipped a coin.
    /// </summary>
    public static event LabEventHandler<PlayerFlippedCoinEventArgs>? FlippedCoin;

    /// <summary>
    /// Gets called when the player is searching an interactable toy.
    /// </summary>
    public static event LabEventHandler<PlayerSearchingToyEventArgs>? SearchingToy;

    /// <summary>
    /// Gets called when the player has searched an interactable toy.
    /// </summary>
    public static event LabEventHandler<PlayerSearchedToyEventArgs>? SearchedToy;

    /// <summary>
    /// Gets called when the player aborts their interactable toy search.
    /// </summary>
    public static event LabEventHandler<PlayerSearchToyAbortedEventArgs>? SearchToyAborted;

    /// <summary>
    /// Gets called when the player is sending a voice message.
    /// </summary>
    public static event LabEventHandler<PlayerIdlingTeslaEventArgs>? IdlingTesla;

    /// <summary>
    /// Gets called when a player was close enough to a Tesla-Gate for it to idle.
    /// </summary>
    public static event LabEventHandler<PlayerIdledTeslaEventArgs>? IdledTesla;

    /// <summary>
    /// Gets called when the player is interacting with a door.
    /// </summary>
    public static event LabEventHandler<PlayerInteractingDoorEventArgs>? InteractingDoor;

    /// <summary>
    /// Gets called when the player has interacted with a door.
    /// </summary>
    public static event LabEventHandler<PlayerInteractedDoorEventArgs>? InteractedDoor;

    /// <summary>
    /// Gets called when the player is interacting with an elevator.
    /// </summary>
    public static event LabEventHandler<PlayerInteractingElevatorEventArgs>? InteractingElevator;

    /// <summary>
    /// Gets called when the player has interacted with an elevator.
    /// </summary>
    public static event LabEventHandler<PlayerInteractedElevatorEventArgs>? InteractedElevator;

    /// <summary>
    /// Gets called when the player is interacting with a generator.
    /// </summary>
    public static event LabEventHandler<PlayerInteractingGeneratorEventArgs>? InteractingGenerator;

    /// <summary>
    /// Gets called when the player has interacted with a generator.
    /// </summary>
    public static event LabEventHandler<PlayerInteractedGeneratorEventArgs>? InteractedGenerator;

    /// <summary>
    /// Gets called when the player is opening a generator.
    /// </summary>
    public static event LabEventHandler<PlayerOpeningGeneratorEventArgs>? OpeningGenerator;

    /// <summary>
    /// Gets called when the player has opened a generator.
    /// </summary>
    public static event LabEventHandler<PlayerOpenedGeneratorEventArgs>? OpenedGenerator;

    /// <summary>
    /// Gets called when the player is activating the generator.
    /// </summary>
    public static event LabEventHandler<PlayerActivatingGeneratorEventArgs>? ActivatingGenerator;

    /// <summary>
    /// Gets called when the player activates the generator.
    /// </summary>
    public static event LabEventHandler<PlayerActivatedGeneratorEventArgs>? ActivatedGenerator;

    /// <summary>
    /// Gets called when the player is deactivating the generator.
    /// </summary>
    public static event LabEventHandler<PlayerDeactivatingGeneratorEventArgs>? DeactivatingGenerator;

    /// <summary>
    /// Gets called when the player has deactivated the generator.
    /// </summary>
    public static event LabEventHandler<PlayerDeactivatedGeneratorEventArgs>? DeactivatedGenerator;

    /// <summary>
    /// Gets called when the player is unlocking the generator.
    /// </summary>
    public static event LabEventHandler<PlayerUnlockingGeneratorEventArgs>? UnlockingGenerator;

    /// <summary>
    /// Gets called when the player unlocked the generator.
    /// </summary>
    public static event LabEventHandler<PlayerUnlockedGeneratorEventArgs>? UnlockedGenerator;

    /// <summary>
    /// Gets called when the player is closing the generator.
    /// </summary>
    public static event LabEventHandler<PlayerClosingGeneratorEventArgs>? ClosingGenerator;

    /// <summary>
    /// Gets called when the player has closed the generator.
    /// </summary>
    public static event LabEventHandler<PlayerClosedGeneratorEventArgs>? ClosedGenerator;

    /// <summary>
    /// Gets called when the player is interacting with a locker.
    /// </summary>
    public static event LabEventHandler<PlayerInteractingLockerEventArgs>? InteractingLocker;

    /// <summary>
    /// Gets called when the player has interacted with a locker.
    /// </summary>
    public static event LabEventHandler<PlayerInteractedLockerEventArgs>? InteractedLocker;

    /// <summary>
    /// Gets called when the player is interacting with SCP-330.
    /// </summary>
    public static event LabEventHandler<PlayerInteractingScp330EventArgs>? InteractingScp330;

    /// <summary>
    /// Gets called when the player has interacted with SCP-330.
    /// </summary>
    public static event LabEventHandler<PlayerInteractedScp330EventArgs>? InteractedScp330;

    /// <summary>
    /// Gets called when the player is interacting with a shooting target.
    /// </summary>
    public static event LabEventHandler<PlayerInteractingShootingTargetEventArgs>? InteractingShootingTarget;

    /// <summary>
    /// Gets called when the player has interacted with a shooting target.
    /// </summary>
    public static event LabEventHandler<PlayerInteractedShootingTargetEventArgs>? InteractedShootingTarget;

    /// <summary>
    /// Gets called when blood of the player is being placed.
    /// </summary>
    public static event LabEventHandler<PlayerPlacingBloodEventArgs>? PlacingBlood;

    /// <summary>
    /// Gets called when blood of the player was placed.
    /// </summary>
    public static event LabEventHandler<PlayerPlacedBloodEventArgs>? PlacedBlood;

    /// <summary>
    /// Gets called when the bullet hole is being placed.
    /// </summary>
    public static event LabEventHandler<PlayerPlacingBulletHoleEventArgs>? PlacingBulletHole;

    /// <summary>
    /// Gets called when the bullet hole has been placed.
    /// </summary>
    public static event LabEventHandler<PlayerPlacedBulletHoleEventArgs>? PlacedBulletHole;

    /// <summary>
    /// Gets called when ragdoll is being spawned.
    /// </summary>
    public static event LabEventHandler<PlayerSpawningRagdollEventArgs>? SpawningRagdoll;

    /// <summary>
    /// Gets called when ragdoll has been spawned.
    /// </summary>
    public static event LabEventHandler<PlayerSpawnedRagdollEventArgs>? SpawnedRagdoll;

    /// <summary>
    /// Gets called when warhead button on surface is being unlocked.
    /// </summary>
    public static event LabEventHandler<PlayerUnlockingWarheadButtonEventArgs>? UnlockingWarheadButton;

    /// <summary>
    /// Gets called when warhead button on surface has been unlocked.
    /// </summary>
    public static event LabEventHandler<PlayerUnlockedWarheadButtonEventArgs>? UnlockedWarheadButton;

    /// <summary>
    /// Gets called when the player has meet the requirements of an achievement.
    /// </summary>
    public static event LabEventHandler<PlayerReceivedAchievementEventArgs>? ReceivedAchievement;

    /// <summary>
    /// Gets called when player's last known room changes.
    /// </summary>
    public static event LabEventHandler<PlayerRoomChangedEventArgs>? RoomChanged;

    /// <summary>
    /// Gets called when player's last known zone changes.
    /// </summary>
    public static event LabEventHandler<PlayerZoneChangedEventArgs>? ZoneChanged;

    /// <summary>
    /// Gets called when player interacts with warhead lever.
    /// </summary>
    public static event LabEventHandler<PlayerInteractingWarheadLeverEventArgs>? InteractingWarheadLever;

    /// <summary>
    /// Gets called when player interacted with warhead lever.
    /// </summary>
    public static event LabEventHandler<PlayerInteractedWarheadLeverEventArgs>? InteractedWarheadLever;

    /// <summary>
    /// Gets called when a hitmarker is being sent to a player.
    /// </summary>
    public static event LabEventHandler<PlayerSendingHitmarkerEventArgs>? SendingHitmarker;

    /// <summary>
    /// Gets called when a hitmarker is sent to a player.
    /// </summary>
    public static event LabEventHandler<PlayerSentHitmarkerEventArgs>? SentHitmarker;

    /// <summary>
    /// Gets called when a hitmarker permission is checked for a player.
    /// </summary>
    public static event LabEventHandler<PlayerCheckedHitmarkerEventArgs>? CheckedHitmarker;

    #endregion

    #region Spectating

    /// <summary>
    /// Gets called when the player has changed the spectated player.
    /// </summary>
    public static event LabEventHandler<PlayerChangedSpectatorEventArgs>? ChangedSpectator;

    #endregion

    #region Hazards

    /// <summary>
    /// Gets called when player is entering any environmental hazard.
    /// </summary>
    public static event LabEventHandler<PlayerEnteringHazardEventArgs>? EnteringHazard;

    /// <summary>
    /// Gets called when player has entered any environmental hazard.
    /// </summary>
    public static event LabEventHandler<PlayerEnteredHazardEventArgs>? EnteredHazard;

    /// <summary>
    /// Gets called when player has entered any environmental hazard.
    /// </summary>
    public static event LabEventHandler<PlayersStayingInHazardEventArgs>? StayingInHazard;

    /// <summary>
    /// Gets called when player is leaving any environmental hazard.
    /// </summary>
    public static event LabEventHandler<PlayerLeavingHazardEventArgs>? LeavingHazard;

    /// <summary>
    /// Gets called when player has left any environmental hazard.
    /// </summary>
    public static event LabEventHandler<PlayerLeftHazardEventArgs>? LeftHazard;

    /// <summary>
    /// Gets called when a player has validated the visibility of a target player.
    /// </summary>
    public static event LabEventHandler<PlayerValidatedVisibilityEventArgs>? ValidatedVisibility;

    #endregion

    #region Scp1344

    /// <summary>
    /// Gets called when player detects enemy player using SCP-1344.
    /// </summary>
    public static event LabEventHandler<PlayerDetectedByScp1344EventArgs>? DetectedByScp1344;

    #endregion
}