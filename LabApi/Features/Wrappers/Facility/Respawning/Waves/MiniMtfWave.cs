using Respawning.Waves;

namespace LabApi.Features.Wrappers;

/// <summary>
/// A class representing <see cref="NtfMiniWave">mini MTF spawn wave</see>.
/// </summary>
public class MiniMtfWave : MiniRespawnWave
{
    /// <inheritdoc cref="MiniRespawnWave(IMiniWave)"/>
    internal MiniMtfWave(NtfMiniWave miniWave)
        : base(miniWave)
    {
        Base = miniWave;
    }

    /// <inheritdoc cref="MiniRespawnWave.Base"/>
    public new NtfMiniWave Base { get; private set; }
}
