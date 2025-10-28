using HarmonyLib;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static MelonLoader.MelonLogger;

namespace PvzReA11y.A11yPatch;

/// <summary>
/// Hook Unity UI Selectable 组件的鼠标事件
/// 用于检测所有可选择 UI 元素的鼠标悬停事件
/// </summary>
[HarmonyPatch(typeof(Selectable))]
internal class SelectablePatch
{
    public static bool NeedSkipA11yOutput(Selectable obj)
    {
        // 获取基本信息
        string objectName = obj.gameObject.name ?? "Unknown";
        string objectType = obj.GetType().Name;
        string objectText = GetSelectableText(obj);
        string objParent = "";
        if (obj.transform != null && obj.transform.parent != null)
        {
            objParent = obj.transform.parent.name;
        }
        bool interactable = obj.interactable;
        bool isActive = obj.IsActive();

        if (string.IsNullOrEmpty(objectText))
        {
            // 非交互元素且无文本，跳过
            if (!interactable) return true;

            // 【帮助】菜单，左右移动箭头黑色背景
            if ("Center" == objParent && "Arrows" == objectName) return true;
        }

        return false;
    }

    /// <summary>
    /// Hook Selectable.OnSelect 方法
    /// 在手柄焦点进入可选择元素时触发
    /// </summary>
    /// <param name="__instance">Selectable 实例</param>
    /// <param name="eventData">事件数据</param>
    [HarmonyPatch("OnSelect")]
    [HarmonyPostfix]
    static void OnSelect_Postfix(Selectable __instance, BaseEventData eventData)
    {
        if (__instance == null) return;

        // 获取基本信息
        string objectName = __instance.gameObject.name ?? "Unknown";
        string objectType = __instance.GetType().Name;
        string objectText = GetSelectableText(__instance);
        string objParent = "";
        if (__instance.transform != null && __instance.transform.parent != null)
        {
            objParent = __instance.transform.parent.name;
        }

        // 语音播报
        string a11yText = objectText;
        if (string.IsNullOrEmpty(a11yText)) a11yText = objectName;
        string a11tCtx = $"{objParent} > {objectName}: {objectType}";
        a11tCtx += $";  Interactable: {__instance.interactable}, IsActive: {__instance.IsActive()}";
        if (NeedSkipA11yOutput(__instance))
        {
            Core.gLogger.Msg($"Selectable.OnSelect(): '{a11yText}'");
            Core.gLogger.Msg($"    {a11tCtx}");
        }
        else
        {
            A11y.SR.SpeakInterrupt(a11yText, a11tCtx);
        }
    }

    /// <summary>
    /// Hook Selectable.OnPointerEnter 方法
    /// 在鼠标进入可选择元素时触发
    /// </summary>  
    /// <param name="__instance">Selectable 实例</param>
    /// <param name="eventData">指针事件数据</param>
    [HarmonyPatch("OnPointerEnter")]
    [HarmonyPostfix]
    static void OnPointerEnter_Postfix(Selectable __instance, PointerEventData eventData)
    {
        if (__instance == null) return;

        // 获取基本信息
        string objectName = __instance.gameObject.name ?? "Unknown";
        string objectType = __instance.GetType().Name;
        string objParent = "";
        if (__instance.transform != null && __instance.transform.parent != null)
        {
            objParent = __instance.transform.parent.name;
        }

        string a11yText = GetSelectableText(__instance);
        if (string.IsNullOrEmpty(a11yText)) a11yText = objectName;
        string a11tCtx = $"{objParent} > {objectName}: {objectType}";
        a11tCtx += $";  Interactable: {__instance.interactable}, IsActive: {__instance.IsActive()}";
        if (NeedSkipA11yOutput(__instance))
        {
            Core.gLogger.Msg($"Selectable.OnPointerEnter(): '{a11yText}'");
            Core.gLogger.Msg($"    {a11tCtx}");
        }
        else
        {
            A11y.SR.SpeakInterrupt(a11yText, a11tCtx);
        }
    }
    
    /// <summary>
    /// 获取 Selectable 元素的文本内容
    /// </summary>
    /// <param name="selectable">Selectable 实例</param>
    /// <returns>文本内容</returns>
    private static string GetSelectableText(Selectable selectable)
    {
        try
        {
            // 检查是否为 Button
            if (selectable is Button button)
            {
                // 获取按钮文本
                var textComponent = button.GetComponentInChildren<Text>();
                if (textComponent != null && !string.IsNullOrEmpty(textComponent.text))
                {
                    return textComponent.text;
                }
                
                // 尝试获取 TextMeshPro 文本
                var tmpComponent = button.GetComponentInChildren<Il2CppTMPro.TextMeshProUGUI>();
                if (tmpComponent != null && !string.IsNullOrEmpty(tmpComponent.text))
                {
                    return tmpComponent.text;
                }
            }
            // 检查是否为 Toggle
            else if (selectable is Toggle toggle)
            {
                var textComponent = toggle.GetComponentInChildren<Text>();
                if (textComponent != null && !string.IsNullOrEmpty(textComponent.text))
                {
                    return $"{textComponent.text} (IsOn: {toggle.isOn})";
                }
                
                var tmpComponent = toggle.GetComponentInChildren<Il2CppTMPro.TextMeshProUGUI>();
                if (tmpComponent != null && !string.IsNullOrEmpty(tmpComponent.text))
                {
                    return $"{tmpComponent.text} (IsOn: {toggle.isOn})";
                }
                
                return $"Toggle (IsOn: {toggle.isOn})";
            }
            // 检查是否为 Slider
            else if (selectable is Slider slider)
            {
                return $"Slider (Value: {slider.value:F2})";
            }
            // 检查是否为 Dropdown
            else if (selectable is Dropdown dropdown)
            {
                var captionText = dropdown.captionText;
                if (captionText != null && !string.IsNullOrEmpty(captionText.text))
                {
                    return $"Dropdown: {captionText.text} (Value: {dropdown.value})";
                }
                return $"Dropdown (Value: {dropdown.value})";
            }
            
            // 通用文本获取
            var generalText = selectable.GetComponentInChildren<Text>();
            if (generalText != null && !string.IsNullOrEmpty(generalText.text))
            {
                return generalText.text;
            }
            
            var generalTmp = selectable.GetComponentInChildren<Il2CppTMPro.TextMeshProUGUI>();
            if (generalTmp != null && !string.IsNullOrEmpty(generalTmp.text))
            {
                return generalTmp.text;
            }
            
            return "";
        }
        catch (Exception ex)
        {
            Core.gLogger.Error($"GetSelectableText error: {ex.Message}");
            return "";
        }
    }
}