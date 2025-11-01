using LabApi.Features.Console;
using System;

namespace LabApi.Loader.Features.NuGet.Models;

/// <summary>
/// Represents a dependency entry within a NuGet package,
/// including its identifier and version, and provides
/// helper methods for installation and status checking.
/// </summary>
public class NuGetDependency
{
    /// <summary>
    /// Gets or sets the unique identifier (name) of the NuGet dependency.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Gets or sets the semantic version string of the dependency (e.g. "1.2.3").
    /// </summary>
    public required string Version { get; set; }

    /// <summary>
    /// Installs this NuGet dependency by downloading it from the configured source.
    /// </summary>
    /// <returns>
    /// A <see cref="NuGetPackage"/> instance representing the downloaded package,
    /// or <see langword="null"/> if the installation failed or the package could not be found.
    /// </returns>
    /// <remarks>
    /// This method delegates to <see cref="NuGetPackagesManager.DownloadNugetPackage(string, string)"/>.
    /// </remarks>
    public NuGetPackage? Install() => NuGetPackagesManager.DownloadNugetPackage(Id, Version);

    /// <summary>
    /// Determines whether this dependency is already installed
    /// or loaded in the current AppDomain.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the dependency is installed or the corresponding assembly is already loaded;
    /// otherwise, <c>false</c>.
    /// </returns>
    public bool IsInstalled()
    {
        if (NuGetPackagesManager.Packages.TryGetValue($"{Id}.{Version}", out NuGetPackage package))
        {
            return string.Equals(package.Version, Version, StringComparison.OrdinalIgnoreCase);
        }

        if (IsAssemblyAlreadyLoaded(Id))
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Checks whether an assembly with the given identifier is already loaded
    /// into the current application domain.
    /// </summary>
    /// <param name="id">The dependency or assembly identifier to check.</param>
    /// <returns>
    /// <c>true</c> if an assembly with the specified ID is already loaded;
    /// otherwise, <c>false</c>.
    /// </returns>
    private bool IsAssemblyAlreadyLoaded(string id)
    {
        try
        {
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                string asmName = asm.GetName().Name ?? string.Empty;

                if (asmName.Equals(id, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Logger.Warn($"{NuGetPackagesManager.Prefix} Failed to check if assembly '{id}' is already loaded: {ex.Message}");
        }

        return false;
    }
}
