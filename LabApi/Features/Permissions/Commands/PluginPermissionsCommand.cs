using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CommandSystem;

namespace LabApi.Features.Permissions.Commands;

/// <summary>
/// Represents a command that allows players to view their plugin permissions.
/// </summary>
[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class PluginPermissionsCommand : ICommand
{
    /// <inheritdoc cref="ICommand.Command"/>
    public string Command { get; } = "pluginpermissions";

    /// <inheritdoc cref="ICommand.Aliases"/>
    public string[] Aliases { get; } = ["pp", "pluginperms"];

    /// <inheritdoc cref="ICommand.Description"/>
    public string Description { get; } = "Views your plugin permissions.";

    /// <inheritdoc cref="ICommand.Execute"/>
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, [UnscopedRef] out string response)
    {
        response = "You have the following plugin permissions:\n";

        foreach ((Type provider, string[] perms) in sender.GetPermissionsByProvider())
        {
            response += $"{provider.Name}:\n";

            if (perms.Length == 0)
            {
                response += "- No permissions.\n";
                continue;
            }

            response = perms.Aggregate(response, static (current, perm) => current + $"+ {perm}\n");
        }

        return true;
    }
}