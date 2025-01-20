using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="ServerEvents.MapGenerated"/> event.
/// </summary>
public class MapGeneratedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MapGeneratedEventArgs"/> class.
    /// </summary>
    /// <param name="seed">The seed of next map.</param>
    public MapGeneratedEventArgs(int seed)
    {
        Seed = seed;
    }

    /// <summary>
    /// Gets seed of next map.
    /// </summary>
    public int Seed { get; }
}
