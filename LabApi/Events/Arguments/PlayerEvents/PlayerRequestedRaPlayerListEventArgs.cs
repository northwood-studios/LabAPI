using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using System.Text;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.RequestedRaPlayerList"/> event.
/// </summary>
public class PlayerRequestedRaPlayerListEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerRequestedRaPlayerListEventArgs"/> class.
    /// </summary>
    /// <param name="commandSender">The <see cref="CommandSender"/> instance of the player that made the request for the RA player list.</param>
    /// <param name="builder">The <see cref="StringBuilder"/> used to build the RA player list.</param>
    public PlayerRequestedRaPlayerListEventArgs(CommandSender commandSender, StringBuilder builder)
    {
        Player = Player.Get(commandSender)!;
        ListBuilder = builder;
    }

    /// <summary>
    /// The player that requested the RA player list.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The <see cref="StringBuilder"/> used to construct the RA player list.
    /// </summary>
    /// <remarks>
    /// String builder is not empty in this event and contains the RA list.
    /// </remarks>
    public StringBuilder ListBuilder { get; }
}
