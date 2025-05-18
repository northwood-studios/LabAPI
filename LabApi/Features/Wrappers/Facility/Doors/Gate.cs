using Interactables.Interobjects;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// A wrapper representing the <see cref="PryableDoor"/>
/// </summary>
public class Gate : Door
{
    /// <summary>
    /// Contains all the cached <see cref="Gate"/> instances, accessible through their <see cref="PryableDoor"/>.
    /// </summary>
    public new static Dictionary<PryableDoor, Gate> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="Gate"/> instances currently in the game.
    /// </summary>
    public new static IReadOnlyCollection<Gate> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="pryableDoor">The base <see cref="PryableDoor"/> object.</param>
    internal Gate(PryableDoor pryableDoor)
        : base(pryableDoor)
    {
        Dictionary.Add(pryableDoor, this);
        Base = pryableDoor;
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
    /// The base <see cref="PryableDoor"/> object.
    /// </summary>
    public new PryableDoor Base { get; }

    /// <summary>
    /// Gets or sets whether SCP-106 can pass through the door when its not closed and locked.
    /// </summary>
    public bool Is106Passable
    {
        get => Base.IsScp106Passable;
        set => Base.IsScp106Passable = value;
    }

    /// <summary>
    /// Try pry the gate with the specified player.
    /// </summary>
    /// <param name="player">The player to pry the gate.</param>
    /// <returns>True if the player can pry the gate, otherwise false.</returns>
    public bool TryPry(Player player) => Base.TryPryGate(player.ReferenceHub);

    /// <summary>
    /// Play the Pry animation.
    /// </summary>
    public void Pry() => Base.RpcPryGate();

    /// <summary>
    /// Gets the <see cref="Gate"/> wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="pryableDoor">The <see cref="PryableDoor"/> of the door.</param>
    /// <returns>The requested door wrapper or null if the input was null.</returns>
    [return: NotNullIfNotNull(nameof(pryableDoor))]
    public static Gate? Get(PryableDoor? pryableDoor)
    {
        if (pryableDoor == null)
            return null;

        if (Dictionary.TryGetValue(pryableDoor, out Gate door))
            return door;

        return (Gate)CreateDoorWrapper(pryableDoor);
    }
}