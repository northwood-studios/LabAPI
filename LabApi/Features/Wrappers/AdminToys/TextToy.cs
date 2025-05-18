using Mirror;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using BaseTextToy = AdminToys.TextToy;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper for the <see cref="BaseTextToy"/> class. <br/>
/// Toy with changable text and formatting arguments.
/// </summary>
public class TextToy : AdminToy
{
    /// <summary>
    /// Contains all the text toys, accessible through their <see cref="Base"/>.
    /// </summary>
    public new static Dictionary<BaseTextToy, TextToy> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="TextToy"/>.
    /// </summary>
    public new static IReadOnlyCollection<TextToy> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseToy">The base <see cref="BaseTextToy"/> object.</param>
    internal TextToy(BaseTextToy baseToy) : base(baseToy)
    {
        Base = baseToy;
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
    /// The <see cref="BaseTextToy"/> object.
    /// </summary>
    public new BaseTextToy Base { get; }

    /// <summary>
    /// Gets or sets the base text format used when formatting the final text string.<br/>
    /// Text can be formatted and arguments are replaced with <see cref="Arguments"/>.
    /// </summary>
    public string TextFormat
    {
        get => Base.TextFormat;
        set => Base.TextFormat = value;
    }

    /// <summary>
    /// Gets or sets the size of text display used by TMP.
    /// </summary>
    public Vector2 DisplaySize
    {
        get => Base.DisplaySize;
        set => Base.DisplaySize = value;
    }

    /// <summary>
    /// Gets the arguments used while formatting the <see cref="TextFormat"/>.<br/>
    /// Missing arguments for <see cref="TextFormat"/> are not replaced and any extra arguments are ignored.
    /// </summary>
    public SyncList<string> Arguments => Base.Arguments;

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static TextToy Create(Transform? parent = null, bool networkSpawn = true)
        => Create(Vector3.zero, parent, networkSpawn);

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static TextToy Create(Vector3 position, Transform? parent = null, bool networkSpawn = true)
        => Create(position, Quaternion.identity, parent, networkSpawn);

    /// <inheritdoc cref="Create(Vector3, Quaternion, Vector3, Transform?, bool)"/>
    public static TextToy Create(Vector3 position, Quaternion rotation, Transform? parent = null, bool networkSpawn = true)
        => Create(position, rotation, Vector3.one, parent, networkSpawn);

    /// <summary>
    /// Creates a new text toy.
    /// </summary>
    /// <param name="position">The initial local position.</param>
    /// <param name="rotation">The initial local rotation.</param>
    /// <param name="scale">The initial local scale.</param>
    /// <param name="parent">The parent transform.</param>
    /// <param name="networkSpawn">Whether to spawn the toy on the client.</param>
    /// <returns>The created text toy.</returns>
    public static TextToy Create(Vector3 position, Quaternion rotation, Vector3 scale, Transform? parent = null, bool networkSpawn = true)
    {
        TextToy toy = Get(Create<BaseTextToy>(position, rotation, scale, parent));

        if (networkSpawn)
            toy.Spawn();

        return toy;
    }

    /// <summary>
    /// Gets the text toy wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseTextToy"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="baseTextToy">The <see cref="Base"/> of the text toy.</param>
    /// <returns>The requested text toy or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(baseTextToy))]
    public static TextToy? Get(BaseTextToy? baseTextToy)
    {
        if (baseTextToy == null)
            return null;

        return Dictionary.TryGetValue(baseTextToy, out TextToy item) ? item : (TextToy)CreateAdminToyWrapper(baseTextToy);
    }

    /// <summary>
    /// Tries to get the text toy wrapper from the <see cref="Dictionary"/>.
    /// </summary>
    /// <param name="baseTextToy">The <see cref="Base"/> of the text toy.</param>
    /// <param name="textToy">The requested text toy.</param>
    /// <returns><see langword="True"/> if the text toy exists, otherwise <see langword="false"/>.</returns>
    public static bool TryGet(BaseTextToy? baseTextToy, [NotNullWhen(true)] out TextToy? textToy)
    {
        textToy = Get(baseTextToy);
        return textToy != null;
    }
}