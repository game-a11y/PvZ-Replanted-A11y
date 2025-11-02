using System.Text;
using HarmonyLib;
using Il2CppUI.Scripts;

namespace PvzReA11y.A11yPatch
{
    [HarmonyPatch(typeof(LevelSelectScreen))]
    public class LevelSelectScreenPatch
    {
        [HarmonyPatch(nameof(LevelSelectScreen.OnEnterLevelSelect))]
        [HarmonyPostfix]
        public static void OnEnterLevelSelect_Postfix()
        {
            Core.gLogger.Msg($"LevelSelectScreen.OnEnterLevelSelect()");
        }

        [HarmonyPatch(nameof(LevelSelectScreen.OnLeaveLevelSelect))]
        [HarmonyPostfix]
        public static void OnLeaveLevelSelect_Postfix()
        {
            Core.gLogger.Msg($"LevelSelectScreen.OnLeaveLevelSelect()");
        }

        [HarmonyPatch(nameof(LevelSelectScreen.SelectLevel), typeof(int), typeof(bool))]
        [HarmonyPostfix]
        public static void SelectLevel_Postfix(int level, bool forceTweenImmediate)
        {
            Core.gLogger.Msg($"LevelSelectScreen.SelectLevel(level={level}, forceTweenImmediate={forceTweenImmediate}) completed");
        }

        [HarmonyPatch(nameof(LevelSelectScreen.SelectNextLevel))]
        [HarmonyPostfix]
        public static void SelectNextLevel_Postfix()
        {
            Core.gLogger.Msg("LevelSelectScreen.SelectNextLevel()");
        }

        [HarmonyPatch(nameof(LevelSelectScreen.SelectPrevLevel))]
        [HarmonyPostfix]
        public static void SelectPrevLevel_Postfix()
        {
            Core.gLogger.Msg("LevelSelectScreen.SelectPrevLevel()");
        }

        // TODO: 鼠标点击会被触发两次。
        [HarmonyPatch(nameof(LevelSelectScreen.SetSelectedLevelIndex), typeof(int))]
        [HarmonyPostfix]
        public static void SetSelectedLevelIndex_Postfix(LevelSelectScreen __instance, int level)
        {
            if (__instance == null) return;

            // TODO: 获取现有的文本
            string a11yText = $"关卡 {level + 1}";
            StringBuilder sb = new StringBuilder();
            sb.Append($"LevelSelectScreen.SetSelectedLevelIndex(level={level})");
            sb.Append($": lastCarouselSel={LevelSelectScreen.s_lastCarouselSelection}, lastCarouselLvSel={LevelSelectScreen.s_lastCarouselLevelSelection}");
            sb.Append($", isFirstVisit={__instance.m_isFirstVisit}, levelSelectIsActive={__instance.m_levelSelectIsActive}");
            sb.Append($", reachedCarousel={__instance.ReachedCarousel}");
            sb.Append($", backButton={__instance.m_backButton}");
            string a11yCtx = sb.ToString();

            A11y.SR.SpeakInterrupt(a11yText, a11yCtx);
        }

        [HarmonyPatch("ShowCarousel", typeof(LevelSelectCarouselGroup))]
        [HarmonyPostfix]
        public static void ShowCarousel_Postfix(LevelSelectCarouselGroup carouselGroup)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"LevelSelectScreen.ShowCarousel(");
            sb.Append($"newCarouselGroup=LevelSelectCarouselGroup#{carouselGroup?.GetHashCode()})");
            sb.Append($": .levelCarousel=LevelSelectCarousel#{carouselGroup?.levelCarousel?.GetHashCode()}");
            sb.Append($", .levelCarousel.offset={carouselGroup?.levelCarousel?.m_offset}");
            
            Core.gLogger.Msg(sb.ToString());
        }

        [HarmonyPatch("UpdateSelectedCarousel", typeof(LevelSelectCarouselGroup))]
        [HarmonyPostfix]
        public static void UpdateSelectedCarousel_Postfix(LevelSelectCarouselGroup newCarouselGroup)
        {
            Core.gLogger.Msg($"LevelSelectScreen.UpdateSelectedCarousel(newCarouselGroup=LevelSelectCarouselGroup#{newCarouselGroup?.GetHashCode()})");
        }
    }
}