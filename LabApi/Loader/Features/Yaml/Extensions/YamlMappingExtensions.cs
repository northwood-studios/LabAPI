using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace LabApi.Loader.Features.Yaml.Extensions;

/// <summary>
/// Extension class for <see cref="IEmitter"/> and <see cref="IParser"/>.
/// </summary>
public static class YamlMappingExtensions
{
    /// <summary>
    /// Emits 2 scalar values as key value pair.
    /// </summary>
    /// <param name="emitter">This emitter to emit from.</param>
    /// <param name="key">The key value.</param>
    /// <param name="value">The value.</param>
    public static void EmitMapping(this IEmitter emitter, string key, string value)
    {
        emitter.Emit(new Scalar(key));
        emitter.Emit(new Scalar(value));
    }

    /// <summary>
    /// Attempts to read a key value pair.
    /// </summary>
    /// <param name="parser">This parser to read from.</param>
    /// <param name="key">The key value.</param>
    /// <param name="value">The value.</param>
    /// <returns>Whether was the kvp succesfully parsed.</returns>
    public static bool TryReadMapping(this IParser parser, out string key, out string value)
    {
        key = string.Empty;
        value = string.Empty;

        if (!parser.TryConsume(out Scalar keyScalar) || !parser.TryConsume(out Scalar valueScalar))
            return false;

        key = keyScalar.Value.Trim();
        value = valueScalar.Value.Trim();
        return true;
    }
}
