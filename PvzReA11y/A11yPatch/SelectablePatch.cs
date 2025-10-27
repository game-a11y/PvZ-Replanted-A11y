using HarmonyLib;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace PvzReA11y.A11yPatch;

/// <summary>
/// Hook Unity UI Selectable 组件的鼠标事件
/// 用于检测所有可选择 UI 元素的鼠标悬停事件
/// </summary>
[HarmonyPatch(typeof(Selectable))]
internal class SelectablePatch
{
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
        try
        {
            if (__instance == null) return;
            
            // 获取基本信息
            string objectName = __instance.gameObject.name ?? "Unknown";
            string objectType = __instance.GetType().Name;
            string objectText = "";
            
            // 尝试获取文本内容
            objectText = GetSelectableText(__instance);
            
            // 输出悬停信息
            Core.gLogger.Msg($"Selectable.OnPointerEnter - Type: {objectType}, Name: '{objectName}', Text: '{objectText}'");
            
            // 输出状态信息
            Core.gLogger.Msg($"  Interactable: {__instance.interactable}, IsActive: {__instance.IsActive()}");
            
            // 输出位置信息
            if (__instance.transform != null)
            {
                var worldPos = __instance.transform.position;
                Core.gLogger.Msg($"  Position: {worldPos}");
                
                // 输出父对象信息
                if (__instance.transform.parent != null)
                {
                    Core.gLogger.Msg($"  Parent: {__instance.transform.parent.name}");
                }
            }
        }
        catch (Exception ex)
        {
            Core.gLogger.Error($"SelectablePatch.OnPointerEnter error: {ex.Message}");
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