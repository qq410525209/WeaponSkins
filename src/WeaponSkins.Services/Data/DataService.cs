namespace WeaponSkins;

public class DataService
{
    public WeaponDataService WeaponDataService { get; init; }

    public KnifeDataService KnifeDataService { get; init; }

    public DataService(
      WeaponDataService weaponDataService,
      KnifeDataService knifeDataService
    )
    {
        WeaponDataService = weaponDataService;
        KnifeDataService = knifeDataService;
    }
}