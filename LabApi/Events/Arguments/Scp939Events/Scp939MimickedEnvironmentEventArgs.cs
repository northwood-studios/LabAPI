using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp939Events;

/// <summary>
/// Represents the event arguments for when SCP-939 has mimicked the environment.
/// </summary>
public class Scp939MimickedEnvironmentEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp939MimickedEnvironmentEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-939 player instance.</param>
    /// <param name="playedSequence">The played environmental sequence.</param>
    public Scp939MimickedEnvironmentEventArgs(ReferenceHub hub, byte playedSequence)
    {
        Player = Player.Get(hub);
        PlayedSequence = playedSequence;
    }

    /// <summary>
    /// Gets the 939 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the played environmental sequence.
    /// </summary>
    public byte PlayedSequence { get; }
}