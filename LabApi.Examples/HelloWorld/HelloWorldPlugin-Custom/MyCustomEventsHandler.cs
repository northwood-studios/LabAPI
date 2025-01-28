using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Console;

namespace HelloWorldPlugin;

public class MyCustomEventsHandler : CustomEventsHandler
{
    public override void OnPlayerJoined(PlayerJoinedEventArgs ev)
    {
        Logger.Info($"Player {ev.Player.DisplayName} joined the server!");
        ev.Player.SendBroadcast("Hello! Thanks for joining!", 10);
    }
}