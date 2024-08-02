using HarmonyLib;
using Il2CppSLZ.Marrow;
using Il2CppSLZ.Marrow.Data;
// ReSharper disable InconsistentNaming

namespace WeatherElectric.InfiniteAmmo.Patching;

[HarmonyPatch(typeof(AmmoInventory))]
public static class AmmoInventoryPatch
{
    [HarmonyPatch(nameof(AmmoInventory.Awake))]
    [HarmonyPostfix]
    public static void Awake(AmmoInventory __instance)
    {
        if (!Preferences.Enabled.Value) return;
        
        __instance.AddCartridge(__instance.lightAmmoGroup, 1000);
        __instance.AddCartridge(__instance.mediumAmmoGroup, 1000);
        __instance.AddCartridge(__instance.heavyAmmoGroup, 1000);
    }

    [HarmonyPatch(nameof(AmmoInventory.RemoveCartridge))]
    [HarmonyPostfix]
    public static void RemoveCartridge(AmmoInventory __instance, CartridgeData cartridge, int count)
    {
        if (!Preferences.Enabled.Value) return;

        var group = __instance.GetGroupByCartridge(cartridge);
        if (group == null) return;

        switch (group)
        {
            case "light":
                __instance.AddCartridge(__instance.lightAmmoGroup, count);
                break;
            case "medium":
                __instance.AddCartridge(__instance.mediumAmmoGroup, count);
                break;
            case "heavy":
                __instance.AddCartridge(__instance.heavyAmmoGroup, count);
                break;
        }
    }
}