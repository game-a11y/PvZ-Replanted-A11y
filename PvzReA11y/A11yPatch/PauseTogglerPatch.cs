using HarmonyLib;
using Il2Cpp;

namespace PvzReA11y.A11yPatch;

/// <summary>
/// PauseToggler类的Harmony补丁，用于提供暂停切换的无障碍支持
/// </summary>
[HarmonyPatch(typeof(PauseToggler))]
public class PauseTogglerPatch
{
    /// <summary>
    /// Hook PauseToggler.TogglePause方法
    /// </summary>
    /// <param name="__instance">PauseToggler实例</param>
    [HarmonyPatch(nameof(PauseToggler.TogglePause))]
    [HarmonyPostfix]
    public static void TogglePause_Postfix(PauseToggler __instance)
    {
        if (__instance == null) return;

        bool isPaused = __instance.m_isPaused;
        Core.gLogger.Msg($"PauseToggler.TogglePause(isPaused={isPaused})");
    }
}