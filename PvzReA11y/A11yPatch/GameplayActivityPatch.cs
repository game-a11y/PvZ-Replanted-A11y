using HarmonyLib;
using Il2CppReloaded.TreeStateActivities;
using Il2CppReloaded.Gameplay;

namespace PvzReA11y.A11yPatch;

/// <summary>
/// GameplayActivity 类的 Harmony 补丁
/// 为游戏流程相关的函数添加后置挂钩
/// </summary>
public class GameplayActivityPatch
{
    /// <summary>
    /// Hook GameplayActivity.NewGame 方法，在新游戏开始后记录日志
    /// </summary>
    [HarmonyPatch(typeof(GameplayActivity), nameof(GameplayActivity.NewGame))]
    [HarmonyPostfix]
    public static void NewGame_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.NewGame() - 新游戏已开始");
    }

    /// <summary>
    /// Hook GameplayActivity.StartPlaying 方法，在开始游戏后记录日志
    /// </summary>
    [HarmonyPatch(typeof(GameplayActivity), nameof(GameplayActivity.StartPlaying))]
    [HarmonyPostfix]
    public static void StartPlaying_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.StartPlaying() - 游戏已开始播放");
    }

    /// <summary>
    /// Hook GameplayActivity.EndLevel 方法，在关卡结束后记录日志
    /// </summary>
    [HarmonyPatch(typeof(GameplayActivity), nameof(GameplayActivity.EndLevel))]
    [HarmonyPostfix]
    public static void EndLevel_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.EndLevel() - 关卡已结束");
    }

    /// <summary>
    /// Hook GameplayActivity.ShowSeedChooserScreen 方法，在显示种子选择界面后记录日志
    /// </summary>
    [HarmonyPatch(typeof(GameplayActivity), nameof(GameplayActivity.ShowSeedChooserScreen))]
    [HarmonyPostfix]
    public static void ShowSeedChooserScreen_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.ShowSeedChooserScreen() - 种子选择界面已显示");
    }

    /// <summary>
    /// Hook GameplayActivity.DoDialog 方法，在创建对话框后记录日志
    /// </summary>
    [HarmonyPatch(typeof(GameplayActivity), nameof(GameplayActivity.DoDialog))]
    [HarmonyPostfix]
    public static void DoDialog_Postfix(Dialogs theDialogId, bool isModal, string theDialogHeader, string theDialogLines, string theDialogFooter, Dialog.ButtonType theButtonMode, Dialog __result)
    {
        Core.gLogger.Msg($"GameplayActivity.DoDialog() - 对话框已创建: DialogId={theDialogId}, Modal={isModal}, Header='{theDialogHeader}', Lines='{theDialogLines}', Footer='{theDialogFooter}', ButtonType={theButtonMode}");
    }

    /// <summary>
    /// Hook GameplayActivity.SaveGame 方法，在保存游戏后记录日志
    /// </summary>
    [HarmonyPatch(typeof(GameplayActivity), nameof(GameplayActivity.SaveGame))]
    [HarmonyPostfix]
    public static void SaveGame_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.SaveGame() - 游戏已保存");
    }

    /// <summary>
    /// Hook GameplayActivity.ShowGameSelector 方法，在显示游戏选择器后记录日志
    /// </summary>
    [HarmonyPatch(typeof(GameplayActivity), nameof(GameplayActivity.ShowGameSelector))]
    [HarmonyPostfix]
    public static void ShowGameSelector_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.ShowGameSelector() - 游戏选择器已显示");
    }

    /// <summary>
    /// Hook GameplayActivity.ShowStoreScreen 方法，在显示商店界面后记录日志
    /// </summary>
    [HarmonyPatch(typeof(GameplayActivity), nameof(GameplayActivity.ShowStoreScreen))]
    [HarmonyPostfix]
    public static void ShowStoreScreen_Postfix(StoreScreen __result)
    {
        Core.gLogger.Msg("GameplayActivity.ShowStoreScreen() - 商店界面已显示");
    }

    /// <summary>
    /// Hook GameplayActivity.ShowAwardScreen 方法，在显示奖励界面后记录日志
    /// </summary>
    [HarmonyPatch(typeof(GameplayActivity), nameof(GameplayActivity.ShowAwardScreen))]
    [HarmonyPostfix]
    public static void ShowAwardScreen_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.ShowAwardScreen() - 奖励界面已显示");
    }

    /// <summary>
    /// Hook GameplayActivity.ShowZombiesWonScreen 方法，在显示僵尸胜利界面后记录日志
    /// </summary>
    [HarmonyPatch(typeof(GameplayActivity), nameof(GameplayActivity.ShowZombiesWonScreen))]
    [HarmonyPostfix]
    public static void ShowZombiesWonScreen_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.ShowZombiesWonScreen() - 僵尸胜利界面已显示");
    }

    /// <summary>
    /// Hook GameplayActivity.ShowReadySetPlantScreen 方法，在显示准备种植界面后记录日志
    /// </summary>
    [HarmonyPatch(typeof(GameplayActivity), nameof(GameplayActivity.ShowReadySetPlantScreen))]
    [HarmonyPostfix]
    public static void ShowReadySetPlantScreen_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.ShowReadySetPlantScreen() - 准备种植界面已显示");
    }

    /// <summary>
    /// Hook GameplayActivity.ShowChallengeScreen 方法，在显示挑战界面后记录日志
    /// </summary>
    [HarmonyPatch(typeof(GameplayActivity), nameof(GameplayActivity.ShowChallengeScreen))]
    [HarmonyPostfix]
    public static void ShowChallengeScreen_Postfix(ChallengePage thePage)
    {
        Core.gLogger.Msg($"GameplayActivity.ShowChallengeScreen() - 挑战界面已显示: Page={thePage}");
    }

    /// <summary>
    /// Hook GameplayActivity.ShowBackScreen 方法，在显示返回界面后记录日志
    /// </summary>
    [HarmonyPatch(typeof(GameplayActivity), nameof(GameplayActivity.ShowBackScreen))]
    [HarmonyPostfix]
    public static void ShowBackScreen_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.ShowBackScreen() - 返回界面已显示");
    }

    /// <summary>
    /// Hook GameplayActivity.OnApplicationQuit 方法，在应用程序退出时记录日志
    /// </summary>
    [HarmonyPatch(typeof(GameplayActivity), nameof(GameplayActivity.OnApplicationQuit))]
    [HarmonyPostfix]
    public static void OnApplicationQuit_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.OnApplicationQuit() - 应用程序正在退出");
    }
}