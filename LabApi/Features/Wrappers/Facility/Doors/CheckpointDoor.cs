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
        set
        {
            foreach (DoorVariant doorVariant in Base.SubDoors)
            {
                if (doorVariant is not IDamageableDoor damageableDoor)
                    continue;

                damageableDoor.IsDestroyed = value;
            }
        }
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
    /// Gets or sets the current <see cref="BaseCheckpointDoor.CheckpointSequenceStage"/> of the checkpoint door.
    /// </summary>
    public BaseCheckpointDoor.CheckpointSequenceStage SequenceState
    {
        get => Base.CurrentSequence;
        set => Base.CurrentSequence = value;
    }

    /// <summary>
    /// Gets or sets the time in seconds to open the doors after the <see cref="SequenceState"/> was set to <see cref="BaseCheckpointDoor.CheckpointSequenceStage.Granted"/>.
    /// </summary>
    /// <remarks>
    /// Does not affect the speed of the animations of the door it only influences the timing of when when to move on to the next stage.
    /// <see cref="SequenceState"/> is set to <see cref="BaseCheckpointDoor.CheckpointSequenceStage.Open"/> after the delay.
    /// </remarks>
    public float OpeningDuration
    {
        get => Base.OpeningTime;
        set => Base.OpeningTime = value;
    }

    /// <summary>
    /// Gets or sets the time in seconds to wait before sounding the warning buzzer after <see cref="SequenceState"/> was set to <see cref="BaseCheckpointDoor.CheckpointSequenceStage.Open"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="SequenceState"/> is set to <see cref="BaseCheckpointDoor.CheckpointSequenceStage.Closing"/> after the duration.
    /// </remarks>
    public float WaitDuration
    {
        get => Base.WaitTime;
        set => Base.WaitTime = value;
    }

    /// <summary>
    /// Gets or sets the time in seconds to play the warning sound after the <see cref="SequenceState"/> was set to <see cref="BaseCheckpointDoor.CheckpointSequenceStage.Closing"/>.
    /// </summary>
    /// <remarks>
    /// The doors close immediately after the warning time ends and <see cref="SequenceState"/> is set to <see cref="BaseCheckpointDoor.CheckpointSequenceStage.Idle"/>.
    /// </remarks>
    public float WarningDuration
    {
        get => Base.WarningTime;
        set => Base.WarningTime = value;
    }

    /// <summary>
    /// Gets or sets the current sequence time in seconds.
    /// </summary>
    /// <remarks>
    /// Represents the value of the internal timer used to switch <see cref="SequenceState"/> depending on <see cref="OpeningDuration"/>, <see cref="WaitDuration"/> and <see cref="WarningDuration"/>. 
    /// </remarks>
    public float SequenceTime
    {
        get => Base.MainTimer;
        set => Base.MainTimer = value;
    }

    /// <summary>
    /// Gets the health as a percentage from 0 to 1.
    /// </summary>
    public float HealthPercent => Base.GetHealthPercent();

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
    /// Plays a sound and sets the panel state to error. Error state can not be undone.
    /// </summary>
    public void PlayErrorAnimation() => Base.RpcPlayBeepSound(2);

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
