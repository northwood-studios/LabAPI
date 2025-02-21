using PlayerRoles;
using Respawning.Waves;

namespace LabApi.Features.Wrappers;

/// <summary>
/// A class representing <see cref="IMiniWave"/> which is used for all smaller scale waves.
/// </summary>
public abstract class MiniRespawnWave : RespawnWave
{
    /// <summary>
    /// Internal constructor preventing external instantiation.
    /// </summary>
    /// <param name="miniWave">The base game object.</param>
    internal MiniRespawnWave(IMiniWave miniWave) : base((TimeBasedWave)miniWave)
    {
        Base = miniWave;
    }

    /// <summary>
    /// Base object.
    /// </summary>
    public new IMiniWave Base { get; private set; }

    /// <inheritdoc/>
    public override int MaxWaveSize
    {
        get
        {
            if (Base is TimeBasedWave baseWave)
                return baseWave.MaxWaveSize;

            return 0;
        }
        set
        {
            if (value < 0)
                return;

            float percentageValue = (float)value / ReferenceHub.AllHubs.Count;
            Base.WaveSizeMultiplier = percentageValue;
        }
    }

    /// <summary>
    /// Gets or sets the default role mini waves will resort to spawning.
    /// </summary>
    public RoleTypeId DefaultRole
    {
        get => Base.DefaultRole;
        set => Base.DefaultRole = value;
    }

    /// <summary>
    /// Gets or sets the special role which the respawn wave will spawn.
    /// </summary>
    public RoleTypeId SpecialRole
    {
        get => Base.SpecialRole;
        set => Base.SpecialRole = value;
    }

    public override void InitiateRespawn()
    {
        Unlock();
        base.InitiateRespawn();
    }

    /// <summary>
    /// Forces this Miniwave instance to be unlocked.
    /// </summary>
    public virtual void Unlock() => Base.Unlock();

    /// <summary>
    /// Resets the <see cref="RespawnWave.RespawnTokens"/> for this miniwave.
    /// </summary>
    public virtual void Lock() => Base.ResetTokens();
}

