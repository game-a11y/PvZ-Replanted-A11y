using HarmonyLib;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Il2CppReloaded.Input;

namespace PvzReA11y.A11yPatch;

/// <summary>
/// Hook GridNavigationContainer 类的关键方法
/// 用于监控网格导航容器的初始化和目标选择
/// </summary>
[HarmonyPatch(typeof(GridNavigationContainer))]
internal class GridNavigationContainerPatch
{
    /// <summary>
    /// Hook GridNavigationContainer.GetTargetSelectable() 方法（无参数版本）
    /// 在获取目标可选择对象时触发
    /// </summary>
    [HarmonyPatch("GetTargetSelectable", new System.Type[] { })]
    [HarmonyPostfix]
    static void GetTargetSelectable_Postfix(GridNavigationContainer __instance, Selectable __result)
    {
        if (__instance == null) return;

        string containerName = __instance.gameObject?.name ?? "Unknown";
        string targetName = __result?.gameObject?.name ?? "None";
        string targetType = __result?.GetType().Name ?? "None";

        Core.gLogger.Msg($"GridNavigationContainer.GetTargetSelectable(): {containerName}");
        Core.gLogger.Msg($"  - Target: {targetName} ({targetType})");
        if (__result != null)
        {
            Core.gLogger.Msg($"  - Target Interactable: {__result.interactable}");
            Core.gLogger.Msg($"  - Target IsActive: {__result.IsActive()}");
        }
    }

    /// <summary>
    /// Hook GridNavigationContainer.GetTargetSelectable(MoveDirection, int) 方法（带参数版本）
    /// 在根据移动方向获取目标可选择对象时触发
    /// </summary>
    [HarmonyPatch("GetTargetSelectable", new System.Type[] { typeof(MoveDirection), typeof(int) })]
    [HarmonyPostfix]
    static void GetTargetSelectable_WithDirection_Postfix(
        GridNavigationContainer __instance, 
        MoveDirection dir, 
        int playerIndex, 
        Selectable __result)
    {
        if (__instance == null) return;

        string containerName = __instance.gameObject?.name ?? "Unknown";
        string targetName = __result?.gameObject?.name ?? "None";
        string targetType = __result?.GetType().Name ?? "None";
        string direction = dir.ToString();

        Core.gLogger.Msg($"GridNavigationContainer.GetTargetSelectable({direction}, P{playerIndex}): {containerName}");
        Core.gLogger.Msg($"  - Target: {targetName} ({targetType})");
        
        if (__result != null)
        {
            Core.gLogger.Msg($"  - Target Interactable: {__result.interactable}");
            Core.gLogger.Msg($"  - Target IsActive: {__result.IsActive()}");
        }
    }
}