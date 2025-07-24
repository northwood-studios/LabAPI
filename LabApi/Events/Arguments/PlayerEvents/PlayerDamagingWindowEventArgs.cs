using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerStatsSystem;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.DamagingWindow"/> event.
/// </summary>
public class PlayerDamagingWindowEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerDamagingWindowEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who is damaging the window.</param>
    /// <param name="window">The window.</param>
    /// <param name="damageHandler">The damage handler.</param>
    public PlayerDamagingWindowEventArgs(ReferenceHub hub, BreakableWindow window, DamageHandlerBase damageHandler)
    {
        IsAllowed = true;
        Player = Player.Get(hub);
        Window = window;
        DamageHandler = damageHandler;
    }

    /// <summary>
    /// Gets the player who is damaging the window.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the window.
    /// </summary>
    public BreakableWindow Window { get; }

    /// <summary>
    /// Gets the damage handler.
    /// </summary>
    public DamageHandlerBase DamageHandler { get; set; }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }
}