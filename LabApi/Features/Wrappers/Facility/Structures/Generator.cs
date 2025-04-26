﻿using Interactables.Interobjects.DoorUtils;
using MapGeneration.Distributors;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using static MapGeneration.Distributors.Scp079Generator;
using Generators;
using MapGeneration;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="Scp079Generator">generators</see>, the in-game generators.
/// </summary>
public class Generator : Structure
{
    /// <summary>
    /// Contains all the cached <see cref="Scp079Generator">generators</see> in the game, accessible through their <see cref="Scp079Generator"/>.
    /// </summary>
    public new static Dictionary<Scp079Generator, Generator> Dictionary { get; } = [];

    /// <summary>
    /// Contains generators in a list by room they are in. Generators that have been spawned without an assigned room are not inside of this collection.
    /// </summary>
    private static Dictionary<Room, List<Generator>> GeneratorsByRoom { get; } = [];

    /// <summary>
    /// A reference to all <see cref="Generator"/> instances currently in the game.
    /// </summary>
    public new static IReadOnlyCollection<Generator> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="generator">The <see cref="Scp079Generator"/> of the generator.</param>
    internal Generator(Scp079Generator generator)
        : base(generator)
    {
        Dictionary.Add(generator, this);
        Base = generator;

        TryRegisterByRoom();
    }

    /// <summary>
    /// Initializes the generators by room caching for map generation.
    /// </summary>
    [InitializeWrapper]
    internal static void InitializeCaching()
    {
        SeedSynchronizer.OnGenerationFinished += SeedSynchronizer_OnGenerationFinished;
    }

    private static void SeedSynchronizer_OnGenerationFinished()
    {
        foreach (Generator generator in List)
            generator.TryRegisterByRoom();
    }

    /// <summary>
    /// An internal method remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();

        if (Base.ParentRoom == null)
        {
            Dictionary.Remove(Base);
            return;
        }

        Room? room = Room.Get(Base.ParentRoom);

        if (room == null) // Room is null after round restart, try find it by iterating over the existing dictionary
        {
            Room? potentialRoom = null;
            foreach (var kvp in GeneratorsByRoom)
            {
                if (kvp.Value.Contains(this))
                {
                    potentialRoom = kvp.Key;
                    break;
                }
            }

            room = potentialRoom;
        }

        if (room != null && GeneratorsByRoom.TryGetValue(room, out List<Generator> list))
        {
            list.Remove(this);

            if (list.Count == 0)
            {
                GeneratorsByRoom.Remove(room);
            }
        }

        Dictionary.Remove(Base);
    }

    /// <summary>
    /// The base object.
    /// </summary>
    public new Scp079Generator Base { get; }

    /// <summary>
    /// Gets or sets the activation time it takes for generator to go from <see cref="TotalActivationTime">maximum time</see> (this value) to 0.
    /// </summary>
    public float TotalActivationTime
    {
        get => Base.TotalActivationTime;
        set => Base.TotalActivationTime = value;
    }

    /// <summary>
    /// Gets or sets the activation time it takes for generator to go from 0 to <see cref="TotalActivationTime">maximum time</see>.
    /// </summary>
    public float TotalDeactivationTime
    {
        get => Base.TotalDeactivationTime;
        set => Base.TotalDeactivationTime = value;
    }

    /// <summary>
    /// Gets or sets the required keycard permissions to unlock the generator.
    /// </summary>
    public DoorPermissionFlags RequiredPermissions
    {
        get => Base.RequiredPermissions;
        set => Base.RequiredPermissions = value;
    }

    /// <summary>
    /// Gets the dropdown speed at which is generator countdown going back up to the maximum value.
    /// </summary>
    public float DropdownSpeed => Base.DropdownSpeed;

    /// <summary>
    /// Gets whether the generation is ready to be activated.
    /// </summary>
    public bool ActivationReady => Base.ActivationReady;

    /// <summary>
    /// Gets or sets whether the generator is opened.
    /// </summary>
    public bool IsOpen
    {
        get => Base.IsOpen;
        set => Base.IsOpen = value;
    }

    /// <summary>
    /// Gets or sets whether the generator is unlocked.
    /// </summary>
    public bool IsUnlocked
    {
        get => Base.IsUnlocked;
        set => Base.IsUnlocked = value;
    }

    /// <summary>
    /// Gets the time it takes the generator to be activated (lever pulled).
    /// </summary>
    public float ActivationTime => Base.ActivationTime;

    /// <summary>
    ///	Gets or sets whether the generator is engaged.
    /// </summary>
    public bool Engaged
    {
        get => Base.Engaged;
        set => Base.Engaged = value;
    }

    /// <summary>
    /// Gets or sets whether the generator is activating.
    /// </summary>
    public bool Activating
    {
        get => Base.Activating;
        set => Base.Activating = value;
    }

    /// <summary>
    /// Gets or sets the remaining amount of seconds till activation.
    /// </summary>
    public short RemainingTime
    {
        get => Base.RemainingTime;
        set => Base.RemainingTime = value;
    }

    /// <summary>
    /// Runs the interaction of specified <see cref="Player"/> on specified <see cref="GeneratorColliderId"/> collider.
    /// </summary>
    /// <param name="player">The player to trigger the interaction.</param>
    /// <param name="collider">The <see cref="GeneratorColliderId"/> triggered.</param>
    public void ServerInteract(Player player, GeneratorColliderId collider) => Base.ServerInteract(player.ReferenceHub, (byte)collider);

    private void TryRegisterByRoom()
    {
        foreach (var kvp in GeneratorsByRoom)
        {
            if (kvp.Value.Contains(this))
                return;
        }

        if (Base.ParentRoom == null)
            return;
        Room? room = Room.Get(Base.ParentRoom);

        if (room == null)
            return;

        if (!GeneratorsByRoom.TryGetValue(room, out List<Generator> list))
        {
            list = new List<Generator>();
            GeneratorsByRoom.Add(room, list);
        }

        list.Add(this);
    }

    /// <summary>
    /// Plays the denied sound cue on the client.
    /// </summary>
    /// <param name="flags">The permissions used to attempt opening the generator. Used to animate the generator panel.</param>
    public void PlayerDeniedBeep(DoorPermissionFlags flags) => Base.RpcDenied(flags);

    /// <summary>
    /// Gets the generator wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist and the provided <see cref="Scp079Generator"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="scp079Generator">The <see cref="Scp079Generator"/> of the generator.</param>
    /// <returns>The requested wrapper or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(scp079Generator))]
    public static Generator? Get(Scp079Generator? scp079Generator)
    {
        if (scp079Generator == null)
            return null;

        return Dictionary.TryGetValue(scp079Generator, out Generator generator) ? generator : (Generator)CreateStructureWrapper(scp079Generator);
    }

    /// <summary>
    /// Gets the generator wrapper from the <see cref="GeneratorsByRoom"/> or returns <see langword="null"/> if specified room does not have any.
    /// </summary>
    /// <param name="room">Target room.</param>
    /// <param name="generators">Generators found.</param>
    /// <returns>Whether the generator was found.</returns>
    public static bool TryGetFromRoom(Room room, [NotNullWhen(true)] out List<Generator>? generators) => GeneratorsByRoom.TryGetValue(room, out generators);
}