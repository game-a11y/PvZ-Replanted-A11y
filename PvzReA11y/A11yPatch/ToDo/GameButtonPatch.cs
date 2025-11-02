using HarmonyLib;
using Il2CppReloaded.Gameplay;
using MelonLoader;
using System;

namespace PvzReA11y.A11yPatch.ToDo;

[HarmonyPatch(typeof(GameButton))]
public class GameButtonPatch
{
    // TODO: 虚函数 hook 报错
    /// <summary>
    /// Hook GameButton.SetLabel 方法的前置处理
    /// </summary>
    /// <param name="__instance">GameButton 实例</param>
    /// <param name="theLabel">按钮标签文本</param>
    // [HarmonyPatch("SetLabel", new Type[] { typeof(string) })]
    // [HarmonyPrefix]
    // public static void SetLabel_Prefix(GameButton __instance, string theLabel)
    // {
    //     if (__instance == null) return;
    //     if (theLabel == null) return;

    //     Core.gLogger.Msg($"GameButton.SetLabel(Label={theLabel})");
    // }

    /// <summary>
    /// Hook GameButton.IsMouseOver 方法的后置处理
    /// </summary>
    /// <param name="__instance">GameButton 实例</param>
    /// <param name="__result">IsMouseOver 方法的返回值</param>
    //[HarmonyPatch("IsMouseOver")]
    //[HarmonyPostfix]
    //public static void IsMouseOver_Postfix(GameButton __instance)
    //{
    //    if (__instance == null) return;
    //    if (__instance.Disabled || __instance.BtnNoDraw) return;
    //    if (!__instance.IsOver) return;

    //    // 未悬停时不输出
    //    // if (!__result) return;

    //    // TODO: 仅在首次悬停时输出
    //    Core.gLogger.Msg($"GameButton#{__instance?.GetHashCode()}.IsMouseOver()");
    //}

}