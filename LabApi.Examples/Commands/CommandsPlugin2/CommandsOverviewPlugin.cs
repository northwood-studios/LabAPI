using CommandsPlugin2.EventHandlers;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Loader;
using LabApi.Loader.Features.Plugins;
using System;
using CommandSystem;

namespace CommandsPlugin2;

/// <summary>
/// Base plugin class.<br/>
/// This plugin examples provides overview on how to create custom Remote Admin, Client and Server Console Commands.<br/>
/// Every command must have the <see cref="CommandHandler"/> attribute with type specified, otherwise it won't be automatically registered (See examples).<br/>
/// All commands are automatically registered when the plugin is loaded so you don't need to run any method.<br/>
/// However, if you wish to unregister and register commands at runtime, you can use the <see cref="CommandLoader.RegisterCommands(Plugin)"/> and <see cref="CommandLoader.UnregisterCommands(Plugin)"/>
/// </summary>
public class CommandsOverviewPlugin : Plugin
{
    /// <summary>
    /// Singleton reference to this plugin class.
    /// </summary>
    public static CommandsOverviewPlugin Singleton { get; private set; }

    public override string Name => "Commands Example Plugin";

    public override string Description => "Plugin written in LabAPI showcasing how to work with custom commands.";

    public override string Author => "LabAPI Team";

    public override Version Version => new Version(1, 0, 0);

    public override Version RequiredApiVersion => new Version(LabApiProperties.CompiledVersion);

    /// <summary>
    /// Events handler object.
    /// </summary>
    public GravityEventHandler EventsHandler { get; private set; }

    public override void Enable()
    {
        Singleton = this;

        EventsHandler = new GravityEventHandler();

        CustomHandlersManager.RegisterEventsHandler(EventsHandler);
    }

    public override void Disable()
    {
        CustomHandlersManager.UnregisterEventsHandler(EventsHandler);
    }
}