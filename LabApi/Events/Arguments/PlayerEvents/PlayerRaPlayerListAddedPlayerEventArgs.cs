using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using System.Text;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.RaPlayerListAddedPlayer"/> event.
/// </summary>
public class PlayerRaPlayerListAddedPlayerEventArgs : EventArgs, IPlayerEvent, ITargetEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerRaPlayerListAddedPlayerEventArgs"/> class.
    /// </summary>
    /// <param name="requesterHub">The <see cref="CommandSender"/> instance of the player that made the request for the RA player list.</param>
    /// <param name="targetHub">The <see cref="ReferenceHub"/> component of the player that is being added to the RA player list.</param>
    /// <param name="builder">The <see cref="StringBuilder"/> used to build the RA player list item.</param>
    public PlayerRaPlayerListAddedPlayerEventArgs(CommandSender requesterHub, ReferenceHub targetHub, StringBuilder builder)
    {
        Player = Player.Get(requesterHub)!;
        Target = Player.Get(targetHub);
        TargetBuilder = builder;
    }

    /// <summary>
    /// The player that requested the RA player list.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The player that was added to the RA player list.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// The <see cref="StringBuilder"/> used to construct the RA player list item for the <see cref="Target"/>.
    /// </summary>
    /// <remarks>
    /// String builder is not empty in this event and contains the RA list item for the <see cref="Target"/>.
    /// </remarks>
    public StringBuilder TargetBuilder { get; }
}
