using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BaseCheckpointDoor = Interactables.Interobjects.CheckpointDoor;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing the <see cref="BaseCheckpointDoor"/>.
/// </summary>
public class CheckpointDoor : Door
{
    /// <summary>
    /// Contains all the cached <see cref="CheckpointDoor"/> instances, accessible through their <see cref="BaseCheckpointDoor"/>.
    /// </summary>
    public new static Dictionary<BaseCheckpointDoor, CheckpointDoor> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="CheckpointDoor"/> instances currently in the game.
    /// </summary>
    public new static IReadOnlyCollection<CheckpointDoor> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseCheckpointDoor">The base <see cref="BaseCheckpointDoor"/> object.</param>
    internal CheckpointDoor(BaseCheckpointDoor baseCheckpointDoor)
        : base(baseCheckpointDoor)
    {
        Dictionary.Add(baseCheckpointDoor, this);
        Base = baseCheckpointDoor;
        SubDoors = new Door[baseCheckpointDoor.SubDoors.Length];

        for (int i = 0; i < baseCheckpointDoor.SubDoors.Length; i++)
            SubDoors[i] = Get(baseCheckpointDoor.SubDoors[i]);
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
    /// The base <see cref="BaseCheckpointDoor"/> object.
    /// </summary>
    public new BaseCheckpointDoor Base { get; }

    /// <summary>
    /// The base <see cref="CheckpointSequenceController"/> object.
    /// </summary>
    public CheckpointSequenceController SequenceController => Base.SequenceCtrl;

    /// <summary>
    /// All <see cref="Door"/> instances operated by this checkpoint.
    /// </summary>
    public Door[] SubDoors { get; }

    /// <summary>
    /// Gets or sets whether all the sub doors are open.
    /// </summary>
    public bool IsSubOpened
    {
        get => SubDoors.All(x => x.IsOpened);
        set => Base.ToggleAllDoors(value);
    }

    /// <summary>
    /// Gets or sets whether the doors are broken.
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
    /// Gets or sets the max health on damageable sub doors used when spawning.
    /// </summary>
    public float MaxHealth
    {
        get => Base.MaxHealth;
        set => Base.MaxHealth = value;
    }

    /// <summary>
    /// Gets or sets the remaining health on damageable sub door.
    /// </summary>
    public float Health
    {
        get => Base.RemainingHealth;
        set => Base.RemainingHealth = value;
    }

    /// <summary>
    /// Gets or sets the current <see cref="BaseCheckpointDoor.SequenceState"/> of the checkpoint door.
    /// </summary>
    public BaseCheckpointDoor.SequenceState SequenceState
    {
        get => Base.CurSequence;
        set => Base.CurSequence = value;
    }

    /// <summary>
    /// Gets or sets the time in seconds for which the checkpoint doors are opened when interacted with.
    /// </summary>
    public float OpenTime
    {
        get => Base.SequenceCtrl.OpenLoopTime;
        set => Base.SequenceCtrl.OpenLoopTime = value;
    }

    /// <summary>
    /// Gets or sets the time in seconds for which the checkpoint alarm is playing the alarm sound.
    /// </summary>
    public float WarningTime
    {
        get => Base.SequenceCtrl.WarningTime;
        set => Base.SequenceCtrl.WarningTime = value;
    }

    /// <summary>
    /// Gets the health as a percentage from 0 to 1.
    /// </summary>
    public float HealthPercent => Base.GetHealthPercent();

    /// <summary>
    /// Plays the warning alarm sound.
    /// </summary>
    public void PlayWarningSound() => Base.RpcPlayWarningSound();

    /// <summary>
    /// Damage all the sub doors by specified amount.
    /// </summary>
    /// <param name="damage">The amount of damage to apply.</param>
    /// <param name="type">The <see cref="DoorDamageType"/> to apply.</param>
    /// <returns>True if the doors took damage, otherwise false.</returns>
    public bool TryDamage(float damage, DoorDamageType type = DoorDamageType.ServerCommand)
        => Base.ServerDamage(damage, type);

    /// <summary>
    /// Break all the sub doors.
    /// </summary>
    /// <param name="type">The <see cref="DoorDamageType"/> to apply.</param>
    /// <returns>True if the doors took damage, otherwise false.</returns>
    public bool TryBreak(DoorDamageType type = DoorDamageType.ServerCommand)
        => TryDamage(float.MaxValue, type);

    /// <summary>
    /// Gets the <see cref="CheckpointDoor"/> wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="baseCheckpointDoor">The <see cref="BaseCheckpointDoor"/> of the door.</param>
    /// <returns>The requested door wrapper or null if the input was null.</returns>
    [return: NotNullIfNotNull(nameof(baseCheckpointDoor))]
    public static CheckpointDoor? Get(BaseCheckpointDoor? baseCheckpointDoor)
    {
        if (baseCheckpointDoor == null)
            return null;

        if (Dictionary.TryGetValue(baseCheckpointDoor, out CheckpointDoor door))
            return door;

        return (CheckpointDoor)CreateDoorWrapper(baseCheckpointDoor);
    }
}
