using System;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;

namespace HelloWorldPlugin;

internal class HelloWorldPlugin : Plugin
{
    public override string Name { get; } = "Hello World";

    public override string Description { get; } = "Simple example plugin that demonstrates showing a broadcast to players when they join. Using Custom Event Handlers.";

    public override string Author { get; } = "Northwood";

    public override Version Version { get; } = new Version(1, 0, 0, 0);

    public override Version RequiredApiVersion { get; } = new Version(LabApiProperties.CompiledVersion);

    public MyCustomEventsHandler Events { get;  } = new();

    public override void Enable()
    {
        CustomHandlersManager.RegisterEventsHandler(Events);
    }

    public override void Disable()
    {
        CustomHandlersManager.UnregisterEventsHandler(Events);
    }
}