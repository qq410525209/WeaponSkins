namespace WeaponSkins.Shared;

public interface IWeaponSkinAPI
{
    void SetWeaponSkins(IEnumerable<WeaponSkinData> skins);

    void SetKnifeSkins(IEnumerable<KnifeSkinData> knives);

    void SetGloveSkins(IEnumerable<GloveData> gloves);
}