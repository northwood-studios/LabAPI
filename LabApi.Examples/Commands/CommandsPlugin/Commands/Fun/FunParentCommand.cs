using System;
using CommandSystem;

namespace CommandsPlugin.Commands.Fun;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
public class FunParentCommand : ParentCommand
{
    public override void LoadGeneratedCommands()
    {
        RegisterCommand(new GrenadeCommand());
        RegisterCommand(new FlashbangCommand());
        RegisterCommand(new EffectCommand());
    }

    protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        response = "Please specify a valid subcommand!, options are, grenade, flash, effect";
        return false;
    }

    public override string Command { get; } = "fun";

    public override string[] Aliases { get; } = Array.Empty<string>();

    public override string Description { get; } = "Fun commands.";
}