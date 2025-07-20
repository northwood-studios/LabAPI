using InventorySystem.Items.Firearms;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ChangedAttachments"/> event.
/// </summary>
public class PlayerChangedAttachmentsEventArgs : EventArgs, IPlayerEvent, IFirearmItemEvent
{
    /// <summary>
    /// Initialized a new instance of <see cref="PlayerChangedAttachmentsEventArgs"/>.
    /// </summary>
    /// <param name="player">The player who is changing the firearm attachments.</param>
    /// <param name="firearm">The firearm whose attachments are being changed.</param>
    /// <param name="oldAttachments">The old attachments code.</param>
    /// <param name="newAttachments">The new attachments code.</param>
    public PlayerChangedAttachmentsEventArgs(ReferenceHub player, Firearm firearm, uint oldAttachments, uint newAttachments)
    {
        Player = Player.Get(player);
        FirearmItem = FirearmItem.Get(firearm);
        OldAttachments = oldAttachments;
        NewAttachments = newAttachments;
    }

    /// <inheritdoc>
    public Player Player { get; }

    /// <inheritdoc>
    public FirearmItem FirearmItem { get; }

    /// <summary>
    /// Gets previous attachments code.
    /// </summary>
    public uint OldAttachments { get; }

    /// <summary>
    /// Gets the new attachments code.
    /// </summary>
    public uint NewAttachments { get; }
}
