using System;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;

namespace LogPlugin;

internal class LogPlugin : Plugin
{
    public override string Name => "LogPlugin";

    public override string Description => "Example Plugin that logs (almost) all events.";

    public override string Author => "Northwood";

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