using HarmonyLib;
using Il2CppReloaded.TreeStateActivities;
using Il2CppReloaded.Gameplay;

namespace PvzReA11y.A11yPatch;

/// <summary>
/// GameplayActivity 类的 Harmony 补丁
/// 为游戏流程相关的函数添加后置挂钩
/// </summary>
[HarmonyPatch(typeof(GameplayActivity))]
public class GameplayActivityPatch
{
    // 缓存 GameplayActivity 对象
    private static GameplayActivity s_gameplayActivity;

    public static GameplayActivity GetCachedGameplayActivity() => s_gameplayActivity;

    /// <summary>
    /// Hook GameplayActivity.NewGame 方法，在新游戏开始后记录日志并缓存实例
    /// </summary>
    [HarmonyPatch(nameof(GameplayActivity.NewGame))]
    [HarmonyPostfix]
    public static void NewGame_Postfix(GameplayActivity __instance)
    {
        Core.gLogger.Msg("GameplayActivity.NewGame()");
        if (s_gameplayActivity != __instance)
        {
            s_gameplayActivity = __instance;
            Core.gLogger.Msg($"  Cached: GameplayActivity#{__instance?.GetHashCode():X8}");
        }
    }

    /// <summary>
    /// Hook GameplayActivity.StartPlaying 方法，在开始游戏后记录日志
    /// </summary>
    [HarmonyPatch(nameof(GameplayActivity.StartPlaying))]
    [HarmonyPostfix]
    public static void StartPlaying_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.StartPlaying()");
    }

    /// <summary>
    /// Hook GameplayActivity.EndLevel 方法，在关卡结束后记录日志
    /// </summary>
    [HarmonyPatch(nameof(GameplayActivity.EndLevel))]
    [HarmonyPostfix]
    public static void EndLevel_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.EndLevel()");
    }

    /// <summary>
    /// Hook GameplayActivity.ShowSeedChooserScreen 方法，在显示种子选择界面后记录日志
    /// </summary>
    [HarmonyPatch(nameof(GameplayActivity.ShowSeedChooserScreen))]
    [HarmonyPostfix]
    public static void ShowSeedChooserScreen_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.ShowSeedChooserScreen()");
    }

    /// <summary>
    /// Hook GameplayActivity.DoDialog 方法，在创建对话框后记录日志
    /// </summary>
    //[HarmonyPatch(nameof(GameplayActivity.DoDialog))]
    //[HarmonyPostfix]
    //public static void DoDialog_Postfix(Dialogs theDialogId, bool isModal, string theDialogHeader, string theDialogLines, string theDialogFooter, Dialog.ButtonType theButtonMode)
    //{
    //    Core.gLogger.Msg($"GameplayActivity.DoDialog(): DialogId={theDialogId}, Modal={isModal}, Header='{theDialogHeader}', Lines='{theDialogLines}', Footer='{theDialogFooter}', ButtonType={theButtonMode}");
    //}

    /// <summary>
    /// Hook GameplayActivity.SaveGame 方法，在保存游戏后记录日志
    /// </summary>
    [HarmonyPatch(nameof(GameplayActivity.SaveGame))]
    [HarmonyPostfix]
    public static void SaveGame_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.SaveGame()");
    }

    // 过多日志
    /// <summary>
    /// Hook GameplayActivity.ShowGameSelector 方法，在显示游戏选择器后记录日志
    /// </summary>
    //[HarmonyPatch(nameof(GameplayActivity.ShowGameSelector))]
    //[HarmonyPostfix]
    //public static void ShowGameSelector_Postfix()
    //{
    //    Core.gLogger.Msg("GameplayActivity.ShowGameSelector()");
    //}

    // TODO: 崩溃
    /// <summary>
    /// Hook GameplayActivity.ShowStoreScreen 方法，在显示商店界面后记录日志
    /// </summary>
    //[HarmonyPatch(nameof(GameplayActivity.ShowStoreScreen))]
    //[HarmonyPostfix]
    //public static void ShowStoreScreen_Postfix()
    //{
    //    Core.gLogger.Msg("GameplayActivity.ShowStoreScreen()");
    //}

    /// <summary>
    /// Hook GameplayActivity.ShowAwardScreen 方法，在显示奖励界面后记录日志
    /// </summary>
    [HarmonyPatch(nameof(GameplayActivity.ShowAwardScreen))]
    [HarmonyPostfix]
    public static void ShowAwardScreen_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.ShowAwardScreen()");
    }

    /// <summary>
    /// Hook GameplayActivity.ShowZombiesWonScreen 方法，在显示僵尸胜利界面后记录日志
    /// </summary>
    [HarmonyPatch(nameof(GameplayActivity.ShowZombiesWonScreen))]
    [HarmonyPostfix]
    public static void ShowZombiesWonScreen_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.ShowZombiesWonScreen()");
    }

    /// <summary>
    /// Hook GameplayActivity.ShowReadySetPlantScreen 方法，在显示准备种植界面后记录日志
    /// </summary>
    [HarmonyPatch(nameof(GameplayActivity.ShowReadySetPlantScreen))]
    [HarmonyPostfix]
    public static void ShowReadySetPlantScreen_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.ShowReadySetPlantScreen()");
    }

    /// <summary>
    /// Hook GameplayActivity.ShowChallengeScreen 方法，在显示挑战界面后记录日志
    /// </summary>
    [HarmonyPatch(nameof(GameplayActivity.ShowChallengeScreen))]
    [HarmonyPostfix]
    public static void ShowChallengeScreen_Postfix(ChallengePage thePage)
    {
        Core.gLogger.Msg($"GameplayActivity.ShowChallengeScreen(): Page={thePage}");
    }

    /// <summary>
    /// Hook GameplayActivity.ShowBackScreen 方法，在显示返回界面后记录日志
    /// </summary>
    [HarmonyPatch(nameof(GameplayActivity.ShowBackScreen))]
    [HarmonyPostfix]
    public static void ShowBackScreen_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.ShowBackScreen()");
    }

    /// <summary>
    /// Hook GameplayActivity.OnApplicationQuit 方法，在应用程序退出时记录日志
    /// </summary>
    [HarmonyPatch(nameof(GameplayActivity.OnApplicationQuit))]
    [HarmonyPostfix]
    public static void OnApplicationQuit_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.OnApplicationQuit()");
    }

    /// <summary>
    /// CrazyDave 进入对话的后置挂钩
    /// </summary>
    [HarmonyPatch(nameof(GameplayActivity.CrazyDaveEnter))]
    [HarmonyPostfix]
    public static void CrazyDaveEnter_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.CrazyDaveEnter()");
    }

    /// <summary>
    /// CrazyDave 说话消息的后置挂钩
    /// </summary>
    [HarmonyPatch(nameof(GameplayActivity.CrazyDaveTalkMessage))]
    [HarmonyPostfix]
    public static void CrazyDaveTalkMessage_Postfix(string message)
    {
        Core.gLogger.Msg($"GameplayActivity.CrazyDaveTalkMessage(message={message})");
    }

    /// <summary>
    /// CrazyDave 离开对话的后置挂钩
    /// </summary>
    [HarmonyPatch(nameof(GameplayActivity.CrazyDaveLeave))]
    [HarmonyPostfix]
    public static void CrazyDaveLeave_Postfix()
    {
        Core.gLogger.Msg("GameplayActivity.CrazyDaveLeave()");
    }

    /// <summary>
    /// 新游戏预处理的后置挂钩
    /// </summary>
    [HarmonyPatch(nameof(GameplayActivity.PreNewGame))]
    [HarmonyPostfix]
    public static void PreNewGame_Postfix(GameMode theGameMode, bool theLookForSavedGame)
    {
        Core.gLogger.Msg($"GameplayActivity.PreNewGame(gameMode={theGameMode}, lookForSavedGame={theLookForSavedGame})");
    }

    // 崩溃
    /// <summary>
    /// 切换慢动作的后置挂钩
    /// </summary>
    //[HarmonyPatch(nameof(GameplayActivity.ToggleSlowMo))]
    //[HarmonyPostfix]
    //public static void ToggleSlowMo_Postfix()
    //{
    //    Core.gLogger.Msg("GameplayActivity.ToggleSlowMo()");
    //}
    // 崩溃
    /// <summary>
    /// 切换快动作的后置挂钩
    /// </summary>
    //[HarmonyPatch(nameof(GameplayActivity.ToggleFastMo))]
    //[HarmonyPostfix]
    //public static void ToggleFastMo_Postfix()
    //{
    //    Core.gLogger.Msg("GameplayActivity.ToggleFastMo()");
    //}

    // 崩溃
    /// <summary>
    /// 打开暂停对话的后置挂钩
    /// </summary>
    //[HarmonyPatch(nameof(GameplayActivity.DoPauseDialog))]
    //[HarmonyPostfix]
    //public static void DoPauseDialog_Postfix()
    //{
    //    Core.gLogger.Msg("GameplayActivity.DoPauseDialog()");
    //}

}