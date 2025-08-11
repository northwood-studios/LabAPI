using LabApi.Loader.Features.Yaml.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Pool;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace LabApi.Loader.Features.Yaml.CustomConverters;

/// <summary>
/// A custom class for serializing/deserializing <see cref="Color"/>.
/// </summary>
public class CustomColorConverter : IYamlTypeConverter
{
    /// <inheritdoc/>
    public object? ReadYaml(IParser parser, Type type)
    {
        parser.Consume<MappingStart>();

        Dictionary<string, float> storedValues = DictionaryPool<string, float>.Get();

        try
        {
            for (int i = 0; i <= 3; i++)
            {
                if (!parser.TryReadMapping(out string key, out string val))
                {
                    throw new ArgumentException($"Unable to parse {nameof(Color)}, no component at index {i} provided");
                }

                if (!(key is "r" or "g" or "b" or "a"))
                {
                    throw new ArgumentException($"Unable to parse {nameof(Color)}, invalid component name {key}. Only 'r', 'g', 'b' and 'a' are allowed");
                }

                storedValues[key] = float.Parse(val, CultureInfo.InvariantCulture);
            }

            parser.Consume<MappingEnd>();

            Color value = new(storedValues["r"], storedValues["g"], storedValues["b"], storedValues["a"]);
            return value;
        }
        catch (ArgumentException)
        {
            throw;
        }
        finally
        {
            DictionaryPool<string, float>.Release(storedValues);
        }
    }

    /// <inheritdoc/>
    public void WriteYaml(IEmitter emitter, object? value, Type type)
    {
        Color color = (Color?)value ?? Color.white;
        emitter.Emit(new MappingStart(AnchorName.Empty, TagName.Empty, isImplicit: true, MappingStyle.Block));

        emitter.EmitMapping("r", color.r.ToString(CultureInfo.InvariantCulture));
        emitter.EmitMapping("g", color.g.ToString(CultureInfo.InvariantCulture));
        emitter.EmitMapping("b", color.b.ToString(CultureInfo.InvariantCulture));
        emitter.EmitMapping("a", color.a.ToString(CultureInfo.InvariantCulture));

        emitter.Emit(new MappingEnd());
    }

    /// <inheritdoc/>
    public bool Accepts(Type type)
    {
        return type == typeof(Color);
    }
}
