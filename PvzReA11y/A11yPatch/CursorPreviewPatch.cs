using System.Text;
using HarmonyLib;
using Il2CppReloaded.Gameplay;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(CursorPreview))]
public class CursorPreviewPatch
{
    /// <summary>
    /// 游戏内植物选择，光标预览更新时的后置钩子
    /// </summary>
    /// <param name="__instance"></param>
    /// <param name="isNewPreview"></param>
    [HarmonyPostfix]
    [HarmonyPatch("UpdateCursorPreview")]
    public static void UpdateCursorPreview_Postfix(CursorPreview __instance, bool isNewPreview)
    {
        if (__instance == null) return;
        if (!isNewPreview) return;
        
        var sb = new StringBuilder();
        sb.Append("CursorPreview.UpdateCursorPreview(isNewPreview=").Append(isNewPreview).Append(") - ");
        sb.Append("Grid(").Append(__instance.mGridX).Append(",").Append(__instance.mGridY).Append(")");
        sb.Append(", Player").Append(__instance.PlayerIndex);
        sb.Append(", Offset=").Append(__instance.mOffsetY.ToString("F1"));
        sb.Append(", Controllers=").Append(__instance.mColumnControllers?.Count ?? 0);
        
        Core.gLogger.Msg(sb.ToString());
    }
}