using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="Scp2176Projectile">SCP-2176</see>.
/// </summary>
public class Scp2176Projectile : TimedGrenadeProjectile
{
    /// <summary>
    /// Contains all the cached item pickups, accessible through their <see cref="InventorySystem.Items.ThrowableProjectiles.Scp2176Projectile"/>.
    /// </summary>
    public static new Dictionary<InventorySystem.Items.ThrowableProjectiles.Scp2176Projectile, Scp2176Projectile> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="InventorySystem.Items.ThrowableProjectiles.Scp2176Projectile"/>.
    /// </summary>
    public static new IReadOnlyCollection<Scp2176Projectile> List => Dictionary.Values;

    /// <summary>
    /// Gets the Scp-2176 from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="InventorySystem.Items.ThrowableProjectiles.Scp2176Projectile"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="projectile">The <see cref="Base"/> of the projectile.</param>
    /// <returns>The requested projectile or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(projectile))]
    public static Scp2176Projectile? Get(InventorySystem.Items.ThrowableProjectiles.Scp2176Projectile? projectile)
    {
        if (projectile == null)
        {
            return null;
        }

        return Dictionary.TryGetValue(projectile, out Scp2176Projectile wrapper) ? wrapper : (Scp2176Projectile)CreateItemWrapper(projectile);
    }

    /// <summary>
    /// A protected constructor to prevent external instantiation.
    /// </summary>
    /// <param name="projectilePickup">The <see cref="InventorySystem.Items.ThrowableProjectiles.Scp2176Projectile"/> of the pickup.</param>
    internal Scp2176Projectile(InventorySystem.Items.ThrowableProjectiles.Scp2176Projectile projectilePickup)
        : base(projectilePickup)
    {
        Base = projectilePickup;

        if (CanCache)
        {
            Dictionary.Add(projectilePickup, this);
        }
    }

    /// <summary>
    /// The <see cref="InventorySystem.Items.ThrowableProjectiles.Scp2176Projectile"/> of the pickup.
    /// </summary>
    public new InventorySystem.Items.ThrowableProjectiles.Scp2176Projectile Base { get; }

    /// <summary>
    /// Gets or sets the lockdown duration in seconds that is applied to the room once this SCP shatters.
    /// </summary>
    public float LockdownDuration
    {
        get => Base.LockdownDuration;
        set => Base.LockdownDuration = value;
    }

    /// <summary>
    /// Plays the shattering sound without shattering the SCP itself. Very spooky indeed.
    /// </summary>
    public void PlaySound() => Base.RpcMakeSound();

    /// <inheritdoc/>
    public override void FuseEnd() => Base.ServerImmediatelyShatter();

    /// <inheritdoc/>
    internal override void OnRemove()
    {
        base.OnRemove();

        Dictionary.Remove(Base);
    }
}
