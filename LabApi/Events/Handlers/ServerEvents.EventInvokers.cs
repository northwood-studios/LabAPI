namespace LabApi.Events.Handlers;

/// <inheritdoc />
public static partial class ServerEvents
{
    /// <summary>
    /// Invokes the <see cref="RoundRestarted"/> event.
    /// </summary>
    public static void OnRoundRestarted() => RoundRestarted.InvokeEvent();
}