namespace WeaponSkins;

public static class Utilities
{
    public static bool IsKnifeDefinitionIndex(int def)
    {
        return def is 42 or 59 or (>= 500 and < 600);
    }

    public static bool IsGloveDefinitionIndex(int def)
    {
        return def > 5000;
    }

    public static bool IsWeaponDefinitionIndex(int def)
    {
        return !IsKnifeDefinitionIndex(def) && !IsGloveDefinitionIndex(def) && def < 100;
    }
}