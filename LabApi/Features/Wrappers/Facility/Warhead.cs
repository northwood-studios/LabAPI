using GameCore;
using Generators;
using LabApi.Events.Handlers;
using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper for various Warhead components.
/// </summary>
public static class Warhead
{
    /// <summary>
    /// The base <see cref="AlphaWarheadController"/>.
    /// Null if they have not been created yet, see <see cref="Exists"/>.
    /// </summary>
    public static AlphaWarheadController? BaseController => AlphaWarheadController.Singleton;

    /// <summary>
    /// The base <see cref="AlphaWarheadNukesitePanel"/>.
    /// Null if they have not been created yet, see <see cref="Exists"/>.
    /// </summary>
    public static AlphaWarheadNukesitePanel? BaseNukesitePanel => AlphaWarheadNukesitePanel.Singleton;

    /// <summary>
    /// The base <see cref="AlphaWarheadOutsitePanel"/>.
    /// Null if they have not been created yet, see <see cref="Exists"/>.
    /// </summary>
    public static AlphaWarheadOutsitePanel? BaseOutsidePanel { get; private set; }

    /// <summary>
    /// A reference to all <see cref="BlastDoor"/> instances currently in the game.
    /// </summary>
    public static HashSet<BlastDoor> BlastDoors => BlastDoor.Instances;

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
            {
                BaseNukesitePanel.Networkenabled = value;
            }
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the surface panel has had a keycard unlock the button.
    /// </summary>
    public static bool IsAuthorized
    {
        get => AlphaWarheadActivationPanel.IsUnlocked;
        set => AlphaWarheadActivationPanel.IsUnlocked = value;
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
            {
                BaseController.IsLocked = value;
            }
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
        set => BaseController?.ForceTime(value);
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
            {
                BaseController.NetworkCooldownEndTime = NetworkTime.time + value;
            }
        }
    }

    /// <summary>
    /// Forces DMS sequence to count down even if conditions are not met.
    /// </summary>
    public static bool ForceCountdownToggle
    {
        get => DeadmanSwitch.ForceCountdownToggle;
        set => DeadmanSwitch.ForceCountdownToggle = value;
    }

    /// <summary>
    /// Indicates how much time is left for the DMS to activate.
    /// Value is capped by <see cref="DeadManSwitchMaxTime"/>.
    /// </summary>
    public static float DeadManSwitchRemaining
    {
        get => DeadmanSwitch.CountdownTimeLeft;
        set => DeadmanSwitch.CountdownTimeLeft = value;
    }

    /// <summary>
    /// Indicates the amount of time it takes for the DMS to activate.
    /// </summary>
    public static float DeadManSwitchMaxTime
    {
        get => DeadmanSwitch.CountdownMaxTime;
        set => DeadmanSwitch.CountdownMaxTime = value;
    }

    /// <summary>
    /// Gets or sets the value for which <see cref="DetonationScenario"/> to use.
    /// </summary>
    /// <remarks>
    /// Must be one of <see cref="StartScenarios"/>, <see cref="ResumeScenarios"/>, <see cref="DeadmanSwitchScenario"/> or the default value for <see cref="DetonationScenario"/>.
    /// If <see cref="DetonationScenario"/> is the default value the Scenario is reset to the default used by the server config.
    /// </remarks>
    public static DetonationScenario Scenario
    {
        get
        {
            if (BaseController == null)
            {
                return default;
            }

            return ScenarioType switch
            {
                WarheadScenarioType.Start => StartScenarios[BaseController.Info.ScenarioId],
                WarheadScenarioType.Resume => ResumeScenarios[BaseController.Info.ScenarioId],
                WarheadScenarioType.DeadmanSwitch => DeadmanSwitchScenario,
                _ => default,
            };
        }

        set
        {
            if (BaseController == null)
            {
                return;
            }

            if (value.Equals(default))
            {
                // Resets warhead to its initial scenario.
                int duration = ConfigFile.ServerConfig.GetInt("warhead_tminus_start_duration", 90);
                var found = StartScenarios.Select((val, index) => new { val, index }).FirstOrDefault(x => x.val.TimeToDetonate == duration);
                byte id = found.Equals(default) ? BaseController.DefaultScenarioId : (byte)found.index;
                BaseController.NetworkInfo = BaseController.Info with
                {
                    ScenarioType = WarheadScenarioType.Start,
                    ScenarioId = id,
                };
            }
            else
            {
                BaseController.NetworkInfo = BaseController.Info with
                {
                    ScenarioType = value.Type,
                    ScenarioId = value.Id,
                };
            }
        }
    }

    /// <summary>
    /// Gets the warhead scenario type for the current <see cref="Scenario"/>.
    /// </summary>
    public static WarheadScenarioType ScenarioType => BaseController?.Info.ScenarioType ?? WarheadScenarioType.Start;

    /// <summary>
    /// Gets an array of all the start scenarios.
    /// </summary>
    /// <remarks>
    /// The scenarios used for the first time the warhead is activated.
    /// </remarks>
    public static IReadOnlyList<DetonationScenario> StartScenarios { get; private set; } = [];

    /// <summary>
    /// Gets an array for all the resume scenarios.
    /// </summary>
    /// <remarks>
    /// The scenarios used for anytime the warhead is resumed.
    /// </remarks>
    public static IReadOnlyList<DetonationScenario> ResumeScenarios { get; private set; } = [];

    /// <summary>
    /// Gets the deadman switch scenario.
    /// </summary>
    public static DetonationScenario DeadmanSwitchScenario { get; private set; } = default;

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
    /// Opens all blast doors.
    /// </summary>
    public static void OpenBlastDoors()
    {
        foreach (BlastDoor door in BlastDoor.Instances)
        {
            door.ServerSetTargetState(true);
        }
    }

    /// <summary>
    /// Closes all blast doors.
    /// </summary>
    public static void CloseBlastDoors()
    {
        foreach (BlastDoor door in BlastDoor.Instances)
        {
            door.ServerSetTargetState(false);
        }
    }

    /// <summary>
    /// Plays the warhead detonation effect on all clients in the facility.
    /// </summary>
    public static void Shake() => BaseController?.RpcShake(false);

    /// <summary>
    /// Initializes the warhead class.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        ServerEvents.WaitingForPlayers += OnWaitingForPlayers;

        // TODO: Might want to handle this a different way as we are missing on destroy
    }

    /// <summary>
    /// Handles the creation of the warhead components.
    /// </summary>
    private static void OnWaitingForPlayers()
    {
        BaseOutsidePanel = UnityEngine.Object.FindObjectOfType<AlphaWarheadOutsitePanel>();

        StartScenarios = BaseController!.StartScenarios.Select((x, i) => new DetonationScenario(x, (byte)i, WarheadScenarioType.Start)).ToArray();
        ResumeScenarios = BaseController.ResumeScenarios.Select((x, i) => new DetonationScenario(x, (byte)i, WarheadScenarioType.Resume)).ToArray();
        DeadmanSwitchScenario = new DetonationScenario(BaseController.DeadmanSwitchScenario, 0, WarheadScenarioType.DeadmanSwitch);
    }

    /// <summary>
    /// Handles the removal of warhead components.
    /// </summary>
    private static void OnMapDestroyed()
    {
        BaseOutsidePanel = null;
    }

    /// <summary>
    /// Readonly wrapper for <see cref="AlphaWarheadController.DetonationScenario"/>.
    /// </summary>
    public readonly struct DetonationScenario
    {
        /// <summary>
        /// The countdown time announced.
        /// </summary>
        public readonly int TimeToDetonate;

        /// <summary>
        /// The additional time needed to play out the scenario's sequence before starting the countdown.
        /// </summary>
        public readonly int AdditionalTime;

        /// <summary>
        /// The <see cref="WarheadScenarioType"/> this scenario is for.
        /// </summary>
        public readonly WarheadScenarioType Type;

        /// <summary>
        /// The index of the scenario for its associated <see cref="Type"/>.
        /// </summary>
        public readonly byte Id;

        /// <summary>
        /// Internal constructor to prevent external instantiation.
        /// </summary>
        /// <param name="detonationScenario">The <see cref="DetonationScenario"/>.</param>
        /// <param name="id">The <see cref="byte"/> id of the scenario.</param>
        /// <param name="type">The <see cref="WarheadScenarioType"/>.</param>
        internal DetonationScenario(AlphaWarheadController.DetonationScenario detonationScenario, byte id, WarheadScenarioType type)
        {
            TimeToDetonate = detonationScenario.TimeToDetonate;
            // TODO: Remove the cast on 2.0.0 and change additional time to float
            AdditionalTime = (int)detonationScenario.AdditionalTime;
            Type = type;
            Id = id;
        }

        /// <summary>
        /// The actual time it takes for the warhead to detonate.
        /// </summary>
        public int TotalTime => TimeToDetonate + AdditionalTime;
    }
}
