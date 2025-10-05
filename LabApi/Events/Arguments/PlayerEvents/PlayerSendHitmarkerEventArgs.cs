using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.SendHitmarker"/> event.
/// </summary>
public class PlayerSendHitmarkerEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance for the <see cref="PlayerSendHitmarkerEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player that canceled the search on the toy.</param>
    /// <param name="size">The target size multiplier.</param>
    /// <param name="playAudio">The hitmarker sound effect play.</param>
    public PlayerSendHitmarkerEventArgs(ReferenceHub hub, float size, bool playAudio)
    {
        Player = Player.Get(hub);
        Size = size;
        PlayAudio = playAudio;
    }

    /// <summary>
    /// Gets the player that canceled the search on the toy.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the target size multiplier.
    /// </summary>
    public float Size { get; }

    /// <summary>
    /// Gets if the hitmarker sound effect play.
    /// </summary>
    public bool PlayAudio { get; }
}
