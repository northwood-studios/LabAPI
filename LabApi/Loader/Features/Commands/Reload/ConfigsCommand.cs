using CommandSystem;
using LabApi.Loader.Features.Plugins;
using NorthwoodLib.Pools;
using System;
using System.Text;

namespace LabApi.Loader.Features.Commands.Reload;

/// <summary>
/// Represents a command used to reload all plugin configs.
/// </summary>
[CommandHandler(typeof(ReloadParentCommand))]
public class ConfigsCommand : ICommand
{
    /// <inheritdoc />
    public string Command => "configs";

    /// <inheritdoc />
    public string[] Aliases => ["c", "cfg"];

    /// <inheritdoc />
    public string Description => "Reloads configs for all LabAPI plugins.";

    /// <inheritdoc />
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!sender.CheckPermission(PlayerPermissions.ServerConfigs, out response))
            return false;

        int pluginCount = PluginLoader.Plugins.Count;
        if (pluginCount == 0)
        {
            response = "No LabAPI plugins installed to reload configs for.";
            return false;
        }

        StringBuilder sb = StringBuilderPool.Shared.Rent();
        sb.AppendLine($"Reloading configs for {pluginCount} plugin{(pluginCount == 1 ? "" : "s")}");

        int successCount = 0;
        bool success = true;
        foreach (Plugin plugin in PluginLoader.Plugins.Keys)
        {
            try
            {
                plugin.LoadConfigs();
                successCount++;
            }
            catch (Exception ex)
            {
                sb.AppendLine($"Failed to reload configs for plugin {plugin} with Exception:\n{ex}");
                success = false;
            }
        }

        sb.AppendLine($"Successfully reloaded configs for {(successCount == PluginLoader.Plugins.Count ? "all" : $"{successCount}/{PluginLoader.Plugins.Count}")} plugins.");
        response = sb.ToString();
        StringBuilderPool.Shared.Return(sb);

        return success;
    }
}
