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
    /// Initializes a new instance for the <see cref="PlayerSendHitmarkerEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player that canceled the search on the toy.</param>
    /// <param name="size">The target size multiplier.</param>
    /// <param name="playAudio">The hitmarker sound effect play.</param>
    public PlayerSendingHitmarkerEventArgs(ReferenceHub hub, float size, bool playAudio)
    {
        Player = Player.Get(hub);
        Size = size;
        PlayAudio = playAudio;

        IsAllowed = true;
    }

    /// <summary>
    /// Gets or sets the player that canceled the search on the toy.
    /// </summary>
    public Player Player { get; set; }

    /// <summary>
    /// Gets or sets the target size multiplier.
    /// </summary>
    public float Size { get; set; }

    /// <summary>
    /// Gets or sets if the hitmarker sound effect play.
    /// </summary>
    public bool PlayAudio { get; set; }

    /// <inheritdoc/>
    public bool IsAllowed { get; set; }
}
