using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PvzReA11y;

internal class A11yText
{
    /// <summary>
    /// Selectable 类的名称映射
    /// </summary>
    /// TODO: 改用"类名-类型"作为键名
    private static readonly Dictionary<string, string> Selectable_Mappings = new()
    {
        /* 【主菜单】 */
        ["MusicP_Slider"] = "音乐 (滑块)",
        ["Sound FXP_Slider"] = "音效 (滑块)",
        // 仅手柄模式显示
        ["Gamepad Speed XP_Slider"] = "光标速度 (滑块)",

        ["Dropdown"] = "分辨率 (下拉菜单)",
        ["全屏显示"] = "屏幕模式 (下拉菜单): 全屏显示",
        ["窗口化"] = "屏幕模式 (下拉菜单): 窗口化",
        ["无边框"] = "屏幕模式 (下拉菜单): 无边框",

        ["VibrationP_CheckBox (1)"] = "震动 (复选框)",
        ["V-SyncP_CheckBox"] = "垂直同步 (复选框)",

        /* 【关卡选择】 */
        ["P_BackButton"] = "返回 (按钮)",

        /* 卡片选择 */
        //["玩家 2"] = "",  // 抑制背景输出（在 SelectablePatch 中抑制）
        ["SeedBackground"] = "植物卡",

        /* 【游戏内】菜单 */
        ["Button"] = "僵尸形象 (按钮)",
        ["headcrab _CheckBox"] = "猎头蟹 (复选框)",
        ["retroZombie _CheckBox"] = "复古僵尸 (复选框)",

        /* 【游戏内】 */
        ["Shovel"] = "铲子",
        ["P_AccelerationButton"] = "游戏加速 (按钮)",
    };

    public static string GetA11yText(string key, string ctx = null)
    {
        string notEmptyKey = key ?? string.Empty;
        return ctx switch
        {
            "Selectable" => Selectable_Mappings.TryGetValue(key, out var text) ? text : notEmptyKey,
            _ => notEmptyKey
        };
    }
}
