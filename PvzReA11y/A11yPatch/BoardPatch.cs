using HarmonyLib;
using Il2CppReloaded.Gameplay;
using System.Text;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(Il2CppReloaded.Gameplay.Board))]
internal class BoardPatch
{
    static Il2CppReloaded.Gameplay.Board cachedBoard;
    static bool isBoardInitialized = false;

    #region 辅助函数
    /// <summary>
    /// 打印 Board 的静态信息
    /// </summary>
    static void PrintBoardStaticInfo()
    {
        if (!BoardPatch.isBoardInitialized)
        {
            Core.gLogger.Msg("Board is not initialized yet.");
            return;
        }
        if (BoardPatch.cachedBoard is null)
        {
            Core.gLogger.Msg("Board is null.");
            return;
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Board#{BoardPatch.cachedBoard?.GetHashCode()} Static Info:");
        sb.AppendLine($"  NumRows = {BoardPatch.cachedBoard?.GetNumRows()}");
        sb.AppendLine($"  StageHas6Rows = {BoardPatch.cachedBoard?.StageHas6Rows()}");
        sb.AppendLine($"  StageIsNight  = {BoardPatch.cachedBoard?.StageIsNight()}");
        sb.AppendLine($"  StageHasPool  = {BoardPatch.cachedBoard?.StageHasPool()}");
        sb.AppendLine($"  StageHasFog   = {BoardPatch.cachedBoard?.StageHasFog()}");
        sb.AppendLine($"  StageHasRoof  = {BoardPatch.cachedBoard?.StageHasRoof()}");
        sb.AppendLine($"  StageHasNoGrass     = {BoardPatch.cachedBoard?.StageHasNoGrass()}");
        sb.AppendLine($"  StageHasGraveStones = {BoardPatch.cachedBoard?.StageHasGraveStones()}");
        // UI
        sb.AppendLine($"  HasProgressMeter = {BoardPatch.cachedBoard?.HasProgressMeter()}");
        Core.gLogger.Msg(sb.ToString());
    }

    /// <summary>
    /// 打印 Board 的静态信息
    /// </summary>
    static void PrintBoardDynamicInfo()
    {
        if (!BoardPatch.isBoardInitialized)
        {
            Core.gLogger.Msg("Board is not initialized yet.");
            return;
        }
        if (BoardPatch.cachedBoard is null)
        {
            Core.gLogger.Msg("Board is null.");
            return;
        }

        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Board#{BoardPatch.cachedBoard?.GetHashCode()} Dynamic Info:");
        sb.AppendLine($"  LevelRandSeed = {BoardPatch.cachedBoard?.GetLevelRandSeed()}");
        sb.AppendLine($"  LiveGargantuarCount = {BoardPatch.cachedBoard?.GetLiveGargantuarCount()}");
        sb.AppendLine($"  NumSeedsInBank = {BoardPatch.cachedBoard?.GetNumSeedsInBank()}");
        sb.AppendLine($"  CountSunFlowers = {BoardPatch.cachedBoard?.CountSunFlowers()}");
        sb.AppendLine($"  GetGraveStoneCount = {BoardPatch.cachedBoard?.GetGraveStoneCount()}");
        sb.AppendLine($"  CountZombiesOnScreen = {BoardPatch.cachedBoard?.CountZombiesOnScreen()}");
        Core.gLogger.Msg(sb.ToString());
    }
    #endregion 辅助函数

    // TODO: 收集当前关卡包含的僵尸类型（FromWave=-2）
    [HarmonyPatch("InitLevel")]
    [HarmonyPostfix]
    static void InitLevel(Il2CppReloaded.Gameplay.Board __instance)
    {
        if (BoardPatch.cachedBoard != __instance)
        {
            BoardPatch.cachedBoard = __instance;
        }
        BoardPatch.isBoardInitialized = BoardPatch.cachedBoard is not null;

        Core.gLogger.Msg($"Board#{__instance?.GetHashCode()}.InitLevel()");

        PrintBoardStaticInfo();
    }

    [HarmonyPatch("StartLevel")]
    [HarmonyPostfix]
    static void StartLevel()
    {
        string a11yCtx = "Board.StartLevel()";
        string a11yText = $"关卡开始";
        // TODO: 草地行数不正确
        //a11yText += $"：共 {BoardPatch.cachedBoard?.GetNumRows()} 行草地";

        A11y.SR.Speak(a11yText, a11yCtx);

        PrintBoardDynamicInfo();
    }


    #region 植物函数
    /// <summary>
    /// 添加植物
    /// </summary>
    /// <param name="theGridX">网格坐标 X</param>
    /// <param name="theGridY">网格坐标 Y</param>
    /// <param name="theSeedType">种子类型</param>
    /// <param name="theImitaterType">模仿者类型</param>
    /// TODO: Prefix 拦截种植，同一位置种植两次，第一次输出文本，第二次种下植物
    [HarmonyPatch("AddPlant")]
    [HarmonyPostfix]
    static void AddPlant(int theGridX, int theGridY, SeedType theSeedType, SeedType theImitaterType)
    {
        string seedType = A11yText.GetSeedTypeZh(theSeedType);
        string a11yText = $"(行 {theGridY+1}, 列 {theGridX+1}) 种下 {seedType}";
        string a11yCtx = $"Board.AddPlant():" +
            $" X={theGridX}, Y={theGridY}, SeedType={theSeedType}, ImitaterType={theImitaterType}";

        A11y.SR.Speak(a11yText, a11yCtx);
    }

    /// <summary>
    /// 移除所有植物
    /// </summary>
    [HarmonyPatch("RemoveAllPlants")]
    [HarmonyPostfix]
    static void RemoveAllPlants()
    {
        Core.gLogger.Msg($"Board.RemoveAllPlants()");
    }

    /// <summary>
    /// 移除半径内的所有植物
    /// </summary>
    /// <param name="theX">中心坐标 X</param>
    /// <param name="theY">中心坐标 Y</param>
    /// <param name="theRadius">半径</param>
    [HarmonyPatch("KillAllPlantsInRadius", new Type[] { typeof(int), typeof(int), typeof(int) })]
    [HarmonyPostfix]
    static void KillAllPlantsInRadius_Int(int theX, int theY, int theRadius)
    {
        Core.gLogger.Msg($"Board.KillAllPlantsInRadius(int X={theX}, Y={theY}, Radius={theRadius})");
    }

    [HarmonyPatch("KillAllPlantsInRadius", new Type[] { typeof(float), typeof(float), typeof(int) })]
    [HarmonyPostfix]
    static void KillAllPlantsInRadius_Float(float theX, float theY, int theRadius)
    {
        Core.gLogger.Msg($"Board.KillAllPlantsInRadius(float X={theX}, Y={theY}, Radius={theRadius})");
    }

    /// <summary>
    /// 清除植物周围的雾气
    /// </summary>
    /// <param name="thePlant"></param>
    /// <param name="theSize"></param>
    [HarmonyPatch("ClearFogAroundPlant")]
    [HarmonyPostfix]
    static void ClearFogAroundPlant(Plant thePlant, int theSize)
    {
        Core.gLogger.Msg($"Board.ClearFogAroundPlant(Plant=Plant#{thePlant?.GetHashCode()}, Size={theSize})");
    }
    #endregion 植物函数


    // 包含阳光的下落
    [HarmonyPatch("AddCoin")]
    [HarmonyPostfix]
    static void AddCoin(float theX, float theY, CoinType theCoinType, CoinMotion theCoinMotion)
    {
        Core.gLogger.Msg($"Board.AddCoin(X={theX}, Y={theY}, CoinType={theCoinType}, CoinMotion={theCoinMotion})");
    }


    #region 僵尸生成
    [HarmonyPatch("AddZombie")]
    [HarmonyPostfix]
    static void AddZombie(ZombieType theZombieType, int theFromWave, bool shakeBrush)
    {
        //Core.gLogger.Msg($"Board.AddZombie(ZombieType={theZombieType}, FromWave={theFromWave}, shakeBrush={shakeBrush})");
    }

    /// <summary>
    /// 会调用 AddZombie
    /// </summary>
    /// <remarks>
    /// 普通的僵尸按波次放置
    /// </remarks>
    /// <param name="theZombieType">僵尸类型</param>
    /// <param name="theRow">行号</param>
    /// <param name="theFromWave">波次号</param>
    /// <param name="shakeBrush">?</param>
    [HarmonyPatch("AddZombieInRow")]
    [HarmonyPostfix]
    static void AddZombieInRow(ZombieType theZombieType, int theRow, int theFromWave, bool shakeBrush)
    {
        string a11yText = $"第 {theRow+1} 行，{theZombieType} 僵尸";
        string a11yCtx = $"Board.AddZombieInRow():" +
            $" ZombieType={theZombieType}, Row={theRow}, Wave={theFromWave}, shakeBrush={shakeBrush})";

        // 僵尸种类预览
        if (theFromWave < 0)
        {
            Core.gLogger.Msg(a11yCtx);
        }
        else
        {
            A11y.SR.Speak(a11yText, a11yCtx);
        }
    }

    [HarmonyPatch("AddZombieAtCell")]
    [HarmonyPostfix]
    static void AddZombieAtCell(ZombieType theZombieType, int x, int y)
    {
        Core.gLogger.Msg($"Board.AddZombieAtCell(ZombieType={theZombieType}, x={x}, y={y})");
    }
    #endregion 僵尸生成


    [HarmonyPatch("Update")]
    [HarmonyPostfix]
    static void Update()
    {
        //Core.gLogger.Msg($"Board.Update()");
    }

    //// void Pause(bool thePause)
    //[HarmonyPatch("Pause")]
    //[HarmonyPostfix]
    //static void Pause()
    //{
    //    Core.gLogger.Msg($"Board.Pause(thePause)");
    //}

    #region 僵尸波次
    [HarmonyPatch("NextWaveComing")]
    [HarmonyPostfix]
    static void NextWaveComing()
    {
        Core.gLogger.Msg($"Board.NextWaveComing()");
    }

    [HarmonyPatch("SpawnZombieWave")]
    [HarmonyPostfix]
    static void SpawnZombieWave()
    {
        Core.gLogger.Msg($"Board.SpawnZombieWave()");
    }

    [HarmonyPatch("SpawnZombiesFromGraves")]
    [HarmonyPostfix]
    static void SpawnZombiesFromGraves()
    {
        Core.gLogger.Msg($"Board.SpawnZombiesFromGraves()");
    }
    #endregion 僵尸波次

    //[HarmonyPatch("LoadGame")]
    //[HarmonyPostfix]
    //static void LoadGame(ref string theFilePath)
    //{
    //    Core.gLogger.Msg($"Board.LoadGame(theFilePath='{theFilePath}')");
    //}

    //[HarmonyPatch("SaveGame")]
    //[HarmonyPrefix]
    //static void Pre__SaveGame(ref string theFilePath)
    //{
    //    Core.gLogger.Msg($"Board.SaveGame(theFilePath='{theFilePath}')");
    //}
}
