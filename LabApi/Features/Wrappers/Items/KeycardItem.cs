using Interactables.Interobjects.DoorUtils;
using InventorySystem;
using InventorySystem.Items.Keycards;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using BaseKeycardItem = InventorySystem.Items.Keycards.KeycardItem;

namespace LabApi.Features.Wrappers;

/// <summary>
/// The wrapper representing <see cref="BaseKeycardItem"/>.
/// </summary>
public class KeycardItem : Item
{
    /// <summary>
    /// Contains all the cached keycard items, accessible through their <see cref="BaseKeycardItem"/>.
    /// </summary>
    public new static Dictionary<BaseKeycardItem, KeycardItem> Dictionary { get; } = [];

    /// <summary>
    /// A reference to all instances of <see cref="KeycardItem"/>.
    /// </summary>
    public new static IReadOnlyCollection<KeycardItem> List => Dictionary.Values;

    /// <summary>
    /// An internal constructor to prevent external instantiation.
    /// </summary>
    /// <param name="baseKeycardItem">The base <see cref="BaseKeycardItem"/> object.</param>
    internal KeycardItem(BaseKeycardItem baseKeycardItem)
        : base(baseKeycardItem)
    {
        Base = baseKeycardItem;

        if (CanCache)
            Dictionary.Add(baseKeycardItem, this);
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
    /// The base <see cref="BaseKeycardItem"/> object.
    /// </summary>
    public new BaseKeycardItem Base { get; }

    /// <summary>
    /// Gets the <see cref="DoorPermissionFlags"/> of the keycard.
    /// </summary>
    public DoorPermissionFlags Permissions => Base.GetPermissions(null);

    /// <summary>
    /// Gets the <see cref="KeycardLevels"/> of the keycard which represent the tiers shown on the keycard.
    /// </summary>
    public KeycardLevels Levels => new KeycardLevels(Permissions);

    #region Custom Keycards

    /// <summary>
    /// Creates custom <see cref="ItemType.KeycardCustomSite02"/> and gives it to the <paramref name="targetPlayer"/>.
    /// </summary>
    /// <param name="targetPlayer">The player who should receive the keycard.</param>
    /// <param name="itemName">Item name of the keycard.</param>
    /// <param name="holderName">Name written as holder of the keycard.</param>
    /// <param name="cardLabel">Label written on the keycard.</param>
    /// <param name="permissions">Permission levels that the keycard has.</param>
    /// <param name="keycardColor">Primary color of the keycard.</param>
    /// <param name="permissionsColor">Color of the permission circles on the keycard.</param>
    /// <param name="labelColor">Color of the <paramref name="cardLabel"/>.</param>
    /// <param name="wearLevel">Wear level of the keycard. A number between 0-3 (inclusive).</param>
    /// <returns>The keycard item. Or <see langword="null"/> if the item couldn't be given to the target <paramref name="targetPlayer"/>.</returns>
    public static KeycardItem? CreateCustomKeycardSite02(Player targetPlayer, string itemName, string holderName, string cardLabel, KeycardLevels permissions, Color keycardColor, Color permissionsColor, Color labelColor, byte wearLevel) =>
        CreateCustomCard(ItemType.KeycardCustomSite02, targetPlayer, itemName, permissions, (Color32)permissionsColor, (Color32)keycardColor, cardLabel, (Color32)labelColor, holderName, wearLevel);

    /// <summary>
    /// Creates custom <see cref="ItemType.KeycardCustomTaskForce"/> and gives it to the <paramref name="targetPlayer"/>.
    /// </summary>
    /// <param name="targetPlayer">The player who should receive the keycard.</param>
    /// <param name="itemName">Item name of the keycard.</param>
    /// <param name="holderName">Name written as holder of the keycard.</param>
    /// <param name="permissions">Permission levels that the keycard has.</param>
    /// <param name="keycardColor">Primary color of the keycard.</param>
    /// <param name="permissionsColor">Color of the permission circles on the keycard.</param>
    /// <param name="serialLabel">12 digit string containing serial number written on the keycard. Any missing digits are prepended with 0 and any extra characters after 12 are ignored.</param>
    /// <param name="rankIndex">Rank level of the keycard. A number between 0-3 (inclusive).</param>
    /// <returns>The keycard item. Or <see langword="null"/> if the item couldn't be given to the target <paramref name="targetPlayer"/>.</returns>
    public static KeycardItem? CreateCustomKeycardTaskForce(Player targetPlayer, string itemName, string holderName, KeycardLevels permissions, Color keycardColor, Color permissionsColor, string serialLabel, int rankIndex) =>
        CreateCustomCard(ItemType.KeycardCustomTaskForce, targetPlayer, itemName, permissions, (Color32)permissionsColor, (Color32)keycardColor, holderName, serialLabel, rankIndex);

    /// <summary>
    /// Creates custom <see cref="ItemType.KeycardCustomMetalCase"/> and gives it to the <paramref name="targetPlayer"/>.
    /// </summary>
    /// <param name="targetPlayer">The player who should receive the keycard.</param>
    /// <param name="itemName">Item name of the keycard.</param>
    /// <param name="holderName">Name written as holder of the keycard.</param>
    /// <param name="cardLabel">Label written on the keycard.</param>
    /// <param name="permissions">Permission levels that the keycard has.</param>
    /// <param name="keycardColor">Primary color of the keycard.</param>
    /// <param name="permissionsColor">Color of the permission circles on the keycard.</param>
    /// <param name="labelColor">Color of the <paramref name="cardLabel"/>.</param>
    /// <param name="wearLevel">Wear level of the keycard. A number between 0-3 (inclusive).</param>
    /// <param name="serialLabel">12 digit string containing serial number written on the keycard. Any missing digits are prepended with 0 and any extra characters after 12 are ignored.</param>
    /// <returns>The keycard item. Or <see langword="null"/> if the item couldn't be given to the target <paramref name="targetPlayer"/>.</returns>
    public static KeycardItem? CreateCustomKeycardMetal(Player targetPlayer, string itemName, string holderName, string cardLabel, KeycardLevels permissions, Color keycardColor, Color permissionsColor, Color labelColor, byte wearLevel, string serialLabel) =>
        CreateCustomCard(ItemType.KeycardCustomMetalCase, targetPlayer, itemName, permissions, (Color32)permissionsColor, (Color32)keycardColor, cardLabel, (Color32)labelColor, holderName, serialLabel, wearLevel);

    /// <summary>
    /// Creates custom <see cref="ItemType.KeycardCustomManagement"/> and gives it to the <paramref name="targetPlayer"/>.
    /// </summary>
    /// <param name="targetPlayer">The player who should receive the keycard.</param>
    /// <param name="itemName">Item name of the keycard.</param>
    /// <param name="cardLabel">Label written on the keycard.</param>
    /// <param name="permissions">Permission levels that the keycard has.</param>
    /// <param name="keycardColor">Primary color of the keycard.</param>
    /// <param name="permissionsColor">Color of the permission circles on the keycard.</param>
    /// <param name="labelColor">Color of the <paramref name="cardLabel"/>.</param>
    /// <returns>The keycard item. Or <see langword="null"/> if the item couldn't be given to the target <paramref name="targetPlayer"/>.</returns>
    public static KeycardItem? CreateCustomKeycardManagement(Player targetPlayer, string itemName, string cardLabel, KeycardLevels permissions, Color keycardColor, Color permissionsColor, Color labelColor) =>
        CreateCustomCard(ItemType.KeycardCustomManagement, targetPlayer, itemName, permissions, (Color32)permissionsColor, (Color32)keycardColor, cardLabel, (Color32)labelColor);

    /// <summary>
    /// Creates a custom keycard of <see cref="ItemType"/>.<br/>
    /// It is recommended to use other methods such as <see cref="CreateCustomKeycardSite02"/> rather than figuring the parameters yourself.
    /// </summary>
    /// <param name="itemType">Type of the custom keycard.</param>
    /// <param name="targetPlayer">Players who should receive the keycard.</param>
    /// <param name="args">Object arguments to be given to the keycard.</param>
    /// <returns>The keycard item. Or <see langword="null"/> if the <paramref name="itemType"/> is not customizable or item couldn't be given to the target <paramref name="targetPlayer"/>.</returns>
    public static KeycardItem? CreateCustomCard(ItemType itemType, Player targetPlayer, params object[] args)
    {
        if (targetPlayer == null)
            return null;

        if (!itemType.TryGetTemplate(out BaseKeycardItem template))
            throw new ArgumentException($"Template for {nameof(itemType)} not found");

        if (!template.Customizable)
            return null;

        int index = 0;
        foreach (DetailBase detailBase in template.Details)
        {
            if (detailBase is not ICustomizableDetail customizableDetail)
                continue;

            customizableDetail.SetArguments(new ArraySegment<object>(args, index, customizableDetail.CustomizablePropertiesAmount));
            index += customizableDetail.CustomizablePropertiesAmount;
        }

        return (KeycardItem?)targetPlayer.AddItem(itemType);
    }

    #endregion

    /// <summary>
    /// Gets the keycard item wrapper from the <see cref="Dictionary"/> or creates a new one if it doesn't exist and the provided <see cref="BaseKeycardItem"/> was not null.
    /// </summary>
    /// <param name="baseKeycardItem">The <see cref="Base"/> of the item.</param>
    /// <returns>The requested item or <see langword="null"/>.</returns>
    [return: NotNullIfNotNull(nameof(baseKeycardItem))]
    public static KeycardItem? Get(BaseKeycardItem? baseKeycardItem)
    {
        if (baseKeycardItem == null)
            return null;

        return Dictionary.TryGetValue(baseKeycardItem, out KeycardItem item) ? item : (KeycardItem)CreateItemWrapper(baseKeycardItem);
    }
}
