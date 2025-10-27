using HarmonyLib;
using Il2CppReloaded.Gameplay;
using System.Text;

namespace PvzReA11y.A11yPatch;

/// <summary>
/// Dialog类的Harmony补丁，用于提供对话框的无障碍支持
/// </summary>
[HarmonyPatch(typeof(Dialog))]
public class DialogPatch
{
    /// <summary>
    /// Dialog.Show 方法，在对话框显示后提供额外信息 (弹窗提示框)
    /// </summary>
    /// <param name="__instance">Dialog实例</param>
    /// <param name="isZenGarden">是否为禅境花园模式</param>
    [HarmonyPatch(nameof(Dialog.Show))]
    [HarmonyPostfix]
    public static void Show_Postfix(Dialog __instance, bool isZenGarden = false)
    {
        if (__instance == null) return;

        Core.gLogger.Msg($"Dialog.Show(isZenGarden={isZenGarden})");
        StringBuilder sb = new StringBuilder();
        sb.Append($" Type: {__instance.mDialogType},");
        sb.Append($" Header: {__instance.mDialogHeader},");
        sb.Append($" Lines:  {__instance.mDialogLines},");
        sb.Append($" Footer: '{__instance.mDialogFooter}',");
        Core.gLogger.Msg(sb.ToString());
    }
}