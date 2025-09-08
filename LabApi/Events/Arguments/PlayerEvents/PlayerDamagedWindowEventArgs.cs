using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using PlayerStatsSystem;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.DamagedWindow"/> event.
/// </summary>
public class PlayerDamagedWindowEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerDamagedWindowEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who damaged the window.</param>
    /// <param name="window">The window.</param>
    /// <param name="damageHandler">The damage handler.</param>
    public PlayerDamagedWindowEventArgs(ReferenceHub hub, BreakableWindow window, DamageHandlerBase damageHandler)
    {
        Player = Player.Get(hub);
        Window = window;
        DamageHandler = damageHandler;
    }

    /// <summary>
    /// Gets the player who damaged the window.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets the window.
    /// </summary>
    public BreakableWindow Window { get; }

    /// <summary>
    /// Gets the damage handler.
    /// </summary>
    public DamageHandlerBase DamageHandler { get; }
}