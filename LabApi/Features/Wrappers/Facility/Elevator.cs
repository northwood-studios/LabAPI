using Interactables.Interobjects;
using MapGeneration.Distributors;
using System.Collections.Generic;

namespace LabApi.Features.Wrappers.Facility;

/// <summary>
/// The wrapper representing <see cref="ElevatorChamber">elevators</see>, the in-game elevators.
/// </summary>
public class Elevator
{
    /// <summary>
    /// Contains all the cached <see cref="ElevatorChamber">generators</see> in the game, accessible through their <see cref="Scp079Generator"/>.
    /// </summary>
    public static Dictionary<ElevatorChamber, Elevator> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="Elevator"/> instances currently in the game.
    /// </summary>
    public static IReadOnlyCollection<Elevator> List => Dictionary.Values;

    /// <summary>
    /// A private constructor to prevent external instantiation.
    /// </summary>
    /// <param name="elevator">The <see cref="ElevatorChamber"/> of the elevator.</param>
    private Elevator(ElevatorChamber elevator)
    {
        Dictionary.Add(elevator, this);
        Base = elevator;
    }

    /// <summary>
    /// The base object.
    /// </summary>
    public ElevatorChamber Base { get; }

    /// <summary>
    /// Gets the elevator wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="elevatorChamber">The <see cref="ElevatorChamber"/> of the elevator.</param>
    /// <returns>The requested elevator.</returns>
    public static Elevator Get(ElevatorChamber elevatorChamber) =>
        Dictionary.TryGetValue(elevatorChamber, out Elevator generator) ? generator : new Elevator(elevatorChamber);
}
