using LabApi.Features.Console;
using Respawning.Config;
using Respawning.Waves;
using System;

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
    [Obsolete("Use SergeantsPercentage instead", true)]
    public int MaxSergeants
    {
        get => 0;
        set => Logger.Error("Plugin Error. Cannot set MaxSergeants, use SergeantsPercentage instead.");
    }

    /// <summary>
    /// Gets the percentage of sergeants that can spawn with the wave.
    /// </summary>
    public float SergeantsPercentage
    {
        get => Base.SergeantPercent;
        set => Base.SergeantPercent = value;
    }

    /// <summary>
    /// Gets or sets the amount of captains that can spawn with the wave.
    /// </summary>
    [Obsolete("Use CaptainsPercentage instead", true)]
    public int MaxCaptains
    {
        get => 0;
        set => Logger.Error("Plugin Error. Cannot set MaxCaptains, use CaptainsPercentage instead.");
    }

    /// <summary>
    /// Gets the percentage of captains that can spawn with the wave.
    /// </summary>
    public float CaptainsPercentage
    {
        get => Base.CaptainPercent;
        set => Base.CaptainPercent = value;
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