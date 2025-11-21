namespace WeaponSkins;

public static class Utilities
{
    public static bool IsKnifeDefinitionIndex(int def)
    {
      return def == 42 || def == 59 || (def >= 500 && def < 600);
    }
}