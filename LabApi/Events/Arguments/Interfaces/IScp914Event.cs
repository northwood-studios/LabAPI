using Scp914;

namespace LabApi.Events.Arguments.Interfaces;

/// <summary>
/// Represents an event that is related to SCP-914.
/// </summary>
public interface IScp914Event
{
    /// <summary>
    /// The current knob setting of SCP-914.
    /// </summary>
    public Scp914KnobSetting KnobSetting { get; }
}