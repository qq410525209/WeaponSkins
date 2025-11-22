namespace WeaponSkins.Shared;

public interface IWeaponSkinAPI
{
    void UpdateWeaponSkins(IEnumerable<WeaponSkinData> skins);

    void UpdateKnifeSkins(IEnumerable<KnifeSkinData> knives);

    void UpdateGloveSkins(IEnumerable<GloveData> gloves);
}