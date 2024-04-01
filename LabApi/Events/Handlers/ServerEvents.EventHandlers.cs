namespace LabApi.Events.Handlers;

/// <summary>
/// Handles all the events related to the server.
/// </summary>
public static partial class ServerEvents
{
    /// <summary>
    /// Gets called when the round is restarted.
    /// </summary>
    public static event EventManager.LabEventHandler RoundRestarted;
}