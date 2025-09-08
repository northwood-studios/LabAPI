using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.RequestedCustomRaInfo"/> event.
/// </summary>
public class PlayerRequestedCustomRaInfoEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Max number of clipboard links that can be created with <see cref="SetClipboardText(string, string, byte)"/>.
    /// </summary>
    public const int MaxClipboardCount = 3;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerRequestedCustomRaInfoEventArgs"/> class.
    /// </summary>
    /// <param name="commandSender">The <see cref="CommandSender"/> instance of the player making the request.</param>
    /// <param name="selectionArgs">The request arguments.</param>
    /// <param name="isSensitiveInfo">Whether the info being requested is sensitive.</param>
    /// <param name="infoBuilder">The <see cref="StringBuilder"/> use to build the response.</param>
    public PlayerRequestedCustomRaInfoEventArgs(CommandSender commandSender, ArraySegment<string> selectionArgs,
        bool isSensitiveInfo, StringBuilder infoBuilder)
    {
        Player = Player.Get(commandSender)!;
        SelectedIdentifiers = selectionArgs.First().Split(".");
        IsSensitiveInfo = isSensitiveInfo;
        InfoBuilder = infoBuilder;
    }

    /// <summary>
    /// The player that made the request.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Array of identifiers selected by the player.
    /// Identifiers come from items in the RA player list that have been placed by plugins <see cref="Handlers.PlayerEvents.RequestingRaPlayerList"/>.
    /// </summary>
    public string[] SelectedIdentifiers { get; }

    /// <summary>
    /// Gets whether the <see cref="Player"/> requested sensitive info.
    /// </summary>
    public bool IsSensitiveInfo { get; }

    /// <summary>
    /// Gets the <see cref="StringBuilder"/> used to construct the response message.
    /// </summary>
    public StringBuilder InfoBuilder { get; }

    private string[]? clipboardTexts = null;

    /// <summary>
    /// Creates a clipboard link for the RA.
    /// </summary>
    /// <remarks>
    /// Usage <c>ev.InfoBuilder.Append(ev.SetClipboardText("Click Me", "Text to copy to clipboard on click", 0))</c>
    /// </remarks>
    /// <param name="linkText">Text to display as the link.</param>
    /// <param name="clipboardText">Text to copy to the clipboard when clicking on the link.</param>
    /// <param name="id">The id of the clipboard, must be between 0 and <see cref="MaxClipboardCount"/>.</param>
    /// <returns>The formated clipboard link text.</returns>
    public string SetClipboardText(string linkText, string clipboardText, byte id)
    {
        if (id >= MaxClipboardCount)
            throw new ArgumentOutOfRangeException(nameof(id), id, $"id must be between 0 and {MaxClipboardCount}");

        clipboardTexts ??= new string[MaxClipboardCount];
        clipboardTexts[id] = clipboardText;

        return $"<link={LinkIdForClipboardId(id)}>{linkText}</link>";
    }

    /// <summary>
    /// Tries to get clipboard text for the specific id.
    /// </summary>
    /// <param name="id">The id associated with the clipboard text.</param>
    /// <param name="text">The found text, otherwise <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if set and not empty, otherwise <see langword="false"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public bool TryGetClipboardText(byte id, [NotNullWhen(true)] out string? text)
    {
        if (id >= MaxClipboardCount)
            throw new ArgumentOutOfRangeException(nameof(id), id, $"id must be between 0 and {MaxClipboardCount}");

        text = null;

        if (clipboardTexts == null)
            return false;

        text = clipboardTexts[id];
        return !string.IsNullOrEmpty(text);
    }

    private string LinkIdForClipboardId(byte id) => id switch
    {
        0 => "CP_ID",
        1 => "CP_IP",
        2 => "CP_USERID",
        _ => throw new ArgumentOutOfRangeException(nameof(id), id, $"id must be between 0 and {MaxClipboardCount}")
    };
}
