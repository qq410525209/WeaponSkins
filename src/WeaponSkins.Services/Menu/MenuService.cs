using Microsoft.Extensions.Logging;

using SwiftlyS2.Core.Menus.OptionsBase;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Menus;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

using WeaponSkins.Econ;

namespace WeaponSkins.Services;

public class MenuService
{
    private ISwiftlyCore Core { get; init; }
    private ILogger<MenuService> Logger { get; init; }
    private WeaponSkinAPI Api { get; init; }
    private EconService EconService { get; init; }

    public MenuService(ISwiftlyCore core,
        ILogger<MenuService> logger,
        WeaponSkinAPI api,
        EconService econService)
    {
        Core = core;
        Logger = logger;
        Api = api;
        EconService = econService;
    }

    public void TestMenu(IPlayer player)
    {
        var main = Core.MenusAPI.CreateBuilder();
        main.Design.SetMenuTitle("Skins");

        main.AddOption(new SubmenuMenuOption("Weapon Skins", BuildWeaponSkinMenu(player)));
        main.AddOption(new SubmenuMenuOption("Knife Skins", BuildKnifeSkinMenu(player)));
        main.AddOption(new SubmenuMenuOption("Glove Skins", BuildGloveSkinMenu(player)));

        Core.MenusAPI.OpenMenuForPlayer(player, main.Build());
    }


    public IMenuAPI BuildWeaponSkinMenu(IPlayer player)
    {
        var main = Core.MenusAPI.CreateBuilder();
        main.Design.SetMenuTitle("Weapon Skins");

        foreach (var (weapon, paintkits) in EconService.WeaponToPaintkits)
        {
            var item = EconService.Items[weapon];
            if (!Utilities.IsWeaponDefinitionIndex(item.Index))
            {
                continue;
            }

            var skinMenu = Core.MenusAPI.CreateBuilder();
            skinMenu.Design.SetMenuTitle(weapon);
            var sorted = paintkits.OrderByDescending(p => p.Rarity.Id).ToList();
            foreach (var paintkit in sorted)
            {
                if (!paintkit.LocalizedNames.ContainsKey("schinese"))
                {
                    throw new Exception($"Paintkit {paintkit} not found in languages schinese");
                    continue;
                }

                var option = new ButtonMenuOption(HtmlGradient.GenerateGradientText(paintkit.LocalizedNames["schinese"],
                    paintkit.Rarity.Color.HexColor));

                option.Click += (_,
                    args) =>
                {
                    Api.SetWeaponSkins([
                        new()
                        {
                            SteamID = args.Player.SteamID,
                            Team = args.Player.Controller.Team,
                            DefinitionIndex = (ushort)item.Index,
                            Paintkit = paintkit.Index,
                        }
                    ]);

                    return ValueTask.CompletedTask;
                };

                skinMenu.AddOption(option);
            }

            main.AddOption(
                new SubmenuMenuOption(EconService.Items[weapon].LocalizedNames["schinese"], skinMenu.Build()));
        }

        return main.Build();
    }

    public IMenuAPI BuildKnifeSkinMenu(IPlayer player)
    {
        var main = Core.MenusAPI.CreateBuilder();
        main.Design.SetMenuTitle("Knife Skins");

        foreach (var (knife, paintkits) in EconService.WeaponToPaintkits)
        {
            var item = EconService.Items[knife];
            if (!Utilities.IsKnifeDefinitionIndex(item.Index))
            {
                continue;
            }

            var skinMenu = Core.MenusAPI.CreateBuilder();
            skinMenu.Design.SetMenuTitle(knife);
            var sorted = paintkits.OrderByDescending(p => p.Rarity.Id).ToList();
            foreach (var paintkit in sorted)
            {
                if (!paintkit.LocalizedNames.ContainsKey("schinese"))
                {
                    throw new Exception($"Paintkit {paintkit} not found in languages schinese");
                    continue;
                }

                var option = new ButtonMenuOption(HtmlGradient.GenerateGradientText(paintkit.LocalizedNames["schinese"],
                    paintkit.Rarity.Color.HexColor));

                option.Click += (_,
                    args) =>
                {
                    Api.SetKnifeSkins([
                        new()
                        {
                            SteamID = args.Player.SteamID,
                            Team = args.Player.Controller.Team,
                            DefinitionIndex = (ushort)item.Index,
                            Paintkit = paintkit.Index,
                        }
                    ]);

                    return ValueTask.CompletedTask;
                };

                skinMenu.AddOption(option);
            }

            main.AddOption(
                new SubmenuMenuOption(EconService.Items[knife].LocalizedNames["schinese"], skinMenu.Build()));
        }

        return main.Build();
    }

    public IMenuAPI BuildGloveSkinMenu(IPlayer player)
    {
        var main = Core.MenusAPI.CreateBuilder();
        main.Design.SetMenuTitle("Glove Skins");

        foreach (var (glove, paintkits) in EconService.WeaponToPaintkits)
        {
            var item = EconService.Items[glove];
            if (!Utilities.IsGloveDefinitionIndex(item.Index))
            {
                continue;
            }

            var skinMenu = Core.MenusAPI.CreateBuilder();
            skinMenu.Design.SetMenuTitle(glove);
            var sorted = paintkits.OrderByDescending(p => p.Rarity.Id).ToList();
            foreach (var paintkit in sorted)
            {
                if (!paintkit.LocalizedNames.ContainsKey("schinese"))
                {
                    throw new Exception($"Paintkit {paintkit} not found in languages schinese");
                    continue;
                }

                var option = new ButtonMenuOption(HtmlGradient.GenerateGradientText(paintkit.LocalizedNames["schinese"],
                    paintkit.Rarity.Color.HexColor));

                option.Click += (_,
                    args) =>
                {
                    Api.SetGloveSkins([
                        new()
                        {
                            SteamID = args.Player.SteamID,
                            Team = args.Player.Controller.Team,
                            DefinitionIndex = (ushort)item.Index,
                            Paintkit = paintkit.Index,
                        }
                    ]);

                    return ValueTask.CompletedTask;
                };

                skinMenu.AddOption(option);
            }

            main.AddOption(
                new SubmenuMenuOption(EconService.Items[glove].LocalizedNames["schinese"], skinMenu.Build()));
        }

        return main.Build();
    }
}