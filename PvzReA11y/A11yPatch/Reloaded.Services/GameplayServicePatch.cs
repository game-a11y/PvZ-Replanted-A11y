using HarmonyLib;
using Il2CppReloaded.Services;
using Il2CppReloaded.Data;

namespace PvzReA11y.A11yPatch.Reloaded.Services;

[HarmonyPatch(typeof(GameplayService))]
public static class GameplayServicePatch
{
    // SetCurrentLevelData 后置挂钩
    [HarmonyPatch(nameof(GameplayService.SetCurrentLevelData))]
    [HarmonyPostfix]
    public static void SetCurrentLevelData_Postfix(GameplayService __instance, LevelEntryData levelData)
    {
        Core.gLogger.Msg($"GameplayService.SetCurrentLevelData(levelData.name={levelData?.m_levelName})");
    }

    // ClearCurrentLevelData 后置挂钩
    [HarmonyPatch(nameof(GameplayService.ClearCurrentLevelData))]
    [HarmonyPostfix]
    public static void ClearCurrentLevelData_Postfix()
    {
        Core.gLogger.Msg("GameplayService.ClearCurrentLevelData()");
    }
}