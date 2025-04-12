using VoiceChat.Networking;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves a voice message.
/// </summary>
public interface IVoiceMessageEvent
{
    /// <summary>
    /// The voice message that is involved in the event.
    /// </summary>
    public ref VoiceMessage Message { get; }
}