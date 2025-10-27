using MelonLoader;
using System.Diagnostics;
using Tolk = PvzReA11y.A11y.TolkMock;

namespace PvzReA11y.A11y;

/// <summary>
/// Tolk 屏幕阅读器管理器
/// 负责初始化和管理 Tolk 库，提供语音输出功能
/// </summary>
public static class SR
{
    #region 私有字段

    /// <summary>
    /// Tolk 是否已初始化
    /// </summary>
    private static bool _isInitialized = false;

    /// <summary>
    /// 当前检测到的屏幕阅读器名称
    /// </summary>
    private static string _detectedScreenReader = null;

    /// <summary>
    /// 是否启用语音输出
    /// </summary>
    private static bool _speechEnabled = true;

    /// <summary>
    /// 是否启用详细日志
    /// </summary>
    private static bool _verboseLogging = false;

    #endregion

    #region 公共属性

    /// <summary>
    /// 获取 Tolk 是否已成功初始化
    /// </summary>
    public static bool IsInitialized => _isInitialized;

    /// <summary>
    /// 获取检测到的屏幕阅读器名称
    /// </summary>
    public static string DetectedScreenReader => _detectedScreenReader;

    /// <summary>
    /// 获取或设置是否启用语音输出
    /// </summary>
    public static bool SpeechEnabled
    {
        get => _speechEnabled;
        set
        {
            _speechEnabled = value;
        }
    }

    /// <summary>
    /// 获取或设置是否启用详细日志
    /// </summary>
    public static bool VerboseLogging
    {
        get => _verboseLogging;
        set => _verboseLogging = value;
    }

    #endregion

    #region 初始化方法
    /// <summary>
    /// 初始化 Tolk 库
    /// </summary>
    /// <returns>初始化是否成功</returns>
    public static bool Initialize()
    {
        if (_isInitialized)
        {
            MelonLogger.Msg("Tolk: Already initialized, skipping...");
            return true;
        }

        try
        {
            MelonLogger.Msg("Tolk: Initializing...");

            // 加载 Tolk 库
            TolkMock.Load();

            // 尝试启用 SAPI
            TolkMock.TrySAPI(true);

            // 检测屏幕阅读器
            _detectedScreenReader = TolkMock.DetectScreenReader();

            if (!string.IsNullOrEmpty(_detectedScreenReader))
            {
                MelonLogger.Msg($"Tolk: Successfully detected screen reader: {_detectedScreenReader}");
                _isInitialized = true;
            }
            else
            {
                MelonLogger.Warning("Tolk: No supported screen reader detected, but Tolk is still available");
                _isInitialized = true; // 即使没有检测到屏幕阅读器，Tolk 仍然可用
            }

            MelonLogger.Msg("Tolk: Initialization completed successfully");
            return true;
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"Tolk: Initialization failed:", ex);
            _isInitialized = false;
            return false;
        }
    }

    /// <summary>
    /// 卸载 Tolk 库
    /// </summary>
    public static void Shutdown()
    {
        if (!_isInitialized)
        {
            return;
        }

        try
        {
            MelonLogger.Msg("Tolk: Shutting down...");
            TolkMock.Unload();
            _isInitialized = false;
            _detectedScreenReader = null;
            MelonLogger.Msg("Tolk: Shutdown completed");
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"Tolk: Shutdown failed:", ex);
        }
    }

    #endregion

    #region 语音输出方法

    /// <summary>
    /// 语音输出文本
    /// </summary>
    /// <param name="text">待输出的文本</param>
    /// <param name="context">调试用上下文信息</param>
    /// <param name="interrupt">是否打断当前语音</param>
    public static void Speak(string text, string context = "", bool interrupt = false)
    {
        if (!_speechEnabled)
        {
            if (_verboseLogging)
            {
                MelonLogger.Warning($"Tolk: Speech disabled, skipping: {text}");
            }
            return;
        }

        if (!_isInitialized)
        {
            MelonLogger.Warning("Tolk: Not initialized, attempting to initialize...");
            if (!Initialize())
            {
                MelonLogger.Error("Tolk: Failed to initialize, cannot speak");
                return;
            }
        }

        // 获取调用上下文
        if (string.IsNullOrEmpty(context))
        {
            try
            {
                var stackTrace = new StackTrace();
                var method = stackTrace.GetFrame(1)?.GetMethod();
                var declaringType = method?.DeclaringType;
                //context = $"{declaringType?.Name}.{method?.Name}()";
                context = $"Caller: {declaringType}()";
            }
            catch
            {
                context = "Caller: Unknown";
            }
        }

        // 处理空文本
        if (string.IsNullOrEmpty(text)) text = string.Empty;

        // 记录日志
        if (_verboseLogging)
        {
            //                                               "INTERRUPT"
            string interruptText = interrupt ? "INTERRUPT" : "  QUEUE++";
            MelonLogger.Warning($"Tolk.Speak({interruptText}): \"{text}\"");
            MelonLogger.Msg($"\tContext: {context}");
        }

        try
        {
            // 调用 Tolk 进行语音输出
            TolkMock.Speak(text, interrupt);
        }
        catch (Exception ex)
        {
            MelonLogger.Error($"Tolk.Speak failed", ex);
        }
    }

    /// <summary>
    /// 语音输出文本（总是打断当前语音）
    /// </summary>
    /// <param name="text">待输出的文本</param>
    /// <param name="context">调试用上下文信息</param>
    public static void SpeakInterrupt(string text, string context = "")
    {
        Speak(text, context, interrupt: true);
    }

    /// <summary>
    /// 语音输出文本（排队输出）
    /// </summary>
    /// <param name="text">待输出的文本</param>
    /// <param name="context">调试用上下文信息</param>
    public static void SpeakQueue(string text, string context = "")
    {
        Speak(text, context, interrupt: false);
    }

    /// <summary>
    /// 停止当前语音输出
    /// </summary>
    public static void StopSpeech(string context = "")
    {
        SpeakInterrupt(text: "", context); // 发送空字符串并打断来停止语音
        MelonLogger.Msg("StopSpeech()");
    }

    #endregion

    #region 诊断和调试方法

    /// <summary>
    /// 获取 Tolk 状态信息
    /// </summary>
    /// <returns>状态信息字符串</returns>
    public static string GetStatusInfo()
    {
        var info = new System.Text.StringBuilder();
        info.AppendLine($"Tolk Status:");
        info.AppendLine($"  Initialized: {_isInitialized}");
        info.AppendLine($"  Screen Reader: {_detectedScreenReader ?? "None"}");
        info.AppendLine($"  Speech Enabled: {_speechEnabled}");
        info.AppendLine($"  Verbose Logging: {_verboseLogging}");

        if (_isInitialized)
        {
            try
            {
                bool hasScreenReader = !string.IsNullOrEmpty(TolkMock.DetectScreenReader());
                info.AppendLine($"  Has Active Screen Reader: {hasScreenReader}");
            }
            catch (Exception ex)
            {
                info.AppendLine($"  Screen Reader Check Failed: {ex.Message}");
            }
        }

        return info.ToString();
    }

    /// <summary>
    /// 测试语音输出
    /// </summary>
    public static void TestSpeech()
    {
        MelonLogger.Msg("Tolk: Testing speech output...");
        Speak("Tolk speech test successful", "TestSpeech", true);
    }
    #endregion
}