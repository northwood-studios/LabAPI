using CommandSystem;
using System;
using static LabApi.Loader.Features.Commands.Extensions.CommandExtensions;

namespace LabApi.Loader.Features.Commands.Reload;

/// <summary>
/// Reload parent command used for all LabAPI reload related sub commands.
/// </summary>
[CommandHandler(typeof(LabApiParentCommand))]
public class ReloadParentCommand : ParentCommand
{
    /// <inheritdoc />
    public override string Command => "reload";

    /// <inheritdoc />
    public override string[] Aliases => ["r", "rl"];

    /// <inheritdoc />
    public override string Description => "Parent command for all reloading related sub commands.";

    /// <inheritdoc />
    public override void LoadGeneratedCommands()
    {
    }

    /// <inheritdoc />
    protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        => ListSubCommands(this, arguments, out response);
}
