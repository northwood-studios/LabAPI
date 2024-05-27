using System;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers.Player;
using PlayerRoles.PlayableScps.Scp939;

namespace LabApi.Events.Arguments.Scp939Events;

/// <summary>
/// Represents the event arguments for when SCP-939 has created an amnestic cloud.
/// </summary>
public class Scp939CreatedAmnesticCloudEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp939CreatedAmnesticCloudEventArgs"/> class.
    /// </summary>
    /// <param name="player">The SCP-939 player instance.</param>
    /// <param name="amnesticCloudInstance">The created amnestic cloud instance.</param>
    public Scp939CreatedAmnesticCloudEventArgs(Player player, Scp939AmnesticCloudInstance amnesticCloudInstance)
    {
        Player = player;
        AmnesticCloudInstance = amnesticCloudInstance;
    }
    
    /// <summary>
    /// The SCP-939 player instance.
    /// </summary>
    public Player Player { get; }
    
    /// <summary>
    /// Gets the created amnestic cloud instance.
    /// </summary>
    public Scp939AmnesticCloudInstance AmnesticCloudInstance { get; }
}