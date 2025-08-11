using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerRoles.PlayableScps.Scp939;
using System;

namespace LabApi.Events.Arguments.Scp939Events;

/// <summary>
/// Represents the event arguments for when SCP-939 has created an amnestic cloud.
/// </summary>
public class Scp939CreatedAmnesticCloudEventArgs : EventArgs, IPlayerEvent, IAmnesticCloudEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp939CreatedAmnesticCloudEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-939 player instance.</param>
    /// <param name="amnesticCloudInstance">The created amnestic cloud instance.</param>
    public Scp939CreatedAmnesticCloudEventArgs(ReferenceHub hub, Scp939AmnesticCloudInstance amnesticCloudInstance)
    {
        Player = Player.Get(hub);
        AmnesticCloud = AmnesticCloudHazard.Get(amnesticCloudInstance);
    }

    /// <summary>
    /// The SCP-939 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the created amnestic cloud instance.
    /// </summary>
    public AmnesticCloudHazard AmnesticCloud { get; }

    /// <inheritdoc cref="AmnesticCloud"/>
    [Obsolete($"Use {nameof(AmnesticCloud)} instead")]
    public AmnesticCloudHazard AmnesticCloudInstance => AmnesticCloud;
}