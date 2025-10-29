using HarmonyLib;
using Il2CppSource.UI;
using MelonLoader;
using PvzReA11y.A11y;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(ConfirmPanelView))]
public class ConfirmPanelViewPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(ConfirmPanelView.OnShow))]
    public static void OnShow_Postfix()
    {
        Core.gLogger.Msg($"ConfirmPanelView.OnShow()");
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(ConfirmPanelView.OnEnable))]
    public static void OnEnable()
    {
        Core.gLogger.Msg($"ConfirmPanelView.OnEnable()");
    }
}