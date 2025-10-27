using MelonLoader;

[assembly: MelonInfo(typeof(PvzReA11y.Core),
    "A11y Mod", "1.0.0", "inkydragon",
    "https://github.com/game-a11y/PvZ-Replanted-A11y")]
[assembly: MelonGame("PopCap Games", "PvZ Replanted")]

namespace PvzReA11y;

/**
 * PvzReA11y 主类。用于初始化和管理 PvzReA11y 模组
 *
 * Melon Callbacks
 *      https://melonwiki.xyz/#/modders/quickstart?id=melon-callbacks
 */
public class Core : MelonMod
{
    public static MelonLogger.Instance gLogger;
    public static HarmonyLib.Harmony gHarmony;

    // OnEarlyInitializeMelon

    /// <summary>
    /// Called after the Melon was registered.
    /// </summary>
    public override void OnInitializeMelon()
    {
        gLogger = LoggerInstance;
        gHarmony = HarmonyInstance;

        A11y.SR.Initialize();
        // 打印所有输出
        A11y.SR.VerboseLogging = true;
        A11y.SR.TestSpeech();

        // NOTE: 此处无需手动注册，MelonLoader 会自动应用带有 [HarmonyPatch] 特性的补丁
        //gHarmony.PatchAll();
        LoggerInstance.Msg("Mod Initialized");
    }

    /// <summary>
    /// Called after OnInitializeMelon.
    /// </summary>
    /// <remarks>
    /// This callback waits until Unity has invoked the first 'Start' messages.
    /// </remarks>
    public override void OnLateInitializeMelon()
    {
        LoggerInstance.Msg("OnLateInitializeMelon: After Mod init");
    }

    /// <summary>
    /// Called before the Melon is unregistered.
    /// Also called before the game is closed.
    /// </summary>
    public override void OnDeinitializeMelon()
    {
        LoggerInstance.Msg("Deinitialized / Game closed.");
    }


    /// <summary>
    /// Called when a new Scene is loaded.
    /// 当场景已加载时运行，并传递场景的构建索引和名称
    /// </summary>
    /// <param name="buildindex">场景的构建索引</param>
    /// <param name="sceneName">场景名称</param>
    public override void OnSceneWasLoaded(int buildindex, string sceneName)
    {
        LoggerInstance.Msg($"OnSceneWasLoaded: [{buildindex}] '{sceneName}'");
    }

    /// <summary>
    /// Called once the active Scene is fully initialized.
    /// 当场景已初始化时运行，并传递场景的构建索引和名称
    /// </summary>
    /// <param name="buildindex">场景的构建索引</param>
    /// <param name="sceneName">场景名称</param>
    public override void OnSceneWasInitialized(int buildindex, string sceneName)
    {
        LoggerInstance.Msg($"OnSceneWasInitied: [{buildindex}] '{sceneName}'");
    }

    /// <summary>
    /// Called once a Scene unloads.
    /// 当场景被卸载时运行
    /// </summary>
    /// <param name="buildIndex">场景的构建索引</param>
    /// <param name="sceneName">场景名称</param>
    public override void OnSceneWasUnloaded(int buildIndex, string sceneName) {
        LoggerInstance.Msg($"OnSceneWasUnloaded: [{buildIndex}] '{sceneName}'");
    }


    /// <summary>
    /// Called once per frame.
    /// 每帧运行一次
    /// </summary>
    public override void OnUpdate()
    {
        // LoggerInstance.Msg("OnUpdate");
    }

    /// <summary>
    /// Called every 0.02 seconds or Time.fixedDeltaTime.
    /// 每帧可能运行多次，主要用于物理计算
    /// </summary>
    /// <remarks>
    /// It is recommended to do all important Physics loops inside this Callback.
    /// </remarks>
    public override void OnFixedUpdate()
    {
        // LoggerInstance.Msg("OnFixedUpdate");
    }

    /// <summary>
    /// Called once per frame after all OnUpdate callbacks have finished.
    /// 在 OnUpdate 和 OnFixedUpdate 完成后每帧运行一次
    /// </summary>
    public override void OnLateUpdate()
    {
        // LoggerInstance.Msg("OnLateUpdate");
    }


    /// <summary>
    /// Called at every IMGUI event. Only use this for drawing IMGUI Elements.
    /// 每帧可能运行多次，主要用于 Unity 的 IMGUI
    /// </summary>
    public override void OnGUI()
    {
        // LoggerInstance.Msg("OnGUI");
    }


    /// <summary>
    /// Called when the game is told to close.
    /// 当游戏被告知关闭时运行
    /// </summary>
    public override void OnApplicationQuit()
    {
        LoggerInstance.Msg("OnApplicationQuit");
    }

    /// <summary>
    /// Called when Melon Preferences get saved.
    /// 当 Melon 首选项被保存时运行
    /// </summary>
    public override void OnPreferencesSaved()
    {
        LoggerInstance.Msg("OnPreferencesSaved");
    }

    /// <summary>
    /// Called when Melon Preferences get loaded.
    /// 当 Melon 首选项被加载时运行
    /// </summary>
    public override void OnPreferencesLoaded()
    {
        LoggerInstance.Msg("OnPreferencesLoaded");
    }
}