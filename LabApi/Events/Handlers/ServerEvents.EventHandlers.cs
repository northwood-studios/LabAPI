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
    public static event LabEventHandler? WaitingForPlayers;

    /// <summary>
    /// Gets called when the round is restarted.
    /// </summary>
    public static event LabEventHandler? RoundRestarted;

    /// <summary>
    /// Gets called when the round is ending.
    /// </summary>
    public static event LabEventHandler<RoundEndingEventArgs>? RoundEnding;

    /// <summary>
    /// Gets called when the round has ended.
    /// </summary>
    public static event LabEventHandler<RoundEndedEventArgs>? RoundEnded;

    /// <summary>
    /// Gets called when the round is starting.
    /// </summary>
    public static event LabEventHandler<RoundStartingEventArgs>? RoundStarting;

    /// <summary>
    /// Gets called when the round has started.
    /// </summary>
    public static event LabEventHandler? RoundStarted;

    /// <summary>
    /// Gets called when the server is issuing a ban.
    /// </summary>
    public static event LabEventHandler<BanIssuingEventArgs>? BanIssuing;

    /// <summary>
    /// Gets called when the server has issued a ban.
    /// </summary>
    public static event LabEventHandler<BanIssuedEventArgs>? BanIssued;

    /// <summary>
    /// Gets called when the server is revoking a ban.
    /// </summary>
    public static event LabEventHandler<BanRevokingEventArgs>? BanRevoking;

    /// <summary>
    /// Gets called when the server has revoked a ban.
    /// </summary>
    public static event LabEventHandler<BanRevokedEventArgs>? BanRevoked;

    /// <summary>
    /// Gets called when the server is updating a ban.
    /// </summary>
    public static event LabEventHandler<BanUpdatingEventArgs>? BanUpdating;

    /// <summary>
    /// Gets called when the server has updated a ban.
    /// </summary>
    public static event LabEventHandler<BanUpdatedEventArgs>? BanUpdated;

    /// <summary>
    /// Gets called when the server is executing a command.
    /// </summary>
    public static event LabEventHandler<CommandExecutingEventArgs>? CommandExecuting;

    /// <summary>
    /// Gets called when the server has executed a command.
    /// </summary>
    public static event LabEventHandler<CommandExecutedEventArgs>? CommandExecuted;

    /// <summary>
    /// Gets called when the server is respawning a wave.
    /// </summary>
    public static event LabEventHandler<WaveRespawningEventArgs>? WaveRespawning;

    /// <summary>
    /// Gets called when the server has respawned a wave.
    /// </summary>
    public static event LabEventHandler<WaveRespawnedEventArgs>? WaveRespawned;

    /// <summary>
    /// Gets called when the server is selecting a team for the wave.
    /// </summary>
    public static event LabEventHandler<WaveTeamSelectingEventArgs>? WaveTeamSelecting;

    /// <summary>
    /// Gets called when the server has selected a team for the wave.
    /// </summary>
    public static event LabEventHandler<WaveTeamSelectedEventArgs>? WaveTeamSelected;

    /// <summary>
    /// Gets called when the server announced LCZ decontamination.
    /// </summary>
    public static event LabEventHandler<LczDecontaminationAnnouncedEventArgs>? LczDecontaminationAnnounced;

    /// <summary>
    /// Gets called when the server starts LCZ decontamiantion.
    /// </summary>
    public static event LabEventHandler<LczDecontaminationStartingEventArgs>? LczDecontaminationStarting;

    /// <summary>
    /// Gets called when the server started LCZ decontamination.
    /// </summary>
    public static event LabEventHandler? LczDecontaminationStarted;

    /// <summary>
    /// Gets called when the server starts generating map.
    /// </summary>
    public static event LabEventHandler<MapGeneratingEventArgs>? MapGenerating;

    /// <summary>
    /// Gets called when the server generated map.
    /// </summary>
    public static event LabEventHandler<MapGeneratedEventArgs>? MapGenerated;
    
    /// <summary>
    /// Gets called when the server is sending an Admin Chat message.
    /// </summary>
    public static event LabEventHandler<SendingAdminChatEventArgs>? SendingAdminChat;
    
    /// <summary>
    /// Gets called when the server sent an Admin Chat message.
    /// </summary>
    public static event LabEventHandler<SentAdminChatEventArgs>? SentAdminChat;

    /// <summary>
    /// Gets called when the server is spawning item on map.
    /// </summary>
    public static event LabEventHandler<ItemSpawningEventArgs>? ItemSpawning;

    /// <summary>
    /// Gets called when the server spawned item on map.
    /// </summary>
    public static event LabEventHandler<ItemSpawnedEventArgs>? ItemSpawned;

    /// <summary>
    /// Gets called when the server starts playing C.A.S.S.I.E sentence.
    /// </summary>
    public static event LabEventHandler<CassieAnnouncingEventArgs>? CassieAnnouncing;

    /// <summary>
    /// Gets called when the server played C.A.S.S.I.E sentence.
    /// </summary>
    public static event LabEventHandler<CassieAnnouncedEventArgs>? CassieAnnounced;

    /// <summary>
    /// Gets called when the server will explode projectile.
    /// </summary>
    public static event LabEventHandler<ProjectileExplodingEventArgs>? ProjectileExploding;

    /// <summary>
    /// Gets called when the server exploded projectile.
    /// </summary>
    public static event LabEventHandler<ProjectileExplodedEventArgs>? ProjectileExploded;

    /// <summary>
    /// Gets called when harmable explosion is spawning.
    /// </summary>
    public static event LabEventHandler<ExplosionSpawningEventArgs>? ExplosionSpawning;

    /// <summary>
    /// Gets called when explosion has spawned.
    /// </summary>
    public static event LabEventHandler<ExplosionSpawnedEventArgs>? ExplosionSpawned;

    /// <summary>
    /// Gets called when the server will activate generator.
    /// </summary>
    public static event LabEventHandler<GeneratorActivatingEventArgs>? GeneratorActivating;

    /// <summary>
    /// Gets called when the server activated generator.
    /// </summary>
    public static event LabEventHandler<GeneratorActivatedEventArgs>? GeneratorActivated;
}