using CommandSystem;
using LabApi.Features.Wrappers;
using System;
using System.Globalization;
using System.Linq;
using ICommand = CommandSystem.ICommand;

namespace CommandsPlugin2.Commands
{
    /// <summary>
    /// Command example on how to change maximum player's HP via Remote Admin Console.
    /// The very basic command example.
    /// </summary>
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ChangeMaxHpCommand : ICommand
    {
        public string Command => "changemaxhp";

        public string[] Aliases => ["maxhp", "hpmax"];

        public string Description => "Changes scale of a specified player";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender.CheckPermission(PlayerPermissions.PlayersManagement))
            {
                if (arguments.Count != 2)
                {
                    response = "Provide player ID and new maximum value";
                    return false;
                }

                if (!int.TryParse(arguments.ElementAt(0), out int id))
                {
                    response = "Enter player's ID as first argument";
                    return false;
                }

                Player? player = Player.Get(id);

                if (player == null)
                {
                    response = $"Player not found by id {id}";
                    return false;
                }

                string maxHealthString = arguments.ElementAt(1);
                if (!float.TryParse(maxHealthString, out float maxhp))
                {
                    response = $"Unable to parse new maximum hp value ({maxHealthString})";
                    return false;
                }

                player.MaxHealth = maxhp; // as part of LabAPI - you can now set player's max HP and it will be synced with health bar on the client :)

                response = $"Changed max HP of player {player.Nickname} to {player.MaxHealth.ToString("0.00", CultureInfo.InvariantCulture)}";
                return true;
            }

            response = "You don't have enough permission to run this command";
            return false;
        }
    }
}