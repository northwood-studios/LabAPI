using LabApi.Events.Arguments.ServerEvents;

namespace LabApi.Events.Handlers;

/// <inheritdoc />
public static partial class ServerEvents
{
    /// <summary>
    /// Invokes the <see cref="WaitingForPlayers"/> event.
    /// </summary>
    public static void OnWaitingForPlayers() => WaitingForPlayers.InvokeEvent();

    /// <summary>
    /// Invokes the <see cref="RoundRestarted"/> event.
    /// </summary>
    public static void OnRoundRestarted() => RoundRestarted.InvokeEvent();

    /// <summary>
    /// Invokes the <see cref="RoundEnding"/> event.
    /// </summary>
    /// <param name="args">The <see cref="RoundEndingEventArgs"/> of the event.</param>
    public static void OnRoundEnding(RoundEndingEventArgs args) => RoundEnding.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="RoundEnded"/> event.
    /// </summary>
    /// <param name="args">The <see cref="RoundEndingEventArgs"/> of the event.</param>
    public static void OnRoundEnded(RoundEndedEventArgs args) => RoundEnded.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="RoundStarting"/> event.
    /// </summary>
    /// <param name="args">The <see cref="RoundStartingEventArgs"/> of the event.</param>
    public static void OnRoundStarting(RoundStartingEventArgs args) => RoundStarting.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="RoundStarted"/> event.
    /// </summary>
    public static void OnRoundStarted() => RoundStarted.InvokeEvent();

    /// <summary>
    /// Invokes the <see cref="BanIssuing"/> event.
    /// </summary>
    /// <param name="args">The <see cref="BanIssuingEventArgs"/> of the event.</param>
    public static void OnBanIssuing(BanIssuingEventArgs args) => BanIssuing.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="BanIssued"/> event.
    /// </summary>
    /// <param name="args">The <see cref="BanIssuedEventArgs"/> of the event.</param>
    public static void OnBanIssued(BanIssuedEventArgs args) => BanIssued.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="BanRevoking"/> event.
    /// </summary>
    /// <param name="args">The <see cref="BanRevokingEventArgs"/> of the event.</param>
    public static void OnBanRevoking(BanRevokingEventArgs args) => BanRevoking.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="BanRevoked"/> event.
    /// </summary>
    /// <param name="args">The <see cref="BanRevokedEventArgs"/> of the event.</param>
    public static void OnBanRevoked(BanRevokedEventArgs args) => BanRevoked.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="BanUpdating"/> event.
    /// </summary>
    /// <param name="args">The <see cref="BanUpdatingEventArgs"/> of the event.</param>
    public static void OnBanUpdating(BanUpdatingEventArgs args) => BanUpdating.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="BanUpdated"/> event.
    /// </summary>
    /// <param name="args">The <see cref="BanUpdatedEventArgs"/> of the event.</param>
    public static void OnBanUpdated(BanUpdatedEventArgs args) => BanUpdated.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="CommandExecuting"/> event.
    /// </summary>
    /// <param name="args">The <see cref="CommandExecutingEventArgs"/> of the event.</param>
    public static void OnCommandExecuting(CommandExecutingEventArgs args) => CommandExecuting.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="CommandExecuted"/> event.
    /// </summary>
    /// <param name="args">The <see cref="CommandExecutedEventArgs"/> of the event.</param>
    public static void OnCommandExecuted(CommandExecutedEventArgs args) => CommandExecuted.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="WaveRespawning"/> event.
    /// </summary>
    /// <param name="args">The <see cref="WaveRespawningEventArgs"/> of the event.</param>
    public static void OnWaveRespawning(WaveRespawningEventArgs args) => WaveRespawning.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="WaveRespawned"/> event.
    /// </summary>
    /// <param name="args">The <see cref="WaveRespawnedEventArgs"/> of the event.</param>
    public static void OnWaveRespawned(WaveRespawnedEventArgs args) => WaveRespawned.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="WaveTeamSelecting"/> event.
    /// </summary>
    /// <param name="args">The <see cref="WaveTeamSelectingEventArgs"/> of the event.</param>
    public static void OnWaveTeamSelecting(WaveTeamSelectingEventArgs args) => WaveTeamSelecting.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="WaveTeamSelected"/> event.
    /// </summary>
    /// <param name="args">The <see cref="WaveTeamSelectedEventArgs"/> of the event.</param>
    public static void OnWaveTeamSelected(WaveTeamSelectedEventArgs args) => WaveTeamSelected.InvokeEvent(args);
}