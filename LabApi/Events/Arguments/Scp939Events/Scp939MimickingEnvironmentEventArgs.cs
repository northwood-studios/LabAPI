using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;

namespace LabApi.Events.Arguments.Scp939Events;

/// <summary>
/// Represents the event arguments for when SCP-939 is mimicking the environment.
/// </summary>
public class Scp939MimickingEnvironmentEventArgs : EventArgs, IPlayerEvent, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp939MimickingEnvironmentEventArgs"/> class.
    /// </summary>
    /// <param name="hub">The SCP-939 player instance.</param>
    /// <param name="selectedSequence">The selected environmental sequence to play.</param>
    /// <param name="cooldownTime">The cooldown for mimicking the environment.</param>
    public Scp939MimickingEnvironmentEventArgs(ReferenceHub hub, byte selectedSequence, float cooldownTime)
    {
        Player = Player.Get(hub);
        SelectedSequence = selectedSequence;
        CooldownTime = cooldownTime;

        IsAllowed = true;
    }

    /// <summary>
    /// Gets the 939 player instance.
    /// </summary>
    public Player Player { get; }

    /// <summary>
    /// Gets or sets the selected sequence to play.
    /// </summary>
    public byte SelectedSequence { get; set; }

    /// <summary>
    /// Gets or sets the cooldown for mimicking the environment.
    /// </summary>
    public float CooldownTime { get; set; }

    /// <inheritdoc/>
    public bool IsAllowed { get; set; }
}