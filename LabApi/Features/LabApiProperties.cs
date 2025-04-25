using System;

namespace LabApi.Features;

/// <summary>
/// Contains LabAPI properties which can be accessed by plugins.
///
/// </summary>
/// <para>Those properties are a mix of constants and static fields such as <see cref="CompiledVersion"/>.</para>
public static class LabApiProperties
{
    /// <summary>
    /// The version of the loader, stored during its compilation.
    ///
    /// <para>Due to being a constant, the value of this field will always be that of its compilation time version.</para>
    /// </summary>
    public const string CompiledVersion = "0.6.0.0";

    /// <summary>
    /// Indicates the value of <see cref="CompiledVersion"/> the server is currently using.
    /// </summary>
    public static readonly Version CurrentVersion = Version.Parse(CompiledVersion);
}