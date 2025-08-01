using CommandSystem;
using System;
using static LabApi.Loader.Features.Commands.Extensions.CommandExtensions;

namespace LabApi.Loader.Features.Commands;

/// <summary>
/// LabAPI parent command used for all LabAPI related subcommands.
/// </summary>
[CommandHandler(typeof(RemoteAdminCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]
public class LabApiParentCommand : ParentCommand
{
    /// <inheritdoc />
    public override string Command => "labapi";

    /// <inheritdoc />
    public override string[] Aliases => ["lab"];

    /// <inheritdoc />
    public override string Description => "Parent command for all LabAPI subcommands.";

    /// <inheritdoc />
    public override void LoadGeneratedCommands() { }

    /// <inheritdoc />
    protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        => ListSubCommands(this, arguments, out response);
}
