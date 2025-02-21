using InventorySystem.Items.MicroHID;
using System.Collections.Generic;

namespace LabApi.Features.Wrappers.Facility.Structures
{
    /// <summary>
    /// Wrapper for <see cref="MicroHIDPedestal"/> structure.
    /// </summary>
    public class MicroPadestal : Locker
    {
        /// <summary>
        /// Contains all the micro padestals, accessible through their <see cref="Base"/>.
        /// </summary>
        public new static Dictionary<MicroHIDPedestal, MicroPadestal> Dictionary { get; } = [];

        /// <summary>
        /// A reference to all <see cref="MicroPadestal"/> instances.
        /// </summary>
        public new static IReadOnlyCollection<MicroPadestal> List => Dictionary.Values;

        /// <summary>
        /// An internal constructor to prevent external instantiation.
        /// </summary>
        /// <param name="padestal">The base <see cref="Base"/> object.</param>
        internal MicroPadestal(MicroHIDPedestal padestal) : base(padestal)
        {
            Dictionary.Add(padestal, this);
            Base = padestal;
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
}
