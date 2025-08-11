using LabApi.Loader.Features.Yaml.CustomConverters;
using Serialization;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using CommentsObjectGraphVisitor = Serialization.CommentsObjectGraphVisitor;

namespace LabApi.Loader.Features.Yaml;

/// <summary>
/// Static class for yaml config serializer and deserializer.
/// </summary>
public static class YamlConfigParser
{
    /// <summary>
    /// Static yaml serializer instance.
    /// </summary>
    public static ISerializer Serializer { get; } = new SerializerBuilder()
        .WithEmissionPhaseObjectGraphVisitor((EmissionPhaseObjectGraphVisitorArgs visitor) => new CommentsObjectGraphVisitor(visitor.InnerVisitor))
        .WithTypeInspector((ITypeInspector typeInspector) => new CommentGatheringTypeInspector(typeInspector))
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .DisableAliases()
        .IgnoreFields()
        .WithTypeConverter(new CustomVectorConverter())
        .WithTypeConverter(new CustomColor32Converter())
        .WithTypeConverter(new CustomColorConverter())
        .WithTypeConverter(new CustomQuaternionConverter())
        .Build();

    /// <summary>
    /// Static yaml deserializer instance.
    /// </summary>
    public static IDeserializer Deserializer { get; } = new DeserializerBuilder()
        .WithNamingConvention(UnderscoredNamingConvention.Instance)
        .IgnoreUnmatchedProperties().IgnoreFields()
        .WithTypeConverter(new CustomVectorConverter())
        .WithTypeConverter(new CustomColor32Converter())
        .WithTypeConverter(new CustomColorConverter())
        .WithTypeConverter(new CustomQuaternionConverter())
        .Build();
}
