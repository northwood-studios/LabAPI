using LabApi.Events.Arguments.ServerEvents;

namespace LabApi.Events.Handlers;

/// <summary>
/// Handles all the events related to the server.
/// </summary>
public static partial class ServerEvents
{
    /// <summary>
    /// Gets called when the server is loaded and waiting for players.
    /// </summary>
    public static event LabEventHandler WaitingForPlayers;
    
    /// <summary>
    /// Gets called when the round is restarted.
    /// </summary>
    public static event LabEventHandler RoundRestarted;
    
    /// <summary>
    /// Gets called when the round is ending.
    /// </summary>
    public static event LabEventHandler<RoundEndingEventArgs> RoundEnding;
    
    /// <summary>
    /// Gets called when the round has ended.
    /// </summary>
    public static event LabEventHandler<RoundEndedEventArgs> RoundEnded;

    /// <summary>
    /// Gets called when the round is starting.
    /// </summary>
    public static event LabEventHandler<RoundStartingEventArgs> RoundStarting;
    
    /// <summary>
    /// Gets called when the round has started.
    /// </summary>
    public static event LabEventHandler RoundStarted;
    
    /// <summary>
    /// Gets called when the server is issuing a ban.
    /// </summary>
    public static event LabEventHandler<BanIssuingEventArgs> BanIssuing;
    
    /// <summary>
    /// Gets called when the server has issued a ban.
    /// </summary>
    public static event LabEventHandler<BanIssuedEventArgs> BanIssued;
    
    /// <summary>
    /// Gets called when the server is revoking a ban.
    /// </summary>
    public static event LabEventHandler<BanRevokingEventArgs> BanRevoking;
    
    /// <summary>
    /// Gets called when the server has revoked a ban.
    /// </summary>
    public static event LabEventHandler<BanRevokedEventArgs> BanRevoked;
    
    /// <summary>
    /// Gets called when the server is updating a ban.
    /// </summary>
    public static event LabEventHandler<BanUpdatingEventArgs> BanUpdating;
    
    /// <summary>
    /// Gets called when the server has updated a ban.
    /// </summary>
    public static event LabEventHandler<BanUpdatedEventArgs> BanUpdated;
    
    /// <summary>
    /// Gets called when the server is executing a command.
    /// </summary>
    public static event LabEventHandler<CommandExecutingEventArgs> CommandExecuting;
    
    /// <summary>
    /// Gets called when the server has executed a command.
    /// </summary>
    public static event LabEventHandler<CommandExecutedEventArgs> CommandExecuted;
    
    /// <summary>
    /// Gets called when the server is respawning a wave.
    /// </summary>
    public static event LabEventHandler<WaveRespawningEventArgs> WaveRespawning;
    
    /// <summary>
    /// Gets called when the server has respawned a wave.
    /// </summary>
    public static event LabEventHandler<WaveRespawnedEventArgs> WaveRespawned;

    /// <summary>
    /// Gets called when the server is selecting a team for the wave.
    /// </summary>
    public static event LabEventHandler<WaveTeamSelectingEventArgs> WaveTeamSelecting;
    
    /// <summary>
    /// Gets called when the server has selected a team for the wave.
    /// </summary>
    public static event LabEventHandler<WaveTeamSelectedEventArgs> WaveTeamSelected;
}