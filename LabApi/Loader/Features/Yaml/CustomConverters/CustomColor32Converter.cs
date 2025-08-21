using System;
using UnityEngine;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace LabApi.Loader.Features.Yaml.CustomConverters;

/// <summary>
/// A custom class for serializing/deserializing <see cref="Color32"/>.
/// </summary>
public class CustomColor32Converter : IYamlTypeConverter
{
    /// <inheritdoc/>
    public object ReadYaml(IParser parser, Type type)
    {
        Scalar scalar = parser.Consume<Scalar>();

        string colorText = scalar.Value;
        if (!ColorUtility.TryParseHtmlString(colorText, out Color color))
        {
            throw new ArgumentException($"Unable to parse {nameof(Color32)} value of {colorText}");
        }

        return (Color32)color;
    }

    /// <inheritdoc/>
    public void WriteYaml(IEmitter emitter, object? value, Type type)
    {
        Color32? color = (Color32?)value;
        emitter.Emit(new Scalar(color?.ToHex() ?? Color.white.ToHex()));
    }

    /// <inheritdoc/>
    public bool Accepts(Type type)
    {
        return type == typeof(Color32);
    }
}
