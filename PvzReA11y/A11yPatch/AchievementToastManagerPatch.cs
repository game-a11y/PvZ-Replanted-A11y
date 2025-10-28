using HarmonyLib;
using Il2CppReloaded.Data;
using Il2CppSource.UI;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(AchievementToastManager))]
public class AchievementToastManagerPatch
{
    [HarmonyPostfix]
    [HarmonyPatch("ShowToast")]
    public static void ShowToast_Postfix(AchievementType achievementType)
    {
        Core.gLogger.Msg($"AchievementToastManager.ShowToast(AchievementType={achievementType})");
    }
}