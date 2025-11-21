using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Events;

namespace WeaponSkins;

public class InventoryService
{
    private ISwiftlyCore Core { get; init; }
    private NativeService NativeService { get; init; }
    private DataService DataService { get; init; }
    private ILogger<InventoryService> Logger { get; init; }

    private Dictionary<ulong /* steamid */, CCSPlayerInventory /* inventory */> SubscribedInventories = new();

    public InventoryService(ISwiftlyCore core, NativeService nativeService, DataService dataService, ILogger<InventoryService> logger)
    {
        Core = core;
        NativeService = nativeService;
        DataService = dataService;
        Logger = logger;

        NativeService.OnSOCacheSubscribed += OnSOCacheSubscribed;

        Core.Event.OnClientDisconnected += OnClientDisconnected;
    }

    public CCSPlayerInventory Get(ulong steamid)
    {
        return SubscribedInventories[steamid];
    }

    private void OnSOCacheSubscribed(CCSPlayerInventory inventory, SOID_t soid)
    {
        Logger.LogInformation($"SOCacheSubscribed: {soid.SteamID}");
        SubscribedInventories[soid.SteamID] = inventory;
        Core.Scheduler.Delay(5, () => {
          UpdateSkin(soid.SteamID);
        });
    }

    private void OnClientDisconnected(IOnClientDisconnectedEvent @event)
    {
        var player = Core.PlayerManager.GetPlayer(@event.PlayerId);
        if (player == null)
        {
            return;
        }
        SubscribedInventories.Remove(player.SteamID);
    }

    public void UpdateSkin(ulong steamid)
    {
      Logger.LogInformation($"UpdateSkin: {steamid}");
      if (SubscribedInventories.TryGetValue(steamid, out var inventory))
      {
        Logger.LogInformation($"UpdateSkin: {steamid}");
        if (DataService.WeaponDataService.TryGetSkins(steamid, out var skins))
        {
          Logger.LogInformation($"UpdateSkin: {skins.Count()}");
          foreach (var skin in skins)
          {
            inventory.UpdateWeaponSkin(skin);
          }
        }
        if (DataService.KnifeDataService.TryGetKnives(steamid, out var knives))
        {
          Logger.LogInformation($"UpdateSkin: {knives.Count()}");
          foreach (var knife in knives)
          {
            inventory.UpdateKnifeSkin(knife);
          }
        }
      }
    }
}