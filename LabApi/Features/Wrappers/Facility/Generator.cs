using MapGeneration.Distributors;
using System.Collections.Generic;

namespace LabApi.Features.Wrappers.Facility;

/// <summary>
/// The wrapper representing <see cref="Scp079Generator">generators</see>, the in-game gennerators.
/// </summary>
public class Generator
{
    /// <summary>
    /// Contains all the cached <see cref="Scp079Generator">generators</see> in the game, accessible through their <see cref="Scp079Generator"/>.
    /// </summary>
    public static Dictionary<Scp079Generator, Generator> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all <see cref="Generator"/> instances currently in the game.
    /// </summary>
    public static IReadOnlyCollection<Generator> List => Dictionary.Values;

    /// <summary>
    /// A private constructor to prevent external instantiation.
    /// </summary>
    /// <param name="generator">The <see cref="Scp079Generator"/> of the generator.</param>
    private Generator(Scp079Generator generator)
    {
        Dictionary.Add(generator, this);
        Base = generator;
    }

    /// <summary>
    /// The base object.
    /// </summary>
    public Scp079Generator Base { get; }

    /// <summary>
    /// Gets the generator wrapper from the <see cref="Dictionary"/>, or creates a new one if it doesn't exist.
    /// </summary>
    /// <param name="scp079generator">The <see cref="Scp079Generator"/> of the generator.</param>
    /// <returns>The requested generator.</returns>
    public static Generator Get(Scp079Generator scp079generator) =>
        Dictionary.TryGetValue(scp079generator, out Generator generator) ? generator : new Generator(scp079generator);
}
