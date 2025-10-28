using HarmonyLib;
using Il2CppUI.Scripts;
using UnityEngine;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(HelpCarousel))]
public class HelpCarouselPatch
{
    [HarmonyPatch("OnEnable")]
    [HarmonyPostfix]
    public static void OnEnable_Postfix()
    {
        Core.gLogger.Msg($"HelpCarousel.OnEnable()");
    }

    [HarmonyPatch("NextScreen")]
    [HarmonyPostfix]
    public static void NextScreen_Postfix()
    {
        Core.gLogger.Msg($"HelpCarousel.NextScreen()");
    }

    [HarmonyPatch("PreviousScreen")]
    [HarmonyPostfix]
    public static void PreviousScreen_Postfix()
    {
        Core.gLogger.Msg($"HelpCarousel.PreviousScreen()");
    }

    [HarmonyPatch("SetPageLabel")]
    [HarmonyPostfix]
    public static void SetPageLabel_Postfix(HelpCarousel __instance)
    {
        if (__instance == null) return;

        Core.gLogger.Msg($"HelpCarousel.SetPageLabel()");
        string pageLabel = __instance.m_pageLabel != null ? __instance.m_pageLabel.text : "";
        Core.gLogger.Msg($"  currentScreen={__instance.m_currentScreen}; pageLabel={pageLabel}");
    }

    [HarmonyPatch("SetPage")]
    [HarmonyPostfix]
    public static void SetPage_Postfix(int page)
    {
        Core.gLogger.Msg($"HelpCarousel.SetPage(page={page})");
    }
}