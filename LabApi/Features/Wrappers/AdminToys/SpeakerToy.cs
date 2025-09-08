using LabApi.Features.Audio;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEngine;
using VoiceChat;
using BaseSpeakerToy = AdminToys.SpeakerToy;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for the <see cref="BaseSpeakerToy"/> class
/// </summary>
public class SpeakerToy : AdminToy
{
    /// <summary>
    /// Contains all the speaker toys, accessible through their <see cref="Base"/>.
    /// </summary>
    public new static Dictionary<BaseSpeakerToy, SpeakerToy> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="SpeakerToy"/>.
    /// </summary>
    public new static IReadOnlyCollection<SpeakerToy> List => Dictionary.Values;

    private static readonly Dictionary<byte, AudioTransmitter> TransmitterByControllerId = [];

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseSpeakerToy">The base <see cref="BaseSpeakerToy"/> object.</param>
    internal SpeakerToy(BaseSpeakerToy baseSpeakerToy) 
        : base(baseSpeakerToy)
    {
        Base = baseSpeakerToy;

        if (CanCache)
            Dictionary.Add(baseSpeakerToy, this);
    }

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }

    /// <summary>
    /// The <see cref="BaseSpeakerToy"/> object.
    /// </summary>
    public new BaseSpeakerToy Base { get; }

    /// <summary>
    /// Gets or sets which audio controller to use for playback based on the id.
    /// </summary>
    /// <remarks>
    /// Multiple speakers can have the same controller id allowing them to emit the same audio.
    /// Calling either <see cref="Play(float[], bool, bool)"/> on one of the instances or using the static method <see cref="Play(byte, float[], bool, bool)"/> will play on all of them.
    /// </remarks>
    public byte ControllerId
    {
        get => Base.ControllerId;
        set => Base.NetworkControllerId = value;
    }

    /// <summary>
    /// Gets or sets whether the sound is 3D.
    /// </summary>
    public bool IsSpatial
    {
        get => Base.IsSpatial;
        set => Base.NetworkIsSpatial = value;
    }

    /// <summary>
    /// Gets or sets the volume of the speaker.
    /// </summary>
    public float Volume
    {
        get => Base.Volume;
        set => Base.NetworkVolume = value;
    }

    /// <summary>
    /// Gets or sets the distance where the audio starts to fall off.
    /// </summary>
    public float MinDistance
    {
        get => Base.MinDistance;
        set => Base.NetworkMinDistance = value;
    }

    /// <summary>
    /// Gets or sets the distance where the audio falls off to zero.
    /// </summary>
    public float MaxDistance
    {
        get => Base.MaxDistance;
        set => Base.NetworkMaxDistance = value;
    }

    /// <summary>
    /// Gets or sets the current sample position of the playback head.
    /// </summary>
    public int CurrentPosition
    {
        get => Transmitter.CurrentPosition;
        set => Transmitter.CurrentPosition = value;
    }

    /// <summary>
    /// Gets or sets whether the last clip in the queue will loop.
    /// </summary>
    public bool IsLooping
    {
        get => Transmitter.Looping;
        set => Transmitter.Looping = value;
    }

    /// <inheritdoc cref="AudioTransmitter.IsPlaying"/>
    public bool IsPlaying => Transmitter.IsPlaying;

    /// <inheritdoc cref="AudioTransmitter.IsPaused"/>
    public bool IsPaused => Transmitter.IsPaused;

    /// <summary>
    /// Number of samples in the current audio clip.
    /// </summary>
    public int CurrentSampleCount => Transmitter.CurrentSampleCount;

    /// <summary>
    /// Duration in seconds of the current audio clip.
    /// </summary>
    public float CurrentDuration => CurrentSampleCount * VoiceChatSettings.SampleToDuartionRate;

    /// <summary>
    /// Number of samples left to play in the current audio clip.
    /// </summary>
    public int CurrentRemainingSampleCount => CurrentSampleCount - CurrentPosition;

    /// <summary>
    /// Duration in seconds left to play in the current audio clip.
    /// </summary>
    public float CurrentRemainingDuration => CurrentRemainingSampleCount * VoiceChatSettings.SampleToDuartionRate;

    /// <summary>
    /// Number of queued audio clips.
    /// Does not include the current clip.
    /// </summary>
    public int QueuedClipsCount => Transmitter.AudioClipSamples.Count;

    /// <summary>
    /// Number of samples of all queued clips.
    /// Does not include the current clip.
    /// </summary>
    public int QueuedClipsSampleCount => Transmitter.AudioClipSamples.IsEmpty() ? 0 : Transmitter.AudioClipSamples.Sum(x => x.Length);

    /// <summary>
    /// Duration in seconds of all queued clips.
    /// Does not include the current clip.
    /// </summary>
    public float QueuedClipsDuration => QueuedClipsSampleCount * VoiceChatSettings.SampleToDuartionRate;

    /// <summary>
    /// Gets or sets the predicate for determining which players to send audio to.
    /// If <see langword="null"/> audio will be sent to all authenticated players.
    /// </summary>
    /// <remarks>
    /// Is called on all authenticated players for each audio message that is sent out.
    /// Multiple audio messages can be sent per frame so this may be called 100s of times per frame.
    /// Try to make the check as fast as possible otherwise the performance of the server will be significantly effected.
    /// </remarks>
    public Func<Player, bool>? ValidPlayers
    {
        get => Transmitter.ValidPlayers;
        set => Transmitter.ValidPlayers = value;
    }

    /// <summary>
    /// Gets the audio transmitter for this speakers <see cref="ControllerId"/>. 
    /// </summary>
    /// <remarks>
    /// Speakers can share <see cref="AudioTransmitter"/> instances if they have the same <see cref="ControllerId"/>.
    /// </remarks>
    public AudioTransmitter Transmitter => GetTransmitter(ControllerId);

    /// <inheritdoc cref="AudioTransmitter.Play(float[], bool, bool)"/>
    public void Play(float[] samples, bool queue = true, bool loop = false)
        => Transmitter.Play(samples, queue, loop);

    /// <inheritdoc cref="AudioTransmitter.Pause"/>
    public void Pause() => Transmitter.Pause();

    /// <inheritdoc cref="AudioTransmitter.Resume"/>
    public void Resume() => Transmitter.Resume();

    /// <inheritdoc cref="AudioTransmitter.Skip(int)"/>
    public void Skip(int count) => Transmitter.Skip(count);

    /// <inheritdoc cref="AudioTransmitter.Stop"/>
    public void Stop() => Transmitter.Stop();

    /// <inheritdoc />
    public override string ToString()
    {
        return $"[SpeakerToy: ControllerId={ControllerId}, IsSpatial={IsSpatial}, Volume={Volume}, MinDistance={MinDistance}, MaxDistance={MaxDistance}, IsPlaying={IsPlaying}]";
    }

    /// <summary>
    /// Plays the PCM samples on the current controller.
    /// </summary>
    /// <remarks>
    /// Samples are played at a sample rate of <see cref="AudioTransmitter.SampleRate"/>, mono channel (non interleaved data) with ranges from -1.0f to 1.0f.
    /// </remarks>
    /// <param name="controllerId">The Id of the controller to play audio on.</param>
    /// <param name="samples">The PCM samples.</param>
    /// <param name="queue">Whether to queue the audio if audio is already playing, otherwise overrides the current audio.</param>
    /// <param name="loop">
    /// Whether to loop this clip. 
    /// Loop ends if another clip is played either immediately if not queued or at the end of the loop if next clip was queued.
    /// </param>
    public static void Play(byte controllerId, float[] samples, bool queue = true, bool loop = false)
        => GetTransmitter(controllerId).Play(samples, queue, loop);

    /// <inheritdoc cref="AudioTransmitter.Pause"/>
    /// <param name="controllerId">The Id of the controller to play audio on.</param>
    public static void Pause(byte controllerId) => GetTransmitter(controllerId).Pause();

    /// <inheritdoc cref="AudioTransmitter.Resume"/>
    /// <param name="controllerId">The Id of the controller to play audio on.</param>
    public static void Resume(byte controllerId) => GetTransmitter(controllerId).Resume();

    /// <summary>
    /// Skips the current or queued clips.
    /// Includes the current clip.
    /// </summary>
    /// <param name="controllerId">The Id of the controller to play audio on.</param>
    /// <param name="count">The number of queued audios clips to skip.</param>
    public static void Skip(byte controllerId, int count) => GetTransmitter(controllerId).Skip(count);

    /// <inheritdoc cref="AudioTransmitter.Stop"/>
    /// <param name="controllerId">The Id of the controller to play audio on.</param>
    public static void Stop(byte controllerId) => GetTransmitter(controllerId).Stop();

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static SpeakerToy Create(Transform? parent = null, bool networkSpawn = true)
        => Create(Vector3.zero, parent, networkSpawn);

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static SpeakerToy Create(Vector3 position, Transform? parent = null, bool networkSpawn = true)
        => Create(position, Quaternion.identity, parent, networkSpawn);

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static SpeakerToy Create(Vector3 position, Quaternion rotation, Transform? parent = null, bool networkSpawn = true)
        => Create(position, rotation, Vector3.one, parent, networkSpawn);

    /// <summary>
    /// Creates a new speaker toy.
    /// </summary>
    /// <param name="position">The initial local position.</param>
    /// <param name="rotation">The initial local rotation.</param>
    /// <param name="scale">The initial local scale.</param>
    /// <param name="parent">The parent transform.</param>
    /// <param name="networkSpawn">Whether to spawn the toy on the client.</param>
    /// <returns>The created speaker toy.</returns>
    public static SpeakerToy Create(Vector3 position, Quaternion rotation, Vector3 scale, Transform? parent = null, bool networkSpawn = true)
    {
        SpeakerToy toy = Get(Create<BaseSpeakerToy>(position, rotation, scale, parent));

        if (networkSpawn)
            toy.Spawn();

        return toy;
    }

    /// <summary>
    /// Gets the speaker toy wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseSpeakerToy"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="baseSpeakerToy">The <see cref="Base"/> of the speaker toy.</param>
    /// <returns>The requested speaker toy or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(baseSpeakerToy))]
    public static SpeakerToy? Get(BaseSpeakerToy? baseSpeakerToy)
    {
        if (baseSpeakerToy == null)
            return null;

        return Dictionary.TryGetValue(baseSpeakerToy, out SpeakerToy toy) ? toy : (SpeakerToy)CreateAdminToyWrapper(baseSpeakerToy);
    }

    /// <summary>
    /// Tries to get the speaker toy wrapper from the <see cref="Dictionary"/>.
    /// </summary>
    /// <param name="baseSpeakerToy">The <see cref="Base"/> of the speaker toy.</param>
    /// <param name="speakerToy">The requested speaker toy.</param>
    /// <returns><see langword="True"/> if the speaker exists, otherwise <see langword="false"/>.</returns>
    public static bool TryGet(BaseSpeakerToy? baseSpeakerToy, [NotNullWhen(true)] out SpeakerToy? speakerToy)
    {
        speakerToy = Get(baseSpeakerToy);
        return speakerToy != null;
    }

    /// <summary>
    /// Gets the <see cref="AudioTransmitter"/> for the <see cref="ControllerId"/>.
    /// If one does not exists, a new one is created for the id.
    /// </summary>
    /// <param name="controllerId">The <see cref="ControllerId"/> for the transmitter.</param>
    /// <returns>Cached transmitter.</returns>
    public static AudioTransmitter GetTransmitter(byte controllerId)
    {
        if (!TransmitterByControllerId.TryGetValue(controllerId, out AudioTransmitter transmitter))
        {
            transmitter = new(controllerId);
            TransmitterByControllerId.Add(controllerId, transmitter);
        }

        return transmitter;
    }
}
