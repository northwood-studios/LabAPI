using System;
using CommandSystem;

namespace CommandsPlugin.Commands;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
[CommandHandler(typeof(ClientCommandHandler))]
public class HelloCommand : ICommand
{
    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (arguments.Count == 0)
        {
            response = "Hi there. You can specify a name to say hello to.";
            return false;
        }
        
        response = $"Hi {arguments.At(0)}!";
        return true;
    }

    public string Command { get; } = "hello";

    public string[] Aliases { get; } = Array.Empty<string>();

    public string Description { get; } = "Prints a hello message.";
}