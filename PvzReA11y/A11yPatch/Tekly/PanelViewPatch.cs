using System.Text;
using HarmonyLib;
using Il2CppTekly.PanelViews;

namespace PvzReA11y.A11yPatch.Tekly;

[HarmonyPatch(typeof(PanelView))]
internal class PanelViewPatch
{
    [HarmonyPatch(nameof(PanelView.Show), new Type[] { typeof(string), typeof(PanelData) })]
    [HarmonyPostfix]
    public static void Show_Postfix(PanelView __instance, string context, PanelData data)
    {
        if (__instance == null) return;

        StringBuilder sb = new StringBuilder();
        sb.Append($"PanelView.Show(context='{context}', data={data})");
        sb.Append($": Id={__instance.Id}");

        string a11yText = $"{__instance.Id}";
        string a11yCtx = sb.ToString();

        // 补充弹窗内容
        A11y.SR.SpeakQueue(a11yText, a11yCtx);
    }
}
