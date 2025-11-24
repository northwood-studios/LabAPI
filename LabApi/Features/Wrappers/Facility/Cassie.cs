using Cassie;
using Cassie.Interpreters;
using PlayerRoles;
using PlayerStatsSystem;
using Respawning.NamingRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper for in game CASSIE announcer.
/// </summary>
public static class Cassie
{
    /// <summary>
    /// Gets whether CASSIE is currently speaking.
    /// </summary>
    public static bool IsSpeaking => CassieAnnouncementDispatcher.CurrentAnnouncement != null;

    /// <summary>
    /// Gets all available voice lines for CASSIE.
    /// </summary>
    public static CassieLine[] AllLines => !CassieTtsAnnouncer.TryGetDatabase(out CassieLineDatabase db) ? [] : db.AllLines;

    /// <summary>
    /// Gets the line database for CASSIE.
    /// </summary>
    public static CassieLineDatabase? LineDatabase => !CassieTtsAnnouncer.TryGetDatabase(out CassieLineDatabase db) ? null : db;

    /// <summary>
    /// Gets all collection names in which voice lines are in.
    /// </summary>
    public static string[] CollectionNames => AllLines.Select(n => n.ApiName).Distinct().ToArray();

    /// <summary>
    /// Checks whether a specified word is valid for CASSIE.
    /// <note>String comparison is case-insensitive.</note>
    /// </summary>
    /// <param name="word">The word to check.</param>
    /// <returns>Whether the word is valid.</returns>
    public static bool IsValid(string word) => CollectionNames.Any(line => line.Equals(word, StringComparison.InvariantCultureIgnoreCase));

    /// <summary>
    /// Calculates duration of specific message.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="rawNumber">Raw numbers.</param>
    /// <param name="speed">The speed of the cassie talking.</param>
    /// <returns>Duration of the specific message in seconds.</returns>
    [Obsolete("Use CalculateDuration(string message, CassiePlaybackModifiers playbackModifiers) instead.", true)]
    public static float CalculateDuration(string message, bool rawNumber = false, float speed = 1f)
    {
        CassiePlaybackModifiers playbackModifiers = new()
        {
            Pitch = speed,
        };
        return (float)CalculateDuration(message, playbackModifiers);
    }

    /// <summary>
    /// Calculates duration of specific message.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="playbackModifiers">The playback modifier.</param>
    /// <returns>Duration of the specific message in seconds.</returns>
    public static double CalculateDuration(string message, CassiePlaybackModifiers playbackModifiers)
    {
        CalculateDuration(message, playbackModifiers, out var time);
        return time;
    }

    /// <summary>
    /// Plays a custom announcement.
    /// Queues a custom announcement.
    /// </summary>
    /// <param name="message">The sentence CASSIE is supposed to say.</param>
    /// <param name="isHeld">Sets a minimal 3-second moment of silence before the announcement. For most cases you wanna keep it true.</param>
    /// <param name="isNoisy">Whether the background noises play.</param>
    /// <param name="isSubtitles">Show subtitles.</param>
    /// <param name="customSubtitles">Custom subtitles to appear instead of the actual message.</param>
    [Obsolete("Use Message(string message, string customSubtitles = \"\", bool playBackground = true, float priority = 0f, float glitchScale = 1f) instead.", true)]
    public static void Message(string message, bool isHeld = false, bool isNoisy = true, bool isSubtitles = true, string customSubtitles = "")
        => Message(message, customSubtitles);

    /// <summary>
    /// Queues a custom announcement.
    /// </summary>
    /// <param name="message">The sentence CASSIE is supposed to say.</param>
    /// <param name="customSubtitles">Custom subtitles to play.</param>
    /// <param name="playBackground">Should play the background track (bells, noise).</param>
    /// <param name="priority">The priority of this message.</param>
    /// <param name="glitchScale">Intensity of glitches and stutters added before sending to clients.</param>
    public static void Message(string message, string customSubtitles = "", bool playBackground = true, float priority = 0f, float glitchScale = 1f)
    {
        CassieTtsPayload payload = new(message, customSubtitles, playBackground);
        Message(payload, priority, glitchScale);
    }

    /// <summary>
    /// Queues an cassie payload announcement.
    /// </summary>
    /// <param name="payload">The payload to sent.</param>
    /// <param name="priority">The priority of this message.</param>
    /// <param name="glitchScale">Intensity of glitches and stutters added before sending to clients.</param>
    public static void Message(CassieTtsPayload payload, float priority = 0f, float glitchScale = 1f)
    {
        CassieAnnouncement announcement = new(payload, priority, glitchScale);
        CassieAnnouncementDispatcher.AddToQueue(announcement);
    }

    /// <summary>
    /// Plays the custom announcement with chance of 0f to 1f of adding a glitch or jam before each word. Values closer to 1f are higher chances.
    /// </summary>
    /// <param name="message">The sentence CASSIE is supposed to say.</param>
    /// <param name="glitchChance">The chance for glitch sound to be added before each word. Range from 0f to 1f.</param>
    /// <param name="jamChance">The chance for jam sound to be added before each word. Range from 0f to 1f.</param>
    public static void GlitchyMessage(string message, float glitchChance, float jamChance) => CassieGlitchifier.Glitchify(message, glitchChance, jamChance);

    /// <summary>
    /// Plays the termination announcement of a SCP player. If the specified player does not have an SCP role then nothing is played.
    /// </summary>
    /// <param name="player">The player who is being terminated as an SCP.</param>
    /// <param name="info">Damage handler causing the death of the player.</param>
    public static void ScpTermination(Player player, DamageHandlerBase info) => CassieScpTerminationAnnouncement.AnnounceScpTermination(player.ReferenceHub, info);

    /// <summary>
    /// Clears the CASSIE announcements queue.
    /// </summary>
    public static void Clear() => CassieAnnouncementDispatcher.ClearAll();

    /// <summary>
    /// Converts player's team into CASSIE-able word. Unit names are converted into NATO_X words, followed by a number. For example "Alpha-5" is converted to "NATO_A 5".
    /// </summary>
    /// <param name="team">Target team.</param>
    /// <param name="unitName">MTF Unit name (for team <see cref="Team.FoundationForces"/>).</param>
    /// <returns>Converted name.</returns>
    public static string ConvertTeam(Team team, string unitName)
    {
        string text = "CONTAINMENTUNIT UNKNOWN";
        switch (team)
        {
            case Team.FoundationForces:
                {
                    if (!NamingRulesManager.TryGetNamingRule(team, out UnitNamingRule unitNamingRule))
                    {
                        return text;
                    }

                    string text2 = unitNamingRule.TranslateToCassie(unitName);
                    return "CONTAINMENTUNIT " + text2;
                }

            case Team.ChaosInsurgency:
                return "BY CHAOSINSURGENCY";
            case Team.Scientists:
                return "BY SCIENCE PERSONNEL";
            case Team.ClassD:
                return "BY CLASSD PERSONNEL";
            default:
                return text;
        }
    }

    /// <summary>
    /// Converts number into string.
    /// </summary>
    /// <param name="num">The number.</param>
    /// <returns>Number converted to string.</returns>
    public static string ConvertNumber(int num)
    {
        if (LineDatabase == null)
        {
            return string.Empty;
        }

        NumberInterpreter? numberInterpreter = (NumberInterpreter?)CassieTtsAnnouncer.Interpreters.FirstOrDefault(x => x is NumberInterpreter);
        if (numberInterpreter == null)
        {
            return string.Empty;
        }

        string word = num.ToString();

        CassiePlaybackModifiers cassiePlaybackModifiers = default;
        StringBuilder sb = new();
        numberInterpreter.GetResults(LineDatabase, ref cassiePlaybackModifiers, word, sb, out bool halt);

        return sb.ToString();
    }

    /// <summary>
    /// Converts player's <see cref="RoleTypeId"/> into an SCP <b>number</b> identifier.
    /// </summary>
    /// <param name="role">The target <see cref="RoleTypeId"/>.</param>
    /// <param name="withoutSpace">The SCP number without spaces between. Used by CASSIE.</param>
    /// <param name="withSpace">The SCP number with spaces between. Used by Subtitles.</param>
    public static void ConvertScp(RoleTypeId role, out string withoutSpace, out string withSpace) => CassieScpTerminationAnnouncement.ConvertSCP(role, out withoutSpace, out withSpace);

    /// <summary>
    /// Converts player's role name into an SCP <b>number</b> identifier.
    /// </summary>
    /// <param name="roleName">The targets role name.</param>
    /// <param name="withoutSpace">The SCP number without spaces between. Used by CASSIE.</param>
    /// <param name="withSpace">The SCP number with spaces between. Used by Subtitles.</param>
    public static void ConvertScp(string roleName, out string withoutSpace, out string withSpace) => CassieScpTerminationAnnouncement.ConvertSCP(roleName, out withoutSpace, out withSpace);

    /// <summary>
    /// Calculates duration of message.
    /// </summary>
    /// <param name="remaining">The remaining message.</param>
    /// <param name="modifiers">The playback modifier.</param>
    /// <param name="time">The duration of the message.</param>
    public static void CalculateDuration(ReadOnlySpan<char> remaining, CassiePlaybackModifiers modifiers, out double time)
    {
        time = 0;

        if (LineDatabase == null)
        {
            return;
        }

        int index = remaining.IndexOf(' ');

        StringBuilder sb = new();

        ReadOnlySpan<char> word = index < 0 ? remaining : remaining[..index];

        foreach (CassieInterpreter inter in CassieTtsAnnouncer.Interpreters)
        {
            List<CassieInterpreter.Result> results = inter.GetResults(LineDatabase, ref modifiers, word, sb, out bool jobDone);

            time += results.Sum(result => result.Modifiers.GetTimeUntilNextWord(result.Line));

            if (jobDone)
            {
                // The interpreter claims there's no need to process the word by other interpreters.
                break;
            }
        }

        if (index < 0)
        {
            return;
        }

        remaining = remaining[(index + 1)..];
        CalculateDuration(remaining, modifiers, out double timeOut);
        time += timeOut;
    }
}