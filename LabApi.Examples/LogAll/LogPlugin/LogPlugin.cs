using System;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;

namespace LogPlugin;

internal class LogPlugin : Plugin
{
    public override string Name { get; } = "LogPlugin";

    public override string Description { get; } = "Example Plugin that logs (almost) all events.";

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