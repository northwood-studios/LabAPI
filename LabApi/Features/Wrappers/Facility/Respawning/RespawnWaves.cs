using Generators;
using Respawning;
using Respawning.Waves;

namespace LabApi.Features.Wrappers;

/// <summary>
/// A static class holding references to the wrapping <see cref="RespawnWave"/>s.
/// </summary>
public static class RespawnWaves
{
    /// <summary>
    /// Gets the primary MTF respawn wave.
    /// </summary>
    public static MtfWave? PrimaryMtfWave { get; private set; }

    /// <summary>
    /// Gets the primary Chaos Insurgency respawn wave.
    /// </summary>
    public static ChaosWave? PrimaryChaosWave { get; private set; }

    /// <summary>
    /// Gets the mini MTF respawn wave.
    /// </summary>
    public static MiniMtfWave? MiniMtfWave { get; private set; }

    /// <summary>
    /// Gets the mini Chaos Insurgency respawn wave.
    /// </summary>
    public static MiniChaosWave? MiniChaosWave { get; private set; }

    /// <summary>
    /// Gets the respawn wave wrapper from the static references or creates a new one if it doesn't exist and the provided <see cref="SpawnableWaveBase"/> was not <see langword="null"/> or not valid subclass.
    /// </summary>
    /// <param name="baseWave">The <see cref="RespawnWave.Base"/> of the respawn wave.</param>
    /// <returns>The requested respawn wave or <see langword="null"/>.</returns>
    public static RespawnWave? Get(SpawnableWaveBase? baseWave)
    {
        return baseWave switch
        {
            NtfSpawnWave => PrimaryMtfWave ??= new MtfWave((NtfSpawnWave)baseWave),
            ChaosSpawnWave => PrimaryChaosWave ??= new ChaosWave((ChaosSpawnWave)baseWave),
            NtfMiniWave => MiniMtfWave ??= new MiniMtfWave((NtfMiniWave)baseWave),
            ChaosMiniWave => MiniChaosWave ??= new MiniChaosWave((ChaosMiniWave)baseWave),
            _ => null,
        };
    }

    /// <summary>
    /// Initializes the <see cref="RespawnWaves"/> wrapper and its wave wrapper instances.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        foreach (SpawnableWaveBase wave in WaveManager.Waves)
        {
            Get(wave);
        }
    }
}
