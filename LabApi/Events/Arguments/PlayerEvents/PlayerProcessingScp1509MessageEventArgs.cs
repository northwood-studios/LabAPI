using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using BaseScp1509Item = InventorySystem.Items.Scp1509.Scp1509Item;
using Scp1509MessageType = InventorySystem.Items.Scp1509.Scp1509MessageType;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ProcessingScp1509Message"/> event.
/// </summary>
public class PlayerProcessingScp1509MessageEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerProcessingScp1509MessageEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The <see cref="ReferenceHub"/> component of the player that owns the SCP-1509 item.</param>
    /// <param name="scp1509Item">The SCP-1509 item the message is for.</param>
    /// <param name="msg">The message sent by the player regarding the SCP-1509 item.</param>
    /// <param name="allowAttack">Whether an attack is allowed to start.</param>
    /// <param name="allowInspect">Whether an inspect is allowed to start.</param>
    public PlayerProcessingScp1509MessageEventArgs(ReferenceHub hub, BaseScp1509Item scp1509Item, Scp1509MessageType msg, bool allowAttack, bool allowInspect)
    {
        Player = Player.Get(hub);
        Scp1509Item = Scp1509Item.Get(scp1509Item);
        Message = msg;
        AllowAttack = allowAttack;
        AllowInspect = allowInspect;
        IsAllowed = true;
    }

    /// <summary>
    /// The owner of the SCP-1509 item.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The Scp1509 item.
    /// </summary>
    public Scp1509Item Scp1509Item { get; }

    /// <summary>
    /// Gets or sets the <see cref="Scp1509MessageType"/> sent by the player.
    /// </summary>
    public Scp1509MessageType Message { get; set; }

    /// <summary>
    /// Gets or sets whether starting an attack is allowed.
    /// </summary>
    public bool AllowAttack { get; set; }

    /// <summary>
    /// Gets or sets whether starting an inspect is allowed.
    /// </summary>
    public bool AllowInspect { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
