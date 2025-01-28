using System;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Handlers;
using LabApi.Features;
using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;

namespace HelloWorldPlugin;

internal class HelloWorldPlugin : Plugin
{
    public override string Name { get; } = "Hello World";

    public override string Description { get; } = "Simple example plugin that demonstrates showing a broadcast to players when they join. Using 'Legacy' (C#) events.";

    public override string Author { get; } = "Northwood";

    public override Version Version { get; } = new Version(1, 0, 0, 0);

    public override Version RequiredApiVersion { get; } = new Version(LabApiProperties.CompiledVersion);

    public override void Enable()
    {
        PlayerEvents.Joined += OnJoined;
    }

    public override void Disable()
    {
        PlayerEvents.Joined -= OnJoined;
    }

    private static void OnJoined(PlayerJoinedEventArgs ev)
    {
        Logger.Info($"Player {ev.Player.DisplayName} joined the server!");
        ev.Player.SendBroadcast("Hello! Thanks for joining!", 10);
    }
}