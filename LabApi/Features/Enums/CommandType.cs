namespace LabApi.Features.Enums;

/// <summary>
/// Represents the type of command that can be executed.
/// </summary>
public enum CommandType
{
    /// <summary>
    /// A command that is executed from the server console.
    /// </summary>
    Console = 0,
    
    /// <summary>
    /// A command that is executed from the remote admin.
    /// <para>Also called slash command.</para>
    /// </summary>
    RemoteAdmin = 1,
    
    /// <summary>
    /// A command that is executed from the players console.
    /// <para>Also called dot command.</para>
    /// </summary>
    Client = 2,
}