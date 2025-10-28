using HarmonyLib;
using Il2CppSource.Controllers;
using PvzReA11y.A11y;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(MessageWidgetController))]
public class MessageWidgetControllerPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(MessageWidgetController.Show))]
    public static void Show_Postfix()
    {
        Core.gLogger.Msg($"MessageWidgetController.Show called");
    }
}