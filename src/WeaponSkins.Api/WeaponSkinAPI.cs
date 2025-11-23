using WeaponSkins.Services;
using WeaponSkins.Shared;

namespace WeaponSkins;

public class WeaponSkinAPI : IWeaponSkinAPI
{
    private InventoryUpdateService InventoryUpdateService { get; init; }

    public WeaponSkinAPI(InventoryUpdateService inventoryUpdateService)
    {
        InventoryUpdateService = inventoryUpdateService;
    }

    public void SetWeaponSkins(IEnumerable<WeaponSkinData> skins)
    {
        InventoryUpdateService.UpdateWeaponSkins(skins);
    }

    public void SetKnifeSkins(IEnumerable<KnifeSkinData> knives)
    {
        InventoryUpdateService.UpdateKnifeSkins(knives);
    }

    public void SetGloveSkins(IEnumerable<GloveData> gloves)
    {
        InventoryUpdateService.UpdateGloveSkins(gloves);
    }
}