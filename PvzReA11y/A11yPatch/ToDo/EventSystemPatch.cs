using HarmonyLib;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PvzReA11y.A11yPatch.ToDo;

/// <summary>
/// Hook Unity EventSystem 来监控全局焦点变化
/// 用于检测手柄焦点的切换
/// </summary>
[HarmonyPatch(typeof(EventSystem))]
internal class EventSystemPatch
{
    private static GameObject _lastSelectedObject = null;

    // TODO: 某些情况下还是会触发空指针，但不影响游戏继续
    /// <summary>
    /// Hook EventSystem.SetSelectedGameObject 方法
    /// 监控手柄焦点的全局变化
    /// </summary>
    /// <param name="__instance">EventSystem 实例</param>
    /// <param name="selected">被选中的游戏对象</param>
    // [HarmonyPatch("SetSelectedGameObject", typeof(GameObject))]
    // [HarmonyPostfix]
    // static void SetSelectedGameObject_Postfix(GameObject selected)
    // {
    //     // 避免重复触发
    //     if (_lastSelectedObject == selected) return;

    //     string lastObjectName = _lastSelectedObject?.name ?? "None";
    //     string curObjectName = selected?.name ?? "None";

    //     Core.gLogger.Msg($"EventSystem.SetSelectedGameObject: {lastObjectName} -> {curObjectName}");
    //     _lastSelectedObject = selected;
    // }

    // TODO: 对局中会报空指针错误
    /// <summary>
    /// Hook EventSystem.Update 方法来持续监控当前选中对象
    /// 用于检测通过其他方式改变的焦点
    /// </summary>
    // [HarmonyPatch("Update")]
    // [HarmonyPostfix]
    // static void Update_Postfix(EventSystem __instance)
    // {
    //     if (__instance == null) return;
    
    //     // 检查当前选中对象是否发生变化
    //     var currentSelected = __instance.currentSelectedGameObject;
    //     if (_lastSelectedObject != currentSelected)
    //     {
    //         string lastObjectName = _lastSelectedObject?.name ?? "None";
    //         string curObjectName = currentSelected?.name ?? "None";

    //         Core.gLogger.Msg($"EventSystem.Update: {lastObjectName} -> {curObjectName}");
    //         _lastSelectedObject = currentSelected;
    //     }

    // }
}
