using InventorySystem.Items.Coin;
using Mirror;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="Coin"/>.
/// </summary>
/// <remarks>
/// The most important wrapper in the Api.
/// </remarks>
public class CoinItem : Item
{
    /// <summary>
    /// Contains all the cached coin items, accessible through their <see cref="Coin"/>.
    /// </summary>
    public new static Dictionary<Coin, CoinItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="CoinItem"/>.
    /// </summary>
    public new static IReadOnlyCollection<CoinItem> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="coin">The base <see cref="Coin"/> object.</param>
    internal CoinItem(Coin coin)
        : base(coin)
    {
        Base = coin;

        if (CanCache)
            Dictionary.Add(coin, this);
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
    /// The base <see cref="Coin"/> object.
    /// </summary>
    public new Coin Base { get; }

    /// <summary>
    /// Gets the outcome of the last flip.
    /// Null if the coin has not been flipped otherwise true = Heads, false = Tails.
    /// </summary>
    public bool? LastFlipResult => Coin.FlipTimes.TryGetValue(Serial, out double time) ? (time > 0.0f) : null;

    /// <summary>
    /// Gets the <see cref="NetworkTime.time"/> of the last flip.
    /// Null if the coin has not been flipped.
    /// </summary>
    public double? LastFlipTime => Coin.FlipTimes.TryGetValue(Serial, out double time) ? Math.Abs(time) : null;

    /// <summary>
    /// Gets the coin item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="Coin"/> was not null.
    /// </summary>
    /// <param name="coin">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or null.</returns>
    [return: NotNullIfNotNull(nameof(coin))]
    public static CoinItem? Get(Coin? coin)
    {
        if (coin == null)
            return null;

        return Dictionary.TryGetValue(coin, out CoinItem item) ? item : (CoinItem)CreateItemWrapper(coin);
    }
}
