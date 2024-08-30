using LabApi.Events.Arguments.Scp106Events;

namespace LabApi.Events.Handlers;

/// <inheritdoc />
public static partial class Scp106Events
{
    /// <summary>
    /// Invokes the <see cref="ChangingStalkMode"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp106ChangingStalkModeEventArgs"/> of the event.</param>
    public static void OnChangingStalkMode(Scp106ChangingStalkModeEventArgs args) => ChangingStalkMode.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ChangedStalkMode"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp106ChangedStalkModeEventArgs"/> of the event.</param>
    public static void OnChangedStalkMode(Scp106ChangedStalkModeEventArgs args) => ChangedStalkMode.InvokeEvent(args);
    
    /// <summary>
    /// Invokes the <see cref="ChangingSubmersionStatus"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp106ChangingSubmersionStatusEventArgs"/> of the event.</param>
    public static void OnChangingSubmersionStatus(Scp106ChangingSubmersionStatusEventArgs args) => ChangingSubmersionStatus.InvokeEvent(args);

    /// <summary>
    /// Invokes the <see cref="ChangedSubmersionStatus"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp106ChangedSubmersionStatusEventArgs"/> of the event.</param>
    public static void OnChangedSubmersionStatus(Scp106ChangedSubmersionStatusEventArgs args) => ChangedSubmersionStatus.InvokeEvent(args);
    
    /// <summary>
    /// Invokes the <see cref="ChangingVigor"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp106ChangingVigorEventArgs"/> of the event.</param>
    public static void OnChangingVigor(Scp106ChangingVigorEventArgs args) => ChangingVigor.InvokeEvent(args);
    
    /// <summary>
    /// Invokes the <see cref="ChangedVigor"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp106ChangedVigorEventArgs"/> of the event.</param>
    public static void OnChangedVigor(Scp106ChangedVigorEventArgs args) => ChangedVigor.InvokeEvent(args);
    
    /// <summary>
    /// Invokes the <see cref="UsedHunterAtlas"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp106UsedHunterAtlasEventArgs"/> of the event.</param>
    public static void OnUsedHunterAtlas(Scp106UsedHunterAtlasEventArgs args) => UsedHunterAtlas.InvokeEvent(args);
    
    /// <summary>
    /// Invokes the <see cref="UsingHunterAtlas"/> event.
    /// </summary>
    /// <param name="args">The <see cref="Scp106UsingHunterAtlasEventArgs"/> of the event.</param>
    public static void OnUsingHunterAtlas(Scp106UsingHunterAtlasEventArgs args) => UsingHunterAtlas.InvokeEvent(args);
    
}