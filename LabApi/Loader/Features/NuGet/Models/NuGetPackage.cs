using LabApi.Features.Console;
using LabApi.Loader.Features.Misc;
using LabApi.Loader.Features.Paths;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace LabApi.Loader.Features.NuGet.Models;

/// <summary>
/// Represents a NuGet package loaded by LabApi, including its metadata,
/// content, and dependency information.
/// </summary>
public class NuGetPackage
{
    /// <summary>
    /// Identifies the plugin tag used to mark NuGet package as plugin.
    /// </summary>
    private const string LabApiPluginTag = "labapi-plugin";

    /// <summary>
    /// Gets or sets the unique package identifier (name).
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the package version string (e.g. "1.2.3").
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the tag metadata from the .nuspec file (used to identify plugins, etc.).
    /// </summary>
    public string Tags { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the compiled assembly content of the package (if applicable).
    /// </summary>
    public byte[]? RawAssembly { get; set; } = null;

    /// <summary>
    /// Gets or sets the full path of NuGet package.
    /// </summary>
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the NuGet package file (e.g. "MyPlugin.1.0.0.nupkg").
    /// </summary>
    public required string FileName { get; set; }

    /// <summary>
    /// Gets or sets the raw file contents of the NuGet package (.nupkg file).
    /// </summary>
    public required byte[] FileContent { get; set; }

    /// <summary>
    /// Gets or sets the list of dependencies defined by this package.
    /// </summary>
    public List<NuGetDependency> Dependencies { get; set; } = new List<NuGetDependency>();

    /// <summary>
    /// Gets a value indicating whether this package is marked as a LabApi plugin.
    /// Determined by the presence of the "labapi-plugin" tag.
    /// </summary>
    public bool IsPlugin => Tags
        .ToLower()
        .Contains(LabApiPluginTag);

    /// <summary>
    /// Gets or sets a value indicating whether the package is already loaded.
    /// </summary>
    public bool IsLoaded { get; set; }

    /// <summary>
    /// Extracts the NuGet package file (.nupkg) to the appropriate directory
    /// (plugins or dependencies), depending on whether it is a plugin.
    /// </summary>
    /// <returns>
    /// The full path to the extracted file if successful; otherwise, <c>null</c>.
    /// </returns>
    public string? ExtractToFolder()
    {
        string? folder = GetFinalFolder();

        if (folder == null)
        {
            Logger.Warn($"{NuGetPackagesManager.Prefix} Could not extract package '{Id}' v{Version} to {(IsPlugin ? "plugins" : "dependencies")} folder: no valid path found!");
            return null;
        }

        string targetFile = Path.Combine(folder, FileName);

        File.WriteAllBytes(targetFile, FileContent);

        FilePath = targetFile;

        return targetFile;
    }

    /// <summary>
    /// Loads package.
    /// </summary>
    public void Load()
    {
        if (IsLoaded)
        {
            return;
        }

        if (RawAssembly?.Length == 0)
        {
            Logger.Warn($"{NuGetPackagesManager.Prefix} Package '{Id}' v{Version} does not contain a valid assembly, skipping...");
            return;
        }

        Assembly assembly = Assembly.Load(RawAssembly);

        if (IsPlugin)
        {
            try
            {
                AssemblyUtils.ResolveEmbeddedResources(assembly);
            }
            catch (Exception e)
            {
                Logger.Error($"{NuGetPackagesManager.Prefix} Failed to resolve embedded resources for package '{Id}' v{Version}");
                Logger.Error(e);
            }

            try
            {
                PluginLoader.InstantiatePlugins(assembly.GetTypes(), assembly, FilePath);
            }
            catch (Exception e)
            {
                Logger.Error($"{NuGetPackagesManager.Prefix} Couldn't load the plugin inside package '{Id}' v{Version}");
                Logger.Error(e);
            }
        }
        else
        {
            PluginLoader.Dependencies.Add(assembly);
        }

        Logger.Info($"{NuGetPackagesManager.Prefix} Package '{Id}' v{Version} was loaded!");

        IsLoaded = true;
    }

    /// <summary>
    /// Resolves and returns the final folder path for the package extraction,
    /// creating directories if necessary.
    /// </summary>
    /// <returns>
    /// The full directory path where the package should be extracted,
    /// or <c>null</c> if no valid path could be determined.
    /// </returns>
    private string? GetFinalFolder()
    {
        foreach (string path in IsPlugin ? PluginLoader.Config.PluginPaths : PluginLoader.Config.DependencyPaths)
        {
            string resolvedPath = PluginLoader.ResolvePath(path);
            string fullPath = Path.Combine(IsPlugin ? PathManager.Plugins.FullName : PathManager.Dependencies.FullName, resolvedPath);

            Directory.CreateDirectory(fullPath);

            return fullPath;
        }

        return null;
    }
}
