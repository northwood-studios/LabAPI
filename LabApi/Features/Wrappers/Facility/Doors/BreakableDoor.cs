using Interactables.Interobjects.DoorUtils;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseBreakableDoor = Interactables.Interobjects.BreakableDoor;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing the <see cref="BaseBreakableDoor"/>.
/// </summary>
public class BreakableDoor : Door
{
    /// <summary>
    /// Contains all the cached <see cref="BreakableDoor"/> instances, accessible through their <see cref="BaseBreakableDoor"/>.
    /// </summary>
    public new static Dictionary<BaseBreakableDoor, BreakableDoor> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="BreakableDoor"/> instances currently in the game.
    /// </summary>
    public new static IReadOnlyCollection<BreakableDoor> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor the prevent external instantiation.
    /// </summary>
    /// <param name="baseBreakableDoor">The base <see cref="BaseBreakableDoor"/> object.</param>
    internal BreakableDoor(BaseBreakableDoor baseBreakableDoor)
        : base(baseBreakableDoor)
    {
        Dictionary.Add(baseBreakableDoor, this);
        Base = baseBreakableDoor;
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
    /// The base <see cref="BaseBreakableDoor"/> object.
    /// </summary>
    public new BaseBreakableDoor Base { get; }

    /// <summary>
    /// Gets or sets whether or not SCP-106 can pass through the door when its not closed and locked.
    /// </summary>
    public bool Is106Passable
    {
        get => Base.IsScp106Passable;
        set => Base.IsScp106Passable = value;
    }

    /// <summary>
    /// Gets or sets the max health used when spawning.
    /// </summary>
    public float MaxHealth
    {
        get => Base.MaxHealth;
        set => Base.MaxHealth = value;
    }

    /// <summary>
    /// Gets or sets the remaining health of the door.
    /// </summary>
    public float Health
    {
        get => Base.RemainingHealth;
        set => Base.RemainingHealth = value;
    }

    /// <summary>
    /// Gets or sets whether or not the door is broken.
    /// </summary>
    /// <remarks>
    /// Some doors can not be unbroken.
    /// </remarks>
    public bool IsBroken
    {
        get => Base.IsDestroyed;
        set => Base.IsDestroyed = value;
    }

    /// <summary>
    /// Gets or sets the <see cref="DoorDamageType"/> to block from taking health away from the door.
    /// </summary>
    public DoorDamageType IgnoreDamageSources
    {
        get => Base.IgnoredDamageSources;
        set => Base.IgnoredDamageSources = value;
    }

    /// <summary>
    /// Damage the door by specified amount.
    /// </summary>
    /// <param name="damage">The amount of damage to apply.</param>
    /// <param name="type">The <see cref="DoorDamageType"/> to apply.</param>
    /// <returns>True if the doors took damage, otherwise false.</returns>
    public bool TryDamage(float damage, DoorDamageType type = DoorDamageType.ServerCommand)
    {
        return Base.ServerDamage(damage, type);
    }

    /// <summary>
    /// Break the door.
    /// </summary>
    /// <param name="type">The <see cref="DoorDamageType"/> to apply.</param>
    /// <returns>True if the doors took damage, otherwise false.</returns>
    public bool TryBreak(DoorDamageType type = DoorDamageType.ServerCommand)
    {
        return Base.ServerDamage(float.MaxValue, type);
    }

    /// <summary>
    /// Tries to repair the door.
    /// <remarks>Sets the doors health back to <see cref="MaxHealth"/> if the door is broken otherwise it does nothing.</remarks>
    /// </summary>
    /// <returns>True if the door was repaired, otherwise false.</returns>
    public bool TryRepair()
    {
        return Base.ServerRepair();
    }

    /// <summary>
    /// Gets the <see cref="BreakableDoor"/> wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="baseBreakableDoor">The <see cref="BaseBreakableDoor"/> of the door.</param>
    /// <returns>The requested door wrapper or null if the input was null.</returns>
    [return: NotNullIfNotNull(nameof(baseBreakableDoor))]
    public static BreakableDoor? Get(BaseBreakableDoor? baseBreakableDoor)
    {
        if (baseBreakableDoor == null)
            return null;

        if (Dictionary.TryGetValue(baseBreakableDoor, out BreakableDoor door))
            return door;

        return (BreakableDoor)CreateDoorWrapper(baseBreakableDoor);
    }
}
