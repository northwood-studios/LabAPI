using Generators;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Attachments;
using InventorySystem.Items.Firearms.Attachments.Components;
using InventorySystem.Items.Firearms.Modules;
using LabApi.Features.Console;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="Firearm"/>.<para/>
/// Firearms are functioning as they close as they would in real life. 
/// This means that there are properties for whether the bolt is closed or opened, whether the hammer is cocked for specific firearms, whether the magazine is inserted and so many other properties you may need to be aware of when adjusting this firearm item.
/// </summary>
public class FirearmItem : Item
{
    /// <summary>
    /// Initializes the <see cref="FirearmItem"/> class by subscribing to <see cref="Firearm"/> events and registers derived wrappers.
    /// </summary>
    [InitializeWrapper]
    internal static void InitializeFirearmWrappers()
    {
        Register(ItemType.ParticleDisruptor, (x) => new ParticleDisruptorItem((ParticleDisruptor)x));
        Register(ItemType.GunRevolver, (x) => new RevolverFirearm(x));
        Register(ItemType.GunSCP127, (x) => new Scp127Firearm(x));
        Register(ItemType.GunShotgun, (x) => new ShotgunFirearm(x));
    }
    /// <summary>
    /// Contains all the cached firearm items, accessible through their <see cref="Firearm"/>.
    /// </summary>
    public new static Dictionary<Firearm, FirearmItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="FirearmItem"/>.
    /// </summary>
    public new static IReadOnlyCollection<FirearmItem> List => Dictionary.Values;

    /// <summary>
    /// Contains all the handlers for constructing wrappers for the associated base game types.
    /// </summary>
    private static readonly Dictionary<ItemType, Func<Firearm, FirearmItem>> typeWrappers = [];

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="firearm">The base <see cref="Firearm"/> object.</param>
    internal FirearmItem(Firearm firearm) : base(firearm)
    {
        Base = firearm;

        if (CanCache)
            Dictionary.Add(firearm, this);

        CacheModules();
    }

    /// <summary>
    /// The base <see cref="Firearm"/> object.
    /// </summary>
    public new Firearm Base { get; }

    /// <summary>
    /// Gets the total firearm's weight including attachments in kilograms.
    /// </summary>
    public new float Weight => Base.Weight;

    /// <summary>
    /// Gets the total length of this firearm in inches.
    /// </summary>
    public float Length => Base.Length;

    /// <summary>
    /// Gets the weight of the firearm in kilograms without any attachments.
    /// </summary>
    public float BaseWeight => Base.BaseLength;

    /// <summary>
    /// Gets the length of the firearm in inches without any attachments.
    /// </summary>
    public float BaseLength => Base.BaseLength;

    /// <summary>
    /// Gets whether the player can reload this firearm.
    /// </summary>
    public bool CanReload => IReloadUnloadValidatorModule.ValidateReload(Base);

    /// <summary>
    /// Gets whether the player can unload this firearm.
    /// </summary>
    public bool CanUnload => IReloadUnloadValidatorModule.ValidateUnload(Base);

    /// <summary>
    /// Gets the firearm's ammo type.
    /// </summary>
    /// <remarks>
    /// May be <see cref="ItemType.None"/> if the firearm item is no longer valid or this firearm is <see cref="ParticleDisruptorItem"/>.
    /// </remarks>
    public ItemType AmmoType
    {
        get
        {
            if (_ammoContainerModule == null)
                return ItemType.None;

            return _ammoContainerModule.AmmoType;
        }
    }

    /// <summary>
    /// Gets or sets whether the firearm's hammer is cocked.
    /// </summary>
    /// <remarks>
    /// Every automatic firearm requires <see cref="Cocked"/> to be <see langword="true"/> and <see cref="BoltLocked"/> to be <see langword="false"/> to be fired properly with chambered ammo.
    /// </remarks>
    public virtual bool Cocked
    {
        get
        {
            if (_actionModule is AutomaticActionModule actionModule)
                return actionModule.Cocked;

            return false;
        }
        set
        {
            if (_actionModule is not AutomaticActionModule actionModule)
            {
                Logger.Error($"Unable to set {nameof(Cocked)} as this firearm's {nameof(IActionModule)} is invalid");
                return;
            }

            actionModule.Cocked = value;
            actionModule.ServerResync();
        }
    }

    /// <summary>
    /// Gets or sets whether the firearm's bolt is in rear position.<para/>
    /// This is only used by closed-bolt firearms.
    /// </summary>
    public virtual bool BoltLocked
    {
        get
        {
            if (_actionModule is AutomaticActionModule actionModule)
                return actionModule.BoltLocked;

            return false;
        }
        set
        {
            if (_actionModule is not AutomaticActionModule actionModule)
            {
                Logger.Error($"Unable to set {nameof(BoltLocked)} as this firearm's {nameof(IActionModule)} is invalid");
                return;
            }

            actionModule.BoltLocked = value;
            actionModule.ServerResync();
        }
    }

    /// <summary>
    /// Gets if the firearm fires from an open bolt, that means the chambers are not capable of storing any ammo in <see cref="ChamberedAmmo"/>.
    /// </summary>
    public virtual bool OpenBolt
    {
        get
        {
            if (_actionModule is AutomaticActionModule actionModule)
                return actionModule.OpenBolt;

            return false;
        }
    }

    /// <summary>
    /// Gets the firerate with current attachment's modifiers applied.
    /// </summary>
    public float Firerate
    {
        get
        {
            if (_actionModule != null)
                return _actionModule.DisplayCyclicRate;

            return 0;
        }
    }

    /// <summary>
    /// Gets or sets the attachments code for this firearm.<br/>
    /// Attachments code is a binary representation of <see cref="Attachments"/> which are enabled/disabled.<br/>
    /// For validation, see <see cref="ValidateAttachmentsCode(uint)"/>.
    /// </summary>
    public uint AttachmentsCode
    {
        get => Base.GetCurrentAttachmentsCode();
        set
        {
            Base.ApplyAttachmentsCode(value, true);
            Base.ServerResendAttachmentCode();
        }
    }

    /// <summary>
    /// Gets or sets whether this firearm has inserted magazine.<br/>
    /// An empty magazine is inserted when set to <see langword="true"/>.<br/>
    /// Remaining ammo from the magazine is inserted back into player's inventory when removed.
    /// </summary>
    /// <remarks>
    /// Firearms with no external or internal magazine will always return <see langword="false"/>.
    /// </remarks>
    public virtual bool MagazineInserted
    {
        get
        {
            if (_magazineControllerModule == null)
                return false;

            return _magazineControllerModule.MagazineInserted;
        }
        set
        {
            if (_magazineControllerModule is not MagazineModule magazineModule)
            {
                Logger.Error($"Unable to set {nameof(MagazineInserted)} as this firearm's {nameof(IMagazineControllerModule)} is null");
                return;
            }

            if (value)
                magazineModule.ServerInsertEmptyMagazine();
            else
                magazineModule.ServerRemoveMagazine();
        }
    }

    /// <summary>
    /// Gets or sets the stored ammo in a <b>ammo container</b> for this firearm.
    /// </summary>
    /// <remarks>
    /// Ammo in magazine beyond <see cref="MaxAmmo"/> when pickup up this firearm again from the ground is added back to player's inventory.
    /// </remarks>
    public virtual int StoredAmmo
    {
        get
        {
            if (_ammoContainerModule == null)
                return 0;

            return _ammoContainerModule.AmmoStored;
        }
        set
        {
            if (_ammoContainerModule == null)
            {
                Logger.Error($"Unable to set {nameof(StoredAmmo)} as this firearm's {nameof(IPrimaryAmmoContainerModule)} is null");
                return;
            }

            int toAdd = value - _ammoContainerModule.AmmoStored;
            _ammoContainerModule.ServerModifyAmmo(toAdd);
        }
    }

    /// <summary>
    /// Gets the maximum ammo the firearm can have in its <b>ammo container</b>. Attachment modifiers are taken into account when calculating it.
    /// </summary>
    public int MaxAmmo
    {
        get
        {
            if (_ammoContainerModule == null)
                return 0;

            return _ammoContainerModule.AmmoMax;
        }
    }

    /// <summary>
    /// Gets or sets the current ammo in the chamber.
    /// <para><see cref="OpenBolt"/> firearms do not use this value and take ammo directly from it's ammo container.</para>
    /// </summary>
    public virtual int ChamberedAmmo
    {
        get
        {
            if (_actionModule is AutomaticActionModule actionModule)
                return actionModule.AmmoStored;

            return 0;
        }
        set
        {
            if (_actionModule is not AutomaticActionModule actionModule)
            {
                Logger.Error($"Unable to set {nameof(ChamberedAmmo)} as this firearm's {nameof(IActionModule)} is not valid.");
                return;
            }

            actionModule.AmmoStored = value;
            actionModule.ServerResync();
        }
    }

    /// <summary>
    /// Gets or sets the maximum ammo in chamber.
    /// Visual side may be incorrect.<para/>
    /// For automatic firearms, this value won't properly sync above 16 as only 4 bits are used for the chambered sync to the client.
    /// </summary>
    public virtual int ChamberMax
    {
        get
        {
            if (_actionModule is AutomaticActionModule actionModule)
                return actionModule.ChamberSize;


            return 0;
        }
        set
        {
            if (_actionModule is not AutomaticActionModule actionModule)
            {
                Logger.Error($"Unable to set {nameof(ChamberMax)} as this firearm's {nameof(IActionModule)} is not valid.");
                return;
            }

            actionModule.ChamberSize = value;
        }
    }

    /// <summary>
    /// Gets or sets whether the firearm's flashlight attachment is enabled and is emitting light.
    /// </summary>
    public bool FlashlightEnabled
    {
        get
        {
            foreach (Attachment attachment in Attachments)
            {
                if (attachment is not FlashlightAttachment flashlightAttachment)
                    continue;

                return flashlightAttachment.IsEnabled && flashlightAttachment.IsEmittingLight;
            }

            return false;
        }
        set
        {
            foreach (Attachment attachment in Attachments)
            {
                if (attachment is not FlashlightAttachment flashlightAttachment)
                    continue;

                flashlightAttachment.ServerSendStatus(value);
            }
        }
    }

    /// <summary>
    /// All attachment used by this firearm.<br/>
    /// <b>Set the attachments status using <see cref="AttachmentsCode"/></b>
    /// </summary>
    public Attachment[] Attachments => Base.Attachments;

    /// <summary>
    /// All modules used by this firearm.
    /// Modules are the main scripts defining all of the functionality of a firearm.
    /// </summary>
    public ModuleBase[] Modules => Base.Modules;

    /// <summary>
    /// Gets all available attachments names of this firearms.
    /// </summary>
    public IEnumerable<AttachmentName> AvailableAttachmentsNames
    {
        get
        {
            foreach (Attachment attachment in Attachments)
            {
                yield return attachment.Name;
            }
        }
    }

    /// <summary>
    /// Gets all enabled attachments of this firearm.
    /// </summary>
    public IEnumerable<Attachment> ActiveAttachments
    {
        get
        {
            foreach (Attachment attachment in Attachments)
            {
                if (attachment.IsEnabled)
                    yield return attachment;
            }
        }
    }

    /// <summary>
    /// Module for the magazine.
    /// </summary>
    protected IPrimaryAmmoContainerModule _ammoContainerModule;

    /// <summary>
    /// Module for firearm's chamber.
    /// </summary>
    protected IActionModule _actionModule;

    /// <summary>
    /// Module for handling gun's reloading and unloading.
    /// </summary>
    protected IReloaderModule _reloaderModule;

    /// <summary>
    /// Module for handling gun's magazine.
    /// </summary>
    protected IMagazineControllerModule _magazineControllerModule;

    /// <summary>
    /// Gets whether the provided attachments code is valid and can be applied.
    /// </summary>
    /// <param name="code">The code to validate.</param>
    /// <returns>Whether the code is valid and can be applied.</returns>
    public bool CheckAttachmentsCode(uint code) => ValidateAttachmentsCode(code) == code;

    /// <summary>
    /// Gets whether the provided attachment names are valid together and only 1 belongs to each category.
    /// </summary>
    /// <param name="attachments">Attachment names.</param>
    /// <returns>Whether the attachments can be applied together.</returns>
    public bool CheckAttachmentsCode(params AttachmentName[] attachments)
    {
        uint rawCode = GetCodeFromAttachmentNamesRaw(attachments);

        return rawCode == ValidateAttachmentsCode(rawCode);
    }

    /// <summary>
    /// Gets validated attachments code.
    /// Validation of the code is following:
    /// <list type="bullet">
    /// <item>Only 1 attachments from the same <see cref="AttachmentSlot"/> is applied. If multiple ones are enabled, only the first one is selected.</item>
    /// <item><see cref="AttachmentSlot"/> without any attachents assigned sets the first one to enabled.</item>
    /// </list>
    /// </summary>
    /// <param name="code">The code to be validated.</param>
    /// <returns>Validated code with missing attachments added for category and only 1 attachment per category selected.</returns>
    public uint ValidateAttachmentsCode(uint code) => Base.ValidateAttachmentsCode(code);

    /// <inheritdoc cref="ValidateAttachmentsCode(uint)"/>
    /// <param name="attachments">Array of attachment names to be applied and validated.</param>
    public uint ValidateAttachmentsCode(params AttachmentName[] attachments)
    {
        return ValidateAttachmentsCode(GetCodeFromAttachmentNamesRaw(attachments));
    }

    /// <summary>
    /// Gets attachments code from <see cref="AttachmentName"/>s. This value is NOT validated.
    /// </summary>
    /// <param name="attachments">Attachment names.</param>
    /// <returns>Unchecked attachments code.</returns>
    public uint GetCodeFromAttachmentNamesRaw(AttachmentName[] attachments)
    {
        uint resultCode = 0;

        uint bin = 1;
        foreach (Attachment attachment in Attachments)
        {
            if (attachments.Contains(attachment.Name))
                resultCode += bin;

            bin *= 2;
        }

        return resultCode;
    }

    /// <summary>
    /// Reloads the firearm if <see cref="CanReload"/> is <see langword="true"/>.
    /// </summary>
    /// <returns>Whether the player started to reload.</returns>
    public bool Reload()
    {
        if (_reloaderModule is not AnimatorReloaderModuleBase animatorModule)
        {
            Logger.Error($"Unable to reload this firearm as it's animator module is invalid");
            return false;
        }

        return animatorModule.ServerTryReload();
    }

    /// <summary>
    /// Unloads the firearm if <see cref="CanUnload"/> is <see langword="true"/>.
    /// </summary>
    /// <returns>Whether the player started the unload.</returns>
    public bool Unload()
    {
        if (_reloaderModule is not AnimatorReloaderModuleBase animatorModule)
        {
            Logger.Error($"Unable to unload this firearm as it's animator module is invalid");
            return false;
        }

        return animatorModule.ServerTryUnload();
    }

    /// <summary>
    /// An internal method to remove itself from the cache when the base object is destroyed.
    /// </summary>
    internal override void OnRemove()
    {
        base.OnRemove();
        Dictionary.Remove(Base);
    }

    /// <summary>
    /// Caches modules used by the firearm.
    /// </summary>
    protected virtual void CacheModules()
    {
        foreach (ModuleBase module in Modules)
        {
            switch (module)
            {
                case IPrimaryAmmoContainerModule ammoModule:
                    _ammoContainerModule = ammoModule;
                    continue;
                case IActionModule actionModule:
                    _actionModule = actionModule;
                    continue;
                case IReloaderModule reloaderModule:
                    _reloaderModule = reloaderModule;
                    continue;
                case IMagazineControllerModule magazineControllerModule:
                    _magazineControllerModule = magazineControllerModule;
                    continue;
            }
        }
    }

    /// <summary>
    /// Gets the firearm item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="Firearm"/> was not <see langword="null"/>.
    /// </summary>
    /// <param name="firearm">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(firearm))]
    public static FirearmItem? Get(Firearm? firearm)
    {
        if (firearm == null)
            return null;

        return Dictionary.TryGetValue(firearm, out FirearmItem item) ? item : (FirearmItem)CreateItemWrapper(firearm);
    }

    /// <summary>
    /// Creates a firearm wrapper or it's subtype.
    /// </summary>
    /// <param name="firearm">The base game firearm.</param>
    /// <returns>Firearm wrapper object.</returns>
    internal static FirearmItem CreateFirearmWrapper(Firearm firearm)
    {
        if (!typeWrappers.TryGetValue(firearm.ItemTypeId, out Func<Firearm, FirearmItem> ctor))
            return new FirearmItem(firearm);

        return ctor(firearm);
    }

    /// <summary>
    /// A private method to handle the addition of wrapper handlers.
    /// </summary>
    /// <param name="itemType">Item type of the target firearm.</param>
    /// <param name="constructor">A handler to construct the wrapper with the base game instance.</param>
    private static void Register(ItemType itemType, Func<Firearm, FirearmItem> constructor)
    {
        typeWrappers.Add(itemType, x => constructor(x));
    }
}
