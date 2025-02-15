using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using LabApi.Features.Interfaces;
using MapGeneration;
using Scp914;
using Scp914.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper over the <see cref="RoomIdentifier"/> that represents the SCP-914 room.
/// </summary>
public class Scp914 : Room
{
    /// <summary>
    /// Gets the current <see cref="Scp914"/> instance.
    /// </summary>
    /// <remarks>
    /// May be null if the map has not been generated yet or was previously destroyed.
    /// </remarks>
    public static Scp914? Instance { get; private set; }

    /// <summary>
    /// Contains all <see cref="IScp914ItemProcessor"/> instances, accessible by their <see cref="ItemBase"/>. 
    /// </summary>
    public static Dictionary<ItemBase, IScp914ItemProcessor> ItemProcessorCache = [];

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="roomIdentifier">The room identifier for the pocket dimension.</param>
    internal Scp914(RoomIdentifier roomIdentifier)
        : base(roomIdentifier)
    {
        Instance = this;
    }

    /// <summary>
    /// An internal method to set the instance to null when the base object is destroyed.
    /// </summary>
    internal override void OnRemoved()
    {
        base.OnRemoved();
        Instance = null;
    }

    /// <summary>
    /// Gets the main <see cref="Wrappers.Gate"/> of the SCP-914 room.
    /// </summary>
    public Gate Gate => (Gate)Doors.FirstOrDefault(x => x is Gate);

    /// <summary>
    /// Gets the entrance <see cref="Door"/> of the SCP-914 room.
    /// </summary>
    public Door Entrance => Doors.FirstOrDefault(x => x.Rooms.Length == 2);

    /// <summary>
    /// Gets the intake <see cref="Door"/> of the SCP-914 machine.
    /// </summary>
    public Door IntakeDoor => Door.Get(Scp914Controller.Singleton.Doors.Last());

    /// <summary>
    /// Gets the output <see cref="Door"/> of the SCP-914 machine.
    /// </summary>
    public Door OutputDoor => Door.Get(Scp914Controller.Singleton.Doors.First());

    /// <summary>
    /// Gets or sets the <see cref="Scp914KnobSetting"/> of the SCP-914 machine.
    /// </summary>
    public static Scp914KnobSetting KnobSetting
    {
        get => Scp914Controller.Singleton.KnobSetting;
        set => Scp914Controller.Singleton.Network_knobSetting = value;
    }

    /// <summary>
    /// Gets or sets whether the SCP-914 machine is currently upgrading.
    /// </summary>
    public static bool IsUpgrading
    {
        get => Scp914Controller.Singleton.IsUpgrading;
        set
        {
            if (Scp914Controller.Singleton.IsUpgrading == value)
                return;

            if (value)
                Scp914Controller.Singleton.Upgrade();
            else
            {
                Scp914Controller.Singleton.IsUpgrading = value;
                SequenceCooldown = 0.0f;
            }
        }
    }

    /// <summary>
    /// Gets or sets the <see cref="Scp914Mode"/> used by SCP-914 for upgrading.
    /// </summary>
    public static Scp914Mode Mode
    {
        get => Scp914Controller.Singleton.ConfigMode.Value;
        set => Scp914Controller.Singleton.ConfigMode.Value = value;
    }

    /// <summary>
    /// Gets the <see cref="Transform"/> of the intake chamber of the SCP-914 machine.
    /// </summary>
    public static Transform IntakeChamberTransform => Scp914Controller.Singleton.IntakeChamber;

    /// <summary>
    /// Gets the <see cref="Transform"/> of the output chamber of the SCP-914 machine.
    /// </summary>
    public static Transform OutputChamberTransform => Scp914Controller.Singleton.OutputChamber;

    /// <summary>
    /// Gets or sets the size of the SCP-914 chamber.
    /// </summary>
    public static Vector3 ChamberSize
    {
        get => Scp914Controller.Singleton.ChamberSize;
        set => Scp914Controller.Singleton.ChamberSize = value;
    }

    /// <summary>
    /// Gets or sets the minimum time before you can interact with the SCP-914 knob after changing it.
    /// </summary>
    public static float KnobChangeSequenceTime
    {
        get => Scp914Controller.Singleton.KnobChangeCooldown;
        set => Scp914Controller.Singleton.KnobChangeCooldown = value;
    }

    /// <summary>
    /// Gets or sets the minimum time before you can interact with the SCP-914 upgrade key after previously activating it.
    /// </summary>
    public static float UpgradeSequenceTime
    {
        get => Scp914Controller.Singleton.TotalSequenceTime;
        set => Scp914Controller.Singleton.TotalSequenceTime = value;
    }

    /// <summary>
    /// Gets or sets the current sequence cooldown.
    /// </summary>
    /// <remarks>
    /// This value is set by both <see cref="KnobChangeSequenceTime"/> and <see cref="UpgradeSequenceTime"/> depending on what interaction was made.
    /// To check the last interaction see <see cref="IsUpgrading"/>.
    /// </remarks>
    public static float SequenceCooldown
    {
        get => Scp914Controller.Singleton.RemainingCooldown;
        set => Scp914Controller.Singleton.RemainingCooldown = value;
    }

    /// <summary>
    /// Gets or sets the time after starting the upgrade sequence to close the SCP-914 chamber doors.
    /// </summary>
    public static float DoorCloseDelay
    {
        get => Scp914Controller.Singleton.DoorCloseTime;
        set => Scp914Controller.Singleton.DoorCloseTime = value;
    }

    /// <summary>
    /// Gets or sets the time after starting the upgrade sequence to upgrade/teleport items/players.
    /// </summary>
    /// <remarks>
    /// Make sure this value is less than the <see cref="UpgradeSequenceTime"/> otherwise it is never triggered.
    /// </remarks>
    public static float ItemUpgradeDelay
    {
        get => Scp914Controller.Singleton.ItemUpgradeTime;
        set => Scp914Controller.Singleton.ItemUpgradeTime = value;
    }

    /// <summary>
    /// Gets or sets the time after starting the upgrade sequence to open the SCP-914 chamber doors.
    /// </summary>
    /// <remarks>
    /// Make sure this value is less than the <see cref="UpgradeSequenceTime"/> otherwise the doors will never open.
    /// </remarks>
    public static float DoorOpenDelay
    {
        get => Scp914Controller.Singleton.DoorOpenTime;
        set => Scp914Controller.Singleton.DoorOpenTime = value;
    }

    /// <summary>
    /// Interact with the SCP-914 machine.
    /// </summary>
    /// <param name="interactCode">The type of interaction.</param>
    /// <param name="player">The <see cref="Player"/> that triggered the interaction or null if not specified.</param>
    /// <remarks>
    /// Interacting will also trigger SCP-914 related events.
    /// If you would not like to trigger events use <see cref="KnobSetting"/> and <see cref="IsUpgrading"/> instead.
    /// </remarks>
    public static void Interact(Scp914InteractCode interactCode, Player? player = null)
    {
        player ??= Server.Host;

        Scp914Controller.Singleton.ServerInteract(player.ReferenceHub, (byte)interactCode);
    }

    /// <summary>
    /// Plays a <see cref="Scp914Sound"/>.
    /// </summary>
    /// <param name="sound">The sound to play.</param>
    public static void PlaySound(Scp914Sound sound)
        => Scp914Controller.Singleton.RpcPlaySound((byte)sound);

    /// <summary>
    /// Gets the <see cref="IScp914ItemProcessor"/> for the specified type.
    /// </summary>
    /// <param name="type">The <see cref="ItemType"/> to get the associated <see cref="IScp914ItemProcessor"/>.</param>
    /// <returns>The associated <see cref="IScp914ItemProcessor"/> for the <see cref="ItemType"/> if found, otherwise null.</returns>
    /// <remarks>
    /// If the item processor is a base game <see cref="Scp914ItemProcessor"/>, <see cref="IScp914ItemProcessor"/> will be a <see cref="BaseGameItemProcessor"/>.
    /// </remarks>
    public static IScp914ItemProcessor? GetItemProcessor(ItemType type)
    {
        if (!InventoryItemLoader.TryGetItem(type, out ItemBase item))
            return null;

        if (ItemProcessorCache.TryGetValue(item, out var processor))
            return processor;

        if (!item.TryGetComponent(out Scp914ItemProcessor baseProcessor))
            return null;

        if (baseProcessor is ItemProcessorAdapter adaptor)
            ItemProcessorCache[item] = adaptor.Processor;
        else
            ItemProcessorCache[item] = new BaseGameItemProcessor(baseProcessor);

        return ItemProcessorCache[item];
    }

    /// <summary>
    /// Gets a <see cref="Dictionary{ItemType, IItemProcessor}"/> of all the item types and their associated item processors.
    /// </summary>
    /// <returns>The Dictionary containing all the item types and associated processors.</returns>
    /// <remarks>
    /// If the item processor is a base game <see cref="Scp914ItemProcessor"/>, <see cref="IScp914ItemProcessor"/> will be a <see cref="BaseGameItemProcessor"/>.
    /// </remarks>
    public static Dictionary<ItemType, IScp914ItemProcessor?> GetAllItemProcessors()
    {
        Dictionary<ItemType, IScp914ItemProcessor?> result = [];
        foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
        {
            if (!InventoryItemLoader.TryGetItem(type, out ItemBase _))
                continue;

            result.Add(type, GetItemProcessor(type));
        }

        return result;
    }

    /// <summary>
    /// Sets the <see cref="IScp914ItemProcessor"/> used by SCP-914 for the specified <see cref="ItemType"/>.
    /// </summary>
    /// <typeparam name="T">The class type that implements the <see cref="IScp914ItemProcessor"/> interface.</typeparam>
    /// <param name="type">The <see cref="ItemType"/> to set the processor to.</param>
    /// <param name="processor">An instance of the processor.</param>
    public static void SetItemProcessor<T>(ItemType type, T processor) where T : class, IScp914ItemProcessor
    {
        if (!InventoryItemLoader.TryGetItem(type, out ItemBase item))
            return;

        if (item.TryGetComponent(out Scp914ItemProcessor baseProcessor))
            UnityEngine.Object.Destroy(baseProcessor);

        item.gameObject.AddComponent<ItemProcessorAdapter>().Processor = processor;
        ItemProcessorCache[item] = processor;
    }

    /// <summary>
    /// Sets the <see cref="IScp914ItemProcessor"/> used by SCP-914 for <see cref="ItemType">Item Types</see> that match the predicate.
    /// </summary>
    /// <typeparam name="T">The class type that implements the <see cref="IScp914ItemProcessor"/> interface.</typeparam>
    /// <param name="predicate">A predicate to match which <see cref="ItemType">Item Types</see> to set the processor on using <see cref="Item"/> as a wrapper.</param>
    /// <param name="processor">An instance of the processor.</param>
    public static void SetItemProcessor<T>(Func<Item, bool> predicate, T processor) where T : class, IScp914ItemProcessor
    {
        foreach(ItemType type in Enum.GetValues(typeof(ItemType)))
        {
            if (!InventoryItemLoader.TryGetItem(type, out ItemBase item) || !predicate(Item.Get(item)))
                continue;

            SetItemProcessor(type, processor);
        }
    }

    /// <summary>
    /// Sets the base game <see cref="Scp914ItemProcessor"/> used by SCP-914 for the specified <see cref="ItemType"/>.
    /// </summary>
    /// <typeparam name="T">The class type that implements the <see cref="Scp914ItemProcessor"/> abstract class.</typeparam>
    /// <param name="type">The <see cref="ItemType"/> to set the processor to.</param>
    /// <remarks>
    /// Note that this is for setting the base game <see cref="Scp914ItemProcessor"/> for an item, you should always use <see cref="IScp914ItemProcessor"/> instead unless using already existing code see <see cref="SetItemProcessor{T}(ItemType, T)"/>.
    /// </remarks>
    public static void SetItemProcessor<T>(ItemType type) where T : Scp914ItemProcessor, new()
    {
        if (!InventoryItemLoader.TryGetItem(type, out ItemBase item))
            return;

        ItemProcessorCache.Remove(item);
        if (item.TryGetComponent(out Scp914ItemProcessor baseProcessor))
            UnityEngine.Object.Destroy(baseProcessor);

        item.gameObject.AddComponent<T>();
    }

    /// <summary>
    /// Sets the base game <see cref="Scp914ItemProcessor"/> used by SCP-914 for <see cref="ItemType">Item Types</see> that match the predicated.
    /// </summary>
    /// <typeparam name="T">The class type that implements the <see cref="Scp914ItemProcessor"/> abstract class.</typeparam>
    /// <param name="predicate">A predicate to match which <see cref="ItemType">Item Types</see> to set the processor on using <see cref="Item"/> as a wrapper.</param>
    /// <remarks>
    /// Note that this is for setting the base game <see cref="Scp914ItemProcessor"/> for an item, you should always use <see cref="IScp914ItemProcessor"/> instead unless using already existing code.
    /// </remarks>
    public static void SetItemProcessor<T>(Func<Item, bool> predicate) where T : Scp914ItemProcessor, new()
    {
        foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
        {
            if (!InventoryItemLoader.TryGetItem(type, out ItemBase item) || !predicate(Item.Get(item)))
                continue;

            SetItemProcessor<T>(type);
        }
    }
}
