using HarmonyLib;
using Il2CppSource.Controllers;
using PvzReA11y.A11y;
using System.Text;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(MessageWidgetController))]
public class MessageWidgetControllerPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(MessageWidgetController.Show))]
    public static void Show_Postfix(MessageWidgetController __instance)
    {
        if (__instance == null) return;

        StringBuilder sb = new StringBuilder();
        sb.Append($"MessageWidgetController.Show()");
        sb.Append($": .k_panelId={MessageWidgetController.k_panelId}");
        sb.Append($", .s_labelKey={MessageWidgetController.s_labelKey}");
        sb.Append($", .m_isShowing={__instance.m_isShowing}");
        sb.Append($", .m_prevString={__instance.m_prevString}");

        Core.gLogger.Msg(sb.ToString());
    }
}