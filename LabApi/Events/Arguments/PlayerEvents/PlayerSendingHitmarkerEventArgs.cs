using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.SendingHitmarker"/> event.
/// </summary>
public class PlayerSendingHitmarkerEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance for the <see cref="PlayerSendingHitmarkerEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player that is sending the hitmarker.</param>
    /// <param name="size">The target size multiplier.</param>
    /// <param name="playAudio">Whether the hitmarker sound effect should play.</param>
    /// <param name="hitmarkerType">The type of the hitmarker.</param>
    public PlayerSendingHitmarkerEventArgs(ReferenceHub hub, float size, bool playAudio, HitmarkerType hitmarkerType)
    {
        Player = Player.Get(hub);
        Size = size;
        PlayAudio = playAudio;
        Hitmarker = hitmarkerType;

        IsAllowed = true;
    }

    /// <summary>
    /// Gets or sets the player that the hitmarker is being sent to.
    /// </summary>
    public Player Player { get; set; }

    /// <summary>
    /// Gets or sets the target size multiplier.
    /// </summary>
    public float Size { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the hitmarker sound effect should play.
    /// </summary>
    public bool PlayAudio { get; set; }

    /// <summary>
    /// Gets or sets a the type of the hitmarker.
    /// </summary>
    public HitmarkerType Hitmarker { get; set; }

    /// <inheritdoc/>
    public bool IsAllowed { get; set; }
}
