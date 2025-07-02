using Hazards;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// A class representing the <see cref="TantrumEnvironmentalHazard"/>.
/// </summary>
public class TantrumHazard : DecayableHazard
{
    /// <summary>
    /// Contains all the cached items, accessible through their <see cref="Base"/>.
    /// </summary>
    public new static Dictionary<TantrumEnvironmentalHazard, TantrumHazard> Dictionary { get; } = [];

    /// <summary>
    /// Gets all currently active tantrum hazards.
    /// </summary>
    public new IReadOnlyCollection<TantrumHazard> List => Dictionary.Values;

    /// <summary>
    /// Prefab used to spawn the hazard.
    /// </summary>
    protected static new TantrumEnvironmentalHazard? BasePrefab;

    /// <summary>
    /// Gets or sets the world position of the hazard as it is synchronized with the client.
    /// Note that this value is slightly inaccurate and is purely for visual effects.<br/>
    /// For actual world position used to calculate whether the player is inside of this hazard use <see cref="Hazard.SourcePosition"/>.
    /// </summary>
    public Vector3 SyncedPosition
    {
        get => Base.SynchronizedPosition.Position;
        set => Base.SynchronizedPosition = new RelativePositioning.RelativePosition(value);
    }

    /// <summary>
    /// Gets or sets whether a slight sizzle sound effect will be played when this object is destroyed. 
    /// It is played by default if the tantrum gets destroyed by an explosion or by <see cref="Tesla"/>.<br/>
    /// Note that this state may change right before it is destroyed by standard game means.
    /// </summary>
    public bool PlaySizzle
    {
        get => Base.PlaySizzle;
        set => Base.PlaySizzle = value;
    }

    /// <summary>
    /// The base object.
    /// </summary>
    public new TantrumEnvironmentalHazard Base { get; }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="hazard">The base game tantrum hazard.</param>
    internal TantrumHazard(TantrumEnvironmentalHazard hazard)
        : base(hazard)
    {
        Base = hazard;

        Dictionary.Add(hazard, this);
    }

    /// <summary>
    /// Spawns a <see cref="TantrumHazard"/> at specified position with specified rotation and scale.
    /// <para> Do note that changing scale doesn't change the effect size. Use the <see cref="Hazard.MaxDistance"/> and <see cref="Hazard.MaxHeightDistance"/> to match the visual size.</para>
    /// </summary>
    /// <param name="position">The target position to spawn this hazard at.</param>
    /// <param name="rotation">The target rotation to spawn this hazard with.</param>
    /// <param name="scale">The target scale to spawn with.</param>
    /// <returns>A new tantrum hazard.</returns>
    public static TantrumHazard Spawn(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        if (BasePrefab == null)
            BasePrefab = Hazard.GetPrefab<TantrumEnvironmentalHazard>();

        TantrumHazard hazard = (TantrumHazard)Hazard.Spawn(BasePrefab, position, rotation, scale);
        hazard.SyncedPosition = position;
        return hazard;
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
    /// Gets the hazard wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="TantrumEnvironmentalHazard"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="hazard">The <see cref="Base"/> of the hazard.</param>
    /// <returns>The requested hazard or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(hazard))]
    public static TantrumHazard? Get(TantrumEnvironmentalHazard? hazard)
    {
        if (hazard == null)
            return null;

        return Dictionary.TryGetValue(hazard, out TantrumHazard decHazard) ? decHazard : (TantrumHazard)CreateItemWrapper(hazard);
    }
}