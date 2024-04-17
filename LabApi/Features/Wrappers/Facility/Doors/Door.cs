using System.Collections.Generic;
using Interactables.Interobjects.DoorUtils;

namespace LabApi.Features.Wrappers.Facility.Doors;

/// <summary>
/// The wrapper representing <see cref="DoorVariant">door variants</see>, the in-game doors.
/// </summary>
public class Door
{
    /// <summary>
    /// Contains all the cached <see cref="Door">doors</see> in the game, accessible through their <see cref="DoorVariant"/>.
    /// </summary>
    public static Dictionary<DoorVariant, Door> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="Door"/> instances currently in the game.
    /// </summary>
    public static IReadOnlyCollection<Door> List => Dictionary.Values;
    
    /// <summary>
    /// A private constructor to prevent external instantiation.
    /// </summary>
    /// <param name="doorVariant">The <see cref="DoorVariant"/> of the door.</param>
    private Door(DoorVariant doorVariant)
    {
        Dictionary.Add(doorVariant, this);
        DoorVariant = doorVariant;
    }
    
    /// <summary>
    /// The <see cref="DoorVariant"/> of the door.
    /// </summary>
    public DoorVariant DoorVariant { get; }
    
    /// <summary>
    /// Gets the door wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="doorVariant">The <see cref="DoorVariant"/> of the door.</param>
    /// <returns>The requested door.</returns>
    public static Door Get(DoorVariant doorVariant) =>
        Dictionary.TryGetValue(doorVariant, out Door door) ? door : new Door(doorVariant);
}