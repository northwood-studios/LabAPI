using LabApi.Events.Arguments.Interfaces;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.MapGenerating"/> event.
/// </summary>
public class MapGeneratingEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MapGeneratingEventArgs"/> class.
    /// </summary>
    /// <param name="seed">The seed of next map.</param>
    public MapGeneratingEventArgs(int seed)
    {
        IsAllowed = true;
        Seed = seed;
    }

    /// <summary>
    /// Gets or sets seed of next map.
    /// </summary>
    public int Seed { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}
