using HarmonyLib;
using Il2CppReloaded.Gameplay;
using PvzReA11y.A11y;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(MessageWidget))]
public class MessageWidgetPatch
{
    // TODO: 注意去重
    [HarmonyPostfix]
    [HarmonyPatch(nameof(MessageWidget.SetLabel))]
    public static void SetLabel_Postfix(string theNewLabel, MessageStyle theMessageStyle)
    {
        //Core.gLogger.Msg($"MessageWidget.SetLabel(NewLabel='{theNewLabel}', style={theMessageStyle})");
    }
}