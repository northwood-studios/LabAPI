using InventorySystem.Items.Firearms.Attachments;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Utils.Networking;
using BaseFirearmPickup = InventorySystem.Items.Firearms.FirearmPickup;

namespace LabApi.Features.Wrappers;

/// <summary>
/// Wrapper for the <see cref="BaseFirearmPickup"/> class.
/// </summary>
public class FirearmPickup : Pickup
{
    /// <summary>
    /// Contains all the cached firearm pickups, accessible through their <see cref="BaseFirearmPickup"/>.
    /// </summary>
    public new static Dictionary<BaseFirearmPickup, FirearmPickup> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="FirearmPickup"/>.
    /// </summary>
    public new static IReadOnlyCollection<FirearmPickup> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseFirearmPickup">The base <see cref="BaseFirearmPickup"/> object.</param>
    internal FirearmPickup(BaseFirearmPickup baseFirearmPickup)
        : base(baseFirearmPickup)
    {
        Base = baseFirearmPickup;

        if (CanCache)
            Dictionary.Add(baseFirearmPickup, this);
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
    /// The <see cref="BaseFirearmPickup"/> object.
    /// </summary>
    public new BaseFirearmPickup Base { get; }

    /// <summary>
    /// Gets or set the attachment code for the firearm.
    /// </summary>
    public uint AttachmentCode
    {
        get => AttachmentCodeSync.TryGet(Serial, out uint code) ? code : 0;
        set
        {
            AttachmentCodeSync.AttachmentCodeMessage msg = new(Serial, value);
            msg.SendToAuthenticated();
            msg.Apply();
        }
    }

    /// <summary>
    /// Gets the firearm pickup from the <see cref="Dictionary"/> or creates a new if it doesn't exist and the provided <see cref="BaseFirearmPickup"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="pickup">The <see cref="Base"/> if the pickup.</param>
    /// <returns>The requested pickup or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(pickup))]
    public static FirearmPickup? Get(BaseFirearmPickup? pickup)
    {
        if (pickup == null)
            return null;

        return Dictionary.TryGetValue(pickup, out FirearmPickup wrapper) ? wrapper : (FirearmPickup)CreateItemWrapper(pickup);
    }
}