using System.Text;
using HarmonyLib;
using Il2CppReloaded.Gameplay;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(GamepadCursor))]
public class GamepadCursorPatch
{
    //[HarmonyPostfix]
    //[HarmonyPatch(nameof(GamepadCursor.UpdatePosition))]
    //public static void UpdatePosition_Postfix(GamepadCursor __instance, float x, float y)
    //{
    //    if (__instance == null) return;

    //    var sb = new StringBuilder();
    //    sb.Append($"GamepadCursor.UpdatePosition() ");
    //    sb.Append($"Player: {__instance.PlayerIndex}, ");
    //    sb.Append($"Position: ({x:F2}, {y:F2}), ");
    //    sb.Append($"GridPos: ({__instance.m_gridX}, {__instance.m_gridY}), ");
    //    sb.Append($"Enabled: {__instance.Enabled}");

    //    Core.gLogger.Msg(sb.ToString());
    //}

    [HarmonyPostfix]
    [HarmonyPatch(nameof(GamepadCursor.UpdateGridPositionFromDelta))]
    public static void UpdateGridPositionFromDelta_Postfix(GamepadCursor __instance, int xDelta, int yDelta)
    {
        if (__instance == null) return;

        var sb = new StringBuilder();
        sb.Append($"GamepadCursor.UpdateGridPositionFromDelta() ");
        sb.Append($"Player: {__instance.PlayerIndex}, ");
        sb.Append($"Delta: ({xDelta}, {yDelta}), ");
        sb.Append($"NewGridPos: ({__instance.m_gridX}, {__instance.m_gridY}), ");
        sb.Append($"Enabled: {__instance.Enabled}");

        // TODO: 获取当前选中的网格状态
        string a11yText = $"行 {__instance.m_gridY + 1}, 列 {__instance.m_gridX + 1}";
        string a11yCtx = sb.ToString();

        A11y.SR.Speak(a11yText, a11yCtx);
    }

    // [HarmonyPostfix]
    // [HarmonyPatch(nameof(GamepadCursor._updateGridPosition))]
    // public static void _updateGridPosition_Postfix(GamepadCursor __instance, int gridX, int gridY)
    // {
    //     if (__instance == null) return;

    //     var sb = new StringBuilder();
    //     sb.Append($"GamepadCursor._updateGridPosition() ");
    //     sb.Append($"Player: {__instance.PlayerIndex}, ");
    //     sb.Append($"TargetGrid: ({gridX}, {gridY}), ");
    //     // 已经更新了
    //     // sb.Append($"CurrentGrid: ({__instance.m_gridX}, {__instance.m_gridY}), ");
    //     sb.Append($"Enabled: {__instance.Enabled}");

    //     Core.gLogger.Msg(sb.ToString());
    // }
}