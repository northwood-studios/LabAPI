using InventorySystem.Items.ThrowableProjectiles;
using System.Collections.Generic;

namespace LabApi.Features.Wrappers
{
    /// <summary>
    /// The wrapper representing <see cref="ThrownProjectile">thrown projectile</see>.
    ///
    /// <para>Note that both pickups and physical projectiles share the same base class so they share the same base properties.</para>
    /// </summary>
    public class Projectile : Pickup
    {
        /// <summary>
        /// Contains all the cached projectiles, accessible through their <see cref="ThrownProjectile"/>.
        /// </summary>
        public static new Dictionary<ThrownProjectile, Projectile> Dictionary { get; } = [];

        /// <summary>
        /// A reference to all instances of <see cref="Projectile"/>.
        /// </summary>
        public static new IReadOnlyCollection<Projectile> List => Dictionary.Values;

        /// <summary>
        /// A protected constructor to prevent external instantiation.
        /// </summary>
        /// <param name="projectilePickup">The <see cref="ThrownProjectile"/> of the pickup.</param>
        protected Projectile(ThrownProjectile projectilePickup) : base(projectilePickup)
        {
            Base = projectilePickup;

            Dictionary.Add(projectilePickup, this);
        }

        /// <summary>
        /// The <see cref="ThrownProjectile"/> of the pickup.
        /// </summary>
        public new ThrownProjectile Base { get; }

        internal override void OnRemove()
        {
            base.OnRemove();

            Dictionary.Remove(Base);
        }
    }
}
