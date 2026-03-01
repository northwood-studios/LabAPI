using System;
using System.Reflection;

namespace LabApi.Features.Console;

/// <summary>
/// LabAPIs console logger.
/// Used to log messages to the server console.
/// </summary>
public static class Logger
{
    private const string DebugPrefix = "DEBUG";
    private const string InfoPrefix = "INFO";
    private const string WarnPrefix = "WARN";
    private const string ErrorPrefix = "ERROR";
    private const string InternalPrefix = "INTERNAL";

    /// <summary>
    /// Logs a message to the server console with the specified color.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="color">The color of the message.</param>
    public static void Raw(string message, ConsoleColor color) => ServerConsole.AddLog(message, color);

    /// <summary>
    /// An O(1) set of Assemblies that should display Debug messages.
    /// Populated automatically by <see cref="LabApi.Loader.Features.Plugins.Configuration.Properties.Debug"/>
    /// </summary>
    public static System.Collections.Generic.HashSet<Assembly> DebugEnabled { get; } = new();

    /// <summary>
    /// Logs a debug message to the server console.
    /// Checks <see cref="DebugEnabled"/> before sending the message.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public static void Debug(object message) => Debug(message,   Loader.PluginLoader.Config?.DebugOverride == true || DebugEnabled.Contains(Assembly.GetCallingAssembly()));

    /// <summary>
    /// Logs a debug message to the server console.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="canBePrinted">Whether the message can be printed.</param>
    /// <remarks>
    /// Uses explicit <paramref name="canBePrinted"/>.
    /// Can be replaced with the single parameter overload to use <see cref="LabApi.Loader.Features.Plugins.Configuration.Properties.Debug"/> Property instead.
    /// </remarks>
    public static void Debug(object message, bool canBePrinted)
    {
        if (!canBePrinted)
        {
            return;
        }

        Raw(FormatLog(message, DebugPrefix, Assembly.GetCallingAssembly()), ConsoleColor.Gray);
    }

    /// <summary>
    /// Logs an info message to the server console.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public static void Info(object message) => Raw(FormatLog(message, InfoPrefix, Assembly.GetCallingAssembly()), ConsoleColor.White);

    /// <summary>
    /// Logs a warning message to the server console.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public static void Warn(object message) => Raw(FormatLog(message, WarnPrefix, Assembly.GetCallingAssembly()), ConsoleColor.Yellow);

    /// <summary>
    /// Logs an error message to the server console.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public static void Error(object message) => Raw(FormatLog(message, ErrorPrefix, Assembly.GetCallingAssembly()), ConsoleColor.Red);

    /// <summary>
    /// Logs an internal warning message to the server console.
    /// </summary>
    /// <remarks>
    /// Should only be used when an issue is caused by an internal fault and not one caused by improper use of the API.
    /// </remarks>
    /// <param name="message">The message to log.</param>
    internal static void InternalWarn(object message) => Raw(InternalFormatLog(message, WarnPrefix), ConsoleColor.DarkYellow);

    /// <summary>
    /// Logs an internal error message to the server console.
    /// </summary>
    /// <remarks>
    /// Should only be used when an exception is caused by an internal fault and not one caused by improper use of the API.
    /// </remarks>
    /// <param name="message">The message to log.</param>
    internal static void InternalError(object message) => Raw(InternalFormatLog(message, ErrorPrefix), ConsoleColor.DarkRed);

    private static string FormatAssemblyName(Assembly assembly) => assembly.GetName().Name;

    private static string FormatLog(object message, string prefix, Assembly assembly) => $"[{prefix}] [{FormatAssemblyName(assembly)}] {message}";

    private static string InternalFormatLog(object message, string prefix) => $"[LabAPI {InternalPrefix} {prefix}] {message}";
}