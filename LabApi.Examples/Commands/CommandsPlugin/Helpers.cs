using Footprinting;
using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.ThrowableProjectiles;
using LabApi.Features.Wrappers;
using Mirror;
using UnityEngine;
using Logger = LabApi.Features.Console.Logger;
using ThrowableItem = InventorySystem.Items.ThrowableProjectiles.ThrowableItem;

namespace CommandsPlugin;

public static class Helpers
{
    public static bool SpawnLiveProjectile(ItemType itemType, Player player)
    {
        if (!InventoryItemLoader.TryGetItem(itemType, out ThrowableItem ib))
        {
            Logger.Error($"Provided item type {itemType} is not a throwable item!");
            return false;
        }

        ThrownProjectile projectile = Object.Instantiate(ib.Projectile, player.Position, player.Rotation);

        PickupSyncInfo psi = new PickupSyncInfo(itemType, ib.Weight, ItemSerialGenerator.GenerateNext())
        {
            Locked = true
        };

        projectile.Info = psi;
        projectile.PreviousOwner = new Footprint(player.ReferenceHub);
        projectile.ServerActivate();
        NetworkServer.Spawn(projectile.gameObject);
        return true;
    }
}