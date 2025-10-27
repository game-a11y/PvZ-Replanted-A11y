using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MelonLoader;

namespace PvzReA11y.A11yPatch;

/// <summary>
/// 悬停检测组件
/// 通过实现 IPointerEnterHandler 和 IPointerExitHandler 来检测鼠标悬停事件
/// </summary>
//[RegisterTypeInIl2Cpp]
public class HoverDetectorComponent : MonoBehaviour
{
    private bool _hasBeenHovered = false;
    private Selectable _selectable;
    private Button _button;
    private Toggle _toggle;
    private Slider _slider;
    private Dropdown _dropdown;

    private void Start()
    {
        _hasBeenHovered = false;

        try
        {
            // 获取各种 UI 组件
            _selectable = GetComponent<Selectable>();
            _button = GetComponent<Button>();
            _toggle = GetComponent<Toggle>();
            _slider = GetComponent<Slider>();
            _dropdown = GetComponent<Dropdown>();
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"HoverDetectorComponent.Start error: {ex.Message}");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LogHoverInfo();
    }

    public void OnMouseOver(PointerEventData eventData)
    {
        LogHoverInfo();

        try
        {
            // 只在首次悬停时输出信息
            if (!_hasBeenHovered)
            {
                _hasBeenHovered = true;
                LogHoverInfo();
            }
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"OnPointerEnter error: {ex.Message}");
        }
    }

    private void LogHoverInfo()
    {
        try
        {
            string objectName = gameObject.name ?? "Unknown";
            string objectType = "Unknown";
            string objectText = "";

            // 根据组件类型获取信息
            if (_button != null)
            {
                objectType = "Button";
                
                // 获取按钮文本
                var textComponent = _button.GetComponentInChildren<Text>();
                if (textComponent != null)
                {
                    objectText = textComponent.text ?? "";
                }

                // 尝试获取 TextMeshPro 文本
                var tmpComponent = _button.GetComponentInChildren<Il2CppTMPro.TextMeshProUGUI>();
                if (tmpComponent != null && string.IsNullOrEmpty(objectText))
                {
                    objectText = tmpComponent.text ?? "";
                }
            }
            else if (_toggle != null)
            {
                objectType = "Toggle";
                objectText = $"IsOn: {_toggle.isOn}";
            }
            else if (_slider != null)
            {
                objectType = "Slider";
                objectText = $"Value: {_slider.value}";
            }
            else if (_dropdown != null)
            {
                objectType = "Dropdown";
                objectText = $"Value: {_dropdown.value}";
            }
            else if (_selectable != null)
            {
                objectType = "Selectable";
            }

            MelonLogger.Msg($"FirstHover - Type: {objectType}, Name: '{objectName}', Text: '{objectText}'");

            // 输出父对象信息
            if (transform.parent != null)
            {
                MelonLogger.Msg($"  Parent: {transform.parent.name}");
            }

            // 输出状态信息
            if (_selectable != null)
            {
                MelonLogger.Msg($"  Interactable: {_selectable.interactable}, IsActive: {_selectable.IsActive()}");
            }

            // 输出位置信息
            Vector3 worldPos = transform.position;
            MelonLogger.Msg($"  Position: {worldPos}");
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"LogHoverInfo error: {ex.Message}");
        }
    }
}