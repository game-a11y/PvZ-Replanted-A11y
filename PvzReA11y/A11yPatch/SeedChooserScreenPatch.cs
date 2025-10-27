using HarmonyLib;
using Il2CppReloaded.Gameplay;
using static Il2CppReloaded.Gameplay.SeedChooserScreen;

namespace PvzReA11y.A11yPatch
{
    /// <summary>
    /// SeedChooserScreen类的Harmony补丁，用于提供种子选择界面的无障碍支持
    /// </summary>
    [HarmonyPatch(typeof(SeedChooserScreen))]
    public class SeedChooserScreenPatch
    {
        /// <summary>
        /// Hook SeedChooserScreen.InitSurvivalRepick方法
        /// </summary>
        [HarmonyPatch(nameof(SeedChooserScreen.InitSurvivalRepick))]
        [HarmonyPostfix]
        public static void InitSurvivalRepick_Postfix()
        {
            Core.gLogger.Msg("SeedChooserScreen.InitSurvivalRepick()");
        }

        /// <summary>
        /// Hook SeedChooserScreen.SetFromSeedbank方法
        /// </summary>
        [HarmonyPatch(nameof(SeedChooserScreen.SetFromSeedbank))]
        [HarmonyPostfix]
        public static void SetFromSeedbank_Postfix()
        {
            Core.gLogger.Msg("SeedChooserScreen.SetFromSeedbank()");
        }

        /// <summary>
        /// Hook SeedChooserScreen.AddChosenSeedToBank方法
        /// </summary>
        /// <param name="chosenSeed">选择的种子</param>
        /// <param name="playerIndex">玩家索引</param>
        [HarmonyPatch(nameof(SeedChooserScreen.AddChosenSeedToBank))]
        [HarmonyPostfix]
        public static void AddChosenSeedToBank_Postfix(ChosenSeed chosenSeed, int playerIndex)
        {
            Core.gLogger.Msg($"SeedChooserScreen.AddChosenSeedToBank(Seed={chosenSeed}, player={playerIndex})");
        }

        /// <summary>
        /// Hook SeedChooserScreen.AddChosenSeedsToBank方法
        /// </summary>
        /// <param name="__instance">SeedChooserScreen实例</param>
        /// <param name="chosenSeeds">选择的种子列表</param>
        /// <param name="playerIndex">玩家索引</param>
        [HarmonyPatch(nameof(SeedChooserScreen.AddChosenSeedsToBank))]
        [HarmonyPostfix]
        public static void AddChosenSeedsToBank_Postfix(Il2CppSystem.Collections.Generic.List<ChosenSeed> chosenSeeds, int playerIndex)
        {
            Core.gLogger.Msg($"SeedChooserScreen.AddChosenSeedsToBank(Seeds={chosenSeeds}, player={playerIndex})");
        }

        /// <summary>
        /// Hook SeedChooserScreen.CrazyDavePickSeeds方法
        /// </summary>
        /// <param name="__instance">SeedChooserScreen实例</param>
        [HarmonyPatch("CrazyDavePickSeeds")]
        [HarmonyPostfix]
        public static void CrazyDavePickSeeds_Postfix()
        {
            Core.gLogger.Msg("SeedChooserScreen.CrazyDavePickSeeds()");
        }
    }
}