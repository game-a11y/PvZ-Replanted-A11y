using HarmonyLib;
using Il2CppReloaded.UI;
using MelonLoader;
using PvzReA11y.A11y;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(GameplayOptionsMenu))]
public class GameplayOptionsMenuPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(GameplayOptionsMenu.OnEnable))]
    public static void OnEnable_Postfix()
    {
        Core.gLogger.Msg($"GameplayOptionsMenu.OnEnable called");
    }
}