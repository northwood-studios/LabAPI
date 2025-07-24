using InventorySystem.Items.Usables.Scp330;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using BaseScp330Item = InventorySystem.Items.Usables.Scp330.Scp330Bag;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseScp330Item"/>.
/// </summary>
public class Scp330Item : UsableItem
{
    /// <summary>
    /// Contains all the cached SCP-330 items, accessible through their <see cref="BaseScp330Item"/>.
    /// </summary>
    public new static Dictionary<BaseScp330Item, Scp330Item> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="Scp330Item"/>.
    /// </summary>
    public new static IReadOnlyCollection<Scp330Item> List => Dictionary.Values;

    /// <summary>
    /// Maximum number of candies that can be contained in a bag.
    /// </summary>
    public const int MaxCandies = BaseScp330Item.MaxCandies;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseScp330Item">The base <see cref="BaseScp330Item"/> object.</param>
    internal Scp330Item(BaseScp330Item baseScp330Item)
        : base(baseScp330Item)
    {
        Base = baseScp330Item;

        if (CanCache)
            Dictionary.Add(baseScp330Item, this);
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
    /// The base <see cref="BaseScp330Item"/> object.
    /// </summary>
    public new BaseScp330Item Base { get; }

    /// <summary>
    /// Gets or sets the <see cref="CandyKindID">candies</see> contained in the bag.
    /// </summary>
    public IEnumerable<CandyKindID> Candies
    {
        get => Base.Candies;
        set => SetCandies(value);
    }

    /// <summary>
    /// Gets the selected candy index.
    /// </summary>
    /// <remarks>
    /// -1 if no candy is selected.
    /// </remarks>
    public int SelectedCandyIndex => Base.SelectedCandyId;

    /// <summary>
    /// Adds a specific candy to the bag.
    /// </summary>
    /// <param name="candy">The candy to add.</param>
    /// <param name="sync">Whether to sync the changes to the client.</param>
    public void AddCandy(CandyKindID candy, bool sync = true)
    {
        if (Base.Candies.Count >= MaxCandies)
            return;
        
        Base.Candies.Add(candy);

        if (sync)
            SyncCandies();
    }
    
    /// <summary>
    /// Adds the specified candies to the bag up to the <see cref="MaxCandies"/>.
    /// </summary>
    /// <param name="candies">The set of candies to add.</param>
    /// <param name="sync">Whether to sync the changes to the client.</param>
    public void AddCandies(IEnumerable<CandyKindID> candies, bool sync = true)
    {
        IEnumerator<CandyKindID> enumerator = candies.GetEnumerator();
        while (enumerator.MoveNext() && Base.Candies.Count < MaxCandies)
            Base.Candies.Add(enumerator.Current);

        enumerator.Dispose();
        if (sync)
            SyncCandies();
    }

    /// <summary>
    /// Removes the specified candy from the bag.
    /// </summary>
    /// <remarks>
    /// If the bag is empty and <paramref name="sync"/> is true the item will destroy itself.
    /// </remarks>
    /// <param name="candy">The candy to remove.</param>
    /// <param name="sync">Whether to sync the changes to the client.</param>
    public void RemoveCandy(CandyKindID candy, bool sync = true)
    {
        Base.Candies.Remove(candy);
        
        if (sync)
            SyncCandies();
    }

    /// <summary>
    /// Tries to remove the specified candy from the bag.
    /// </summary>
    /// <remarks>
    /// If the bag is empty and <paramref name="sync"/> is true the item will destroy itself.
    /// </remarks>
    /// <param name="candy">The candy to remove.</param>
    /// <param name="sync">Whether to sync the changes to the client.</param>
    /// <returns>True if the candy was in the bag and removed successfully.</returns>
    public bool TryRemoveCandy(CandyKindID candy, bool sync = true)
    {
        bool wasRemoved = Base.Candies.Remove(candy);
        
        if (sync)
            SyncCandies();

        return wasRemoved;
    }

    /// <summary>
    /// Removes the specified candies from the bag.
    /// </summary>
    /// <remarks>
    /// If the bag is empty and <paramref name="sync"/> is true the item will destroy itself.
    /// </remarks>
    /// <param name="candies">The set of candies to remove.</param>
    /// <param name="sync">Whether to sync the changes to the client.</param>
    public void RemoveCandies(IEnumerable<CandyKindID> candies, bool sync = true)
    {
        IEnumerator<CandyKindID> enumerator = candies.GetEnumerator();
        while (enumerator.MoveNext() && !Base.Candies.IsEmpty())
            Base.Candies.Remove(enumerator.Current);

        enumerator.Dispose();
        if (sync)
            SyncCandies();
    }

    /// <summary>
    /// Tries to remove the specified candies from the bag.
    /// </summary>
    /// <remarks>
    /// If the bag is empty and <paramref name="sync"/> is true the item will destroy itself.
    /// </remarks>
    /// <param name="candies">The set of candies to remove.</param>
    /// <param name="removedCandies">The candies that were successfully removed from the item.></param>
    /// <param name="sync">Whether to sync the changes to the client.</param>
    /// <returns>True if at least one of the candies was in the bag and removed successfully.</returns>
    public bool TryRemoveCandies(IEnumerable<CandyKindID> candies, out List<CandyKindID> removedCandies, bool sync = true)
    {
        removedCandies = [];
        bool anyRemoved = false;
        
        IEnumerator<CandyKindID> enumerator = candies.GetEnumerator();
        while (enumerator.MoveNext() && !Base.Candies.IsEmpty())
        {
            bool successfullyRemoved = Base.Candies.Remove(enumerator.Current);
            if (!successfullyRemoved)
                continue;
            
            removedCandies.Add(enumerator.Current);
            anyRemoved = true;
        }
        
        enumerator.Dispose();
        if (sync)
            SyncCandies();
        
        return anyRemoved;
    }

    /// <summary>
    /// Sets the bags candy contents.
    /// </summary>
    /// <param name="candies">The candies to have.</param>
    /// <param name="sync">Whether to sync the changes to the client.</param>
    public void SetCandies(IEnumerable<CandyKindID> candies, bool sync = true)
    {
        Base.Candies.Clear();
        AddCandies(candies, sync);
    }

    /// <summary>
    /// Sync candy bag contents to the client.
    /// If the bag does not contain any candies this item will destroy itself.
    /// </summary>
    public void SyncCandies() => Base.ServerRefreshBag();

    /// <summary>
    /// Tries to drop the specified candy from the bag.
    /// </summary>
    /// <param name="kind">The candy kind to drop.</param>
    /// <param name="dropped">The dropped candy pickup.</param>
    /// <returns>True if the item was contained in the bag and it dropped successfully.</returns>
    public bool TryDrop(CandyKindID kind, [NotNullWhen(true)] out Pickup? dropped)
    {
        dropped = null;
        if(!Candies.Contains(kind))
            return false;

        if (CurrentOwner == null)
            return false;

        dropped = Pickup.Create(Type, CurrentOwner.Position);
        if (dropped == null)
            return false;

        Scp330Pickup scp330 = (Scp330Pickup)dropped;
        scp330.ExposedCandy = kind;

        RemoveCandy(kind);
        return true;
    }

    /// <summary>
    /// Gets the SCP-330 item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseScp330Item"/> was not null.
    /// </summary>
    /// <param name="baseScp330Item">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(baseScp330Item))]
    public static Scp330Item? Get(BaseScp330Item? baseScp330Item)
    {
        if (baseScp330Item == null)
            return null;

        return Dictionary.TryGetValue(baseScp330Item, out Scp330Item item) ? item : (Scp330Item)CreateItemWrapper(baseScp330Item);
    }
}
