using CustomPlayerEffects;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ReceviedEffect"/> event.
/// </summary>
public class PlayerEffectUpdatedEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerReceivingLoadoutEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player whose effect has been updated.</param>
    /// <param name="effect">The effect that is being updated.</param>
    /// <param name="intensity">Intesity of the effect.</param>
    /// <param name="duration">Duration of the effect in seconds.</param>
    public PlayerEffectUpdatedEventArgs(ReferenceHub player, StatusEffectBase effect, byte intensity, float duration)
    {
        Player = Player.Get(player);
        Effect = effect;
        Intensity = intensity;
        Duration = duration;
    }

    /// <summary>
    /// Gets the player whose effect has been updated.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the effect that is being updated.
    /// </summary>
    public StatusEffectBase Effect { get; }

    /// <summary>
    /// Gets the new intesity of the effect.
    /// </summary>
    public byte Intensity { get; }

    /// <summary>
    /// Gets the new duration of the effect in seconds.
    /// </summary>
    public float Duration { get; }
}