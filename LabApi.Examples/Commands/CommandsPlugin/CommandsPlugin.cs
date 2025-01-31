using System;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;

namespace CommandsPlugin;

public class CommandsPlugin : Plugin
{
    public override string Name { get; } = "CommandsPlugin";

    public override string Description { get; } = "Simple example plugin that demonstrates adding commands.";

    public override string Author { get; } = "Northwood";

    public override Version Version { get; } = new Version(1, 0, 0, 0);

    public override Version RequiredApiVersion { get; } = new Version(LabApiProperties.CompiledVersion);

    public override void Enable()
    {
    }

    public override void Disable()
    {
    }
}