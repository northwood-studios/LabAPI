using Respawning.Config;
using Respawning.Waves;

namespace LabApi.Features.Wrappers;

/// <summary>
/// A class representing <see cref="ChaosSpawnWave">primary Chaos Insurgency spawn wave</see>.
/// </summary>
public class ChaosWave : RespawnWave
{
    /// <inheritdoc cref="RespawnWave(TimeBasedWave)"/>
    internal ChaosWave(ChaosSpawnWave wave)
        : base(wave)
    {
        Base = wave;
    }

    /// <summary>
    /// The base <see cref="ChaosSpawnWave"/> object.
    /// </summary>
    public new ChaosSpawnWave Base { get; private set; }

    /// <summary>
    /// Percentage of chaos suppressors per wave.
    /// </summary>
    public float LogicerPercent
    {
        get => Base.LogicerPercent;
        set => Base.LogicerPercent = value;
    }

    /// <summary>
    /// Percentage of chaos marauders per wave.
    /// </summary>
    public float ShotgunPercent
    {
        get => Base.ShotgunPercent;
        set => Base.ShotgunPercent = value;
    }

    /// <inheritdoc/>
    public override int MaxWaveSize
    {
        get => Base.MaxWaveSize;
        set
        {
            float percentageValue = (float)value / ReferenceHub.AllHubs.Count;
            if (Base.Configuration is PrimaryWaveConfig<ChaosSpawnWave> config)
            {
                config.SizePercentage = percentageValue;
            }
        }
    }
}
