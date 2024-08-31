using System;
using System.IO;
using System.Linq;

namespace LabApi.Loader.Features.Paths;

/// <summary>
/// The manager for all paths used by LabAPI.
/// It is recommended to use this class instead of hard-coded paths.
/// </summary>
public static class PathManager
{
    private const string SecretLabFolderName = "SCP Secret Laboratory";
    private const string LabApiFolderName = "LabAPI-Beta";
    private const string PluginsFolderName = "plugins";
    private const string DependenciesFolderName = "dependencies";
    private const string ConfigsFolderName = "configs";

    static PathManager()
    {
        AppData = GetAppDataDirectory();
        SecretLab = AppData.CreateSubdirectory(SecretLabFolderName);
        LabApi = SecretLab.CreateSubdirectory(LabApiFolderName);
        Plugins = LabApi.CreateSubdirectory(PluginsFolderName);
        Dependencies = LabApi.CreateSubdirectory(DependenciesFolderName);
        Configs = LabApi.CreateSubdirectory(ConfigsFolderName);
    }

    /// <summary>
    /// Gets the path to the system's AppData folder.
    /// </summary>
    public static DirectoryInfo AppData { get; }

    /// <summary>
    /// Gets the path to the <see cref="SecretLabFolderName"/> folder, located inside <see cref="AppData"/>.
    /// </summary>
    public static DirectoryInfo SecretLab { get; }

    /// <summary>
    /// Gets the path to the <see cref="LabApiFolderName"/> folder, located inside <see cref="SecretLab"/>.
    /// </summary>
    public static DirectoryInfo LabApi { get; }

    /// <summary>
    /// Gets the path to the plugins folder, located inside <see cref="LabApi"/>.
    /// </summary>
    public static DirectoryInfo Plugins { get; }

    /// <summary>
    /// Gets the path to the dependencies folder, located inside <see cref="LabApi"/>.
    /// </summary>
    public static DirectoryInfo Dependencies { get; }

    /// <summary>
    /// Gets the path to the configs folder, located inside <see cref="LabApi"/>.
    /// </summary>
    public static DirectoryInfo Configs { get; }

    private static DirectoryInfo GetAppDataDirectory()
    {
        return Directory.CreateDirectory(GetHosterPolicy()
            ? "AppData"
            : Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
    }

    private static bool GetHosterPolicy()
    {
        return File.Exists("hoster_policy.txt")
               && File.ReadAllLines("hoster_policy.txt").Any(l => l.Contains("gamedir_for_configs: true", StringComparison.OrdinalIgnoreCase));
    }
}