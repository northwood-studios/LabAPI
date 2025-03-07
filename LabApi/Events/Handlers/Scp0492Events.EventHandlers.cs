using LabApi.Events.Arguments.Scp0492Events;

namespace LabApi.Events.Handlers;

/// <summary>
/// Handles all events related to SCP-049-2.
/// </summary>
public static partial class Scp0492Events
{
    /// <summary>
    /// Gets called when SCP-049-2 is starting to consume a corpse.
    /// </summary>
    public static event LabEventHandler<Scp0492StartingConsumingCorpseEventArgs>? StartingConsumingCorpse;
    
    /// <summary>
    /// Gets called when SCP-049-2 started to consume a corpse.
    /// </summary>
    public static event LabEventHandler<Scp0492StartedConsumingCorpseEventArgs>? StartedConsumingCorpse;
    
    /// <summary>
    /// Gets called when SCP-049-2 is consuming a corpse.
    /// </summary>
    public static event LabEventHandler<Scp0492ConsumingCorpseEventArgs>? ConsumingCorpse;
    
    /// <summary>
    /// Gets called when SCP-049-2 consumed a corpse.
    /// </summary>
    public static event LabEventHandler<Scp0492ConsumedCorpseEventArgs>? ConsumedCorpse;
}