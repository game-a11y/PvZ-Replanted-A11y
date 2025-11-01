using HarmonyLib;
using Il2CppSource.Controllers;
using UnityEngine;
using System.Text;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(GamepadCursorController))]
public class GamepadCursorControllerPatch
{
    // 手柄摇杆
    //[HarmonyPostfix]
    //[HarmonyPatch("_moveIndicator", typeof(Vector2))]
    //public static void MoveIndicator_Postfix(GamepadCursorController __instance, Vector2 delta)
    //{
    //    if (__instance == null) return;

    //    var log = new StringBuilder();
    //    log.Append($"GamepadCursorController._moveIndicator(): Instance: {__instance.GetHashCode():X8}");
    //    log.Append($", Delta: ({delta.x:F2}, {delta.y:F2})");
    //    log.Append($", PlayerIndex: {__instance.PlayerIndex}");

    //    Core.gLogger.Msg(log.ToString());
    //}

    //[HarmonyPostfix]
    //[HarmonyPatch("_updateMousePosition", new System.Type[] { })]
    //public static void UpdateMousePosition_Postfix(GamepadCursorController __instance)
    //{
    //    if (__instance == null) return;

    //    var log = new StringBuilder();
    //    log.Append($"GamepadCursorController._updateMousePosition(): Instance: {__instance.GetHashCode():X8}");
    //    log.Append($", PlayerIndex: {__instance.PlayerIndex}");

    //    Core.gLogger.Msg(log.ToString());
    //}

    //[HarmonyPostfix]
    //[HarmonyPatch("_updateMousePosition", typeof(Vector3))]
    //public static void UpdateMousePositionWithWorldPos_Postfix(GamepadCursorController __instance, Vector3 worldPos)
    //{
    //    if (__instance == null) return;

    //    var log = new StringBuilder();
    //    log.Append($"GamepadCursorController._updateMousePosition(Vector3): Instance: {__instance.GetHashCode():X8}");
    //    log.Append($", WorldPos: ({worldPos.x:F2}, {worldPos.y:F2}, {worldPos.z:F2})");
    //    log.Append($", PlayerIndex: {__instance.PlayerIndex}");

    //    Core.gLogger.Msg(log.ToString());
    //}

    [HarmonyPostfix]
    [HarmonyPatch("ResetPosition")]
    public static void ResetPosition_Postfix(GamepadCursorController __instance)
    {
        if (__instance == null) return;

        var log = new StringBuilder();
        log.Append($"GamepadCursorController.ResetPosition(): Instance: {__instance.GetHashCode():X8}");
        log.Append($", PlayerIndex: {__instance.PlayerIndex}");

        Core.gLogger.Msg(log.ToString());
    }

    [HarmonyPostfix]
    [HarmonyPatch("SetVisualsActive", typeof(bool))]
    public static void SetVisualsActive_Postfix(GamepadCursorController __instance, bool active)
    {
        if (__instance == null) return;

        var log = new StringBuilder();
        log.Append($"GamepadCursorController.SetVisualsActive(): Instance: {__instance.GetHashCode():X8}");
        log.Append($", Active: {active}");
        log.Append($", PlayerIndex: {__instance.PlayerIndex}");

        Core.gLogger.Msg(log.ToString());
    }
}