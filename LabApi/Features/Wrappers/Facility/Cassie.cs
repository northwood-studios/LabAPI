using PlayerRoles;
using PlayerStatsSystem;
using Respawning;
using System;
using System.Linq;
using static NineTailedFoxAnnouncer;

namespace LabApi.Features.Wrappers
{
    /// <summary>
    /// The wrapper for in game Cassie announcer.
    /// </summary>
    public static class Cassie
    {
        /// <summary>
        /// Gets whether Cassie is currently speaking.
        /// </summary>
        public static bool IsSpeaking => singleton.queue.Count != 0;

        /// <summary>
        /// Gets all available voice lines for Cassie.
        /// </summary>
        public static VoiceLine[] AllLines => singleton.voiceLines;

        /// <summary>
        /// Gets all collection names in which voicelines are in.
        /// </summary>
        public static string[] CollectionNames => singleton.voiceLines.Select(n => n.collection).Distinct().ToArray();

        /// <summary>
        /// Checks whether a specified word is valid for cassie.
        /// <note>String comparison is case-insensitive.</note>
        /// </summary>
        /// <param name="word">The word to check.</param>
        /// <returns>Whether the word is valid.</returns>
        public static bool IsValid(string word) => singleton.voiceLines.Any(line => line.apiName.Equals(word, StringComparison.InvariantCultureIgnoreCase));

        /// <summary>
        /// Calculates duration of specific message.
        /// </summary>
        /// <param name="tts">The message.</param>
        /// <param name="rawNumber">Raw numbers.</param>
        /// <param name="speed">The speed of message.</param>
        /// <returns>Duration of the specific message in seconds.</returns>
        public static float CalculateDuration(string message, bool rawNumber = false) => singleton.CalculateDuration(message, rawNumber);

        /// <summary>
        ///	Plays a custom announcement.
        /// </summary>
        ///	<param name="message">The sentence Cassie is supposed to say.</param>
        ///	<param name="isHeld">Sets a minimal 3-second moment of silence before the announcement. For most cases you wanna keep it true.</param>
        ///	<param name="isNoisy">Whether the background noises play.</param>
        ///	<param name="isSubtitles">Show subtitles.</param>
        ///	<param name="customSubtitles">Custom subtitles to appear instead of the actual message.</param>
        //TODO: Perhaps add the custom subtitles functionality to RA & base game commands?
        public static void Message(string message, bool isHeld = false, bool isNoisy = true, bool isSubtitles = false, string customSubtitles = "") => RespawnEffectsController.PlayCassieAnnouncement(message, isHeld, isNoisy, isSubtitles, customSubtitles);

        /// <summary>
        /// Plays the custom announcement with chance of 0f to 1f of adding a glitch or jam before each word. Values closer to 1f are higher chances.
        /// </summary>
        /// <param name="message">The sentence Cassie is supposed to say.</param>
        /// <param name="glitchChance">The chance for glitch sound to be added before each word. Range from 0f to 1f.</param>
        /// <param name="jamChance">The chance for jam sound to be added before each word. Range from 0f to 1f.</param>
        public static void GlitchyMessage(string message, float glitchChance, float jamChance) => singleton.ServerOnlyAddGlitchyPhrase(message, glitchChance, jamChance);

        /// <summary>
        /// Plays the termination announcement of a SCP player. If the specified player does not have an SCP role then nothing is played.
        /// </summary>
        /// <param name="player">The player who is being terminated as an SCP.</param>
        /// <param name="info">Damage handler causing the death of the player.</param>
        public static void ScpTermination(Player player, DamageHandlerBase info) => AnnounceScpTermination(player.ReferenceHub, info);

        /// <summary>
        /// Clears the Cassie announcements queue.
        /// </summary>
        public static void Clear() => singleton.ClearQueue();

        /// <summary>
        /// Converts player's team into Cassie-able word. Unit names are converted into NATO_X words, followed by a number. For example "Alpha-5" is converted to "NATO_A 5".
        /// </summary>
        /// <param name="team">Target team.</param>
        /// <param name="unitName">MTF Unit name (for team <see cref="Team.FoundationForces"/>).</param>
        /// <returns>Converted name.</returns>
        public static string ConvertTeam(Team team, string unitName) => NineTailedFoxAnnouncer.ConvertTeam(team, unitName);

        /// <summary>
        /// Converts number into string.
        /// </summary>
        /// <param name="num">The number.</param>
        /// <returns>Number converted to string.</returns>
        public static string ConvertNumber(int num) => NineTailedFoxAnnouncer.ConvertNumber(num);

        /// <summary>
        /// Converts player's <see cref="RoleTypeId"/> into an SCP <b>number</b> identifier.
        /// </summary>
        /// <param name="role">The target <see cref="RoleTypeId"/>.</param>
        /// <param name="withoutSpace">The SCP number without spaces between. Used by Cassie.</param>
        /// <param name="withSpace">The SCP number with spaces between. Used by Subtitles.</param>
        public static void ConvertSCP(RoleTypeId role, out string withoutSpace, out string withSpace) => NineTailedFoxAnnouncer.ConvertSCP(role, out withoutSpace, out withSpace);

        /// <summary>
        /// Converts player's role name into an SCP <b>number</b> identifier.
        /// </summary>
        /// <param name="roleName">The targets role name</param>
        /// <param name="withoutSpace">The SCP number without spaces between. Used by Cassie.</param>
        /// <param name="withSpace">The SCP number with spaces between. Used by Subtitles.</param>
        public static void ConvertSCP(string roleName, out string withoutSpace, out string withSpace) => NineTailedFoxAnnouncer.ConvertSCP(roleName, out withoutSpace, out withSpace);
    }
}