using CentralAuth;
using GameCore;
using PlayerRoles;
using RoundRestarting;
using System;
using Utils.NonAllocLINQ;
using static ServerStatic;

namespace LabApi.Features.Wrappers;

/// <summary>
/// A static wrapper for any round related features.
/// </summary>
public static class Round
{
    /// <summary>
    /// Gets or sets whether the round has started or not.
    /// </summary>
    public static bool IsRoundStarted => RoundSummary.RoundInProgress();

    /// <summary>
    /// Gets a value indicating whether the round is ended or not.
    /// </summary>
    public static bool IsRoundEnded => !IsRoundStarted && Duration.Seconds > 1;

    /// <summary>
    /// Gets whether the round can end if there is only 1 team alive remaining.<br/>
    /// <remarks>IMPORTANT: This does NOT check win conditions! Only whether the round is locked and if there is a required amount of players.</remarks>
    /// </summary>
    public static bool CanRoundEnd
    {
        get
        {
            if (IsLocked || KeepRoundOnOne && ReferenceHub.AllHubs.Count(x => x.authManager.InstanceMode != ClientInstanceMode.DedicatedServer) < 2 || !IsRoundStarted)
                return false;

            return IsRoundStarted && !IsLocked;
        }
    }

    /// <summary>
    /// Gets or sets whether the round should end if is active and there is only one player on the server.
    /// </summary>
    public static bool KeepRoundOnOne
    {
        get => RoundSummary.singleton.KeepRoundOnOne;
        set => RoundSummary.singleton.KeepRoundOnOne = value;
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
    /// Gets or sets the current extra targets count for SCPs.
    /// </summary>
    public static int ExtraTargets
    {
        get => RoundSummary.singleton.Network_extraTargets;
        set => RoundSummary.singleton.Network_extraTargets = value;
    }

    /// <summary>
    /// Gets the current amount of targets for SCPs. Use <see cref="ExtraTargets"/> to add/remove any extra.
    /// </summary>
    public static int ScpTargetsAmount => ReferenceHub.AllHubs.Count(hub => hub.GetFaction() is Faction.FoundationStaff or Faction.FoundationEnemy) + ExtraTargets;

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

        if (!CanRoundEnd)
            return false;

        RoundSummary.singleton.ForceEnd();
        return true;
    }
}