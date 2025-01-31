using System;
using CommandSystem;
using LabApi.Features.Wrappers;

namespace CommandsPlugin.Commands.Fun;

[CommandHandler(typeof(FunParentCommand))]
public class FlashbangCommand : ICommand
{
    public string Command { get; } = "flashbang";

    public string[] Aliases { get; } = new[] { "flash", "f" };

    public string Description { get; } = "Spawns a live flash on yourself.";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!sender.CheckPermission(PlayerPermissions.FacilityManagement))
        {
            response = "You don't have permission to use this command! (Facility Management)";
            return false;
        }

        if (!Player.TryGet(sender, out Player? player))
        {
            response = "You must be a player to use this command!";
            return false;
        }

        if (!player.IsAlive)
        {
            response = "You must be alive to use this command!";
            return false;
        }

        bool success = Helpers.SpawnLiveProjectile(ItemType.GrenadeFlash, player);

        if (success)
            response = "Flashbang has been spawned!";
        else
            response = "Failed to spawn flashbang!";

        return true;
    }
}