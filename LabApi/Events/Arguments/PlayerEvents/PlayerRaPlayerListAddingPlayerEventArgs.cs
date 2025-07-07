using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using System.Text;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.RaPlayerListAddingPlayer"/> event.
/// </summary>
public class PlayerRaPlayerListAddingPlayerEventArgs : EventArgs, IPlayerEvent, ITargetEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerRaPlayerListAddingPlayerEventArgs"/> class.
    /// </summary>
    /// <param name="commandSender">The <see cref="CommandSender"/> instance of the player that made the request for the RA player list.</param>
    /// <param name="targetHub">The <see cref="ReferenceHub"/> component of the player that is being added to the RA player list.</param>
    /// <param name="builder">The <see cref="StringBuilder"/> used to build the RA player list item.</param>
    /// <param name="prefix">The prefix string for the RA list item.</param>
    /// <param name="inOverwatch">Whether to include the overwatch icon in the list item.</param>
    /// <param name="isMuted">Whether to include the is muted icon and link in the list item.</param>
    /// <param name="body">The body string for the Ra list item.</param>
    public PlayerRaPlayerListAddingPlayerEventArgs(CommandSender commandSender, ReferenceHub targetHub, 
        StringBuilder builder, string prefix, bool inOverwatch, bool isMuted, string body)
    {
        Player = Player.Get(commandSender)!;
        Target = Player.Get(targetHub);
        TargetBuilder = builder;
        Prefix = prefix;
        InOverwatch = inOverwatch;
        IsMuted = isMuted;
        Body = body;
        IsAllowed = true;
    }

    /// <summary>
    /// The player that requested the RA player list.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The player being added to the RA player list.
    /// </summary>
    public Player Target { get; }

    /// <summary>
    /// The <see cref="StringBuilder"/> used to construct the RA player list item for the <see cref="Target"/>.
    /// </summary>
    /// <remarks>
    /// String builder is empty in this event.
    /// </remarks>
    public StringBuilder TargetBuilder { get; }

    /// <summary>
    /// Gets or sets the RA player list item prefix.
    /// Can contain a RA badge and/or link for whether the <see cref="Target"/> is one of either a Dummy, has RemoteAdminGlobalAccess, is NorthwoodStaff, or has RemoteAdmin.
    /// Otherwise this is <see cref="string.Empty"/>.
    /// </summary>
    public string Prefix { get; set; }

    /// <summary>
    /// Gets or sets whether the <see cref="Target"/> appears to be in overwatch on the RA player list.
    /// </summary>
    public bool InOverwatch { get; set; }

    /// <summary>
    /// Gets or sets whether the <see cref="Target"/> appears to be muted on the RA player list.
    /// </summary>
    public bool IsMuted { get; set; }

    /// <summary>
    /// Gets or sets the RA player list item body.
    /// Contains the color tags, identifier as the <see cref="Player.PlayerId"/> and <see cref="NicknameSync.CombinedName"/> of the <see cref="Target"/>.
    /// </summary>
    public string Body { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
