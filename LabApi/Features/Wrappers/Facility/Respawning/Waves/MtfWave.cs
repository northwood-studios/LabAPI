using Respawning.Config;
using Respawning.Waves;

namespace LabApi.Features.Wrappers;

/// <summary>
/// A class representing <see cref="NtfSpawnWave">primary MTF spawn wave</see>.
/// </summary>
public class MtfWave : RespawnWave
{
    /// <inheritdoc cref="RespawnWave(TimeBasedWave)"/>
    internal MtfWave(NtfSpawnWave wave)
        : base(wave)
    {
        Base = wave;
    }

    /// <summary>
    /// The base <see cref="NtfSpawnWave"/> object.
    /// </summary>
    public new NtfSpawnWave Base { get; private set; }

    /// <summary>
    /// Gets or sets the amount of sergeants that can spawn with the wave.
    /// </summary>
    public int MaxSergeants
    {
        get => Base.MaxSergeants;
        set => Base.MaxSergeants = value;
    }

    /// <summary>
    /// Gets or sets the amount of captains that can spawn with the wave.
    /// </summary>
    public int MaxCaptains
    {
        get => Base.MaxCaptains;
        set => Base.MaxCaptains = value;
    }

    /// <inheritdoc/>
    public override int MaxWaveSize
    {
        get => Base.MaxWaveSize;
        set
        {
            float percentageValue = (float)value / ReferenceHub.AllHubs.Count;
            if (Base.Configuration is PrimaryWaveConfig<NtfSpawnWave> config)
            {
                config.SizePercentage = percentageValue;
            }
        }
    }
}