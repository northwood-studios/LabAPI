using LabApi.Events.Arguments.Interfaces;
using LabApi.Events.Arguments.Interfaces.Items;
using LabApi.Features.Wrappers;
using System;
using BaseJailbirdItem = InventorySystem.Items.Jailbird.JailbirdItem;
using JailbirdMessageType = InventorySystem.Items.Jailbird.JailbirdMessageType;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ProcessingJailbirdMessage"/> event.
/// </summary>
public class PlayerProcessingJailbirdMessageEventArgs : EventArgs, IPlayerEvent, IJailbirdEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerProcessingJailbirdMessageEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The <see cref="ReferenceHub"/> component of the player that owns the jailbird.</param>
    /// <param name="jailbird">Jailbird the message is for.</param>
    /// <param name="msg">The message sent by the player for the jailbird.</param>
    /// <param name="allowAttack">Whether an attack is allowed to start.</param>
    /// <param name="allowInspect">Whether an inspect is allowed to start.</param>
    public PlayerProcessingJailbirdMessageEventArgs(ReferenceHub hub, BaseJailbirdItem jailbird, JailbirdMessageType msg, bool allowAttack, bool allowInspect)
    {
        Player = Player.Get(hub);
        JailbirdItem = JailbirdItem.Get(jailbird);
        Message = msg;
        AllowAttack = allowAttack;
        AllowInspect = allowInspect;
        IsAllowed = true;
    }

    /// <summary>
    /// The owner of the jailbird.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// The jailbird item.
    /// </summary>
    public JailbirdItem JailbirdItem { get; }

    /// <summary>
    /// Gets or sets the <see cref="JailbirdMessageType"/> sent by the player.
    /// </summary>
    public JailbirdMessageType Message { get; set; }

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
