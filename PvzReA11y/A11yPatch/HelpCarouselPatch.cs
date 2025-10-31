using HarmonyLib;
using Il2CppUI.Scripts;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(HelpCarousel))]
public class HelpCarouselPatch
{
    [HarmonyPatch("OnEnable")]
    [HarmonyPostfix]
    public static void OnEnable_Postfix()
    {
        string a11yText = $"已打开帮助页面";
        string a11yCtx = "HelpCarousel.OnEnable()";
        A11y.SR.SpeakInterrupt(a11yText, a11yCtx);
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
        string a11yCtx = "HelpCarousel.SetPageLabel()";

        int currentScreen = __instance.m_currentScreen;
        string pageLabel = __instance.m_pageLabel != null ? __instance.m_pageLabel.text : "";
        string pageNumStr = $"页面 {pageLabel}";
        if (string.IsNullOrEmpty(pageLabel))
        {
            pageNumStr = $"第 {currentScreen + 1} 页";
        }
        a11yCtx += $";  currentScreen={currentScreen}";

        // TODO: 拆分帮助文本显示
        //bool IsUsingController = InputService.IsAnyPlayerUsingController();
        // NOTE: 只有控制器会多出“控制”页，故直接检测 pageNumStr 是否包含“6”字符
        bool IsUsingController = pageNumStr.Contains('6');
        string a11yText = "";
        switch (currentScreen)
        {
            case 0:
                a11yText = $"帮助 ({pageNumStr})";
                break;
            case 1:
                a11yText = $"实用帮助 ({pageNumStr})";
                break;
            case 2:
                // NOTE: 使用手柄时，pageNum=3 为“控制“页
                a11yText = IsUsingController ? $"控制" : $"合作模式";
                a11yText = $"{a11yText} ({pageNumStr})";
                break;
            case 3:
                a11yText = IsUsingController ? $"合作模式" : $"对战模式";
                a11yText = $"{a11yText} ({pageNumStr})";
                break;
            case 4:
                a11yText = IsUsingController ? $"对战模式" : $"冒险安息";
                a11yText = $"{a11yText} ({pageNumStr})";
                break;
            case 5:
                a11yText = $"冒险安息 ({pageNumStr})";
                break;
            default:
                Core.gLogger.Warning($"HelpCarousel.SetPageLabel(): Unknown page num: {currentScreen}");
                break;
        }

        A11y.SR.SpeakInterrupt(a11yText, a11yCtx);
    }

    [HarmonyPatch("SetPage")]
    [HarmonyPostfix]
    public static void SetPage_Postfix(int page)
    {
        Core.gLogger.Msg($"HelpCarousel.SetPage(page={page})");
    }
}