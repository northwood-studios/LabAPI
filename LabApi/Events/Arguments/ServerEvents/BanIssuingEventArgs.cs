using LabApi.Events.Arguments.Interfaces;
using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.BanIssuing"/> event.
/// </summary>
public class BanIssuingEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BanIssuingEventArgs"/> class.
    /// </summary>
    /// <param name="banType">The type of the ban.</param>
    /// <param name="banDetails">The details of the ban.</param>
    public BanIssuingEventArgs(BanHandler.BanType banType, BanDetails banDetails)
    {
        IsAllowed = true;
        BanType = banType;
        BanDetails = banDetails;
    }

    /// <inheritdoc />
    public bool IsAllowed { get; set; }

    /// <summary>
    /// The type of ban that is being issued.
    /// </summary>
    public BanHandler.BanType BanType { get; set; }

    /// <summary>
    /// The details of the ban that is being issued.
    /// </summary>
    public BanDetails BanDetails { get; set; }
}