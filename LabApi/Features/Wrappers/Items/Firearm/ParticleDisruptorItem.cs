using InventorySystem.Items.Firearms;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="ParticleDisruptor"/>.
/// </summary>
public class ParticleDisruptorItem : FirearmItem
{
    /// <summary>
    /// Contains all the cached particle disruptor items, accessible through their <see cref="ParticleDisruptor"/>.
    /// </summary>
    public new static Dictionary<ParticleDisruptor, ParticleDisruptorItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="ParticleDisruptorItem"/>.
    /// </summary>
    public new static IReadOnlyCollection<ParticleDisruptorItem> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="particleDisruptor">The base <see cref="ParticleDisruptor"/> object.</param>
    internal ParticleDisruptorItem(ParticleDisruptor particleDisruptor)
        : base(particleDisruptor)
    {
        Base = particleDisruptor;

        if (CanCache)
            Dictionary.Add(particleDisruptor, this);
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
    /// The base <see cref="ParticleDisruptor"/> object.
    /// </summary>
    public new ParticleDisruptor Base { get; }

    /// <summary>
    /// Gets the particle disruptor item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="ParticleDisruptor"/> was not null.
    /// </summary>
    /// <param name="particleDisruptor">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(particleDisruptor))]
    public static ParticleDisruptorItem? Get(ParticleDisruptor? particleDisruptor)
    {
        if (particleDisruptor == null)
            return null;

        return Dictionary.TryGetValue(particleDisruptor, out ParticleDisruptorItem item) ? item : (ParticleDisruptorItem)CreateItemWrapper(particleDisruptor);
    }
}