using CustomPlayerEffects;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.UpdatingEffect"/> event.
/// </summary>
public class PlayerEffectUpdatingEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerEffectUpdatingEventArgs"/> class.
    /// </summary>
    /// <param name="player">The player whose effect is being updated.</param>
    /// <param name="effect">The effect that is being updated.</param>
    /// <param name="intensity">Intensity of the effect.</param>
    /// <param name="duration">Duration of the effect in seconds.</param>
    public PlayerEffectUpdatingEventArgs(ReferenceHub player, StatusEffectBase effect, byte intensity, float duration)
    {
        IsAllowed = true;
        Player = Player.Get(player);
        Effect = effect;
        Intensity = intensity;
        Duration = duration;
    }

    /// <summary>
    /// Gets the player whose effect is being updated.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the effect that is being updated.
    /// </summary>
    public StatusEffectBase Effect { get; }

    /// <summary>
    /// Gets or sets the new intensity of the effect.
    /// </summary>
    public byte Intensity { get; set; }

    /// <summary>
    /// Gets or sets the new duration of the effect in seconds.
    /// </summary>
    public float Duration { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}