using System;

namespace LabApi.Events.Arguments.ServerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.ServerEvents.BanUpdated"/> event.
/// </summary>
public class BanUpdatedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BanUpdatedEventArgs"/> class.
    /// </summary>
    /// <param name="banType">The type of the ban.</param>
    /// <param name="banDetails">The new details of the ban.</param>
    /// <param name="oldBanDetails">The old details of the ban.</param>
    public BanUpdatedEventArgs(BanHandler.BanType banType, BanDetails banDetails, BanDetails oldBanDetails)
    {
        BanType = banType;
        BanDetails = banDetails;
        OldBanDetails = oldBanDetails;
    }
    
    /// <summary>
    /// The type of ban that was updated.
    /// </summary>
    public BanHandler.BanType BanType { get; }
    
    /// <summary>
    /// The new details of the ban that was updated.
    /// </summary>
    public BanDetails BanDetails { get; }
    
    /// <summary>
    /// The old details of the ban that was updated.
    /// </summary>
    public BanDetails OldBanDetails { get; }
}