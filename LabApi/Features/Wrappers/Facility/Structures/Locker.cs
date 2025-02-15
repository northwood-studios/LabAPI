using MapGeneration.Distributors;
using NorthwoodLib.Pools;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using BaseLocker = MapGeneration.Distributors.Locker;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper over the <see cref="BaseLocker"/> object.
/// </summary>
public class Locker : Structure
{
    /// <summary>
    /// Contains all the cached lockers, accessible through their <see cref="BaseLocker"/>.
    /// </summary>
    public new static Dictionary<BaseLocker, Locker> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="Locker"/> instances.
    /// </summary>
    public new static IReadOnlyCollection<Locker> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseLocker">The base <see cref="BaseLocker"/> object.</param>
    internal Locker(BaseLocker baseLocker)
        : base(baseLocker)
    {
        Dictionary.Add(baseLocker, this);
        Base = baseLocker;
        Chambers = baseLocker.Chambers.Select(x => LockerChamber.Get(x)).ToArray();
    }

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);

        foreach (LockerChamber chamber in Chambers)
            chamber.OnRemove();
    }

    /// <summary>
    /// The base <see cref="BaseLocker"/> object.
    /// </summary>
    public new BaseLocker Base { get; }

    /// <summary>
    /// Gets a readonly list of all locker's <see cref="LockerChamber"/> instances.
    /// </summary>
    public IReadOnlyList<LockerChamber> Chambers { get; }

    /// <summary>
    /// Gets or sets the <see cref="LockerLoot"/> used to fill the chamber.
    /// </summary>
    /// <remarks>
    /// Loot is used when the locker is first spawned and when calling one of the <see cref="FillChambers"/> methods.
    /// Use <see cref="LockerChamber.AddItem(ItemType)"/> or <see cref="LockerChamber.RemoveItem(Pickup)"/> if you want to edit the contents of chambers directly.
    /// </remarks>
    public LockerLoot[] Loot
    {
        get => Base.Loot;
        set => Base.Loot = value;
    }

    /// <summary>
    /// Gets or sets the minimum number of chambers to fill.
    /// </summary>
    /// <remarks>
    /// Setting this to 0 will fill all chambers.
    /// </remarks>
    public int MinChambersToFill
    {
        get => Base.MinChambersToFill;
        set => Base.MinChambersToFill = value;
    }

    /// <summary>
    /// Gets or sets the maximum number of chambers to fill.
    /// This value is ignored if <see cref="MinChambersToFill"/> is 0.
    /// </summary>
    public int MaxChambersToFill
    {
        get => Base.MaxChambersToFill;
        set => Base.MaxChambersToFill = value;
    }

    /// <summary>
    /// Gets whether all the <see cref="Chambers"/> are <see cref="LockerChamber.IsEmpty"/>.
    /// </summary>
    public bool IsEmpty => Chambers.All(x => x.IsEmpty);

    /// <summary>
    /// Adds a new <see cref="LockerLoot"/> entry to the possible spawnable <see cref="Loot"/>.
    /// </summary>
    /// <param name="type">The <see cref="ItemType"/> to spawn. <see cref="LockerChamber"/> might only support certain <see cref="ItemType"/> values see <see cref="LockerChamber.AcceptableItems"/></param>
    /// <param name="remainingUses">The number of times this loot is selected to spawn in a chamber.</param>
    /// <param name="probabilityPoints">The probability weight given for this loot to spawn over other <see cref="LockerLoot"/> instances.</param>
    /// <param name="minPerChamber">The minimum number of items to spawn per chamber.</param>
    /// <param name="maxPerChamber">The maximum number of items to spawn per chamber.</param>
    /// <remarks>
    /// Note that after a chamber is filled the <see cref="LockerLoot"/> remaining uses are modified based on how many times it was added to the locker.
    /// <see cref="LockerLoot"/> is only used for when filling a locker see <see cref="FillChambers"/>, if you want to change the contents of a locker see <see cref="LockerChamber.AddItem(ItemType)"/> and <see cref="LockerChamber.RemoveItem(Pickup)"/>.
    /// </remarks>
    public void AddLockerLoot(ItemType type, int remainingUses, int probabilityPoints, int minPerChamber, int maxPerChamber)
    {
        LockerLoot loot = new()
        {
            TargetItem = type,
            RemainingUses = remainingUses,
            ProbabilityPoints = probabilityPoints,
            MinPerChamber = minPerChamber,
            MaxPerChamber= maxPerChamber
        };

        Base.Loot = [.. Base.Loot, loot];
    }

    /// <summary>
    /// Removes an existing <see cref="LockerLoot"/> from the possible spawnable <see cref="Loot"/>.
    /// </summary>
    /// <param name="loot">The <see cref="LockerLoot"/> instance to remove.</param>
    public void RemoveLockerLoot(LockerLoot loot) => Base.Loot = Base.Loot.Except([loot]).ToArray();

    /// <summary>
    /// Removes all <see cref="LockerLoot"/> instances from <see cref="Loot"/>.
    /// </summary>
    public void ClearLockerLoot() => Base.Loot = [];

    /// <summary>
    /// Fill chambers randomly with items chosen from <see cref="Loot"/>.
    /// </summary>
    public void FillChambers()
    {
        List<LockerChamber> chambers = ListPool<LockerChamber>.Shared.Rent();
        if (MinChambersToFill != 0 && MaxChambersToFill >= MinChambersToFill)
        {
            int removeCount = Chambers.Count - Random.Range(MinChambersToFill, MaxChambersToFill + 1);
            for (int i = 0; i < removeCount; i++)
                chambers.RemoveAt(Random.Range(0, chambers.Count));
        }

        foreach (LockerChamber chamber in chambers)
            chamber.Fill();

        ListPool<LockerChamber>.Shared.Return(chambers);
    }

    /// <summary>
    /// Fill all chambers randomly with items chosen from <see cref="Loot"/>.
    /// </summary>
    /// <remarks>
    /// Ignores <see cref="MinChambersToFill"/> and <see cref="MaxChambersToFill"/>.
    /// </remarks>
    public void FillAllChambers()
    {
        foreach (LockerChamber chamber in Chambers)
            chamber.Fill();
    }

    /// <summary>
    /// Removes all items from all chambers.
    /// </summary>
    public void ClearAllChambers()
    {
        foreach (LockerChamber chamber in Chambers)
            chamber.RemoveAllItems();
    }

    /// <summary>
    /// Opens all chamber doors.
    /// </summary>
    public void OpenAllChambers()
    {
        foreach (LockerChamber chamber in Chambers)
            chamber.IsOpen = true;
    }

    /// <summary>
    /// Closes all chamber doors.
    /// </summary>
    public void CloseAllChambers()
    {
        foreach (LockerChamber chamber in Chambers)
            chamber.IsOpen = false;
    }
}

