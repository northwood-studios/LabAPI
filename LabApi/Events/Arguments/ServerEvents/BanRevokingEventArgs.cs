using System;
using LabApi.Events.Arguments.Interfaces;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.BanRevoking"/> event.
/// </summary>
public class BanRevokingEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BanRevokingEventArgs"/> class.
    /// </summary>
    /// <param name="banType">The type of the ban.</param>
    /// <param name="banDetails">The details of the ban.</param>
    public BanRevokingEventArgs(BanHandler.BanType banType, BanDetails banDetails)
    {
        IsAllowed = true;
        BanType = banType;
        BanDetails = banDetails;
    }
    
    /// <inheritdoc />
    public bool IsAllowed { get; set; }
    
    /// <summary>
    /// The type of ban that is being revoked.
    /// </summary>
    public BanHandler.BanType BanType { get; }
    
    /// <summary>
    /// The details of the ban that is being revoked.
    /// </summary>
    public BanDetails BanDetails { get; }
}