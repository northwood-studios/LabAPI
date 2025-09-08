using LabApi.Events.Arguments.Interfaces;
using LabApi.Events.Arguments.Interfaces.Items;
using LabApi.Features.Wrappers;
using System;
using BaseJailbirdItem = InventorySystem.Items.Jailbird.JailbirdItem;
using JailbirdMessageType = InventorySystem.Items.Jailbird.JailbirdMessageType;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ProcessedJailbirdMessage"/> event.
/// </summary>
public class PlayerProcessedJailbirdMessageEventArgs : EventArgs, IPlayerEvent, IJailbirdEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerProcessedJailbirdMessageEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The <see cref="ReferenceHub"/> component of the player that owns the jailbird.</param>
    /// <param name="jailbird">Jailbird the message is for.</param>
    /// <param name="msg">The message sent by the player for the jailbird.</param>
    public PlayerProcessedJailbirdMessageEventArgs(ReferenceHub hub, BaseJailbirdItem jailbird, JailbirdMessageType msg)
    {
        Player = Player.Get(hub);
        JailbirdItem = JailbirdItem.Get(jailbird);
        Message = msg;
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
    /// The <see cref="JailbirdMessageType"/> processed.
    /// </summary>
    public JailbirdMessageType Message { get; }
}
