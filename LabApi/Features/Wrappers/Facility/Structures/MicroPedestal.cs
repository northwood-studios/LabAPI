using InventorySystem.Items.MicroHID;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for <see cref="MicroHIDPedestal"/> structure.
/// </summary>
public class MicroPedestal : Locker
{
    /// <summary>
    /// Contains all the micro pedestals, accessible through their <see cref="Base"/>.
    /// </summary>
    public static new Dictionary<MicroHIDPedestal, MicroPedestal> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="MicroPedestal"/> instances.
    /// </summary>
    public static new IReadOnlyCollection<MicroPedestal> List => Dictionary.Values;

    /// <summary>
    /// Gets the micro pedestal wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist and the provided <see cref="MicroHIDPedestal"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="basePedestal">The <see cref="Base"/> of the experimental weapon locker.</param>
    /// <returns>The requested wrapper or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(basePedestal))]
    public static MicroPedestal? Get(MicroHIDPedestal? basePedestal)
    {
        if (basePedestal == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(basePedestal, out MicroPedestal found) ? found : (MicroPedestal)CreateStructureWrapper(basePedestal);
    }

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="pedestal">The base <see cref="Base"/> object.</param>
    internal MicroPedestal(MicroHIDPedestal pedestal)
        : base(pedestal)
    {
        Base = pedestal;

        if (CanCache)
        {
            Dictionary.Add(pedestal, this);
        }
    }

    /// <summary>
    /// The base <see cref="Base"/> object.
    /// </summary>
    public new MicroHIDPedestal Base { get; }

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }
}
