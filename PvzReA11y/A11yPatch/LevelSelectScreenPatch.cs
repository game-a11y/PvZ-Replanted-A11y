using HarmonyLib;
using Il2CppUI.Scripts;
using MelonLoader;

namespace PvzReA11y.A11yPatch
{
    [HarmonyPatch(typeof(LevelSelectScreen))]
    public class LevelSelectScreenPatch
    {
        [HarmonyPatch(nameof(LevelSelectScreen.OnEnterLevelSelect))]
        [HarmonyPostfix]
        public static void OnEnterLevelSelect_Postfix()
        {
            MelonLogger.Msg($"LevelSelectScreen.OnEnterLevelSelect()");
        }

        [HarmonyPatch(nameof(LevelSelectScreen.OnLeaveLevelSelect))]
        [HarmonyPostfix]
        public static void OnLeaveLevelSelect_Postfix()
        {
            MelonLogger.Msg($"LevelSelectScreen.OnLeaveLevelSelect()");
        }

        [HarmonyPatch(nameof(LevelSelectScreen.SelectLevel), typeof(int), typeof(bool))]
        [HarmonyPostfix]
        public static void SelectLevel_Postfix(int level, bool forceTweenImmediate)
        {
            MelonLogger.Msg($"LevelSelectScreen.SelectLevel(level={level}, forceTweenImmediate={forceTweenImmediate}) completed");
        }

        [HarmonyPatch(nameof(LevelSelectScreen.SelectNextLevel))]
        [HarmonyPostfix]
        public static void SelectNextLevel_Postfix()
        {
            MelonLogger.Msg("LevelSelectScreen.SelectNextLevel()");
        }

        [HarmonyPatch(nameof(LevelSelectScreen.SelectPrevLevel))]
        [HarmonyPostfix]
        public static void SelectPrevLevel_Postfix()
        {
            MelonLogger.Msg("LevelSelectScreen.SelectPrevLevel()");
        }

        // TODO: 鼠标点击会被触发两次。
        [HarmonyPatch(nameof(LevelSelectScreen.SetSelectedLevelIndex), typeof(int))]
        [HarmonyPostfix]
        public static void SetSelectedLevelIndex_Postfix(int level)
        {
            MelonLogger.Msg($"LevelSelectScreen.SetSelectedLevelIndex(level={level})");
        }

        [HarmonyPatch("ShowCarousel", typeof(Il2CppUI.Scripts.LevelSelectCarouselGroup))]
        [HarmonyPostfix]
        public static void ShowCarousel_Postfix(Il2CppUI.Scripts.LevelSelectCarouselGroup carouselGroup)
        {
            MelonLogger.Msg($"LevelSelectScreen.ShowCarousel(carouselGroup=LevelSelectCarouselGroup#{carouselGroup?.GetHashCode()})");
        }

        [HarmonyPatch("UpdateSelectedCarousel", typeof(Il2CppUI.Scripts.LevelSelectCarouselGroup))]
        [HarmonyPostfix]
        public static void UpdateSelectedCarousel_Postfix(Il2CppUI.Scripts.LevelSelectCarouselGroup newCarouselGroup)
        {
            MelonLogger.Msg($"LevelSelectScreen.UpdateSelectedCarousel(newCarouselGroup=LevelSelectCarouselGroup#{newCarouselGroup?.GetHashCode()})");
        }
    }
}