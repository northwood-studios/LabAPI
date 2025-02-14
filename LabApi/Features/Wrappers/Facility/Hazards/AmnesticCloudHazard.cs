using PlayerRoles.PlayableScps.Scp939;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using static PlayerRoles.PlayableScps.Scp939.Scp939AmnesticCloudInstance;

namespace LabApi.Features.Wrappers;

public class AmnesticCloudHazard : DecayableHazard
{
    /// <summary>
    /// Contains all the cached items, accessible through their <see cref="Base"/>.
    /// </summary>
    public new static Dictionary<Scp939AmnesticCloudInstance, AmnesticCloudHazard> Dictionary { get; } = [];

    /// <summary>
    /// Gets all currently active tantrum hazards.
    /// </summary>
    public new IReadOnlyCollection<AmnesticCloudHazard> List => Dictionary.Values;

    /// <summary>
    /// Prefab used to spawn the hazard.
    /// </summary>
    protected static new Scp939AmnesticCloudInstance? BasePrefab;

    /// <summary>
    /// Gets or sets the world position of the hazard as it is synchronized with the client.
    /// Note that this value is slightly inaccurate and is purely for visual effects.<br/>
    /// For actual world position used to calculate whether the player is inside of this hazard use <see cref="Hazard.SourcePosition"/>.
    /// </summary>
    public Vector3 SyncedPosition
    {
        get => Base.SyncedPosition.Position;
        set => Base.SyncedPosition = new RelativePositioning.RelativePosition(value);
    }

    /// <summary>
    /// Gets or sets the visual size of the amnestic cloud. Only visible to SCP-939. For actual effect size use <see cref="Hazard.MaxDistance"/>.
    /// </summary>
    public byte VisualSize
    {
        get => Base.HoldDuration;
        set => Base.HoldDuration = value;
    }

    /// <summary>
    /// Pause duration that the <see cref="Pause"/> uses.
    /// </summary>
    public float PauseDuration
    {
        get => Base.PauseDuration;
        set => Base.PauseDuration = value;
    }

    /// <summary>
    /// Duration of the amnesia that is applied when the player is inside this hazard.
    /// </summary>
    public float AmnesiaDuration
    {
        get => Base.AmnesiaDuration;
        set => Base.AmnesiaDuration = value;
    }

    /// <summary>
    /// Gets or sets the owner of this amnestic cloud.
    /// </summary>
    public Player? Owner
    {
        get => Player.Get(Base.Owner);
        set
        {
            if (value == null)
            {
                Base.Network_syncOwner = 0;
                return;
            }

            Base.Owner = value.ReferenceHub;
        }
    }

    /// <summary>
    /// Gets the state of this amnestic cloud.
    /// </summary>
    public CloudState State => Base.State;

    /// <summary>
    /// The base object.
    /// </summary>
    public new Scp939AmnesticCloudInstance Base { get; }

    /// <summary>
    /// Internal constructor preventing external instantiation.
    /// </summary>
    /// <param name="hazard">The base amnestic cloud hazard.</param>
    internal AmnesticCloudHazard(Scp939AmnesticCloudInstance hazard)
        : base(hazard)
    {
        Base = hazard;
        Dictionary.Add(hazard, this);
    }

    /// <summary>
    /// Spawns a <see cref="AmnesticCloudHazard"/> at specified position with specified rotation, scale duration and size.
    /// <para> Do note that changing scale doesn't change the effect size. Use the <see cref="Hazard.MaxDistance"/> and <see cref="Hazard.MaxHeightDistance"/> to match the visual size.</para>
    /// </summary>
    /// <param name="position">The target position to spawn this hazard at.</param>
    /// <param name="rotation">The target rotation to spawn this hazard with.</param>
    /// <param name="scale">The target scale to spawn with.</param>
    /// <param name="duration">The duration in seconds for which this cloud will be alive for.</param>
    /// <param name="size">The size of the cloud.</param>
    /// <param name="owner">The owner of the cloud.</param>
    /// <returns>A new hazard.</returns>
    public static AmnesticCloudHazard Spawn(Vector3 position, Quaternion rotation, Vector3 scale, float duration = 90f, byte size = 255, Player? owner = null)
    {
        if (BasePrefab == null)
            BasePrefab = GetPrefab<Scp939AmnesticCloudInstance>();

        AmnesticCloudHazard hazard = (AmnesticCloudHazard)Hazard.Spawn(BasePrefab, position, rotation, scale);
        hazard.Base.State = CloudState.Created;
        hazard.LiveDuration = duration;
        hazard.VisualSize = size;
        hazard.SyncedPosition = position;

        Vector2 minMax = hazard.Base.MinMaxTime;
        hazard.MaxDistance = Mathf.Lerp(minMax.x, minMax.y, size / byte.MaxValue);

        if (owner != null)
            hazard.Owner = owner;
        return hazard;
    }

    /// <summary>
    /// Temporary pauses all amnesia effects based on <see cref="PauseDuration"/> or custom time.
    /// </summary>
    /// <param name="customDuration">Custom duration of the pause. Values less than 0 will use the <see cref="PauseDuration"/></param>
    public void Pause(float customDuration = -1f)
    {
        if (customDuration > 0f)
        {
            float prev = PauseDuration;
            PauseDuration = customDuration;
            Base.PauseAll();
            PauseDuration = prev;

            return;
        }

        Base.PauseAll();
    }

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }

    /// <summary>
    /// Gets the hazard wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="Scp939AmnesticCloudInstance"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="hazard">The <see cref="Base"/> of the hazard.</param>
    /// <returns>The requested hazard or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(hazard))]
    public static AmnesticCloudHazard? Get(Scp939AmnesticCloudInstance? hazard)
    {
        if (hazard == null)
            return null;

        return Dictionary.TryGetValue(hazard, out AmnesticCloudHazard decHazard) ? decHazard : (AmnesticCloudHazard)CreateItemWrapper(hazard);
    }
}