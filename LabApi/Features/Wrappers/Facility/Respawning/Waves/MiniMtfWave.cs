using Respawning.Waves;

namespace LabApi.Features.Wrappers;

/// <summary>
/// A class representing <see cref="NtfMiniWave">mini MTF spawn wave</see>.
/// </summary>
public class MiniMtfWave : MiniRespawnWave
{
    /// <inheritdoc/>
    internal MiniMtfWave(NtfMiniWave miniWave) : base(miniWave)
    {
        Base = miniWave;
    }

    /// <inheritdoc/>
    public new NtfMiniWave Base { get; private set; }
}