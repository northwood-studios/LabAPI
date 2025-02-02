using CommandSystem;
using LabApi.Features.Wrappers;
using PlayerRoles;
using PlayerStatsSystem;
using System;
using UserSettings.ServerSpecific;

namespace CommandsPlugin2.Commands
{
    /// <summary>
    /// A Melee command example as a dot command executable by any client.<br/>
    /// Do NOT add the dot (.) in the command's name when creating any dot command as this is handled by game itself.<br/>
    /// This logic can also be used within <see cref="SSKeybindSetting"/> to create some sort of melee combat system. <b>Note that there is no cooldown check so please do add it if you wish to use this.</b>
    /// </summary>
    [CommandHandler(typeof(ClientCommandHandler))]
    public class MeleeCommand : ICommand
    {
        public string Command => "melee";

        public string[] Aliases => [];

        public string Description => "Damages player within close range a small damage - simulating a melee attack";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);

            if (player == null)
            {
                response = "You are not a player?";
                return false;
            }

            if (!player.IsAlive)
            {
                response = "You cannot damage someone as dead...";
                return false;
            }

            if (player.Team == Team.SCPs)
            {
                response = "You cannot punch someone as an SCP!";
                return false;
            }

            Player toHit = null;
            float closestMagnitude = float.MaxValue;
            foreach (Player plr in Player.List)
            {
                if (plr == player) // Yeah lets not punch ourselfs
                    continue;

                if (!plr.IsAlive) // Can't exactly hit a dead person
                    continue;

                if (plr.Team == Team.SCPs) // I wouldnt want to fist punch peanut
                    continue;

                float sqrMagnitude = (player.Position - plr.Position).sqrMagnitude;
                if (sqrMagnitude > 5)
                    continue;

                if (toHit == null || sqrMagnitude < closestMagnitude) // Get the closest one if youu are standing in a crowd
                    toHit = plr;
            }

            if (toHit == null)
            {
                response = "No nearby player(s) found";
                return false;
            }

            float mutliplier = 1f;

            // Multiply the damage by something based on the item's category - I wouldnt expect a plastic keycard to do anything
            if (player.CurrentItem != null)
            {
                mutliplier = player.CurrentItem.Category switch
                {
                    ItemCategory.None => 1f,
                    ItemCategory.Keycard => 0.1f,
                    ItemCategory.Medical => 0.5f,
                    ItemCategory.Radio => 0.95f,
                    ItemCategory.Firearm => 1.3f,
                    ItemCategory.Grenade => 1f,
                    ItemCategory.SCPItem => 1.2f,
                    ItemCategory.SpecialWeapon => 1.3f,
                    ItemCategory.Ammo => 0f,
                    ItemCategory.Armor => 0f,
                    _ => 1f,
                };
            }

            toHit.Damage(new CustomReasonDamageHandler("Punched to death", UnityEngine.Random.Range(0.5f, 1.5f) * mutliplier));
            player.SendHitMarker(0.5f); // Damage handler itself doesn't send hitmark, you have to send it yourself

            response = "Hit!";
            return true;
        }
    }
}
