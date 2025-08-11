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
/// A custom class for serializing/deserializing <see cref="Vector2"/>, <see cref="Vector3"/> and <see cref="Vector4"/>.
/// </summary>
public class CustomVectorConverter : IYamlTypeConverter
{
    /// <inheritdoc/>
    public object? ReadYaml(IParser parser, Type type)
    {
        parser.Consume<MappingStart>();

        Dictionary<string, float> storedValues = DictionaryPool<string, float>.Get();

        int idx = 0;
        try
        {
            while (!parser.TryConsume(out MappingEnd _))
            {
                if (!parser.TryReadMapping(out string key, out string val))
                {
                    throw new ArgumentException($"Unable to parse Vector, no component at index {idx} provided");
                }

                if (!(key is "x" or "y" or "z" or "w"))
                {
                    throw new ArgumentException($"Unable to parse Vector, invalid component name {key}. Only 'x' 'y' 'z' and 'w' are allowed");
                }

                if (storedValues.ContainsKey(key))
                {
                    throw new ArgumentException($"Unable to parse Vector, duplicate component {key}");
                }

                storedValues[key] = float.Parse(val, CultureInfo.InvariantCulture);
                idx++;
            }

            object result = storedValues.Count switch
            {
                2 => new Vector2(storedValues["x"], storedValues["y"]),
                3 => new Vector3(storedValues["x"], storedValues["y"], storedValues["z"]),
                4 => new Vector4(storedValues["x"], storedValues["y"], storedValues["z"], storedValues["w"]),
                _ => throw new ArgumentException($"Unable to deserialize vector with {storedValues.Count} components"),
            };

            Type createdType = result.GetType();
            if (createdType != type)
            {
                throw new ArgumentException($"Attempting to deserialize {createdType.Name} for config type of {type.Name}");
            }

            return result;
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
    public void WriteYaml(IEmitter emitter, object? value, Type _)
    {
        emitter.Emit(new MappingStart(AnchorName.Empty, TagName.Empty, isImplicit: true, MappingStyle.Block));
        switch (value)
        {
            case Vector2 v:
                emitter.EmitMapping("x", v.x.ToString(CultureInfo.InvariantCulture));
                emitter.EmitMapping("y", v.y.ToString(CultureInfo.InvariantCulture));
                break;
            case Vector3 v:
                emitter.EmitMapping("x", v.x.ToString(CultureInfo.InvariantCulture));
                emitter.EmitMapping("y", v.y.ToString(CultureInfo.InvariantCulture));
                emitter.EmitMapping("z", v.z.ToString(CultureInfo.InvariantCulture));
                break;
            case Vector4 v:
                emitter.EmitMapping("x", v.x.ToString(CultureInfo.InvariantCulture));
                emitter.EmitMapping("y", v.y.ToString(CultureInfo.InvariantCulture));
                emitter.EmitMapping("z", v.z.ToString(CultureInfo.InvariantCulture));
                emitter.EmitMapping("w", v.z.ToString(CultureInfo.InvariantCulture));
                break;
        }

        emitter.Emit(new MappingEnd());
    }

    /// <inheritdoc/>
    public bool Accepts(Type type)
    {
        return type == typeof(Vector2) || type == typeof(Vector3) || type == typeof(Vector4);
    }
}