using System;
using System.Collections.Generic;
using YamlDotNet.Serialization;
using UnityEngine;
using System.Globalization;
using UnityEngine.Pool;
using YamlDotNet.Core.Events;
using YamlDotNet.Core;
using LabApi.Loader.Features.Yaml.Extensions;

namespace LabApi.Loader.Features.Yaml.CustomConverters;

/// <summary>
/// A custom class for serializing/deserializing <see cref="Quaternion"/> as euler angles.
/// </summary>
public class CustomQuaternionConverter : IYamlTypeConverter
{
    /// <inheritdoc/>
    public object? ReadYaml(IParser parser, Type type)
    {
        parser.Consume<MappingStart>();

        Dictionary<string, float> storedValues = DictionaryPool<string, float>.Get();

        try
        {
            for (int i = 0; i <= 2; i++)
            {
                if (!parser.TryReadMapping(out string key, out string val))
                    throw new ArgumentException($"Unable to parse {nameof(Quaternion)}, no component at index {i} provided");

                if (!(key is "x" or "y" or "z"))
                    throw new ArgumentException($"Unable to parse {nameof(Quaternion)}, invalid component name {key}. Only 'x', 'y' and 'z' euler angles are allowed");

                storedValues[key] = float.Parse(val, CultureInfo.InvariantCulture);
            }
            parser.Consume<MappingEnd>();

            Quaternion value = Quaternion.Euler(storedValues["x"], storedValues["y"], storedValues["z"]);
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
        Vector3 rotation = ((Quaternion)value).eulerAngles;
        emitter.Emit(new MappingStart(AnchorName.Empty, TagName.Empty, isImplicit: true, MappingStyle.Block));

        emitter.EmitMapping("x", rotation.x.ToString(CultureInfo.InvariantCulture));
        emitter.EmitMapping("y", rotation.y.ToString(CultureInfo.InvariantCulture));
        emitter.EmitMapping("z", rotation.z.ToString(CultureInfo.InvariantCulture));

        emitter.Emit(new MappingEnd());
    }

    /// <inheritdoc/>
    public bool Accepts(Type type)
    {
        return type == typeof(Quaternion);
    }
}
