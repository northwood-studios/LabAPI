using LiteNetLib;
using System;

namespace LabApi.Events.Arguments.PlayerEvents;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.PlayerEvents.PreAuthenticated"/> event.
/// </summary>
public class PlayerPreAuthenticatedEventArgs : EventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerPreAuthenticatedEventArgs"/> class.
    /// </summary>
    /// <param name="userId">User ID of the player.</param>
    /// <param name="ipAddress">IP Address the of player.</param>
    /// <param name="expiration">Expiration of the authentication.</param>
    /// <param name="flags">Pre-authentication flags.</param>
    /// <param name="region">Region of the origin.</param>
    /// <param name="signature">Signature of auth.</param>
    /// <param name="connectionRequest">Connection request to server.</param>
    /// <param name="readerStartPosition">Start position of stream.</param>
    public PlayerPreAuthenticatedEventArgs(string userId, string ipAddress, long expiration, CentralAuthPreauthFlags flags, string region, byte[]? signature, ConnectionRequest connectionRequest, int readerStartPosition)
    {
        UserId = userId;
        IpAddress = ipAddress;
        Expiration = expiration;
        Flags = flags;
        Region = region;
        Signature = signature;
        ConnectionRequest = connectionRequest;
        ReaderStartPosition = readerStartPosition;
    }

    /// <summary>
    /// Gets the user ID of the player.
    /// </summary>
    public string UserId { get; }

    /// <summary>
    /// Gets the IP Address of the player.
    /// </summary>
    public string IpAddress { get; }

    /// <summary>
    /// Gets the expiration of the authentication.
    /// </summary>
    public long Expiration { get; }

    /// <summary>
    /// Gets the pre-authentication flags.
    /// </summary>
    public CentralAuthPreauthFlags Flags { get; }

    /// <summary>
    /// Gets the region of the origin.
    /// </summary>
    public string Region { get; }

    /// <summary>
    /// Gets the signature of auth.
    /// </summary>
    public byte[]? Signature { get; }

    /// <summary>
    /// Gets the connection request to server.
    /// </summary>
    public ConnectionRequest ConnectionRequest { get; }

    /// <summary>
    /// Gets the start position of stream.
    /// </summary>
    public int ReaderStartPosition { get; }
}