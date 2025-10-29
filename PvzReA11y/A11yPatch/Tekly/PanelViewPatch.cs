using HarmonyLib;
using Il2CppTekly.PanelViews;

namespace PvzReA11y.A11yPatch.Tekly;

[HarmonyPatch(typeof(PanelView))]
internal class PanelViewPatch
{
    [HarmonyPatch(nameof(PanelView.Show))]
    [HarmonyPostfix]
    public static void Show_Postfix(string context, PanelData data = null)
    {
        Core.gLogger.Msg($"PanelView.Show(context='{context}', data={data})");
    }
}
