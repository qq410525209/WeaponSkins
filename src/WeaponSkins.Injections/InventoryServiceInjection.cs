using Microsoft.Extensions.DependencyInjection;

namespace WeaponSkins;

public static class InventoryServiceInjection
{
  public static IServiceCollection AddInventoryService(this IServiceCollection services)
  {
    return services.AddSingleton<InventoryService>();
  }

  public static IServiceProvider UseInventoryService(this IServiceProvider provider)
  {
    provider.GetRequiredService<InventoryService>();
    return provider;
  }
}