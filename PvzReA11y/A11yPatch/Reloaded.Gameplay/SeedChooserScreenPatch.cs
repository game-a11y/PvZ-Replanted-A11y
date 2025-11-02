using HarmonyLib;
using Il2CppReloaded.Gameplay;
using PvzReA11y.ReplantAPI;
using System.Text;
using static Il2CppReloaded.Gameplay.SeedChooserScreen;
using static MelonLoader.MelonLogger;
using static UnityEngine.ParticleSystem.PlaybackState;

namespace PvzReA11y.A11yPatch.Reloaded.Gameplay
{
    /// <summary>
    /// SeedChooserScreen类的Harmony补丁，用于提供种子选择界面的无障碍支持
    /// </summary>
    [HarmonyPatch(typeof(SeedChooserScreen))]
    public class SeedChooserScreenPatch
    {
        /// <summary>
        /// Hook SeedChooserScreen.InitSurvivalRepick方法
        /// </summary>
        [HarmonyPatch(nameof(SeedChooserScreen.InitSurvivalRepick))]
        [HarmonyPostfix]
        public static void InitSurvivalRepick_Postfix()
        {
            Core.gLogger.Msg("SeedChooserScreen.InitSurvivalRepick()");
            BoardHelper.ResetLevelZombieTypes();
        }

        /// <summary>
        /// Hook SeedChooserScreen.SetFromSeedbank方法
        /// </summary>
        [HarmonyPatch(nameof(SeedChooserScreen.SetFromSeedbank))]
        [HarmonyPostfix]
        public static void SetFromSeedbank_Postfix()
        {
            Core.gLogger.Msg("SeedChooserScreen.SetFromSeedbank()");
        }

        /// <summary>
        /// Hook SeedChooserScreen.AddChosenSeedToBank方法
        /// </summary>
        /// <param name="chosenSeed">选择的种子</param>
        /// <param name="playerIndex">玩家索引</param>
        [HarmonyPatch(nameof(SeedChooserScreen.AddChosenSeedToBank))]
        [HarmonyPostfix]
        public static void AddChosenSeedToBank_Postfix(ChosenSeed chosenSeed, int playerIndex)
        {
            Core.gLogger.Msg($"SeedChooserScreen.AddChosenSeedToBank(Seed={chosenSeed}, player={playerIndex})");
        }

        /// <summary>
        /// Hook SeedChooserScreen.AddChosenSeedsToBank方法
        /// </summary>
        /// <param name="__instance">SeedChooserScreen实例</param>
        /// <param name="chosenSeeds">选择的种子列表</param>
        /// <param name="playerIndex">玩家索引</param>
        [HarmonyPatch(nameof(SeedChooserScreen.AddChosenSeedsToBank))]
        [HarmonyPostfix]
        public static void AddChosenSeedsToBank_Postfix(Il2CppSystem.Collections.Generic.List<ChosenSeed> chosenSeeds, int playerIndex)
        {
            Core.gLogger.Msg($"SeedChooserScreen.AddChosenSeedsToBank(Seeds={chosenSeeds}, player={playerIndex})");
        }

        /// <summary>
        /// Hook SeedChooserScreen.CrazyDavePickSeeds方法
        /// </summary>
        /// <param name="__instance">SeedChooserScreen实例</param>
        [HarmonyPatch("CrazyDavePickSeeds")]
        [HarmonyPostfix]
        public static void CrazyDavePickSeeds_Postfix()
        {
            Core.gLogger.Msg("SeedChooserScreen.CrazyDavePickSeeds()");
        }

        /// <summary>
        /// Hook SeedChooserScreen.ClickedSeedInBank方法（ChosenSeed版本）
        /// 在点击银行中的种子后触发
        /// </summary>
        /// <param name="seed">被点击的种子</param>
        /// <param name="playerIndex">玩家索引</param>
        [HarmonyPatch(nameof(SeedChooserScreen.ClickedSeedInBank), typeof(ChosenSeed), typeof(int))]
        [HarmonyPostfix]
        public static void ClickedSeedInBank_ChosenSeed_Postfix(ChosenSeed seed, int playerIndex)
        {
            if (seed == null)
            {
                Core.gLogger.Msg($"SeedChooserScreen.ClickedSeedInBank(ChosenSeed) - Player {playerIndex}, seed is null");
                return;
            }

            string seedType = seed.mSeedType.ToString();
            string seedState = seed.mSeedState.ToString();
            int seedIndexInBank = seed.mSeedIndexInBank;
            bool isImitater = seed.mIsImitater;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"SeedChooserScreen.ClickedSeedInBank(ChosenSeed#{seed?.GetHashCode()}, player={playerIndex})");
            sb.AppendLine($"  - SeedType: {seedType}");
            sb.AppendLine($"  - SeedState: {seedState}");
            sb.AppendLine($"  - SeedIndexInBank: {seedIndexInBank}");
            sb.AppendLine($"  - IsImitater: {isImitater}");

            if (isImitater)
            {
                string imitaterType = seed.mImitaterType.ToString();
                sb.AppendLine($"  - ImitaterType: {imitaterType}");
            }

            string a11yText = $"移除植物 {A11yText.GetSeedTypeZh(seed.mSeedType)}";
            //if (isImitater) a11yText += "，模仿者";

            A11y.SR.Speak(a11yText, sb.ToString());
        }

        /// <summary>
        /// Hook SeedChooserScreen.ClickedSeedInBank方法（SeedPacket版本）
        /// 在点击银行中的种子包后触发
        /// </summary>
        /// <param name="thePacket">被点击的种子包</param>
        /// <param name="playerIndex">玩家索引</param>
        [HarmonyPatch(nameof(SeedChooserScreen.ClickedSeedInBank), typeof(SeedPacket), typeof(int))]
        [HarmonyPostfix]
        public static void ClickedSeedInBank_SeedPacket_Postfix(SeedPacket thePacket, int playerIndex)
        {
            if (thePacket == null)
            {
                Core.gLogger.Msg($"SeedChooserScreen.ClickedSeedInBank(SeedPacket) - Player {playerIndex}, packet is null");
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine($"SeedChooserScreen.ClickedSeedInBank(SeedPacket) - Player {playerIndex}");
            
            string packetType = thePacket.mPacketType.ToString();
            int index = thePacket.mIndex;
            bool active = thePacket.mActive;
            bool refreshing = thePacket.mRefreshing;
            bool flying = thePacket.mFlying;

            sb.AppendLine($"  - PacketType: {packetType}");
            sb.AppendLine($"  - Index: {index}");
            sb.AppendLine($"  - Active: {active}");
            sb.AppendLine($"  - Refreshing: {refreshing}");
            sb.AppendLine($"  - Flying: {flying}");

            if (thePacket.mImitaterType != SeedType.None)
            {
                string imitaterType = A11yText.GetSeedTypeZh(thePacket.mImitaterType);
                sb.AppendLine($"  - ImitaterType: {imitaterType}");
            }

            Core.gLogger.Msg(sb.ToString());
        }

        /// <summary>
        /// Hook SeedChooserScreen.ClickedSeedInChooser方法
        /// 在点击选择器中的种子后触发
        /// </summary>
        /// <param name="theChosenSeed">被点击的选择种子</param>
        /// <param name="playerIndex">玩家索引</param>
        [HarmonyPatch(nameof(SeedChooserScreen.ClickedSeedInChooser))]
        [HarmonyPostfix]
        public static void ClickedSeedInChooser_Postfix(ChosenSeed theChosenSeed, int playerIndex)
        {
            if (theChosenSeed == null)
            {
                Core.gLogger.Msg($"SeedChooserScreen.ClickedSeedInChooser() - Player {playerIndex}, chosen seed is null");
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine($"SeedChooserScreen.ClickedSeedInChooser() - Player {playerIndex}");

            string seedType = theChosenSeed.mSeedType.ToString();
            string seedState = theChosenSeed.mSeedState.ToString();
            bool isAddSeed = theChosenSeed.mSeedState == ChosenSeedState.SeedFlyingToBank;
            bool isImitater = theChosenSeed.mIsImitater;
            bool isFlashing = theChosenSeed.mFlashing;
            bool isFlying = theChosenSeed.mFlying;
            bool notSuggested = theChosenSeed.mNotSuggested;

            sb.AppendLine($"  - SeedType: {seedType}");
            sb.AppendLine($"  - SeedState: {seedState}");
            sb.AppendLine($"  - IsImitater: {isImitater}");
            sb.AppendLine($"  - IsFlashing: {isFlashing}");
            sb.AppendLine($"  - IsFlying: {isFlying}");
            sb.AppendLine($"  - NotSuggested: {notSuggested}");
            sb.AppendLine($"  - Position: ({theChosenSeed.mX}, {theChosenSeed.mY})");

            if (isImitater)
            {
                string imitaterType = A11yText.GetSeedTypeZh(theChosenSeed.mImitaterType);
                sb.AppendLine($"  - ImitaterType: {imitaterType}");
            }

            // TODO: 处理手牌满的情况
            seedType = A11yText.GetSeedTypeZh(theChosenSeed.mSeedType);
            string a11yText = $"植物选择已满，点击植物 {seedType}";
            if (isAddSeed)
            {
                a11yText = $"添加植物 {seedType}";
                //if (isImitater) a11yText += "，模仿者";
                if (notSuggested) a11yText += "（不推荐）";
            }
            else if (theChosenSeed.mSeedState == ChosenSeedState.SeedFlyingToChooser)
            {
                a11yText = $"取消添加植物 {seedType}";
            }

            A11y.SR.Speak(a11yText, sb.ToString());
        }

        /// <summary>
        /// Hook SeedChooserScreen.EnableStartButton方法
        /// 在启用/禁用开始按钮后触发
        /// </summary>
        /// <param name="theEnabled">是否启用按钮</param>
        /// <param name="info">种子银行信息</param>
        [HarmonyPatch(nameof(SeedChooserScreen.EnableStartButton))]
        [HarmonyPostfix]
        public static void EnableStartButton_Postfix(bool theEnabled, SeedBankInfo info)
        {
            if (info == null)
            {
                Core.gLogger.Msg($"SeedChooserScreen.EnableStartButton(enabled={theEnabled}) - SeedBankInfo is null");
                return;
            }
            if (!theEnabled) return;

            var sb = new StringBuilder();
            int playerIndex = info.PlayerIndex;
            bool isReady = info.mIsReady;
            bool dirtyBank = info.DirtyBank;

            sb.AppendLine($"SeedChooserScreen.EnableStartButton() - Player {playerIndex}");
            sb.AppendLine($"  - Enabled: {theEnabled}");
            sb.AppendLine($"  - IsReady: {isReady}");
            sb.AppendLine($"  - DirtyBank: {dirtyBank}");

            // 如果有种子银行，显示种子数量信息
            if (info.mSeedBank != null)
            {
                int numPackets = info.mSeedBank.mNumPackets;
                sb.AppendLine($"  - NumPackets in Bank: {numPackets}");
            }

            Core.gLogger.Msg(sb.ToString());
        }
    }
}