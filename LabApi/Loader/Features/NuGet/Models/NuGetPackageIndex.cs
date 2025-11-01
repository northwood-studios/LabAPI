using System.Runtime.Serialization;

namespace LabApi.Loader.Features.NuGet.Models;

/// <summary>
/// Represents the root structure of a NuGet service index (<c>index.json</c>),
/// which describes the available service endpoints for a NuGet source.
/// </summary>
/// <remarks>
/// The NuGet service index (usually located at <c>https://api.nuget.org/v3/index.json</c>)
/// provides metadata about the repository’s available APIs, such as
/// <c>PackageBaseAddress</c>, <c>SearchQueryService</c>, and others.
/// </remarks>
public class NuGetPackageIndex
{
    /// <summary>
    /// Gets or sets the list of service resources exposed by the NuGet source.
    /// </summary>
    /// <remarks>
    /// Each resource entry describes an endpoint (for example,
    /// a package base address or search service) and its supported version.
    /// These are typically represented by the <see cref="NuGetPackageResource"/> type.
    /// </remarks>
    [DataMember(Name = "resources")]
    public NuGetPackageResource[] Resources { get; set; } = [];
}
