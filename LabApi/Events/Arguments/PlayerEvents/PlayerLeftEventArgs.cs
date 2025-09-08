﻿using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.Left"/> event.
/// </summary>
public class PlayerLeftEventArgs : EventArgs, IPlayerEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerLeftEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The player who left.</param>
    public PlayerLeftEventArgs(ReferenceHub hub)
    {
        Player = Player.Get(hub);
    }

    /// <summary>
    /// Gets the player who left.
    /// </summary>
    public Player Player { get; }
}