using HarmonyLib;
using Il2CppReloaded.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PvzReA11y.A11yPatch;

/// <summary>
/// ListNavigationContainer 的 Harmony 补丁
/// 用于钩取列表导航容器的目标选择方法，提供无障碍支持
/// </summary>
[HarmonyPatch(typeof(ListNavigationContainer))]
public class ListNavigationContainerPatch
{

    /// <summary>
    /// 钩取 GetTargetSelectable() 方法的后置处理
    /// 提供默认目标选择的语音反馈
    /// </summary>
    [HarmonyPatch("GetTargetSelectable", new System.Type[0])]
    [HarmonyPostfix]
    public static void GetTargetSelectable_NoParams_Postfix(ListNavigationContainer __instance, Selectable __result)
    {
        if (__instance == null) return;

        string containerName = __instance.gameObject?.name ?? "Unknown";
        string targetName = __result?.gameObject?.name ?? "None";
        string targetType = __result?.GetType().Name ?? "None";

        Core.gLogger.Msg($"ListNavigationContainer.GetTargetSelectable(): {containerName}");
        Core.gLogger.Msg($"  - Target: {targetName} ({targetType})");
        if (__result != null)
        {
            Core.gLogger.Msg($"  - Target Interactable: {__result.interactable}");
            Core.gLogger.Msg($"  - Target IsActive: {__result.IsActive()}");
        }
    }

    /// <summary>
    /// 钩取 GetTargetSelectable(MoveDirection dir, int playerIndex) 方法的后置处理
    /// 提供导航结果的语音反馈
    /// </summary>
    [HarmonyPatch("GetTargetSelectable", new[] { typeof(MoveDirection), typeof(int) })]
    [HarmonyPostfix]
    public static void GetTargetSelectable_Postfix(ListNavigationContainer __instance, MoveDirection dir, int playerIndex, Selectable __result)
    {
        if (__instance == null) return;

        string containerName = __instance.gameObject?.name ?? "Unknown";
        string targetName = __result?.gameObject?.name ?? "None";
        string targetType = __result?.GetType().Name ?? "None";
        string direction = dir.ToString();

        Core.gLogger.Msg($"ListNavigationContainer.GetTargetSelectable({direction}, P{playerIndex}): {containerName}");
        Core.gLogger.Msg($"  - Target: {targetName} ({targetType})");

        if (__result != null)
        {
            Core.gLogger.Msg($"  - Target Interactable: {__result.interactable}");
            Core.gLogger.Msg($"  - Target IsActive: {__result.IsActive()}");
        }
    }

}