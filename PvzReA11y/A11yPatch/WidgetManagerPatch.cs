using HarmonyLib;
using Il2CppReloaded.Gameplay;
using System;

namespace PvzReA11y.A11yPatch;

/// <summary>
/// WidgetManager类的Harmony补丁，用于提供组件管理器的无障碍支持
/// </summary>
[HarmonyPatch(typeof(WidgetManager))]
public class WidgetManagerPatch
{
    // TODO: System.AccessViolationException
    /// <summary>
    /// Hook WidgetManager.BringToFront方法
    /// </summary>
    /// <param name="aDialog">要置前的对话框</param>
    // [HarmonyPatch(nameof(WidgetManager.BringToFront))]
    // [HarmonyPrefix]
    // public static void BringToFront(Dialog aDialog)
    // {
    //     if (aDialog == null) return;
    //     Core.gLogger.Msg($"WidgetManager.BringToFront(Dialog={aDialog})");
    // }

    // TODO: 递归栈溢出
    /// <summary>
    /// Hook WidgetManager.RemoveWidget方法
    /// </summary>
    /// <param name="widget">要移除的组件</param>
    // [HarmonyPatch(nameof(WidgetManager.RemoveWidget))]
    // [HarmonyPostfix]
    // public static void RemoveWidget_Postfix(WidgetContainer widget)
    // {
    //     Core.gLogger.Msg($"WidgetManager.RemoveWidget(WidgetContainer={widget})");
    // }
}