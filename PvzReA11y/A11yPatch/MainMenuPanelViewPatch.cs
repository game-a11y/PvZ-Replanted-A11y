using HarmonyLib;
using Il2CppReloaded.UI;
using MelonLoader;

namespace PvzReA11y.A11yPatch
{
    [HarmonyPatch(typeof(MainMenuPanelView))]
    public class MainMenuPanelViewPatch
    {
        [HarmonyPatch(nameof(MainMenuPanelView.Start))]
        [HarmonyPostfix]
        public static void Start_Postfix()
        {
            Core.gLogger.Msg($"MainMenuPanelView.Start()");
        }

        [HarmonyPatch(nameof(MainMenuPanelView.OnEnterLevelSelect))]
        [HarmonyPostfix]
        public static void OnEnterLevelSelect_Postfix()
        {
            Core.gLogger.Msg($"MainMenuPanelView.OnEnterLevelSelect()");
        }

        [HarmonyPatch(nameof(MainMenuPanelView.OnExitLevelSelect))]
        [HarmonyPostfix]
        public static void OnExitLevelSelect_Postfix()
        {
            Core.gLogger.Msg($"MainMenuPanelView.OnExitLevelSelect()");
        }
    }
}