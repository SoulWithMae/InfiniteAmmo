using HarmonyLib;
using Il2CppSLZ.Bonelab;

namespace WeatherElectric.InfiniteAmmo.Patching;

[HarmonyPatch(typeof(AmmoInventory))]
public static class AmmoInventoryPatch
{
    [HarmonyPatch(nameof(AmmoInventory.Awake))]
    [HarmonyPostfix]
    public static void Awake(AmmoInventory __instance)
    {
        if (!Preferences.Enabled.Value) return;
        ModConsole.Msg("Adding shit ton of ammo", 1);
        __instance.AddCartridge(__instance.lightAmmoGroup, 500000);
        __instance.AddCartridge(__instance.mediumAmmoGroup, 500000);
        __instance.AddCartridge(__instance.heavyAmmoGroup, 500000);
    }

    [HarmonyPatch(nameof(AmmoInventory.RemoveCartridge))]
    [HarmonyPostfix]
    public static void RemoveCartridge(AmmoInventory __instance)
    {
        if (!Preferences.Enabled.Value) return;
#if DEBUG
        ModConsole.Msg("Adding a shit ton of ammo", 1);
#endif
        __instance.AddCartridge(__instance.lightAmmoGroup, 500000);
        __instance.AddCartridge(__instance.mediumAmmoGroup, 500000);
        __instance.AddCartridge(__instance.heavyAmmoGroup, 500000);
    }
}