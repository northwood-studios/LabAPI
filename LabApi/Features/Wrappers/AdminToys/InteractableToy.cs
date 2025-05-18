using AdminToys;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for the <see cref="InvisibleInteractableToy"/> class.
/// </summary>
public class InteractableToy : AdminToy
{
    /// <summary>
    /// Contains all the interactable toys, accessible through their <see cref="Base"/>.
    /// </summary>
    public new static Dictionary<InvisibleInteractableToy, InteractableToy> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="InteractableToy"/>.
    /// </summary>
    public new static IReadOnlyCollection<InteractableToy> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseInteractableToy">The base <see cref="InvisibleInteractableToy"/> object.</param>
    internal InteractableToy(InvisibleInteractableToy baseInteractableToy)
        :base(baseInteractableToy)
    {
        Dictionary.Add(baseInteractableToy, this);
        Base = baseInteractableToy;
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
    /// The <see cref="InvisibleInteractableToy"/> object.
    /// </summary>
    public new InvisibleInteractableToy Base { get; }

    /// <summary>
    /// Event called when a <see cref="Player"/> interacts with the toy.
    /// </summary>
    public event Action<Player> OnInteracted
    {
        add
        {
            if (InternalOnInteracted == null)
                Base.OnInteracted += InvokeOnInteracted;

            InternalOnInteracted += value;
        }
        remove
        {
            InternalOnInteracted -= value;

            if (InternalOnInteracted == null)
                Base.OnInteracted -= InvokeOnInteracted;
        }
    }

    /// <summary>
    /// Event called when a <see cref="Player"/> initiates a search on the toy.
    /// </summary>
    public event Action<Player> OnSearching
    {
        add
        {
            if (InternalOnSearching == null)
                Base.OnSearching += InvokeOnSearching;

            InternalOnSearching += value;
        }
        remove
        {
            InternalOnSearching -= value;

            if (InternalOnSearching == null)
                Base.OnSearching -= InvokeOnSearching;
        }
    }

    /// <summary>
    /// Event called when a <see cref="Player"/> completes a search on the toy.
    /// </summary>
    public event Action<Player> OnSearched
    {
        add
        {
            if (InternalOnSearched == null)
                Base.OnSearched += InvokeOnSearched;

            InternalOnSearched += value;
        }
        remove
        {
            InternalOnSearched -= value;

            if (InternalOnSearched == null)
                Base.OnSearched -= InvokeOnSearched;
        }
    }

    /// <summary>
    /// Event called when a <see cref="Player"/> aborts their search on the toy.
    /// </summary>
    public event Action<Player> OnSearchAborted
    {
        add
        {
            if (InternalOnSearchAborted == null)
                Base.OnSearched += InvokeOnSearchAborted;

            InternalOnSearchAborted += value;
        }
        remove
        {
            InternalOnSearchAborted -= value;

            if (InternalOnSearchAborted == null)
                Base.OnSearched -= InvokeOnSearchAborted;
        }
    }

    /// <summary>
    /// Gets or sets the shape of the collider used for interactions.
    /// </summary>
    public InvisibleInteractableToy.ColliderShape Shape
    {
        get => Base.Shape;
        set => Base.NetworkShape = value;
    }

    /// <summary>
    /// Gets or sets the interaction duration in seconds.
    /// </summary>
    /// <remarks>
    /// A value of 0 indicates that this toy is not searchable like a pickup and will only fire OnInteracted events.
    /// A value greater than 0 indicates that this toy is searchable like a pickup and will only fire OnSearch events.
    /// </remarks>
    public float InteractionDuration
    {
        get => Base.InteractionDuration;
        set => Base.NetworkInteractionDuration = value;
    }

    /// <summary>
    /// Gets or sets whether a search can be started by the client.
    /// </summary>
    /// <remarks>
    /// If the toy is considered searchable see <see cref="InteractionDuration"/>, this prevents a search from being initiated by the client if <see langword="true"/>.
    /// Useful if you want behaviour similar to pickups where only 1 player can search the toy at once.
    /// Note. Unlike pickups this property is not set automatically and by default is never used.
    /// </remarks>
    public bool IsLocked
    {
        get => Base.IsLocked;
        set => Base.NetworkIsLocked = value;
    }

    /// <summary>
    /// Gets whether the client can initiate a search based on the current state of the toy.
    /// </summary>
    public bool CanSearch => Base.CanSearch;

    private event Action<Player>? InternalOnInteracted;

    private event Action<Player>? InternalOnSearching;

    private event Action<Player>? InternalOnSearched;

    private event Action<Player>? InternalOnSearchAborted;

    private void InvokeOnInteracted(ReferenceHub hub) => InternalOnInteracted?.Invoke(Player.Get(hub));

    private void InvokeOnSearching(ReferenceHub hub) => InternalOnSearching?.Invoke(Player.Get(hub));

    private void InvokeOnSearched(ReferenceHub hub) => InternalOnSearched?.Invoke(Player.Get(hub));

    private void InvokeOnSearchAborted(ReferenceHub hub) => InternalOnSearchAborted?.Invoke(Player.Get(hub));

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static InteractableToy Create(Transform? parent = null, bool networkSpawn = true)
        => Create(Vector3.zero, parent, networkSpawn);

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static InteractableToy Create(Vector3 position, Transform? parent = null, bool networkSpawn = true)
        => Create(position, Quaternion.identity, parent, networkSpawn);

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static InteractableToy Create(Vector3 position, Quaternion rotation, Transform? parent = null, bool networkSpawn = true)
        => Create(position, rotation, Vector3.one, parent, networkSpawn);

    /// <summary>
    /// Creates a new interactable toy.
    /// </summary>
    /// <param name="position">The initial local position.</param>
    /// <param name="rotation">The initial local rotation.</param>
    /// <param name="scale">The initial local scale.</param>
    /// <param name="parent">The parent transform.</param>
    /// <param name="networkSpawn">Whether to spawn the toy on the client.</param>
    /// <returns>The created interactable toy.</returns>
    public static InteractableToy Create(Vector3 position, Quaternion rotation, Vector3 scale, Transform? parent = null, bool networkSpawn = true)
    {
        InteractableToy toy = Get(Create<InvisibleInteractableToy>(position, rotation, scale, parent));

        if (networkSpawn)
            toy.Spawn();

        return toy;
    }

    /// <summary>
    /// Gets the interactable toy wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="InvisibleInteractableToy"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="baseInteractableToy">The <see cref="Base"/> of the interactable toy.</param>
    /// <returns>The requested interactable toy or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(baseInteractableToy))]
    public static InteractableToy? Get(InvisibleInteractableToy? baseInteractableToy)
    {
        if (baseInteractableToy == null)
            return null;

        return Dictionary.TryGetValue(baseInteractableToy, out InteractableToy item) ? item : (InteractableToy)CreateAdminToyWrapper(baseInteractableToy);
    }

    /// <summary>
    /// Tries to get the interactable toy wrapper from the <see cref="Dictionary"/>.
    /// </summary>
    /// <param name="baseInteractableToy">The <see cref="Base"/> of the interactable toy.</param>
    /// <param name="interactableToy">The requested interactable toy.</param>
    /// <returns>True if the interactable toy exists, otherwise false.</returns>
    public static bool TryGet(InvisibleInteractableToy? baseInteractableToy, [NotNullWhen(true)] out InteractableToy? interactableToy)
    {
        interactableToy = Get(baseInteractableToy);
        return interactableToy != null;
    }
}