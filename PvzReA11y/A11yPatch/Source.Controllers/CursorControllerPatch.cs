using HarmonyLib;
using Il2CppReloaded.Gameplay;
using Il2CppSource.Controllers;
using PvzReA11y;
using System.Text;

namespace PvzReA11y.A11yPatch.Source.Controllers;

[HarmonyPatch(typeof(CursorController))]
public class CursorControllerPatch
{
    // TODO: hook 失败
    // [HarmonyPostfix]
    // [HarmonyPatch("Reset")]
    // public static void Reset_Postfix(CursorController __instance)
    // {
    //     if (__instance == null) return;

    //     var log = new StringBuilder();
    //     log.Append($"CursorController.Reset(): Instance: {__instance.GetHashCode():X8}");
    //     log.Append($", CursorType: {__instance.m_cursorType}");
    //     log.Append($", VariantType: {__instance.m_variantCursorType}");
    //     log.Append($", RenderCount: {__instance.m_cursorRenders?.Count ?? 0}");

    //     Core.gLogger.Msg(log.ToString());
    // }

    [HarmonyPostfix]
    [HarmonyPatch("SetCursorType", typeof(CursorType))]
    public static void SetCursorType_Postfix(CursorController __instance, CursorType cursorType)
    {
        if (__instance == null) return;

        var log = new StringBuilder();
        log.Append($"CursorController.SetCursorType(): Instance: {__instance.GetHashCode():X8}");
        log.Append($", NewType: {cursorType}");
        log.Append($", CurrentType: {__instance.m_cursorType}");
        log.Append($", VariantType: {__instance.m_variantCursorType}");

        Core.gLogger.Msg(log.ToString());
    }

    [HarmonyPostfix]
    [HarmonyPatch("SetCursorOffset", typeof(float), typeof(float))]
    public static void SetCursorOffset_Postfix(CursorController __instance, float offsetX, float offsetY)
    {
        if (__instance == null) return;

        var log = new StringBuilder();
        log.Append($"CursorController.SetCursorOffset(): Instance: {__instance.GetHashCode():X8}");
        log.Append($", Offset: ({offsetX:F1}, {offsetY:F1})");
        log.Append($", CursorType: {__instance.m_cursorType}");

        Core.gLogger.Msg(log.ToString());
    }
}