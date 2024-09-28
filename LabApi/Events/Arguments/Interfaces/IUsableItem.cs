using InventorySystem.Items.Usables;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves an usable item.
/// </summary>
public interface IUsableItem
{
    /// <summary>
    /// The item that is involved in the event.
    /// </summary>
    public UsableItem Item { get; }
}