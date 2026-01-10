using Cassie;
using PlayerRoles;
using PlayerStatsSystem;
using System;

namespace LabApi.Features.Wrappers;

/// <inheritdoc cref="Announcer"/>
[Obsolete("Use Announcer instead of Cassie.", true)]
public static class Cassie
{
    /// <inheritdoc cref="Announcer.IsSpeaking"/>
    [Obsolete("Use Announcer.IsSpeaking instead.", true)]
    public static bool IsSpeaking
        => Announcer.IsSpeaking;

    /// <inheritdoc cref="Announcer.LineDatabase"/>
    [Obsolete("Use Announcer.LineDatabase instead.", true)]
    public static CassieLineDatabase? LineDatabase
        => Announcer.LineDatabase;

    /// <inheritdoc cref="Announcer.AllLines"/>
    [Obsolete("Use Announcer.AllLines instead.", true)]
    public static CassieLine[] AllLines
        => Announcer.AllLines;

    /// <inheritdoc cref="Announcer.CollectionNames"/>
    [Obsolete("Use Announcer.CollectionNames instead.", true)]
    public static string[] CollectionNames
        => Announcer.CollectionNames;

    /// <inheritdoc cref="Announcer.IsValid(string)"/>
    [Obsolete("Use Announcer.IsValid(string) instead.", true)]
    public static bool IsValid(string word)
        => Announcer.IsValid(word);

    /// <inheritdoc cref="Announcer.CalculateDuration(string, bool, float)"/>
    [Obsolete("Use Announcer.CalculateDuration(string message, CassiePlaybackModifiers playbackModifiers) instead.", true)]
    public static float CalculateDuration(string message, bool rawNumber = false, float speed = 1f)
        => Announcer.CalculateDuration(message, rawNumber, speed);

    /// <inheritdoc cref="Announcer.CalculateDuration(string, CassiePlaybackModifiers)"/>
    [Obsolete("Use Announcer.CalculateDuration(string, CassiePlaybackModifiers) instead.", true)]
    public static double CalculateDuration(string message, CassiePlaybackModifiers playbackModifiers)
        => Announcer.CalculateDuration(message, playbackModifiers);

    /// <inheritdoc cref="Announcer.Message(string, bool, bool, bool, string)"/>
    [Obsolete("Use Announcer.Message(string message, string customSubtitles = \"\", bool playBackground = true, float priority = 0f, float glitchScale = 1f) instead.", true)]
    public static void Message(string message, bool isHeld = false, bool isNoisy = true, bool isSubtitles = true, string customSubtitles = "")
        => Announcer.Message(message, customSubtitles);

    /// <inheritdoc cref="Announcer.Message(string, string, bool, float, float)"/>
    [Obsolete("Use Announcer.Message(string, string, bool, float, float) instead.", true)]
    public static void Message(string message, string customSubtitles = "", bool playBackground = true, float priority = 0f, float glitchScale = 1f)
        => Announcer.Message(message, customSubtitles, playBackground, priority, glitchScale);

    /// <inheritdoc cref="Announcer.Message(CassieTtsPayload, float, float)"/>
    [Obsolete("Use Announcer.Message(CassieTtsPayload, float, float) instead.", true)]
    public static void Message(CassieTtsPayload payload, float priority = 0f, float glitchScale = 1f)
        => Announcer.Message(payload, priority, glitchScale);

    /// <inheritdoc cref="Announcer.GlitchyMessage(string, float, float)"/>
    [Obsolete("Use Announcer.GlitchyMessage(string, float, float) instead.", true)]
    public static void GlitchyMessage(string message, float glitchChance, float jamChance)
        => Announcer.GlitchyMessage(message, glitchChance, jamChance);

    /// <inheritdoc cref="Announcer.ScpTermination(Player, DamageHandlerBase)"/>
    [Obsolete("Use Announcer.ScpTermination(Player, DamageHandlerBase) instead.", true)]
    public static void ScpTermination(Player player, DamageHandlerBase info)
        => Announcer.ScpTermination(player, info);

    /// <inheritdoc cref="Announcer.Clear()"/>
    [Obsolete("Use Announcer.Clear() instead.", true)]
    public static void Clear()
        => Announcer.Clear();

    /// <inheritdoc cref="Announcer.ConvertTeam(Team, string)"/>
    [Obsolete("Use Announcer.ConvertTeam(Team, string) instead.", true)]
    public static string ConvertTeam(Team team, string unitName)
        => Announcer.ConvertTeam(team, unitName);

    /// <inheritdoc cref="Announcer.ConvertNumber(int)"/>
    [Obsolete("Use Announcer.ConvertNumber(int) instead.", true)]
    public static string ConvertNumber(int num)
        => Announcer.ConvertNumber(num);

    /// <inheritdoc cref="Announcer.ConvertScp(RoleTypeId, out string, out string)"/>
    [Obsolete("Use Announcer.ConvertScp(RoleTypeId, out string, out string) instead.", true)]
    public static void ConvertScp(RoleTypeId role, out string withoutSpace, out string withSpace)
        => Announcer.ConvertScp(role, out withoutSpace, out withSpace);

    /// <inheritdoc cref="Announcer.ConvertScp(string, out string, out string)"/>
    [Obsolete("Use Announcer.ConvertScp(string, out string, out string) instead.", true)]
    public static void ConvertScp(string roleName, out string withoutSpace, out string withSpace)
        => Announcer.ConvertScp(roleName, out withoutSpace, out withSpace);

    /// <inheritdoc cref="Announcer.CalculateDuration(ReadOnlySpan{char}, CassiePlaybackModifiers, out double)"/>
    [Obsolete("Use Announcer.CalculateDuration(ReadOnlySpan{char}, CassiePlaybackModifiers, out double) instead.", true)]
    public static void CalculateDuration(ReadOnlySpan<char> remaining, CassiePlaybackModifiers modifiers, out double time)
        => Announcer.CalculateDuration(remaining, modifiers, out time);
}