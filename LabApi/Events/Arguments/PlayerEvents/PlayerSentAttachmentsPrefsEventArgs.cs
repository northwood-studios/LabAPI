using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.SentAttachmentsPrefs"/> event.
/// </summary>
public class PlayerSentAttachmentsPrefsEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initialized a new instance of <see cref="PlayerSentAttachmentsPrefsEventArgs"/>.
    /// </summary>
    /// <param name="player">The player who is changing the firearm attachments.</param>
    /// <param name="firearm">The firearm type whose attachments preferences are being changed.</param>
    /// <param name="oldAttachments">The old attachments code.</param>
    /// <param name="newAttachments">The new attachments code.</param>
    public PlayerSentAttachmentsPrefsEventArgs(ReferenceHub player, ItemType firearm, uint oldAttachments, uint newAttachments)
    {
        Player = Player.Get(player);
        FirearmType = firearm;
        OldAttachments = oldAttachments;
        NewAttachments = newAttachments;
    }

    /// <inheritdoc />
    public Player Player { get; }

    /// <summary>
    /// The <see cref="ItemType"/> of the firearm.
    /// </summary>
    public ItemType FirearmType { get; }

    /// <summary>
    /// Gets previous attachments code stored on the server.
    /// </summary>
    public uint OldAttachments { get; }

    /// <summary>
    /// Gets the new attachments code stored on the server.
    /// </summary>
    public uint NewAttachments { get; }
}
