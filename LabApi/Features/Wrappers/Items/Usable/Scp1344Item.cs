using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseScp1344Item = InventorySystem.Items.Usables.Scp1344.Scp1344Item;

namespace LabApi.Features.Wrappers.Items.Usable
{
    /// <summary>
    /// The wrapper representing <see cref="BaseScp1344Item"/>.
    /// </summary>
    public class Scp1344Item : UsableItem
    {
        /// <summary>
        /// Contains all the cached SCP-1344 items, accessible through their <see cref="BaseScp1344Item"/>.
        /// </summary>
        public new static Dictionary<BaseScp1344Item, Scp1344Item> Dictionary { get; } = [];

        /// <summary>
        /// A reference to all instances of <see cref="Scp1344Item"/>.
        /// </summary>
        public new static IReadOnlyCollection<Scp1344Item> List => Dictionary.Values;

        /// <summary>
        /// The base <see cref="BaseScp1344Item"/> object.
        /// </summary>
        public new BaseScp1344Item Base { get; }

        /// <summary>
        /// An internal constructor to prevent external instantiation.
        /// </summary>
        /// <param name="baseItem">The base <see cref="BaseScp1344Item"/> object.</param>
        internal Scp1344Item(BaseScp1344Item baseItem) : base(baseItem)
        {
            Dictionary.Add(baseItem, this);
            Base = baseItem;
        }

        /// <summary>
        /// Gets the SCP-1576 item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseScp1344Item"/> was not null.
        /// </summary>
        /// <param name="baseItem">The <see cref="Base"/> of the item.</param>
        /// <returns>The requested item or null.</returns>
        [return: NotNullIfNotNull(nameof(baseItem))]
        public static Scp1344Item? Get(BaseScp1344Item? baseItem)
        {
            if (baseItem == null)
                return null;

            return Dictionary.TryGetValue(baseItem, out Scp1344Item item) ? item : (Scp1344Item)CreateItemWrapper(baseItem);
        }

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
