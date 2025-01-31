using System;
using CommandSystem;
using CustomPlayerEffects;
using LabApi.Features.Wrappers;

namespace CommandsPlugin.Commands.Fun;

[CommandHandler(typeof(FunParentCommand))]
public class EffectCommand : ICommand
{
    public string Command { get; } = "effect";

    public string[] Aliases { get; } = new[] { "e" };

    public string Description { get; } = "Gives yourself a random effect";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (!sender.CheckPermission(PlayerPermissions.PlayersManagement))
        {
            response = "You don't have permission to use this command! (Players Management)";
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

        StatusEffectBase[] allEffects = player.ReferenceHub.playerEffectsController.AllEffects;
        
        StatusEffectBase randomEffect = allEffects[UnityEngine.Random.Range(0, allEffects.Length)];
        player.EnableEffect(randomEffect, 1, 15f, true);

        response = "You have been given a random effect.";
        return true;
    }
}