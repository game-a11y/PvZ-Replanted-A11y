using HarmonyLib;
using Il2CppReloaded.Gameplay;
using MelonLoader;
using System;

namespace PvzReA11y.A11yPatch;

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

    //     MelonLogger.Msg($"GameButton.SetLabel(Label={theLabel})");
    // }

}