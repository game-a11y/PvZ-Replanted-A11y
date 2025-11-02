using Il2CppReloaded.Gameplay;

namespace PvzReA11y.ReplantAPI;

public static class BoardHelper
{
    private static Board s_cachedBoard;

    public static void CacheBoard(Board s_cachedBoard)
    {
        BoardHelper.s_cachedBoard = s_cachedBoard;
    }

    public static Board GetCachedBoard() => s_cachedBoard;

    public static bool HasCachedBoard => s_cachedBoard != null;

    /* ---- 僵尸预览缓存 ---- */
    private static System.Collections.Generic.HashSet<ZombieType> s_levelZombieTypes = new();
    public static void ResetLevelZombieTypes() => s_levelZombieTypes.Clear();
    public static void CacheLevelZombieType(ZombieType zombieType)
    {
        if (!s_levelZombieTypes.Contains(zombieType))
            s_levelZombieTypes.Add(zombieType);
    }
    public static System.Collections.Generic.IReadOnlyCollection<ZombieType> GetLevelZombieTypes() => s_levelZombieTypes;

    /// <summary>
    /// 获取当前玩家正在种植的种子类型
    /// </summary>
    /// <param name="playerIdx">玩家索引，默认 0</param>
    /// <returns>当前玩家正在种植的种子类型</returns>
    public static SeedType GetCurentSeedType(int playerIdx)
    {
        if (!HasCachedBoard) return SeedType.None;

        if (s_cachedBoard.IsPlantInCursor(playerIdx))
        {
            return s_cachedBoard.GetSeedTypeInCursor(playerIdx);
        }

        return SeedType.None;
    }

    /// <summary>
    /// 获取当前玩家无法种植的原因
    /// </summary>
    /// <param name="playerIdx">玩家索引，默认 0</param>
    /// <returns>当前玩家无法种植的原因</returns>
    public static string GetGridState(int x, int y,int playerIdx=0)
    {
        if (!HasCachedBoard) return "";

        SeedType seedType = GetCurentSeedType(playerIdx);

        /* ==== 格子状态 ==== */
        string gridState = "";
        // 检查是否有植物
        var plant = s_cachedBoard.GetTopPlantAt(x, y, PlantPriority.Any);
        bool hasPlant = plant != null;
        if (hasPlant)
        {
            gridState = A11yText.GetSeedTypeZh(plant.mSeedType);
        }

        // TODO: 检查是否为正常地面
        // 冰层
        bool hasIce = s_cachedBoard.IsIceAt(x, y);
        if (hasIce) gridState = "冰层";

        /* ==== 是否可种植 ==== */
        var plantingReason = s_cachedBoard.CanPlantAt(x, y, seedType);
        string plantingState = plantingReason switch
        {
            PlantingReason.Ok => "可种植",

            // 有植物
            PlantingReason.NotHere => "不可种植",
            PlantingReason.OnlyOnGraves => "只能在种在墓碑上",
            PlantingReason.OnlyInPool => "只能种在池塘中",
            PlantingReason.OnlyOnGround => "只能种在地面上",

            PlantingReason.NeedsPot => "需要花盆",
            PlantingReason.NotOnArt => "Not On Art",
            PlantingReason.NotPassedLine => "Not Passed Line",
            PlantingReason.NeedsUpgrade => "需要升级",
            PlantingReason.NotOnGrave => "不能种在墓碑上",
            PlantingReason.NotOnCrater => "不能种在 crater 上",
            PlantingReason.NotOnWater => "不能种在水上",

            PlantingReason.NeedsGround => "需要地面",
            PlantingReason.NeedsSleeping => "需要睡眠",

            PlantingReason.VSSuddenDeath => "VS Sudden Death",
            PlantingReason.VSNotHere => "VS Not Here",
            _ => ""
        };

        /* ==== 其他信息 ==== */
        // 行有割草机
        var mower = s_cachedBoard.FindLawnMowerInRow(y);
        string mowerText = mower != null ? "有割草机" : "无割草机";

        return $"{gridState}; {plantingState}; {mowerText}";
    }

    public static void AnnounceLevelZombieTypes()
    {
        // 关卡介绍时输出本关僵尸类型
        var types = BoardHelper.GetLevelZombieTypes();
        if (types != null && types.Count > 0)
        {
            var listZh = new System.Text.StringBuilder();
            bool first = true;
            foreach (var t in types)
            {
                if (!first) listZh.Append('、');
                listZh.Append(A11yText.GetZombieTypeZh(t));
                first = false;
            }
            string a11yText = $"本关会出现的僵尸类型：{listZh}";
            string a11yCtx = "CutScene.StartLevelIntro() - LevelZombieTypes";

            A11y.SR.Speak(a11yText, a11yCtx);
        }
        else
        {
            Core.gLogger.Msg("CutScene.StartLevelIntro() - 无僵尸类型缓存");
        }
    }

    //public static string GetCellStatus(SeedType seedType, int x, int y)
    //{
    //    if (!HasCachedBoard) return "";

    //    // 植物
    //    var plant = s_cachedBoard.GetTopPlantAt(x, y, PlantPriority.Any);
    //    string plantText = plant != null ? A11yText.GetSeedTypeZh(plant.mSeedType) : "无植物";

    //    // 网格物品（花盆、睡莲、墓碑等）
    //    var gridItem = s_cachedBoard.GetGridItemAt(GridItemType.Gravestone, x, y);
    //    string gridItemText = gridItem != null ? gridItem.mGridItemType.ToString() : "无网格物品";

    //    // 冰层
    //    bool hasIce = s_cachedBoard.IsIceAt(x, y);

    //    // 泳池格（有的 API 只按 y 判断，如果有重载优先用 x,y 版本）
    //    bool isPool = s_cachedBoard.IsPoolSquare(x, y);

    //    // 是否可种植
    //    var plantingReason = s_cachedBoard.CanPlantAt(x, y, seedType);

    //    // 行有割草机
    //    //var mower = s_cachedBoard.FindLawnMowerInRow(y);
    //    //bool hasMower = mower != null;

    //    return $"行 {y + 1}, 列 {x + 1}：" +
    //           $"{plantText}；{gridItemText}；" +
    //           $"冰层={hasIce}；泳池={isPool}；可种植={plantingReason}；";
    //}

}