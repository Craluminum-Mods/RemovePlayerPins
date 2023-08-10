using System.Collections.Generic;
using System.Linq;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Vintagestory.GameContent;

[assembly: ModInfo(name: "Remove Player Pins", modID: "removeplayerpins")]

namespace RemovePlayerPins;

public class Core : ModSystem
{
    public override void Start(ICoreAPI api)
    {
        base.Start(api);
        api.World.Logger.Event("started '{0}' mod", Mod.Info.Name);
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
        base.StartServerSide(api);

        api.Event.SaveGameLoaded += () => RemovePlayerMapLayers(api);
    }

    public override void StartClientSide(ICoreClientAPI api)
    {
        base.StartClientSide(api);

        api.Event.LevelFinalize += () => RemovePlayerMapLayers(api);
    }

    public static void RemovePlayerMapLayers<T>(T api) where T : ICoreAPI
    {
        List<MapLayer> mapLayers = api.ModLoader.GetModSystem<WorldMapManager>().MapLayers;

        foreach (MapLayer val in new List<MapLayer>(mapLayers.OfType<PlayerMapLayer>()))
        {
            mapLayers.Remove(val);
        }
    }
}