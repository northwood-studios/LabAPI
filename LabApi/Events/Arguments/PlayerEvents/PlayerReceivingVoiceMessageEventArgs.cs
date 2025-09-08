using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using VoiceChat.Networking;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.ReceivingVoiceMessage"/> event.
/// </summary>
public class PlayerReceivingVoiceMessageEventArgs : EventArgs, ICancellableEvent, IPlayerEvent, IVoiceMessageEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerReceivingVoiceMessageEventArgs"/> class.
    /// </summary>
    /// <param name="listener">The <see cref="ReferenceHub" /> who is going to receive the voice message.</param>
    /// <param name="message">The <see cref="VoiceMessage" /> being received.</param>
    public PlayerReceivingVoiceMessageEventArgs(ReferenceHub listener, ref VoiceMessage message)
    {
        IsAllowed = true;
        Player = Player.Get(listener);
        _message = message;
    }

    /// <summary>
    /// Gets the player who is going to receive the voice message.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the player that is sending the voice message.
    /// </summary>
    public Player Sender => Player.Get(Message.Speaker);

    /// <summary>
    /// Gets the <see cref="VoiceMessage" /> being received.
    /// </summary>
    public ref VoiceMessage Message => ref _message;

    /// <summary>
    /// Gets or sets a value indicating whether the voice message is allowed to be received.
    /// </summary>
    /// <remarks>
    /// If this is <see langword="true" /> but the <see cref="VoiceChat.VoiceChatChannel" /> is
    /// <see cref="VoiceChat.VoiceChatChannel.None" /> this will be ignored.
    /// </remarks>
    public bool IsAllowed { get; set; }

    private VoiceMessage _message;
}