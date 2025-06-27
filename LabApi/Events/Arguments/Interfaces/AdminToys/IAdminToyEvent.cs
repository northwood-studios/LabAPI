using LabApi.Features.Wrappers;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that involves an admin toy.
/// </summary>
public interface IAdminToyEvent
{
    /// <summary>
    /// The admin toy that is involved in the event.
    /// </summary>
    AdminToy? AdminToy { get; }
}