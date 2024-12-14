using GameCore;
using RoundRestarting;
using System;
using static ServerStatic;

namespace LabApi.Features.Wrappers
{
    /// <summary>
    /// A static wrapper for any round related features.
    /// </summary>
    public static class Round
    {
        /// <summary>
        /// Gets or sets whether the round has started or not.
        /// </summary>
        public static bool IsRoundStarted
        {
            get => RoundSummary.RoundInProgress();
            set
            {
                if (value) Start();
            }
        }

        /// <summary>
        /// Gets a value indicating whether the round is ended or not.
        /// </summary>
        public static bool IsRoundEnded
        {
            get => !IsRoundStarted && Duration.Seconds > 1;
            set
            {
                if (value) End(true);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the round is locked or not.
        /// </summary>
        public static bool IsLocked
        {
            get => RoundSummary.RoundLock;
            set => RoundSummary.RoundLock = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the lobby is locked or not.
        /// </summary>
        public static bool IsLobbyLocked
        {
            get => RoundStart.LobbyLock;
            set => RoundStart.LobbyLock = value;
        }

        /// <summary>
        /// Gets or sets the current chaos target count.s
        /// </summary>
        public static int ExtraTargets
        {
            get => RoundSummary.singleton.ExtraTargets;
            set => RoundSummary.singleton.ExtraTargets = value;
        }

        /// <summary>
        /// Gets the amount of total deaths during the round.
        /// </summary>
        public static int TotalDeaths => RoundSummary.Kills;

        /// <summary>
        /// Gets the amount of total kills by SCPs.
        /// </summary>
        public static int KilledBySCPs => RoundSummary.KilledBySCPs;

        /// <summary>
        /// Gets the amount of escaped Class D.
        /// </summary>
        public static int EscapedClassD => RoundSummary.EscapedClassD;

        /// <summary>
        /// Gets rhe amount of escaped Scientists.
        /// </summary>
        public static int EscapedScientists => RoundSummary.EscapedScientists;

        /// <summary>
        /// Gets the amount of SCPs alive at the end of round.
        /// </summary>
        public static int SurvivingSCPs => RoundSummary.SurvivingSCPs;

        /// <summary>
        /// Gets the amount of people that turned into zombies.
        /// </summary>
        public static int ChangedIntoZombies => RoundSummary.ChangedIntoZombies;

        /// <summary>
        /// Gets the duration of the current round.
        /// </summary>
        public static TimeSpan Duration => RoundStart.RoundLength;

        /// <summary>
        /// Gets or sets whether the round should end if is active and there is only one player on the server.
        /// </summary>
        public static bool KeepRoundOnOne
        {
            get => RoundSummary.singleton.KeepRoundOnOne;
            set => RoundSummary.singleton.KeepRoundOnOne = value;
        }

        /// <summary>
        /// Start the round.
        /// </summary>
        public static void Start() => CharacterClassManager.ForceRoundStart();

        /// <summary>
        /// Restarts the round.
        /// </summary>
        /// <param name="fastRestart">Whether or not it fast restart is enabled.</param>
        /// <param name="overrideRestartAction">Overrides stop next round action.</param>
        /// <param name="restartAction">The restart action.</param>
        public static void Restart(bool fastRestart = false, bool overrideRestartAction = false, NextRoundAction restartAction = NextRoundAction.DoNothing)
        {
            if (overrideRestartAction)
                StopNextRound = restartAction;

            bool prevValue = CustomNetworkManager.EnableFastRestart;
            CustomNetworkManager.EnableFastRestart = fastRestart;
            RoundRestart.InitiateRoundRestart();
            CustomNetworkManager.EnableFastRestart = prevValue;
        }

        /// <summary>
        /// Restarts the round silently.
        /// </summary>
        public static void RestartSilently() => Restart(true, true, NextRoundAction.DoNothing);

        /// <summary>
        /// Attempts to end the current round.<br></br>
        /// </summary>
        /// <param name="force">Whether the round should be forced to end.</param>
        /// <returns>If the round was ended.</returns>
        public static bool End(bool force = false)
        {
            if (force)
            {
                RoundSummary.singleton.ForceEnd();
                return true;
            }

            if (KeepRoundOnOne && Player.Count < 2) return false;

            if (!IsRoundStarted || IsLocked)
                return false;

            RoundSummary.singleton.ForceEnd();
            return true;
        }
    }
}
