using System;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;

namespace CommandsPlugin;

public class CommandsPlugin : Plugin
{
    public override string Name => "CommandsPlugin";

    public override string Description => "Simple example plugin that demonstrates adding commands.";

    public override string Author => "Northwood";

    public override Version RequiredApiVersion { get; } = new Version(LabApiProperties.CompiledVersion);

    public override void Enable()
    {
    }

    public override void Disable()
    {
    }
}