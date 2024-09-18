using LabApi.Events.Arguments.ServerEvents;
using LabApi.Events.Handlers;

namespace LabApi.Events.CustomHandlers;

/// <inheritdoc />
public abstract partial class CustomEventsHandler
{
    /// <inheritdoc cref="ServerEvents.WaitingForPlayers"/>
    public virtual void OnWaitingForPlayers() {}
    
    /// <inheritdoc cref="ServerEvents.RoundRestarted"/>
    public virtual void OnRoundRestarted() {}

    /// <inheritdoc cref="ServerEvents.RoundEnding"/>
    public virtual void OnRoundEnding(RoundEndingEventArgs args) {}

    /// <inheritdoc cref="ServerEvents.RoundEnded"/>
    public virtual void OnRoundEnded(RoundEndedEventArgs args) {}

    /// <inheritdoc cref="ServerEvents.RoundStarting"/>
    public virtual void OnRoundStarting(RoundStartingEventArgs args) {}

    /// <inheritdoc cref="ServerEvents.RoundStarted"/>
    public virtual void OnRoundStarted() {}

    /// <inheritdoc cref="ServerEvents.BanIssuing"/>
    public virtual void OnBanIssuing(BanIssuingEventArgs args) {}

    /// <inheritdoc cref="ServerEvents.BanIssued"/>
    public virtual void OnBanIssued(BanIssuedEventArgs args) {}

    /// <inheritdoc cref="ServerEvents.BanRevoking"/>
    public virtual void OnBanRevoking(BanRevokingEventArgs args) {}

    /// <inheritdoc cref="ServerEvents.BanRevoked"/>
    public virtual void OnBanRevoked(BanRevokedEventArgs args) {}

    /// <inheritdoc cref="ServerEvents.BanUpdating"/>
    public virtual void OnBanUpdating(BanUpdatingEventArgs args) {}

    /// <inheritdoc cref="ServerEvents.BanUpdated"/>
    public virtual void OnBanUpdated(BanUpdatedEventArgs args) {}

    /// <inheritdoc cref="ServerEvents.CommandExecuting"/>
    public virtual void OnCommandExecuting(CommandExecutingEventArgs args) {}

    /// <inheritdoc cref="ServerEvents.CommandExecuted"/>
    public virtual void OnCommandExecuted(CommandExecutedEventArgs args) {}

    /// <inheritdoc cref="ServerEvents.WaveRespawning"/>
    public virtual void OnWaveRespawning(WaveRespawningEventArgs args) {}

    /// <inheritdoc cref="ServerEvents.WaveRespawned"/>
    public virtual void OnWaveRespawned(WaveRespawnedEventArgs args) {}

    /// <inheritdoc cref="ServerEvents.WaveTeamSelecting"/>
    public virtual void OnWaveTeamSelecting(WaveTeamSelectingEventArgs args) {}

    /// <inheritdoc cref="ServerEvents.WaveTeamSelected"/>
    public virtual void OnWaveTeamSelected(WaveTeamSelectedEventArgs args) {}
}