using InventorySystem.Items.Radio;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BaseRadioPickup = InventorySystem.Items.Radio.RadioPickup;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for the <see cref="BaseRadioPickup"/> class.
/// </summary>
public class RadioPickup : Pickup
{
    /// <summary>
    /// Contains all the cached radio pickups, accessible through their <see cref="BaseRadioPickup"/>.
    /// </summary>
    public new static Dictionary<BaseRadioPickup, RadioPickup> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="RadioPickup"/>.
    /// </summary>
    public new static IReadOnlyCollection<RadioPickup> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseRadioPickup">The base <see cref="BaseRadioPickup"/> object.</param>
    internal RadioPickup(BaseRadioPickup baseRadioPickup)
        : base(baseRadioPickup)
    {
        Base = baseRadioPickup;

        if (CanCache)
            Dictionary.Add(baseRadioPickup, this);
    }

    /// <summary>
    /// A internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }

    /// <summary>
    /// The <see cref="BaseRadioPickup"/> object.
    /// </summary>
    public new BaseRadioPickup Base { get; }

    /// <summary>
    /// Gets or sets whether the radio is on.
    /// </summary>
    public bool IsEnabled
    {
        get => Base.SavedEnabled;
        set => Base.NetworkSavedEnabled = value;
    }

    /// <summary>
    /// Gets or set the <see cref="RadioMessages.RadioRangeLevel"/>.
    /// </summary>
    public RadioMessages.RadioRangeLevel RangeLevel
    {
        get => (RadioMessages.RadioRangeLevel)Base.SavedRange;
        set
        {
            if (value == RadioMessages.RadioRangeLevel.RadioDisabled)
                IsEnabled = false;
            else
                Base.NetworkSavedRange = (byte)value;
        }
    }

    /// <summary>
    /// Gets or sets the battery percentage from 0.0 to 1.0.
    /// </summary>
    public float Battery
    {
        get => Base.SavedBattery;
        set => Base.SavedBattery = value;
    }

    /// <summary>
    /// Gets the radio pickup from the <see cref="Dictionary"/> or creates a new if it doesn't exist and the provided <see cref="BaseRadioPickup"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="pickup">The <see cref="Base"/> if the pickup.</param>
    /// <returns>The requested pickup or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(pickup))]
    public static RadioPickup? Get(BaseRadioPickup? pickup)
    {
        if (pickup == null)
            return null;

        return Dictionary.TryGetValue(pickup, out RadioPickup wrapper) ? wrapper : (RadioPickup)CreateItemWrapper(pickup);
    }
}
