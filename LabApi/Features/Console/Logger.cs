﻿using System;
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

    /// <summary>
    /// Logs a message to the server console with the specified color.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="color">The color of the message.</param>
    public static void Raw(string message, ConsoleColor color) => ServerConsole.AddLog(message, color);

    /// <summary>
    /// Logs a debug message to the server console.
    /// </summary>
    /// <param name="message">The message to log.</param>
    /// <param name="canBePrinted">Whether the message can be printed.</param>
    public static void Debug(object message, bool canBePrinted = true)
    {
        if (!canBePrinted)
            return;

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

    private static string FormatAssemblyName(Assembly assembly) => assembly.GetName().Name;

    private static string FormatLog(object message, string prefix, Assembly assembly) => $"[{prefix}] [{FormatAssemblyName(assembly)}] {message}";
}