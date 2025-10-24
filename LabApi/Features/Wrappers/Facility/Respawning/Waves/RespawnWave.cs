using System;
using System.Collections.Generic;
using System.Linq;
using PlayerRoles;
using Respawning;
using Respawning.Config;
using Respawning.Waves;
using Respawning.Waves.Generic;

namespace LabApi.Features.Wrappers;

/// <summary>
/// A class representing <see cref="TimeBasedWave"/>. Can be both <see cref="RespawnWave">primary wave</see> and <see cref="MiniRespawnWave">mini wave</see>.
/// </summary>
public abstract class RespawnWave
{
    /// <summary>
    /// Internal constructor preventing external instantiation.
    /// </summary>
    /// <param name="wave">The base game object.</param>
    internal RespawnWave(TimeBasedWave wave)
    {
        Base = wave;
    }

    /// <summary>
    /// The base <see cref="TimeBasedWave"/> object.
    /// </summary>
    public TimeBasedWave Base { get; private set; }

    /// <summary>
    /// Gets the faction this respawn wave belong to.
    /// </summary>
    public Faction Faction => Base.TargetFaction;

    /// <summary>
    /// Gets or sets the second that is added to the next respawn timer after the wave has respawned.
    /// </summary>
    public float AdditionalSecondsPerSpawn
    {
        get => Base.AdditionalSecondsPerSpawn;
        set => Base.AdditionalSecondsPerSpawn = value;
    }

    /// <summary>
    /// Gets or sets the amount of <see cref="Player"/>s that are going to spawn with the wave.<br/>
    /// Amount is based on the amount of all <see cref="Player"/>s including dummies.
    /// </summary>
    public abstract int MaxWaveSize { get; set; }

    /// <summary>
    /// Gets the time the spawn animations takes in seconds.
    /// </summary>
    public float AnimationTime
    {
        get
        {
            if (Base is IAnimatedWave wave)
            {
                return wave.AnimationDuration;
            }

            return 0f;
        }
    }

    /// <summary>
    /// Gets or sets the amount of respawn tokens this spawn wave has.
    /// </summary>
    public int RespawnTokens
    {
        get
        {
            if (Base is ILimitedWave wave)
            {
                return wave.RespawnTokens;
            }

            return 0;
        }

        set
        {
            if (Base is not ILimitedWave wave)
            {
                return;
            }

            wave.RespawnTokens = value;
            WaveUpdateMessage.ServerSendUpdate(Base, UpdateMessageFlags.Tokens);
        }
    }

    /// <summary>
    /// Gets or sets the amount of influence this wave's <see cref="Faction"/> has.
    /// </summary>
    public float Influence
    {
        get => FactionInfluenceManager.Get(Faction);
        set => FactionInfluenceManager.Set(Faction, value);
    }

    /// <summary>
    /// Gets or sets the time in seconds it takes for this wave to spawn.
    /// </summary>
    public float TimeLeft
    {
        get => Base.Timer.TimeLeft;
        set => Base.Timer.SetTime(value);
    }

    /// <summary>
    /// Gets or sets the time this wave's timer is paused.
    /// </summary>
    /// <remarks>
    /// Currently the wave timer pauses only at about 10% left.
    /// </remarks>
    public float PausedTime
    {
        get => Base.Timer.PauseTimeLeft;
        set => Base.Timer.Pause(value);
    }

    /// <summary>
    /// Gets or sets the time that has passed since last wave respawn.
    /// </summary>
    public float TimePassed => Base.Timer.TimePassed;

    /// <summary>
    /// Attempts to get milestone for next <see cref="RespawnTokens"/>.
    /// Returns <see langword="false"/> if this <see cref="Faction"/> has maximum influence possible.
    /// </summary>
    /// <param name="influenceThreshold">Out param containing next target influence.</param>
    /// <returns>Whether there is next available milestone.</returns>
    public bool TryGetCurrentMilestone(out int influenceThreshold) => RespawnTokensManager.TryGetNextThreshold(Faction, Influence, out influenceThreshold);

    /// <summary>
    /// Initiates the respawn with animation.
    /// </summary>
    public virtual void InitiateRespawn() => WaveManager.InitiateRespawn(Base);

    /// <summary>
    /// Instantly respawns this wave.
    /// </summary>
    public void InstantRespawn() => WaveManager.Spawn(Base);

    /// <summary>
    /// Plays the respawn announcement.
    /// </summary>
    [Obsolete("Use PlayAnnouncement(IEnumerable<Player>) instead.", true)]
    public void PlayAnnouncement()
    {
        PlayAnnouncement([]);
    }

    /// <summary>
    /// Plays the respawn announcement.
    /// </summary>
    /// <param name="spawnedPlayers">The players that have spawned to take into account for the announcement.</param>
    public void PlayAnnouncement(IEnumerable<Player> spawnedPlayers)
    {
        if (Base is IAnnouncedWave wave)
        {
            wave.Announcement.PlayAnnouncement(spawnedPlayers.Select(p => p.ReferenceHub).ToList());
        }
    }

    /// <summary>
    /// Plays the respawn animation without spawning the wave.
    /// </summary>
    public void PlayRespawnEffect()
    {
        if (Base is not IAnimatedWave)
        {
            return;
        }

        WaveUpdateMessage.ServerSendUpdate(Base, UpdateMessageFlags.Trigger);
    }
}
