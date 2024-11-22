using GameCore;
using Generators;
using MapGeneration;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper for various Warhead components.
/// </summary>
// TODO: Add a way in the base game to reset the blastdoors to enable resetting of the warhead + otherstuff.
public static class Warhead
{
    [InitializeWrapper]
    internal static void Initialize()
    {
        SeedSynchronizer.OnGenerationFinished += OnMapGenerated;
        // TODO: Might want to handle this a different way as we are missing on destroy
    }

    /// <summary>
    /// The base <see cref="AlphaWarheadController"/>.
    /// Null if they have not been created yet, see <see cref="Exists"/>.
    /// </summary>
    public static AlphaWarheadController? BaseController { get; private set; }

    /// <summary>
    /// The base <see cref="AlphaWarheadNukesitePanel"/>.
    /// Null if they have not been created yet, see <see cref="Exists"/>.
    /// </summary>
    public static AlphaWarheadNukesitePanel? BaseNukesitePanel { get; private set; }

    /// <summary>
    /// The base <see cref="AlphaWarheadOutsitePanel"/>.
    /// Null if they have not been created yet, see <see cref="Exists"/>.
    /// </summary>
    public static AlphaWarheadOutsitePanel? BaseOutsidePanel { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the Warhead components have been created.
    /// </summary>
    /// <remarks>
    /// Warhead components are created after map generation.
    /// </remarks>
    public static bool Exists => BaseController != null;

    /// <summary>
    /// Gets or sets a value indicating whether the <see cref="BaseNukesitePanel"/> lever has been enabled.
    /// </summary>
    public static bool LeverStatus
    {
        get => BaseNukesitePanel?.enabled ?? false;
        set
        {
            if (BaseNukesitePanel != null)
                BaseNukesitePanel.Networkenabled = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the <see cref="AlphaWarheadOutsitePanel"/> has had a keycard unlock the button.
    /// </summary>
    public static bool IsAuthorized
    {
        get => BaseOutsidePanel?.keycardEntered ?? false;
        set
        {
            if (BaseOutsidePanel != null)
                BaseOutsidePanel.NetworkkeycardEntered = value;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the warhead status can be changed by players.
    /// </summary>
    public static bool IsLocked
    {
        get => BaseController?.IsLocked ?? false;
        set
        {
            if (BaseController != null)
                BaseController.IsLocked = value;
        }
    }

    /// <summary>
    /// Gets a value indicating whether the warhead has been detonated.
    /// </summary>
    public static bool IsDetonated => AlphaWarheadController.Detonated;

    /// <summary>
    /// Gets a value indicating whether detonation is in progress.
    /// </summary>
    public static bool IsDetonationInProgress => AlphaWarheadController.InProgress;

    /// <summary>
    /// Gets or sets a value for the detonation time measured in seconds.
    /// </summary>
    public static float DetonationTime
    {
        get => AlphaWarheadController.TimeUntilDetonation;
        set
        {
            if (BaseController != null)
                BaseController.ForceTime(value);
        }
    }

    /// <summary>
    /// Gets or sets a value for the reactivation cooldown time measured in seconds.
    /// </summary>
    public static double CooldownTime
    {
        get => Math.Max(0.0, BaseController?.CooldownEndTime - NetworkTime.time ?? 0.0);
        set
        {
            if (BaseController != null)
                BaseController.NetworkCooldownEndTime = NetworkTime.time + value;
        }
    }

    /// <summary>
    /// Gets or sets the value for which <see cref="DetonationScenario"/> to use.
    /// </summary>
    /// <remarks>
    /// Must be one of <see cref="StartScenarios"/>, <see cref="ResumeScenarios"/> or the default value for <see cref="DetonationScenario"/>.
    /// If <see cref="DetonationScenario"/> is the default value the Scenario is reset to the default used by the server config.
    /// </remarks>

    public static DetonationScenario Scenario
    {
        get
        {
            if (BaseController == null) return new DetonationScenario();

            return (IsStartScenario ? StartScenarios : ResumeScenarios)[BaseController.Info.ScenarioId];
        }
        set
        {
            if (BaseController == null) return;

            if (value.Equals(default))
            {
                // Resets warhead to its initial scenario.
                int duration = ConfigFile.ServerConfig.GetInt("warhead_tminus_start_duration", 90);
                var found = StartScenarios.Select((val, index) => new { val, index }).FirstOrDefault(x => x.val.TimeToDetonate == duration);
                int id = found.Equals(default) ? BaseController.DefaultScenarioId : found.index;
                BaseController.NetworkInfo = BaseController.Info with
                {
                    ResumeScenario = false,
                    ScenarioId = id,
                };
            }
            else
            {
                bool resumeScenrio = true;
                var found = StartScenarios.Select((val, index) => new { val, index }).FirstOrDefault(x => x.val.Equals(value));
                if (found.Equals(default))
                    found = ResumeScenarios.Select((val, index) => new { val, index }).First(x => x.val.Equals(value));
                else
                    resumeScenrio = false;

                BaseController.NetworkInfo = BaseController.Info with
                {
                    ResumeScenario = resumeScenrio,
                    ScenarioId = found.index,
                };
            }
        }
    }


    /// <summary>
    /// Gets a value indicating whether a <see cref="DetonationScenario"/> from <see cref="StartScenarios"> is being used.
    /// If false a <see cref="ResumeScenarios">Resume Scenario</see> is being used.
    /// </summary>
    public static bool IsStartScenario => !BaseController?.Info.ResumeScenario ?? true;

    /// <summary>
    /// Gets an array of all the start scenarios.
    /// </summary>
    /// <remarks>
    /// The scenarios used for the first time the warhead is activated.
    /// </remarks>
    public static IReadOnlyList<DetonationScenario> StartScenarios => BaseController?.StartScenarios.Select(x => new DetonationScenario(x.TimeToDetonate, x.AdditionalTime)).ToArray() ?? new DetonationScenario[] { };

    /// <summary>
    /// Gets an array for all the resume scenarios.
    /// </summary>
    /// <remarks>
    /// The scenarios used for anytime the warhead is resumed.
    /// </remarks>
    public static IReadOnlyList<DetonationScenario> ResumeScenarios => BaseController?.ResumeScenarios.Select(x => new DetonationScenario(x.TimeToDetonate, x.AdditionalTime)).ToArray() ?? new DetonationScenario[] { };

    /// <summary>
    /// Starts the detonation countdown.
    /// </summary>
    /// <param name="isAutomatic">Designates the detonation as automatic causing the warhead to become <see cref="IsLocked">Locked</see> during the countdown.</param>
    /// <param name="suppressSubtitles">Determines whether subtitles should be suppressed.</param>
    /// <param name="activator">The <see cref="Player"/> that activated the countdown if any.</param>
    public static void Start(bool isAutomatic = false, bool suppressSubtitles = false, Player? activator = null)
    {
        BaseController?.InstantPrepare();
        BaseController?.StartDetonation(isAutomatic, suppressSubtitles, activator?.ReferenceHub);
    }

    /// <summary>
    /// Stops the detonation countdown.
    /// </summary>
    /// <param name="activator">The <see cref="Player"/> that activated the cancellation if any.</param>
    public static void Stop(Player? activator = null)
    {
        BaseController?.CancelDetonation(activator?.ReferenceHub);
    }

    /// <summary>
    /// Instantly detonates the Warhead.
    /// </summary>
    public static void Detonate()
    {
        DetonationTime = 0.0f;
    }

    /// <summary>
    /// Plays the warhead detonation effect on all clients in the facility.
    /// </summary>
    public static void Shake()
    {
        BaseController?.RpcShake(false);
    }

    /// <summary>
    /// Handles the creation of the warhead components.
    /// </summary>
    private static void OnMapGenerated()
    {
        BaseController = UnityEngine.Object.FindObjectOfType<AlphaWarheadController>();
        BaseNukesitePanel = UnityEngine.Object.FindObjectOfType<AlphaWarheadNukesitePanel>();
        BaseOutsidePanel = UnityEngine.Object.FindObjectOfType<AlphaWarheadOutsitePanel>();
    }

    /// <summary>
    /// Handles the removal of warhead components.
    /// </summary>
    private static void OnMapDestroyed()
    {
        BaseController = null;
        BaseNukesitePanel = null;
        BaseOutsidePanel = null;
    }

    /// <summary>
    /// Readonly wrapper for <see cref="AlphaWarheadController.DetonationScenario"/>.
    /// </summary>
    public readonly struct DetonationScenario
    {
        /// <summary>
        /// Internal constructor to prevent external instantiation.
        /// </summary>
        /// <param name="timeToDetonate"></param>
        /// <param name="additionalTime"></param>
        internal DetonationScenario(int timeToDetonate, int additionalTime)
        {
            TimeToDetonate = timeToDetonate;
            AdditionalTime = additionalTime;
        }

        /// <summary>
        /// The countdown time announced.
        /// </summary>
        public readonly int TimeToDetonate;

        /// <summary>
        /// The additional time needed to play out the scenario's sequence before starting the countdown.
        /// </summary>
        public readonly int AdditionalTime;

        /// <summary>
        /// The actual time it takes for the warhead to detonate.
        /// </summary>
        public int TotalTime => TimeToDetonate + AdditionalTime;
    }
}
