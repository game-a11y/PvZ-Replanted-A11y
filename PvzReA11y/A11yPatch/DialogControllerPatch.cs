using HarmonyLib;
using Il2CppSource.Controllers;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(DialogController))]
public class DialogControllerPatch
{
    // 后置挂钩：在 DialogController.Show 执行完成后触发
    [HarmonyPostfix]
    [HarmonyPatch(nameof(DialogController.Show))]
    public static void Show_Postfix(DialogController __instance)
    {
        if (__instance == null) return;

        Core.gLogger.Msg("DialogController.Show()");
    }
}