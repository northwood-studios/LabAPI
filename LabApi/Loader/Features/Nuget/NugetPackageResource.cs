using System.Runtime.Serialization;

namespace LabApi.Loader.Features.NuGet;

/// <summary>
/// Represents a single resource entry within a NuGet service index (<c>index.json</c>).
/// </summary>
/// <remarks>
/// Each resource describes a specific NuGet service endpoint and its purpose,
/// such as the <c>PackageBaseAddress</c> (used to download packages) or
/// <c>SearchQueryService</c> (used to search packages).
/// </remarks>
public class NuGetPackageResource
{
    /// <summary>
    /// Gets or sets the type of the NuGet service resource.
    /// </summary>
    /// <remarks>
    /// The <c>@type</c> field defines the role of the resource, for example:
    /// <list type="bullet">
    /// <item><description><c>PackageBaseAddress/3.0.0</c> — base URL for downloading package files</description></item>
    /// </list>
    /// </remarks>
    [DataMember(Name = "@type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the absolute URL of the service endpoint.
    /// </summary>
    /// <remarks>
    /// The <c>@id</c> value is typically a fully qualified HTTPS URL that identifies
    /// the service’s base address. For example:
    /// <c>https://api.nuget.org/v3-flatcontainer/</c> or
    /// </remarks>
    [DataMember(Name = "@id")]
    public string Id { get; set; } = string.Empty;
}
