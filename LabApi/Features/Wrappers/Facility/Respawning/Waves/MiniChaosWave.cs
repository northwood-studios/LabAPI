using Respawning.Waves;

namespace LabApi.Features.Wrappers;

/// <summary>
/// A class representing <see cref="ChaosMiniWave">mini Chaos Insurgency spawn wave</see>.
/// </summary>
public class MiniChaosWave : MiniRespawnWave
{
    /// <inheritdoc  cref="MiniRespawnWave(IMiniWave)"/>
    internal MiniChaosWave(ChaosMiniWave miniWave)
        : base(miniWave)
    {
        Base = miniWave;
    }

    /// <inheritdoc cref="MiniRespawnWave.Base"/>
    public new ChaosMiniWave Base { get; private set; }
}
