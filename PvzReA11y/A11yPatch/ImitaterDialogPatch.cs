using HarmonyLib;
using Il2CppReloaded.Gameplay;

namespace PvzReA11y.A11yPatch;

/// <summary>
/// ImitaterDialog类的Harmony补丁，用于提供模仿者对话框的无障碍支持
/// </summary>
[HarmonyPatch(typeof(ImitaterDialog))]
public class ImitaterDialogPatch
{
    /// <summary>
    /// Hook ImitaterDialog.ShowToolTip方法，在显示工具提示后记录日志
    /// </summary>
    /// <param name="__instance">ImitaterDialog实例</param>
    [HarmonyPatch(nameof(ImitaterDialog.ShowToolTip))]
    [HarmonyPostfix]
    public static void ShowToolTip_Postfix()
    {
        Core.gLogger.Msg("ImitaterDialog.ShowToolTip()");
    }
}
