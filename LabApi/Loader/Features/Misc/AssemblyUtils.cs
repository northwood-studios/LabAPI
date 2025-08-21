using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;

namespace LabApi.Loader.Features.Misc;

/// <summary>
/// A collection of utilities for working with <see cref="Assembly"/>s.
/// </summary>
public static class AssemblyUtils
{
    private const string LoggerPrefix = "[ASSEMBLY_LOADER]";

    /// <summary>
    /// Checks whether the specified <see cref="Assembly"/> has missing dependencies and gets the types if it is loaded.
    /// </summary>
    /// <param name="assembly">The assembly to check missing dependencies for.</param>
    /// <param name="assemblyPath">The path of the assembly to log an error message.</param>
    /// <param name="types">The types of the assembly if it is loaded.</param>
    /// <returns>Whether the specified <see cref="Assembly"/> has missing dependencies.</returns>
    public static bool HasMissingDependencies(Assembly assembly, string assemblyPath, [NotNullWhen(false)] out Type[]? types)
    {
        // We convert the missing dependencies to an array to avoid multiple iterations.
        string[] missingDependencies = GetMissingDependencies(assembly).ToArray();

        try
        {
            if (missingDependencies.Length != 0)
            {
                // In the case that there are missing dependencies, we try to resolve possible embedded resources.
                ResolveEmbeddedResources(assembly);
            }

            // If the assembly has missing dependencies after resolving embedded resources this will throw an exception.
            types = assembly.GetTypes();
            return false; // False = no missing dependencies.
        }
        catch (Exception exception)
        {
            Logger.Error($"{LoggerPrefix} Couldn't load the assembly inside '{assemblyPath}'");

            if (missingDependencies.Length != 0)
            {
                // If there are missing dependencies, we log them.
                Logger.Error($"{LoggerPrefix} Missing dependencies:\n{string.Join("\n", missingDependencies.Select(x => $"-\t {x}"))}");
            }

            Logger.Error(exception.ToString());

            types = null;
            return true; // True = missing dependencies.
        }
    }

    /// <summary>
    /// Gets the loaded assemblies in the current <see cref="AppDomain"/>.
    /// </summary>
    /// <returns>Returns a list of the formatted loaded assemblies.</returns>
    public static IEnumerable<string> GetLoadedAssemblies()
    {
        return AppDomain.CurrentDomain.GetAssemblies().Select(NameSelector);

        // We will use this selector to format the assembly names to the format we want.
        static string NameSelector(Assembly assembly) => FormatAssemblyName(assembly.GetName());
    }

    /// <summary>
    /// Gets the missing dependencies of the specified <see cref="Assembly"/>.
    /// </summary>
    /// <param name="assembly">The assembly to check missing dependencies from.</param>
    /// <returns>A formatted <see cref="IEnumerable{T}"/> with the missing dependencies.</returns>
    public static IEnumerable<string> GetMissingDependencies(Assembly assembly)
    {
        IEnumerable<string> loadedAssemblies = GetLoadedAssemblies();

        // Using the same format, we will get the missing dependencies.
        return assembly.GetReferencedAssemblies().Select(FormatAssemblyName).Where(name => !loadedAssemblies.Contains(name));
    }

    /// <summary>
    /// Resolves embedded resources from the specified <see cref="Assembly"/>.
    /// </summary>
    /// <param name="assembly">The assembly to resolve embedded resources from.</param>
    public static void ResolveEmbeddedResources(Assembly assembly)
    {
        const string dllExtension = ".dll";
        const string compressedDllExtension = ".dll.compressed";

        // We get all the resource names from the specified assembly.
        string[] resourceNames = assembly.GetManifestResourceNames();
        foreach (string resourceName in resourceNames)
        {
            // We check if the resource is a dll.
            if (resourceName.EndsWith(dllExtension, StringComparison.OrdinalIgnoreCase))
            {
                // If the resource is a dll, we load it as an embedded dll.
                LoadEmbeddedDll(assembly, resourceName);
            }

            // We check if the resource is a compressed dll.
            else if (resourceName.EndsWith(compressedDllExtension, StringComparison.OrdinalIgnoreCase))
            {
                // If the resource is a compressed dll, we load it as a compressed embedded dll.
                LoadCompressedEmbeddedDll(assembly, resourceName);
            }
        }
    }

    /// <summary>
    /// Loads an embedded dll from the specified <see cref="Assembly"/>.
    /// </summary>
    /// <param name="target">The assembly to load the embedded dll from.</param>
    /// <param name="name">The resource name of the embedded dll.</param>
    public static void LoadEmbeddedDll(Assembly target, string name)
    {
        // We try to get the data stream of the specified resource name.
        if (!TryGetDataStream(target, name, out Stream? dataStream))
        {
            return;
        }

        // We copy the data stream to a memory stream and load the assembly from the memory stream.
        using MemoryStream stream = new();
        dataStream.CopyTo(stream);
        Assembly.Load(stream.ToArray());
    }

    /// <summary>
    /// Loads a compressed embedded dll from the specified <see cref="Assembly"/>.
    /// </summary>
    /// <param name="target">The assembly to load the compressed embedded dll from.</param>
    /// <param name="name">The resource name of the compressed embedded dll.</param>
    public static void LoadCompressedEmbeddedDll(Assembly target, string name)
    {
        // We try to get the data stream of the specified resource name.
        if (!TryGetDataStream(target, name, out Stream? dataStream))
        {
            return;
        }

        // We decompress the data stream and load the assembly from the memory stream.
        using DeflateStream stream = new(dataStream, CompressionMode.Decompress);

        // We use a memory stream to load the assembly from the decompressed data stream.
        using MemoryStream memStream = new();
        stream.CopyTo(memStream);
        Assembly.Load(memStream.ToArray());
    }

    /// <summary>
    /// Try to get the data stream of the specified resource name from the specified <see cref="Assembly"/>.
    /// </summary>
    /// <param name="target">The assembly to get the data stream from.</param>
    /// <param name="name">The resource name to get the data stream from.</param>
    /// <param name="dataStream">The data stream of the specified resource name if it was successfully retrieved, otherwise <see langword="null"/>.</param>
    /// <returns>Whether or not the data stream was successfully retrieved.</returns>
    public static bool TryGetDataStream(Assembly target, string name, [NotNullWhen(true)] out Stream? dataStream)
    {
        // We try to get the data stream of the specified resource name.
        dataStream = target.GetManifestResourceStream(name);

        // If the data stream is not null, we successfully retrieved the data stream and therefore return true.
        if (dataStream is not null)
        {
            return true;
        }

        // If the data stream is null, we log an error message and return false.
        Logger.Error($"{LoggerPrefix} Unable to resolve {name} Stream was null");
        return false;
    }

    /// <summary>
    /// Tries to get the assembly of a loaded <see cref="Plugin"/>.
    /// </summary>
    /// <param name="plugin">The <see cref="Plugin"/> to get the assembly from.</param>
    /// <param name="assembly">The assembly of the loaded <see cref="Plugin"/>.</param>
    /// <returns>Whether the assembly was successfully retrieved.</returns>
    public static bool TryGetLoadedAssembly(this Plugin plugin, out Assembly assembly)
    {
        // We try to get the assembly of the specified plugin inside the plugin loader.
        return PluginLoader.Plugins.TryGetValue(plugin, out assembly);
    }

    // Used for missing assembly comparisons.
    private static string FormatAssemblyName(AssemblyName assemblyName) => $"{assemblyName.Name} v{assemblyName.Version}";
}