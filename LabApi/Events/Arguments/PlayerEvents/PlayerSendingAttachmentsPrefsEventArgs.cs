using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.SendingAttachmentsPrefs"/> event.
/// </summary>
public class PlayerSendingAttachmentsPrefsEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initialized a new instance of <see cref="PlayerSendingAttachmentsPrefsEventArgs"/>.
    /// </summary>
    /// <param name="player">The player who is changing the firearm attachments.</param>
    /// <param name="firearm">The firearm type whose attachments preferences are being changed.</param>
    /// <param name="oldAttachments">The old attachments code.</param>
    /// <param name="newAttachments">The new attachments code requested by the player.</param>
    public PlayerSendingAttachmentsPrefsEventArgs(ReferenceHub player, ItemType firearm, uint oldAttachments, uint newAttachments)
    {
        Player = Player.Get(player);
        FirearmType = firearm;
        OldAttachments = oldAttachments;
        NewAttachments = newAttachments;
        IsAllowed = true;
    }

    /// <inheritdoc />
    public Player Player { get; }

    /// <summary>
    /// Gets the <see cref="ItemType"/> of the firearm.
    /// </summary>
    public ItemType FirearmType { get; set; }

    /// <summary>
    /// Gets previous attachments code stored on the server.
    /// </summary>
    public uint OldAttachments { get; }

    /// <summary>
    /// Gets or sets new attachments code. <b>Edited values are NOT propagated back to the client and are stored on the server for on-spawn purposes.</b>
    /// </summary>
    public uint NewAttachments { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
