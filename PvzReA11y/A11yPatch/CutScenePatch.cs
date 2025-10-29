using HarmonyLib;
using Il2CppReloaded.Gameplay;

namespace PvzReA11y.A11yPatch;

/// <summary>
/// CutScene 类的 Harmony 补丁
/// 为过场动画和关卡介绍相关的函数添加后置挂钩
/// </summary>
[HarmonyPatch(typeof(CutScene))]
public class CutScenePatch
{
    /// <summary>
    /// Hook CutScene.StartLevelIntro 方法，在关卡介绍开始后记录日志
    /// </summary>
    [HarmonyPatch(nameof(CutScene.StartLevelIntro))]
    [HarmonyPostfix]
    public static void StartLevelIntro_Postfix()
    {
        Core.gLogger.Msg("CutScene.StartLevelIntro()");
    }

    /// <summary>
    /// Hook CutScene.StartSeedChooser 方法，在种子选择器启动后记录日志
    /// </summary>
    [HarmonyPatch(nameof(CutScene.StartSeedChooser))]
    [HarmonyPostfix]
    public static void StartSeedChooser_Postfix()
    {
        Core.gLogger.Msg("CutScene.StartSeedChooser()");
    }

    /// <summary>
    /// Hook CutScene.AdvanceCrazyDaveDialog 方法，在疯狂戴夫对话推进后记录日志
    /// </summary>
    [HarmonyPatch(nameof(CutScene.AdvanceCrazyDaveDialog))]
    [HarmonyPostfix]
    public static void AdvanceCrazyDaveDialog_Postfix(bool theJustSkipping)
    {
        Core.gLogger.Msg($"CutScene.AdvanceCrazyDaveDialog(JustSkipping={theJustSkipping})");
    }

    /// <summary>
    /// Hook CutScene.ShowShovel 方法，在显示铲子后记录日志
    /// </summary>
    [HarmonyPatch(nameof(CutScene.ShowShovel))]
    [HarmonyPostfix]
    public static void ShowShovel_Postfix()
    {
        Core.gLogger.Msg("CutScene.ShowShovel()");
    }
}