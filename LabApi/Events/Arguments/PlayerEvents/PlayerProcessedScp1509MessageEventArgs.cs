using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using BaseScp1509Item = InventorySystem.Items.Scp1509.Scp1509Item;
using Scp1509MessageType = InventorySystem.Items.Scp1509.Scp1509MessageType;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ProcessedScp1509Message"/> event.
/// </summary>
public class PlayerProcessedScp1509MessageEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerProcessedScp1509MessageEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The <see cref="ReferenceHub"/> component of the player that owns the SCP-1509 item.</param>
    /// <param name="scp1509Item">The SCP-1509 item the message is for.</param>
    /// <param name="msg">The message sent by the player regarding the SCP-1509 item.</param>
    public PlayerProcessedScp1509MessageEventArgs(ReferenceHub hub, BaseScp1509Item scp1509Item, Scp1509MessageType msg)
    {
        Player = Player.Get(hub);
        Scp1509Item = Scp1509Item.Get(scp1509Item);
        Message = msg;
    }

    /// <summary>
    /// The owner of the SCP-1509 item.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The SCP-1509 item.
    /// </summary>
    public Scp1509Item Scp1509Item { get; }

    /// <summary>
    /// The <see cref="Scp1509MessageType"/> processed.
    /// </summary>
    public Scp1509MessageType Message { get; }
}
