using LabApi.Loader.Features.Plugins.Configuration;
using LabApi.Loader.Features.Plugins.Enums;
using System;

namespace LabApi.Loader.Features.Plugins;

/// <summary>
/// Represents a plugin which can be loaded by the <see cref="PluginLoader"/>.
/// </summary>
public abstract class Plugin
{
    /// <summary>
    /// The name of the <see cref="Plugin"/>.
    /// </summary>
    public abstract string Name { get; }

    /// <summary>
    /// A description of the <see cref="Plugin"/>.
    /// </summary>
    public abstract string Description { get; }

    /// <summary>
    /// The author of the <see cref="Plugin"/>.
    /// </summary>
    public abstract string Author { get; }

    /// <summary>
    /// The <see cref="Version"/> of the <see cref="Plugin"/>.
    /// </summary>
    public abstract Version Version { get; }

    /// <summary>
    /// The <see cref="Version"/> of LabAPI required by the <see cref="Plugin"/>.
    /// </summary>
    public abstract Version RequiredApiVersion { get; }

    /// <summary>
    /// The <see cref="LoadPriority"/> of the <see cref="Plugin"/>.
    /// </summary>
    public virtual LoadPriority Priority { get; } = LoadPriority.Medium;

    /// <summary>
    /// Whether this plugin is considered transparent.<br/>
    /// A plugin can be marked as transparent if the server’s modifications are strictly limited to non-intrusive features that do not affect gameplay balance or make significant alterations to the user interface.
    /// Examples of transparent modifications are: admin tools, automated timed broadcasts for tips, message of the day or other administrative utilities.<br/>
    /// For more information, see article 5.2 in the official documentation: https://scpslgame.com/csg
    /// </summary>
    /// <remarks>
    /// You can keep using the 'transparently modded' flag during occasional short events organised and supervised by 
    /// Server Staff, regardless of the Modifications used for these events.
    /// </remarks>
    public virtual bool IsTransparent { get; } = false;

    /// <summary>
    /// The <see cref="Properties"/> of the <see cref="Plugin"/>.
    /// </summary>
    public Properties? Properties { get; internal set; }

    /// <summary>
    /// The full path to the plugin's DLL file.
    /// </summary>
    public string FilePath { get; internal set; } = string.Empty;

    /// <summary>
    /// Called when the <see cref="Plugin"/> is enabled.
    /// Should be used to register events, etc.
    /// </summary>
    public abstract void Enable();

    /// <summary>
    /// Called when the <see cref="Plugin"/> is disabled.
    /// Should be used to unregister events, etc.
    /// </summary>
    public abstract void Disable();

    /// <summary>
    /// Called before the <see cref="Plugin"/> is enabled.
    /// 
    /// <para>Commonly used to load configurations, or any data files before the plugin is enabled.</para>
    /// </summary>
    public virtual void LoadConfigs() { }

    /// <inheritdoc/>
    public override string ToString() => $"'{Name}', Version: {Version}, Author: '{Author}'";
}