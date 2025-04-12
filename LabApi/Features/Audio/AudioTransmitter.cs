using CentralAuth;
using LabApi.Features.Wrappers;
using MEC;
using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils.Networking;
using VoiceChat;
using VoiceChat.Codec;
using VoiceChat.Codec.Enums;
using VoiceChat.Networking;

namespace LabApi.Features.Audio;

/// <summary>
/// Encodes and sends audio to certain players which plays on speakers with the specified controller id.
/// </summary>
/// <remarks>
/// Its possible to create multiple transmitters with the same controller id, but you must filter the receiving players such that no one player receives audio from multiple transmitters. 
/// This is done to allow you to send different audio to certain players using the same speakers.
/// </remarks>
public class AudioTransmitter
{
    private static readonly float[] EmptyData = new float[FrameSize];

    private static readonly float[] TempSampleData = new float[FrameSize];

    private static readonly byte[] TempEncodedData = new byte[MaxEncodedSize];

    /// <summary>
    /// The sample rate in samples per second.
    /// </summary>
    public const int SampleRate = VoiceChatSettings.SampleRate;

    /// <summary>
    /// The number of samples per audio frame.
    /// </summary>
    public const int FrameSize = VoiceChatSettings.PacketSizePerChannel;

    /// <summary>
    /// The number of seconds between each audio frame.
    /// </summary>
    public const float FramePeriod = (float)FrameSize / SampleRate;

    /// <summary>
    /// The max number of bytes allowed to be encoded per audio frame.
    /// </summary>
    public const int MaxEncodedSize = VoiceChatSettings.MaxEncodedSize;

    /// <summary>
    /// The <see cref="SpeakerToy.ControllerId"/> of the speakers to play the audio on.
    /// </summary>
    public readonly byte ControllerId;

    /// <summary>
    /// The queued audio clips.
    /// Includes the current playing clip.
    /// </summary>
    public readonly Queue<float[]> AudioClipSamples = [];

    /// <summary>
    /// The predicate for determining which players receive audio.
    /// </summary>
    /// <remarks>
    /// If <see langword="null"/>, all authenticated players will receive audio packets.
    /// </remarks>
    public Func<Player, bool>? ValidPlayers = null;

    /// <summary>
    /// Whether the last queued clip will loop.
    /// </summary>
    public bool Looping = false;

    /// <summary>
    /// The position in samples of the current clip. 
    /// </summary>
    public int CurrentPosition = 0;

    /// <summary>
    /// Number of samples in the current clip.
    /// </summary>
    public int CurrentSampleCount => currentSamples == EmptyData ? 0 : currentSamples.Length;

    /// <summary>
    /// Whether playback is active and can be stopped or paused.
    /// </summary>
    public bool IsPlaying => update.IsRunning && !IsPaused;

    /// <summary>
    /// Whether playback is paused and can be resumed.
    /// </summary>
    public bool IsPaused => update.IsAliveAndPaused;

    private readonly OpusEncoder opusEncoder;

    private CoroutineHandle update;

    private double targetTime = 0.0;

    private bool breakCurrent = false;

    private float[] currentSamples = EmptyData;

    private bool CanLoop => AudioClipSamples.Count == 0 && Looping;

    /// <summary>
    /// Creates a new audio transmitter for the specified controller.
    /// </summary>
    /// <param name="controllerId">The controller specified by its Id.</param>
    /// <param name="type">What kind of audio the encoder should optimise's for.</param>
    public AudioTransmitter(byte controllerId, OpusApplicationType type = OpusApplicationType.Audio)
    {
        ControllerId = controllerId;
        opusEncoder = new OpusEncoder(type);
    }

    /// <summary>
    /// Plays the PCM samples on the current controller.
    /// </summary>
    /// <remarks>
    /// Samples are played at a sample rate of <see cref="SampleRate"/>, mono channel (non interleaved data) with ranges from -1.0f to 1.0f.
    /// </remarks>
    /// <param name="samples">The PCM samples.</param>
    /// <param name="queue">Whether to queue the audio if audio is already playing, otherwise overrides the current audio.</param>
    /// <param name="loop">
    /// Whether to loop this clip. 
    /// Loop ends if another clip is played either immediately if not queued or at the end of the loop if next clip was queued.
    /// </param>
    public void Play(float[] samples, bool queue, bool loop)
    {
        if (samples.IsEmpty())
            throw new InvalidOperationException($"Audio clip samples must not be empty");

        if (!queue)
        {
            breakCurrent = true;
            AudioClipSamples.Clear();
            CurrentPosition = 0;
        }

        AudioClipSamples.Enqueue(samples);
        Looping = loop;

        if (!update.IsRunning)
            update = Timing.RunCoroutine(Transmit(), Segment.Update);
    }

    /// <summary>
    /// Pauses transmission of audio.
    /// </summary>
    public void Pause()
    {
        update.IsAliveAndPaused = true;
    }

    /// <summary>
    /// Resumes transmission of audio.
    /// </summary>
    public void Resume()
    {
        update.IsAliveAndPaused = false;
        targetTime = NetworkTime.time;
    }

    /// <summary>
    /// Skips the current or queued clips.
    /// Includes the current clip.
    /// </summary>
    /// <param name="count">The number of queued audios clips to skip.</param>
    public void Skip(int count)
    {
        if (count == 0)
            return;

        breakCurrent = true;
        CurrentPosition = 0;
        for (int i = 1; i < count; i++)
            AudioClipSamples.Dequeue();
    }

    /// <summary>
    /// Stops transmission of audio.
    /// </summary>
    public void Stop()
    {
        Timing.KillCoroutines(update);
        AudioClipSamples.Clear();
        CurrentPosition = 0;
    }

    private IEnumerator<float> Transmit()
    {
        float root2 = MathF.Sqrt(2.0f);
        targetTime = NetworkTime.time;
        while (!AudioClipSamples.IsEmpty())
        {
            currentSamples = AudioClipSamples.Dequeue();

            breakCurrent = false;
            while (!breakCurrent && (CanLoop || CurrentPosition < currentSamples.Length))
            {
                try
                {
                    while (targetTime < NetworkTime.time)
                    {
                        int read = 0;
                        while (read != FrameSize)
                        {
                            int remaining = FrameSize - read;
                            int count = Mathf.Max(Mathf.Min(CurrentPosition + remaining, currentSamples.Length) - CurrentPosition, 0);
                            Array.Copy(currentSamples, CurrentPosition, TempSampleData, read, count);

                            if (remaining == count)
                                CurrentPosition += count;
                            else
                            {
                                CurrentPosition = 0;
                                if (!CanLoop)
                                    currentSamples = AudioClipSamples.IsEmpty() ? EmptyData : AudioClipSamples.Dequeue();
                            }

                            read += count;
                        }

                        // Client sided bug causes output to be 3db quieter than expected
                        // so we correct for that here
                        for (int i = 0; i < TempSampleData.Length; i++)
                            TempSampleData[i] *= root2;

                        int length = opusEncoder.Encode(TempSampleData, TempEncodedData, FrameSize);
                        if (length > 2)
                        {
                            AudioMessage msg = new()
                            {
                                ControllerId = ControllerId,
                                DataLength = length,
                                Data = TempEncodedData,
                            };

                            if (ValidPlayers != null)
                                msg.SendToHubsConditionally(x => x.Mode != ClientInstanceMode.Unverified && ValidPlayers.Invoke(Player.Get(x)));
                            else
                                msg.SendToAuthenticated();
                        }

                        targetTime += FramePeriod;
                    }
                }
                catch (Exception ex)
                {
                    Console.Logger.Error(ex);
                    CurrentPosition = 0;
                    yield break;
                }

                yield return Timing.WaitForOneFrame;
            }
        }

        CurrentPosition = 0;
    }
}
