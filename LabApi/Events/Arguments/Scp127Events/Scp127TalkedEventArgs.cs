using InventorySystem.Items.Firearms.Modules.Scp127;
using InventorySystem.Items.Firearms;
using LabApi.Events.Arguments.Interfaces;
using LabApi.Features.Wrappers;
using System;
using static InventorySystem.Items.Firearms.Modules.Scp127.Scp127VoiceTriggerBase;

namespace LabApi.Events.Arguments.Scp127Events;

/// <summary>
/// Represents the arguments for the <see cref="Handlers.Scp127Events.Talked"/> event.
/// </summary>
public class Scp127TalkedEventArgs : EventArgs, IScp127ItemEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Scp127TalkingEventArgs"/> class.
    /// </summary>
    /// <param name="weapon">The Scp-127 firearm.</param>
    /// <param name="voiceLine">The voiceline to be played.</param>
    /// <param name="priority">The priority of the voiceline.</param>
    public Scp127TalkedEventArgs(Firearm weapon, Scp127VoiceLinesTranslation voiceLine, VoiceLinePriority priority)
    {
        Scp127Item = (Scp127Firearm)FirearmItem.Get(weapon);
        VoiceLine = voiceLine;
        Priority = priority;
    }

    /// <summary>
    /// Gets the SCP-127 firearm.
    /// </summary>
    public Scp127Firearm Scp127Item { get; }

    /// <summary>
    /// Gets the voiceline the SCP-127 played.
    /// </summary>
    public Scp127VoiceLinesTranslation VoiceLine { get; }

    /// <summary>
    /// Gets the priority the voiceline has been played with.
    /// </summary>
    public VoiceLinePriority Priority { get; }
}
