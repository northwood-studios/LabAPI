using LabApi.Features.Wrappers;

namespace LabApi.Features.Enums;

/// <summary>
/// Represents the different types of audio mixer groups that can be assigned to a <see cref="SpeakerToy"/>.
/// Audio mixers group starting with Sfx are effected by the player Sound effect volume while ones with Vc are effected by Voice chat volume.
/// </summary>
public enum SpeakerToyMixerGroup
{
	/// <summary>
	/// Mixer group used for general sound effects audio.
	/// No audio effects are applied in this group.
	/// </summary>
	SfxGeneral,

	/// <summary>
	/// Mixer group used for CASSIE related broadcasts.
	/// Same audio effects used by CASSIE are applied.
	/// </summary>
	SfxCassie,

	/// <summary>
	/// Mixer group used for weapons/gun shots.
	/// </summary>
	SfxWeapons,

	/// <summary>
	/// Mixer group used to apply reverb.
	/// </summary>
	SfxReverbNoDucking,

	/// <summary>
	/// Mixer group used to apply reverb and audio ducking.
	/// </summary>
	SfxReverbDucking,

	/// <summary>
	/// Mixer group used for the games backgroud music.
	/// </summary>
	SfxMusic,

    /// <summary>
    /// Mixer group used for general voice chat audio.
    /// No audio effects are applied in this group.
    /// </summary>
    VcGeneral,

	/// <summary>
	/// Mixer group used for intercom voice broadcasts.
	/// Same effects used by the intercom are applied.
	/// </summary>
	VcIntercom,

	/// <summary>
	/// Mixer group used for radio voice chat.
	/// Same effects used by radio char are applied.
	/// </summary>
	VcRadio,

	/// <summary>
	/// One of the two mixer groups used by proximity chat.
	/// This is the dry unfiltered part of the proximity voice.
	/// </summary>
	/// <remarks>
	/// True proximity chat cannot be replicated by a speaker toy.
	/// </remarks>
	VcProximity,

    /// <summary>
    /// One of the two mixer group used by proximity chat.
    /// This is the wet reverberated part of the proximity voice.
    /// </summary>
    /// <remarks>
    /// True proximity chat cannot be replicated by a speaker toy.
    /// </remarks>
    VcProximityReverb,

	/// <summary>
	/// Mixer group used by SCP-079 speakers.
	/// Same effects used by SCP-079 speakers are applied.
	/// </summary>
	VcScp079
}