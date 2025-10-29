using HarmonyLib;
using Il2CppReloaded;

namespace PvzReA11y.A11yPatch;

/// <summary>
/// Bootstrap 类的 Harmony 补丁
/// 为游戏启动和初始化相关的函数添加后置挂钩
/// </summary>
[HarmonyPatch(typeof(Bootstrap))]
public class BootstrapPatch
{
    /// <summary>
    /// Hook Bootstrap.Awake 方法，在Bootstrap组件唤醒后记录日志
    /// </summary>
    [HarmonyPatch("Awake")]
    [HarmonyPostfix]
    public static void Awake_Postfix()
    {
        Core.gLogger.Msg("Bootstrap.Awake()");
    }

    /// <summary>
    /// Hook Bootstrap.OnEnable 方法，在Bootstrap组件启用后记录日志
    /// </summary>
    [HarmonyPatch("OnEnable")]
    [HarmonyPostfix]
    public static void OnEnable_Postfix()
    {
        Core.gLogger.Msg("Bootstrap.OnEnable()");
    }

    /// <summary>
    /// Hook Bootstrap.OnPlayGameClicked 方法，在点击开始游戏按钮后记录日志
    /// </summary>
    [HarmonyPatch(nameof(Bootstrap.OnPlayGameClicked))]
    [HarmonyPostfix]
    public static void OnPlayGameClicked_Postfix()
    {
        Core.gLogger.Msg("Bootstrap.OnPlayGameClicked()");
    }
}