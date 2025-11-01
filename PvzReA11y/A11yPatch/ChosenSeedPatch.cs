using HarmonyLib;
using Il2CppReloaded.Gameplay;
using System.Text;

namespace PvzReA11y.A11yPatch;

/// <summary>
/// ChosenSeed类的Harmony补丁，用于监控种子选择和模仿者种子的相关操作
/// </summary>
[HarmonyPatch(typeof(ChosenSeed))]
public class ChosenSeedPatch
{
    /// <summary>
    /// Hook ChosenSeed.Selected方法的后置钩子
    /// 在种子被选择后触发
    /// </summary>
    /// <param name="__instance">ChosenSeed实例</param>
    /// <param name="seedChooserScreen">种子选择界面实例</param>
    /// <param name="playerIndex">玩家索引</param>
    [HarmonyPatch(nameof(ChosenSeed.Selected))]
    [HarmonyPostfix]
    public static void Selected_Postfix(ChosenSeed __instance, SeedChooserScreen seedChooserScreen, int playerIndex)
    {
        if (__instance == null)
        {
            Core.gLogger.Msg("ChosenSeed.Selected() - __instance is null");
            return;
        }

        var sb = new StringBuilder();
        sb.AppendLine($"ChosenSeed.Selected(seedChooserScreen={seedChooserScreen}, player={playerIndex})");

        // 获取种子信息
        string seedType = __instance.mSeedType.ToString();
        string seedState = __instance.mSeedState.ToString();
        int seedIndexInBank = __instance.mSeedIndexInBank;
        bool isImitater = __instance.mIsImitater;
        bool isFlashing = __instance.mFlashing;
        bool isRefreshing = __instance.mRefreshing;

        sb.AppendLine($"  - SeedType: {seedType}");
        sb.AppendLine($"  - SeedState: {seedState}");
        sb.AppendLine($"  - SeedIndexInBank: {seedIndexInBank}");
        sb.AppendLine($"  - IsImitater: {isImitater}");
        sb.AppendLine($"  - IsFlashing: {isFlashing}");
        sb.AppendLine($"  - IsRefreshing: {isRefreshing}");

        // 如果是模仿者种子，还要显示模仿的种子类型
        if (isImitater)
        {
            string imitaterType = __instance.mImitaterType.ToString();
            sb.AppendLine($"  - ImitaterType: {imitaterType}");
        }

        Core.gLogger.Msg(sb.ToString());
    }

    /// <summary>
    /// Hook ChosenSeed.Imitated方法的后置钩子
    /// 在模仿者种子被设置后触发
    /// </summary>
    /// <param name="__instance">ChosenSeed实例</param>
    /// <param name="seedChooserScreen">种子选择界面实例</param>
    /// <param name="index">索引</param>
    /// <param name="playerIndex">玩家索引</param>
    [HarmonyPatch(nameof(ChosenSeed.Imitated))]
    [HarmonyPostfix]
    public static void Imitated_Postfix(ChosenSeed __instance, SeedChooserScreen seedChooserScreen, int index, int playerIndex)
    {
        if (__instance == null)
        {
            Core.gLogger.Msg("ChosenSeed.Imitated() - __instance is null");
            return;
        }

        var sb = new StringBuilder();
        sb.AppendLine($"ChosenSeed.Imitated(seedChooserScreen={seedChooserScreen}, index={index}， player={playerIndex})");

        // 获取种子信息
        string seedType = __instance.mSeedType.ToString();
        string imitaterType = __instance.mImitaterType.ToString();
        string seedState = __instance.mSeedState.ToString();
        int seedIndexInBank = __instance.mSeedIndexInBank;
        bool isImitater = __instance.mIsImitater;
        bool isFlashing = __instance.mFlashing;
        bool isRefreshing = __instance.mRefreshing;

        sb.AppendLine($"  - SeedType: {seedType}");
        sb.AppendLine($"  - ImitaterType: {imitaterType}");
        sb.AppendLine($"  - SeedState: {seedState}");
        sb.AppendLine($"  - SeedIndexInBank: {seedIndexInBank}");
        sb.AppendLine($"  - IsImitater: {isImitater}");
        sb.AppendLine($"  - IsFlashing: {isFlashing}");
        sb.AppendLine($"  - IsRefreshing: {isRefreshing}");

        // 显示位置信息
        sb.AppendLine($"  - Position: ({__instance.mX}, {__instance.mY})");

        Core.gLogger.Msg(sb.ToString());
    }
}