using LabApi.Features.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Utf8Json;

namespace LabApi.Loader.Features.NuGet;

/// <summary>
/// Provides functionality for reading, downloading, extracting,
/// and managing NuGet packages within the LabApi loader system.
/// </summary>
public class NuGetPackagesManager
{
    /// <summary>
    /// Prefix used for console log messages originating from NuGet operations.
    /// </summary>
    public const string Prefix = "[NUGET]";

    private static readonly Dictionary<string, string> _packageBaseAddressCache = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Defines the ordered list of human-readable framework names (e.g. ".NETFramework4.8"),
    /// used to determine preferred dependency groups in nuspec metadata.
    /// </summary>
    private static string[] _frameworkPriority = new[]
    {
        ".NETFramework4.8",
        ".NETFramework4.7.2",
        ".NETFramework4.7.1",
        ".NETFramework4.7",
        ".NETFramework4.6.2",
        ".NETFramework4.6.1",
        ".NETFramework4.6",
        ".NETFramework4.5.2",
        ".NETFramework4.5.1",
        ".NETFramework4.5",
        ".NETFramework4.0",
        ".NETStandard2.1",
        ".NETStandard2.0",
        ".NETStandard1.6",
        ".NETStandard1.5",
        ".NETStandard1.4",
        ".NETStandard1.3",
        ".NETStandard1.2",
        ".NETStandard1.1",
        ".NETStandard1.0",
    };

    /// <summary>
    /// Defines the ordered list of NuGet Target Framework Monikers (TFMs)
    /// used to prioritize assembly selection inside .nupkg archives.
    /// </summary>
    private static string[] _frameworkVersionPriorities = new[]
    {
        "net48",
        "net472",
        "net471",
        "net47",
        "net462",
        "net461",
        "net46",
        "net452",
        "net451",
        "net45",
        "net40",
        "netstandard2.1",
        "netstandard2.0",
        "netstandard1.6",
        "netstandard1.5",
        "netstandard1.4",
        "netstandard1.3",
        "netstandard1.2",
        "netstandard1.1",
        "netstandard1.0",
    };

    /// <summary>
    /// Gets the dictionary of all loaded NuGet dependencies,
    /// indexed by their package ID (case-insensitive).
    /// </summary>
    /// <remarks>
    /// This collection stores all NuGet packages that have been
    /// successfully loaded or installed by the loader.
    /// Keys represent the dependency ID, and values contain the corresponding
    /// <see cref="NuGetPackage"/> instance and its metadata.
    /// </remarks>
    public static Dictionary<string, NuGetPackage> Packages { get; } = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Resolves any missing NuGet dependencies by checking all loaded packages
    /// and automatically downloading missing ones from configured NuGet sources.
    /// </summary>
    public static void ResolveMissingNugetDependencies()
    {
        Logger.Info($"{Prefix} Resolving missing NuGet dependencies...");

        int resolvedCount = 0;

        Queue<NuGetPackage> packagesToResolve = new Queue<NuGetPackage>(Packages.Values);

        while (packagesToResolve.Count != 0)
        {
            NuGetPackage package = packagesToResolve.Dequeue();

            foreach (NuGetDependency dep in package.Dependencies)
            {
                if (dep.IsInstalled())
                {
                    continue;
                }

                Logger.Warn($"{Prefix} Package '{package.Id}' v{package.Version} has missing dependency '{dep.Id}' v{dep.Version}, attempting to resolve...");

                try
                {
                    packagesToResolve.Enqueue(DownloadNugetPackage(dep.Id, dep.Version));
                    resolvedCount++;
                }
                catch (Exception ex)
                {
                    Logger.Error($"{Prefix} Failed to resolve dependency '{dep.Id}' v{dep.Version}");
                    Logger.Error(ex);
                }
            }
        }
    }

    /// <summary>
    /// Reads and parses a NuGet package from the specified file path.
    /// </summary>
    /// <param name="path">The full file system path to the .nupkg file.</param>
    /// <returns>A populated <see cref="NuGetPackage"/> instance.</returns>
    /// <exception cref="FileNotFoundException">Thrown when the specified package file does not exist.</exception>
    public static NuGetPackage ReadPackage(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Package file not found: {path}");
        }

        byte[] data = File.ReadAllBytes(path);
        string name = Path.GetFileName(path);

        return ReadPackage(name, path, data);
    }

    /// <summary>
    /// Reads and parses a NuGet package from a byte array.
    /// </summary>
    /// <param name="name">The file name of the package.</param>
    /// <param name="data">The binary contents of the .nupkg file.</param>
    /// <returns>A populated <see cref="NuGetPackage"/> instance.</returns>
    public static NuGetPackage ReadPackage(string name, string fullPath, byte[] data)
    {
        using MemoryStream ms = new MemoryStream(data);
        return ReadPackage(name, fullPath, ms);
    }

    /// <summary>
    /// Reads and parses a NuGet package from a memory stream.
    /// </summary>
    /// <param name="name">The package file name.</param>
    /// <param name="fullPath">The full path of a package.</param>
    /// <param name="stream">A memory stream containing the .nupkg content.</param>
    /// <returns>A populated <see cref="NuGetPackage"/> with metadata, dependencies, and assembly data loaded.</returns>
    public static NuGetPackage ReadPackage(string name, string fullPath, MemoryStream stream)
    {
        NuGetPackage package = new NuGetPackage()
        {
            FilePath = fullPath,
            FileName = name,
            FileContent = stream.ToArray(),
        };

        using ZipArchive archive = new ZipArchive(stream);
        ZipArchiveEntry bestEntry = GetBestVersion(archive);

        if (bestEntry != null)
        {
            using MemoryStream ms = new MemoryStream();
            using Stream entryStream = bestEntry.Open();

            entryStream.CopyTo(ms);

            package.RawAssembly = ms.ToArray();
        }

        string? nuspecXml = ReadNuspecFromNupkg(archive);

        if (nuspecXml == null)
        {
            return package;
        }

        GetMetadata(package, nuspecXml);

        return package;
    }

    /// <summary>
    /// Downloads a NuGet package from the official NuGet repository and installs it
    /// into the appropriate directory (plugins or dependencies).
    /// </summary>
    /// <param name="packageId">The ID of the package to download.</param>
    /// <param name="version">The version of the package to download.</param>
    public static NuGetPackage DownloadNugetPackage(string packageId, string version)
    {
        string[] sources = PluginLoader.Config.NugetPackageSources;

        if (sources == null || sources.Length == 0)
        {
            Logger.Error($"{Prefix} No NuGet package sources defined in configuration!");
            return null;
        }

        byte[]? packageData = null;
        string? successfulSource = null;

        foreach (string source in sources)
        {
            string baseAddress = GetCachedPackageBaseAddress(source);
            string idLower = packageId.ToLowerInvariant();
            string verLower = version.ToLowerInvariant();

            if (string.IsNullOrEmpty(baseAddress))
            {
                continue;
            }

            string downloadUrl = $"{baseAddress.TrimEnd('/')}/{idLower}/{verLower}/{idLower}.{verLower}.nupkg";

            try
            {
                using WebClient web = new WebClient();
                if (Uri.TryCreate(source, UriKind.Absolute, out Uri? uri) && !string.IsNullOrEmpty(uri.UserInfo))
                {
                    string userInfo = uri.UserInfo;
                    string[] parts = userInfo.Split(':', 2);

                    string username = parts.Length > 0 ? Uri.UnescapeDataString(parts[0]) : string.Empty;
                    string password = parts.Length > 1 ? Uri.UnescapeDataString(parts[1]) : string.Empty;

                    string token = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));
                    web.Headers[HttpRequestHeader.Authorization] = $"Basic {token}";
                }

                packageData = web.DownloadData(downloadUrl);
                successfulSource = source;
                break;
            }
            catch (WebException ex)
            {
                Logger.Warn($"{Prefix} Failed to download '{packageId}' v{version} from {downloadUrl}");
                if (ex.Response is HttpWebResponse resp)
                {
                    Logger.Error($"{Prefix} HTTP {(int)resp.StatusCode} - {resp.StatusDescription}");
                }
            }
            catch (Exception ex)
            {
                Logger.Warn($"{Prefix} Unexpected error while downloading from '{source}': {ex.Message}");
            }
        }

        if (packageData == null)
        {
            Logger.Error($"{Prefix} Failed to download package '{packageId}' v{version} from all configured sources.");
            return null;
        }

        // Proceed to install
        NuGetPackage package = ReadPackage($"{packageId}.{version}.nupkg", string.Empty, packageData);
        string? path = package.Extract();

        if (path == null)
        {
            Logger.Error($"{Prefix} Failed to extract package '{packageId}' v{version}");
            return null;
        }

        Packages.Add($"{package.Id}.{package.Version}", package);

        Logger.Info($"{Prefix} Successfully downloaded '{packageId}' v{version} from {successfulSource}");
        return package;
    }

    /// <summary>
    /// Resolves and caches the PackageBaseAddress for a given NuGet v3 source.
    /// If the source does not expose a valid index.json or lacks the resource, returns an empty string.
    /// </summary>
    /// <param name="sourceUrl">The NuGet feed base URL (e.g. https://api.nuget.org/v3/index.json).</param>
    /// <returns>The resolved PackageBaseAddress URL, or an empty string if unavailable.</returns>
    private static string GetCachedPackageBaseAddress(string sourceUrl)
    {
        if (_packageBaseAddressCache.TryGetValue(sourceUrl, out string cached))
        {
            return cached;
        }

        string normalized = sourceUrl.TrimEnd('/');

        string indexUrl = normalized.EndsWith("index.json", StringComparison.OrdinalIgnoreCase)
            ? normalized
            : $"{normalized}/index.json";

        using WebClient client = new WebClient();

        if (Uri.TryCreate(indexUrl, UriKind.Absolute, out Uri? uri) && !string.IsNullOrEmpty(uri.UserInfo))
        {
            string userInfo = uri.UserInfo;
            string[] parts = userInfo.Split(':', 2);

            string username = parts.Length > 0 ? Uri.UnescapeDataString(parts[0]) : string.Empty;
            string password = parts.Length > 1 ? Uri.UnescapeDataString(parts[1]) : string.Empty;

            string token = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));
            client.Headers[HttpRequestHeader.Authorization] = $"Basic {token}";

            UriBuilder cleanUri = new(uri)
            {
                UserName = string.Empty,
                Password = string.Empty
            };
            indexUrl = cleanUri.Uri.ToString();
        }

        string data;
        try
        {
            data = client.DownloadString(indexUrl);
        }
        catch (WebException ex)
        {
            Logger.Warn($"{Prefix} Failed to read packages source from '{indexUrl}'");
            if (ex.Response is HttpWebResponse resp)
            {
                Logger.Error($"{Prefix} HTTP {(int)resp.StatusCode} - {resp.StatusDescription}");
            }

            _packageBaseAddressCache[sourceUrl] = string.Empty;
            return string.Empty;
        }

        NuGetPackageIndex index = JsonSerializer.Deserialize<NuGetPackageIndex>(data);

        foreach (NuGetPackageResource resource in index.Resources)
        {
            if (!resource.Type.Contains("PackageBaseAddress"))
            {
                continue;
            }

            _packageBaseAddressCache[sourceUrl] = resource.Id;
            return resource.Id;
        }

        _packageBaseAddressCache[sourceUrl] = string.Empty;
        return string.Empty;
    }

    /// <summary>
    /// Selects the most appropriate assembly file from a NuGet archive
    /// based on the internal framework version priority list.
    /// </summary>
    /// <param name="archive">The opened <see cref="ZipArchive"/> representing the .nupkg file.</param>
    /// <returns>The <see cref="ZipArchiveEntry"/> that best matches the preferred framework; otherwise <c>null</c>.</returns>
    private static ZipArchiveEntry GetBestVersion(ZipArchive archive)
    {
        IEnumerable<ZipArchiveEntry> libEntries = archive.Entries.Where(x => x.FullName.StartsWith("lib/net") && x.FullName.EndsWith(".dll"));

        Dictionary<int, ZipArchiveEntry> sortedEntries = new Dictionary<int, ZipArchiveEntry>();

        foreach (ZipArchiveEntry entry in libEntries)
        {
            string[] name = entry.FullName.Split('/');

            string frameworkVersion = name[1];

            if (!_frameworkVersionPriorities.Contains(frameworkVersion))
            {
                continue;
            }

            int index = _frameworkVersionPriorities.IndexOf(frameworkVersion);

            sortedEntries.Add(index, entry);
        }

        KeyValuePair<int, ZipArchiveEntry> bestEntry = sortedEntries.OrderBy(x => x.Key).FirstOrDefault();

        return bestEntry.Value;
    }

    /// <summary>
    /// Extracts and returns the XML content of the .nuspec file from within a NuGet archive.
    /// </summary>
    /// <param name="entry">The <see cref="ZipArchive"/> instance representing the package.</param>
    /// <returns>The XML string of the .nuspec file, or <c>null</c> if not found.</returns>
    private static string? ReadNuspecFromNupkg(ZipArchive entry)
    {
        ZipArchiveEntry nuspecEntry = entry.Entries.FirstOrDefault(e => e.FullName.EndsWith(".nuspec", StringComparison.OrdinalIgnoreCase));

        if (nuspecEntry == null)
        {
            return null;
        }

        using StreamReader reader = new StreamReader(nuspecEntry.Open());
        return reader.ReadToEnd();
    }

    /// <summary>
    /// Reads metadata and dependency information from the .nuspec XML
    /// and populates the corresponding fields in the <see cref="NugetPackage"/> instance.
    /// </summary>
    /// <param name="package">The package instance to populate.</param>
    /// <param name="nuspecXml">The raw XML content of the .nuspec file.</param>
    private static void GetMetadata(NuGetPackage package, string nuspecXml)
    {
        XDocument doc = XDocument.Parse(nuspecXml);
        XNamespace ns = doc.Root.GetDefaultNamespace();

        XElement metadata = doc.Descendants(ns + "metadata").FirstOrDefault();

        if (metadata == null)
        {
            return;
        }

        package.Id = metadata.Element(ns + "id")?.Value ?? string.Empty;
        package.Version = metadata.Element(ns + "version")?.Value ?? string.Empty;
        package.Tags = metadata.Element(ns + "tags")?.Value ?? string.Empty;

        XElement depsElement = metadata.Element(ns + "dependencies");

        if (depsElement == null)
        {
            return;
        }

        IEnumerable<XElement> groups = depsElement.Elements(ns + "group");
        XElement? selectedGroup = null;

        foreach (string framework in _frameworkPriority)
        {
            selectedGroup = groups.FirstOrDefault(g =>
                string.Equals(g.Attribute("targetFramework")?.Value, framework, StringComparison.OrdinalIgnoreCase));

            if (selectedGroup != null)
            {
                break;
            }
        }

        if (selectedGroup == null)
        {
            selectedGroup = groups.LastOrDefault();
        }

        List<NuGetDependency> dependencies = new List<NuGetDependency>();

        if (selectedGroup != null)
        {
            string? groupFramework = selectedGroup.Attribute("targetFramework")?.Value;
            foreach (XElement dep in selectedGroup.Elements(ns + "dependency"))
            {
                dependencies.Add(new NuGetDependency
                {
                    Id = dep.Attribute("id")?.Value ?? string.Empty,
                    Version = dep.Attribute("version")?.Value ?? string.Empty,
                });
            }
        }

        foreach (XElement dep in depsElement.Elements(ns + "dependency"))
        {
            dependencies.Add(new NuGetDependency
            {
                Id = dep.Attribute("id")?.Value ?? string.Empty,
                Version = dep.Attribute("version")?.Value ?? string.Empty,
            });
        }

        package.Dependencies = dependencies;
    }
}
