using Il2CppReloaded.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PvzReA11y;

internal class A11yText
{
    private static readonly Dictionary<SeedType, string> SeedType_ZH = new()
    {
        // 主线植物
        [SeedType.Peashooter] = "豌豆射手",
        [SeedType.Sunflower] = "向日葵",
        [SeedType.Cherrybomb] = "樱桃炸弹",
        [SeedType.Wallnut] = "坚果墙",
        [SeedType.Potatomine] = "土豆地雷",
        [SeedType.Snowpea] = "寒冰射手",
        [SeedType.Chomper] = "食人花",
        [SeedType.Repeater] = "双发射手",

        // 夜晚植物
        [SeedType.Puffshroom] = "小喷菇",
        [SeedType.Sunshroom] = "阳光菇",
        [SeedType.Fumeshroom] = "大喷菇",
        [SeedType.Gravebuster] = "墓碑吞噬者",
        [SeedType.Hypnoshroom] = "催眠菇",
        [SeedType.Scaredyshroom] = "胆小菇",
        [SeedType.Iceshroom] = "寒冰菇",
        [SeedType.Doomshroom] = "毁灭菇",

        // 水面植物
        [SeedType.Lilypad] = "荷叶",
        [SeedType.Squash] = "窝瓜",
        [SeedType.Threepeater] = "三发射手",
        [SeedType.Tanglekelp] = "缠绕海草",
        [SeedType.Jalapeno] = "火爆辣椒",
        [SeedType.Spikeweed] = "地刺",
        [SeedType.Torchwood] = "火炬树桩",
        [SeedType.Tallnut] = "高坚果",
        [SeedType.Seashroom] = "海蘑菇",

        // 工具与功能类
        [SeedType.Plantern] = "灯笼草",
        [SeedType.Cactus] = "仙人掌",
        [SeedType.Blover] = "三叶草",
        [SeedType.Splitpea] = "裂荚射手",
        [SeedType.Starfruit] = "杨桃",
        [SeedType.Pumpkinshell] = "南瓜头",
        [SeedType.Magnetshroom] = "磁力菇",

        // 屋顶植物
        [SeedType.Cabbagepult] = "卷心菜投手",
        [SeedType.Flowerpot] = "花盆",
        [SeedType.Kernelpult] = "玉米投手",
        [SeedType.InstantCoffee] = "咖啡豆",
        [SeedType.Garlic] = "大蒜",
        [SeedType.Umbrella] = "伞叶",
        [SeedType.Marigold] = "金盏花",
        [SeedType.Melonpult] = "西瓜投手",

        // 升级植物
        [SeedType.Gatlingpea] = "机枪射手",
        [SeedType.Twinsunflower] = "双子向日葵",
        [SeedType.Gloomshroom] = "忧郁菇",
        [SeedType.Cattail] = "香蒲",
        [SeedType.Wintermelon] = "冬瓜投手",
        [SeedType.GoldMagnet] = "金磁铁",
        [SeedType.Spikerock] = "地刺王",
        [SeedType.Cobcannon] = "玉米加农炮",

        // 其他
        [SeedType.Imitater] = "模仿者",
        [SeedType.ExplodeONut] = "爆炸坚果",
        [SeedType.GiantWallnut] = "巨型坚果",
        [SeedType.Sprout] = "幼苗",
        [SeedType.Leftpeater] = "左发射手",
    };

    /// <summary>
    /// SeedType 转中文植物名。未映射则返回英文名，None 返回“无”。
    /// </summary>
    public static string GetSeedTypeZh(SeedType seedType)
    {
        if (SeedType_ZH.TryGetValue(seedType, out var name))
            return name;

        return seedType == SeedType.None ? "" : seedType.ToString();
    }

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
