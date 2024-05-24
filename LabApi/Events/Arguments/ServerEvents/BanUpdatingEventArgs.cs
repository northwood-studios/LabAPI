using System;
using LabApi.Events.Arguments.Interfaces;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.BanUpdating"/> event.
/// </summary>
public class BanUpdatingEventArgs : EventArgs, ICancellableEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BanUpdatingEventArgs"/> class.
    /// </summary>
    /// <param name="banType">The type of the ban.</param>
    /// <param name="banDetails">The new details of the ban.</param>
    /// <param name="oldBanDetails">The old details of the ban.</param>
    public BanUpdatingEventArgs(BanHandler.BanType banType, BanDetails banDetails, BanDetails oldBanDetails)
    {
        IsAllowed = true;
        BanType = banType;
        BanDetails = banDetails;
        OldBanDetails = oldBanDetails;
    }
    
    /// <inheritdoc />
    public bool IsAllowed { get; set; }
    
    /// <summary>
    /// The type of ban that is being updated.
    /// </summary>
    public BanHandler.BanType BanType { get; set; }
    
    /// <summary>
    /// The new details of the ban that is being updated.
    /// </summary>
    public BanDetails BanDetails { get; set; }
    
    /// <summary>
    /// The old details of the ban that is being updated.
    /// </summary>
    public BanDetails OldBanDetails { get; }
}