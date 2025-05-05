using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves an interactable toy.
/// </summary>
public interface IInteractableEvent : IAdminToyEvent
{
    /// <inheritdoc/>
    AdminToy? IAdminToyEvent.AdminToy => Interactable;

    /// <summary>
    /// The interactable toy that is involved in the event.
    /// </summary>
    public InteractableToy? Interactable { get; }
}
