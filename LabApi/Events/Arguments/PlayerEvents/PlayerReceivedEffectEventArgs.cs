using CustomPlayerEffects;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ReceviedEffect"/> event.
/// </summary>
public class PlayerReceivedEffectEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerReceivingLoadoutEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player who is received the effect.</param>
    /// <param name="effect">The effect that is being applied.</param>
    /// <param name="intensity">Intesity of the effect.</param>
    /// <param name="duration">Duration of the effect in seconds.</param>
    public PlayerReceivedEffectEventArgs(ReferenceHub player, StatusEffectBase effect, byte intensity, float duration)
    {
        Player = Player.Get(player);
        Effect = effect;
        Intensity = intensity;
        Duration = duration;
    }

    /// <summary>
    /// Gets the player who is received the effect.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the effect that is being applied.
    /// </summary>
    public StatusEffectBase Effect { get; }

    /// <summary>
    /// Gets the intesity of the effect.
    /// </summary>
    public byte Intensity { get; }

    /// <summary>
    /// Gets the duration of the effect in seconds.
    /// </summary>
    public float Duration { get; }
}