using HarmonyLib;
using Il2CppReloaded.Services;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(SettingsService))]
public static class SettingsServicePatch
{

    [HarmonyPostfix]
    [HarmonyPatch("SetGamepadCursorSpeed", new System.Type[] { typeof(int), typeof(float) })]
    public static void GetGamepadCursorSpeed_Postfix(SettingsService __instance, int index, float value)
    {
        Core.gLogger.Msg($"SettingsService.SetGamepadCursorSpeed(index={index}, value={value})");
    }

    [HarmonyPostfix]
    [HarmonyPatch("SetQualityForResolution", new System.Type[] { typeof(int) })]
    public static void SetQualityForResolution_Postfix(SettingsService __instance, int screenHeight)
    {
        Core.gLogger.Msg($"SettingsService.SetQualityForResolution(screenHeight={screenHeight})");
    }
}