using LabApi.Features.Wrappers;

namespace LabApi.Features.Stores;

/// <summary>
/// Represents a Custom Data Store that plugins can use to store data with a player.
/// </summary>
/// <typeparam name="TStore">The type of the <see cref="CustomDataStore"/>.</typeparam>
public abstract class CustomDataStore<TStore> : CustomDataStore
    where TStore : CustomDataStore<TStore>
{
    /// <summary>
    /// Gets the <see cref="CustomDataStore"/> for the specified <see cref="Player"/>.
    /// </summary>
    /// <param name="player">The <see cref="Player"/> to get the <see cref="CustomDataStore"/> for.</param>
    /// <returns>The <see cref="CustomDataStore"/> for the specified <see cref="Player"/>.</returns>
    public static TStore Get(Player player) => GetOrAdd<TStore>(player);

    /// <summary>
    /// Initializes a new instance of the <see cref="CustomDataStore"/> class.
    /// </summary>
    /// <param name="owner">The <see cref="Player"/> that this instance is associated with.</param>
    protected CustomDataStore(Player owner)
        : base(owner)
    {
    }
}
