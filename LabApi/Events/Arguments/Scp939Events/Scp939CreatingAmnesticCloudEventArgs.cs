using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp939Events;

/// <summary>
/// Represents the event arguments for when SCP-939 is creating an amnestic cloud.
/// </summary>
public class Scp939CreatingAmnesticCloudEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp939CreatingAmnesticCloudEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-939 player instance.</param>
    public Scp939CreatingAmnesticCloudEventArgs(ReferenceHub hub)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
    }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <summary>
    /// The 939 player instance.
    /// </summary>
    public Player Player { get; }
}