using Hazards;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// A class representing the <see cref="SinkholeEnvironmentalHazard"/>.
/// Note that this is static hazard and position, rotation and scale isn't applied on clients unless you respawn this object via <see cref="NetworkServer"/>.
/// </summary>
public class SinkholeHazard : Hazard
{
    /// <summary>
    /// Contains all the cached items, accessible through their <see cref="Base"/>.
    /// </summary>
    public new static Dictionary<SinkholeEnvironmentalHazard, SinkholeHazard> Dictionary { get; } = [];

    /// <summary>
    /// Gets all currently active sinkholes.
    /// </summary>
    public new IReadOnlyCollection<SinkholeHazard> List => Dictionary.Values;

    /// <summary>
    /// Prefab used to spawn the hazard.
    /// </summary>
    protected static new SinkholeEnvironmentalHazard? BasePrefab;

    /// <summary>
    /// The base object.
    /// </summary>
    public new SinkholeEnvironmentalHazard Base { get; }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="hazard">The base <see cref="SinkholeEnvironmentalHazard"/> object.</param>
    internal SinkholeHazard(SinkholeEnvironmentalHazard hazard)
        : base(hazard)
    {
        Base = hazard;
        Dictionary.Add(hazard, this);
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
    /// Spawns a <see cref="SinkholeHazard"/> at specified position with specified rotation and scale.
    /// <para> Do note that changing scale doesn't change the effect size. Use the <see cref="Hazard.MaxDistance"/> and <see cref="Hazard.MaxHeightDistance"/> to match the visual size.</para>
    /// </summary>
    /// <param name="position">The target position to spawn this hazard at.</param>
    /// <param name="rotation">The target rotation to spawn this hazard with.</param>
    /// <param name="scale">The target scale to spawn with. Also affects the size of the decal.</param>
    /// <returns>The newly created hazard.</returns>
    public static SinkholeHazard Spawn(Vector3 position, Quaternion rotation, Vector3 scale)
    {
        if (BasePrefab == null)
            BasePrefab = GetPrefab<SinkholeEnvironmentalHazard>();

        SinkholeHazard hazard = (SinkholeHazard)Hazard.Spawn(BasePrefab!, position, rotation, scale);
        hazard.IsActive = true;
        return hazard;
    }

    /// <summary>
    /// Gets the sinkhole wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="SinkholeEnvironmentalHazard"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="hazard">The <see cref="Base"/> of the hazard.</param>
    /// <returns>The requested hazard or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(hazard))]
    public static SinkholeHazard? Get(SinkholeEnvironmentalHazard? hazard)
    {
        if (hazard == null)
            return null;

        return Dictionary.TryGetValue(hazard, out SinkholeHazard sinkhole) ? sinkhole : (SinkholeHazard)CreateItemWrapper(hazard);
    }
}