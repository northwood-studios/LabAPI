using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using VoiceChat.Networking;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.SendingVoiceMessage"/> event.
/// </summary>
public class PlayerSendingVoiceMessageEventArgs : EventArgs, ICancellableEvent, IPlayerEvent, IVoiceMessageEvent
{
    private VoiceMessage _message;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerSendingVoiceMessageEventArgs"/> class.
    /// </summary>
    /// <param name="message">The <see cref="VoiceMessage" /> being sent.</param>
    public PlayerSendingVoiceMessageEventArgs(ref VoiceMessage message)
    {
        IsAllowed = true;
        Player = Player.Get(message.Speaker);
        _message = message;
    }

    /// <summary>
    /// Gets the player who is going to send the voice message.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the <see cref="VoiceMessage" /> being sent.
    /// </summary>
    public ref VoiceMessage Message => ref _message;

    /// <summary>
    /// Gets or sets a value indicating whether the voice message is allowed to be sent.
    /// </summary>
    /// <remarks>
    /// If this is <see langword="true" /> but the <see cref="VoiceChat.VoiceChatChannel" /> is
    /// <see cref="VoiceChat.VoiceChatChannel.None" /> this will be ignored.
    /// </remarks>
    public bool IsAllowed { get; set; }
}