using Il2CppReloaded.Gameplay;
using Il2CppTekly.DataModels.Binders;
using PvzReA11y.A11yPatch;
using System.Text;

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
            PlantingReason.NotOnArt => "不能种在装饰地块上",
            PlantingReason.NotPassedLine => "不能越过分界线",
            PlantingReason.NeedsUpgrade => "需要升级",
            PlantingReason.NotOnGrave => "不能种在墓碑上",
            PlantingReason.NotOnCrater => "不能种在坑洞上",
            PlantingReason.NotOnWater => "不能种在水上",

            PlantingReason.NeedsGround => "需要地面",
            PlantingReason.NeedsSleeping => "（夜间植物）需要睡眠",

            PlantingReason.VSSuddenDeath => "对战模式：突然死亡规则",
            PlantingReason.VSNotHere => "对战模式：此处不可种植",
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

    /// <summary>
    /// 输出当前关卡的关卡信息与植物网格信息（简要语音+详细日志）
    /// </summary>
    public static void AnnounceLevelIntro()
    {
        if (!HasCachedBoard) return;

        var board = s_cachedBoard;

        // 关卡信息
        int numRows = board.GetNumRows();
        bool isNight = board.StageIsNight();
        bool hasPool = board.StageHasPool();
        bool hasRoof = board.StageHasRoof();
        bool hasFog = board.StageHasFog();
        bool hasGraves = board.StageHasGraveStones();
        bool noGrass = board.StageHasNoGrass();
        bool hasProgress = board.HasProgressMeter();

        StringBuilder a11ySb = new StringBuilder();
        a11ySb.Append($"{(isNight ? "夜晚" : "白天")}");
        a11ySb.Append($"{(hasPool ? "、泳池" : "")}");
        a11ySb.Append($"{(hasRoof ? "、屋顶" : "")}");
        a11ySb.Append($"{(hasFog ? "、浓雾" : "")}");
        a11ySb.Append($"{(noGrass ? "、无草皮" : "")}");
        a11ySb.Append($"{(hasGraves ? "、有墓碑" : "")}");
        // TODO: 草地行数不准确
        //a11ySb.Append($"，行数 {numRows}");
        GameMode gameMode = GameplayActivityPatch.GetCachedGameplayActivity()?.GameMode ?? GameMode.NumGameModes;
        // a11ySb.Append($"，游戏模式：{gameMode}");

        // 统计泳池行、草地可种植行、是否存在墓碑与 ScaryPot
        var poolRows = new System.Collections.Generic.List<int>(); // 1-based 行号
        bool hasAnyScaryPot = false;

        int numCols = 9; // PvZ 标准 9 列
        for (int y = 0; y < numRows; y++)
        {
            bool isPoolRow = false;

            for (int x = 0; x < numCols; x++)
            {
                // 泳池格判断（按格）
                if (board.IsPoolSquare(x, y))
                {
                    isPoolRow = true;
                }

                // 网格物品存在性（关卡是否有墓碑、ScaryPot）
                var scaryPot = board.GetGridItemAt(GridItemType.ScaryPot, x, y);
                if (scaryPot != null) hasAnyScaryPot = true;
            }

            if (isPoolRow)
            {
                poolRows.Add(y + 1); // 输出使用 1-based 行号
            }
        }
        
        // 泳池行
        if (poolRows.Count > 0) a11ySb.Append($"；泳池行：{string.Join(", ", poolRows)}");
        // 花瓶
        if (hasAnyScaryPot) a11ySb.Append($"；有惊吓花瓶");

        var ctx = new StringBuilder();
        ctx.Append($"[AnnounceLevelIntro] NumRows={numRows}, NoGrass={noGrass}");
        ctx.Append($", PoolRows={(poolRows.Count > 0 ? string.Join(",", poolRows) : "None")}");
        ctx.Append($", HasScaryPot={hasAnyScaryPot}");
        ctx.Append($", GameMode={gameMode}");

        A11y.SR.Speak(a11ySb.ToString(), ctx.ToString());
    }
}