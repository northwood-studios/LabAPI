using CommandSystem;
using System;
using System.Linq;
using UnityEngine;

namespace CommandsPlugin2.Commands
{
    /// <summary>
    /// Command example on how to change default gravity for all players.
    /// Here you can see that you can register the command to multiple handlers, so this command is executable both from RA and server console.
    /// </summary>
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class ChangeDefaultGravityCommand : ICommand
    {
        public string Command => "defaultgravity";

        public string[] Aliases => [];

        public string Description => "Changes the default gravity for all players aswell as keeping the value after they change role/respawn";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender.CheckPermission(PlayerPermissions.FacilityManagement))
            {
                if (arguments.Count != 1)
                {
                    response = "Please provide the gravity value. Negatives make the player go down and positive permanently up.";
                    return false;
                }

                if (!float.TryParse(arguments.ElementAt(0), out float gravity))
                {
                    response = "Unable to parse new gravity value. Please provide decimal value.";
                    return false;
                }

                CommandsOverviewPlugin.Singleton.EventsHandler.NewDefaultGravity = new Vector3(0f, gravity, 0f);

                response = "Weeeee";
                return true;
            }

            response = "You don't have enough permission to run this command";
            return false;
        }
    }
}
