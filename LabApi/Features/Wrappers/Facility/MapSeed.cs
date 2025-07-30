using LabApi.Events.Handlers;
using LabApi.Features.Console;
using MapGeneration;
using System;
using Generators;
using LabApi.Events.Arguments.ServerEvents;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Manages map seed setting and retrieval.
/// </summary>
public static class MapSeed
{
    private static int? _pendingSeed;
    private static bool _seedSet = false;
    private static bool _seedWasApplied = false;

    /// <summary>
    /// Initializes the MapSeedManager by subscribing to map generation events.
    /// </summary>
    [InitializeWrapper]
    internal static void Initialize()
    {
        ServerEvents.MapGenerating += OnMapGenerating;
        ServerEvents.MapGenerated += OnMapGenerated;
    }

    /// <summary>
    /// Sets the seed for the next map generation.
    /// This must be called before map generation starts.
    /// </summary>
    /// <param name="seed">The seed to use for map generation.</param>
    /// <returns>True if the seed was set successfully, false if map generation has already started.</returns>
    public static bool SetNextMapSeed(int seed)
    {
        if (seed < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(seed), "Seed value must be non-negative.");
        }

        if (_seedSet)
        {
            return false;
        }

        _pendingSeed = seed;
        _seedWasApplied = false;
        return true;
    }

    /// <summary>
    /// Gets the currently set pending seed.
    /// </summary>
    /// <returns>The pending seed if set, otherwise null.</returns>
    public static int? GetPendingSeed() => _pendingSeed;

    /// <summary>
    /// Clears the pending seed, allowing the game to use its default seed generation.
    /// </summary>
    public static void ClearPendingSeed()
    {
        _pendingSeed = null;
        _seedWasApplied = false;
    }

    /// <summary>
    /// Checks if a seed has been set for the next map generation.
    /// </summary>
    /// <returns>True if a seed is pending, false otherwise.</returns>
    public static bool HasPendingSeed() => _pendingSeed.HasValue;

    /// <summary>
    /// Gets the current map seed.
    /// </summary>
    /// <returns>The current map seed.</returns>
    public static int GetCurrentSeed() => SeedSynchronizer.Seed;

    /// <summary>
    /// Checks if the last set seed was successfully applied during map generation.
    /// </summary>
    /// <returns>True if the seed was applied, false otherwise.</returns>
    public static bool WasSeedApplied() => _seedWasApplied;

    private static void OnMapGenerating(MapGeneratingEventArgs ev)
    {
        try
        {
            if (_pendingSeed.HasValue)
            {
                ev.Seed = _pendingSeed.Value;
                _seedSet = true;
                _seedWasApplied = true;
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to apply pending seed during map generation: {ex.Message}", ex);
        }
    }

    private static void OnMapGenerated(MapGeneratedEventArgs ev)
    {
        try
        {
            // Reset for next round
            _pendingSeed = null;
            _seedSet = false;
            _seedWasApplied = false;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to reset seed state after map generation: {ex.Message}", ex);
        }
    }
}