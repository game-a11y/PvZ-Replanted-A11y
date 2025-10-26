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
        Core.gLogger.Msg($"Board.StartLevel()");

        PrintBoardDynamicInfo();
    }

    [HarmonyPatch("AddPlant")]
    [HarmonyPostfix]
    static void AddPlant(int theGridX, int theGridY, SeedType theSeedType, SeedType theImitaterType)
    {
        Core.gLogger.Msg($"Board.AddPlant(X={theGridX}, Y={theGridY}, SeedType={theSeedType}, ImitaterType={theImitaterType})");
    }

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
        Core.gLogger.Msg($"Board.AddZombieInRow(ZombieType={theZombieType}, Row={theRow}, FromWave={theFromWave}, shakeBrush={shakeBrush})");
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
