using CommandSystem;
using NorthwoodLib.Pools;
using System;
using System.Text;

namespace LabApi.Loader.Features.Commands.Extensions;

/// <summary>
/// Extension class for LabAPI commands.
/// </summary>
public static class CommandExtensions
{
    /// <summary>
    /// Responds to a parent command execution by listing out all sub commands, and/or printing an invalid arg message if any invalid args were provided.
    /// </summary>
    /// <param name="parent">The parent command instance.</param>
    /// <param name="arguments">The arguments passed to the parent command.</param>
    /// <param name="response">The output response.</param>
    /// <returns><see langword="true"/> if there were no arguments, otherwise <see langword="false"/>.</returns>
    public static bool ListSubCommands(ParentCommand parent, ArraySegment<string> arguments, out string response)
    {
        StringBuilder sb = StringBuilderPool.Shared.Rent();

        sb.AppendLine(arguments.Count > 0
            ? $"'{arguments.At(0)}' was not a valid sub command."
            : "You must specify a subcommand when executing this command.");

        sb.AppendLine("Valid subcommands:");

        foreach (ICommand subCommand in parent.AllCommands)
        {
            sb.AppendLine($"    {subCommand.Command} - {subCommand.Description} Aliases [{string.Join(" ", subCommand.Aliases)}]");
        }

        response = StringBuilderPool.Shared.ToStringReturn(sb);
        return arguments.Count == 0;
    }
}
