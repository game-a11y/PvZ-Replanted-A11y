using MelonLoader.Utils;
using System.Runtime.InteropServices;

namespace PvzReA11y.A11y;

/// <summary>
/// Tolk 屏幕阅读器库的 C# 包装器
/// 提供对 Tolk.dll 的安全访问，包含 DLL 不存在时的回退机制
/// </summary>
public static class TolkSafe
{
    #region 私有字段

    /// <summary>
    /// Tolk.dll 是否可用
    /// </summary>
    private static bool? _isDllAvailable = null;

    /// <summary>
    /// 是否已检查过 DLL 可用性
    /// </summary>
    private static bool _hasCheckedDll = false;

    /// <summary>
    /// DLL 检查时的错误信息
    /// </summary>
    private static string _dllCheckError = null;

    #endregion

    #region DLL 导入声明

    [DllImport("Tolk.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Tolk_Load();

    [DllImport("Tolk.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    private static extern bool Tolk_IsLoaded();

    [DllImport("Tolk.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Tolk_Unload();

    [DllImport("Tolk.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Tolk_TrySAPI([MarshalAs(UnmanagedType.I1)] bool trySAPI);

    [DllImport("Tolk.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern void Tolk_PreferSAPI([MarshalAs(UnmanagedType.I1)] bool preferSAPI);

    [DllImport("Tolk.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr Tolk_DetectScreenReader();

    [DllImport("Tolk.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    private static extern bool Tolk_HasSpeech();

    [DllImport("Tolk.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    private static extern bool Tolk_HasBraille();

    [DllImport("Tolk.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    private static extern bool Tolk_Output(
        [MarshalAs(UnmanagedType.LPWStr)] string str,
        [MarshalAs(UnmanagedType.I1)] bool interrupt);

    [DllImport("Tolk.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    private static extern bool Tolk_Speak(
        [MarshalAs(UnmanagedType.LPWStr)] string str,
        [MarshalAs(UnmanagedType.I1)] bool interrupt);

    [DllImport("Tolk.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    private static extern bool Tolk_Braille([MarshalAs(UnmanagedType.LPWStr)] string str);

    [DllImport("Tolk.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    private static extern bool Tolk_IsSpeaking();

    [DllImport("Tolk.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    private static extern bool Tolk_Silence();

    #endregion

    #region 公共属性

    /// <summary>
    /// 获取 Tolk.dll 是否可用
    /// </summary>
    public static bool IsDllAvailable
    {
        get
        {
            if (!_hasCheckedDll)
            {
                CheckDllAvailability();
            }
            return _isDllAvailable ?? false;
        }
    }

    /// <summary>
    /// 获取 DLL 检查时的错误信息
    /// </summary>
    public static string DllCheckError => _dllCheckError;

    #endregion

    #region DLL 可用性检查

    /// <summary>
    /// 检查 Tolk.dll 是否可用
    /// </summary>
    private static void CheckDllAvailability()
    {
        if (_hasCheckedDll)
            return;

        _hasCheckedDll = true;

        try
        {
            // 首先检查文件是否存在
            string dllPath = Path.Combine(MelonEnvironment.UserLibsDirectory, "Tolk.dll");
            if (File.Exists(dllPath))
            {
                _isDllAvailable = true;
                return;
            }

            // 尝试调用一个简单的函数来验证 DLL 是否可以正常加载
            Tolk_Load();
            _isDllAvailable = true;
            _dllCheckError = null;
        }
        catch (DllNotFoundException ex)
        {
            _isDllAvailable = false;
            _dllCheckError = $"Tolk.dll not found: {ex.Message}";
        }
        catch (BadImageFormatException ex)
        {
            _isDllAvailable = false;
            _dllCheckError = $"Tolk.dll format error (architecture mismatch?): {ex.Message}";
        }
        catch (Exception ex)
        {
            _isDllAvailable = false;
            _dllCheckError = $"Tolk.dll load error: {ex.Message}";
        }
    }

    /// <summary>
    /// 安全执行 Tolk 函数调用
    /// </summary>
    /// <typeparam name="T">返回值类型</typeparam>
    /// <param name="func">要执行的函数</param>
    /// <param name="fallbackValue">DLL 不可用时的回退值</param>
    /// <param name="functionName">函数名称（用于日志）</param>
    /// <returns>函数执行结果或回退值</returns>
    private static T SafeCall<T>(Func<T> func, T fallbackValue, string functionName)
    {
        if (!IsDllAvailable)
        {
            return fallbackValue;
        }

        try
        {
            return func();
        }
        catch (Exception ex)
        {
            // 可以在这里记录日志
            // Core.gLogger?.Warning($"Tolk.{functionName} failed: {ex.Message}");
            return fallbackValue;
        }
    }

    /// <summary>
    /// 安全执行无返回值的 Tolk 函数调用
    /// </summary>
    /// <param name="action">要执行的动作</param>
    /// <param name="functionName">函数名称（用于日志）</param>
    private static void SafeCall(Action action, string functionName)
    {
        if (!IsDllAvailable)
        {
            return;
        }

        try
        {
            action();
        }
        catch (Exception ex)
        {
             Core.gLogger?.Warning($"Tolk.{functionName} failed: {ex.Message}");
        }
    }

    #endregion

    #region 公共 API

    /// <summary>
    /// 加载 Tolk 库
    /// </summary>
    public static void Load()
    {
        SafeCall(() => Tolk_Load(), nameof(Load));
    }

    /// <summary>
    /// 检查 Tolk 是否已加载
    /// </summary>
    /// <returns>如果已加载返回 true，否则返回 false</returns>
    public static bool IsLoaded()
    {
        return SafeCall(() => Tolk_IsLoaded(), false, nameof(IsLoaded));
    }

    /// <summary>
    /// 卸载 Tolk 库
    /// </summary>
    public static void Unload()
    {
        SafeCall(() => Tolk_Unload(), nameof(Unload));
    }

    /// <summary>
    /// 设置是否尝试使用 SAPI
    /// </summary>
    /// <param name="trySAPI">是否尝试使用 SAPI</param>
    public static void TrySAPI(bool trySAPI)
    {
        SafeCall(() => Tolk_TrySAPI(trySAPI), nameof(TrySAPI));
    }

    /// <summary>
    /// 设置是否优先使用 SAPI
    /// </summary>
    /// <param name="preferSAPI">是否优先使用 SAPI</param>
    public static void PreferSAPI(bool preferSAPI)
    {
        SafeCall(() => Tolk_PreferSAPI(preferSAPI), nameof(PreferSAPI));
    }

    /// <summary>
    /// 检测当前运行的屏幕阅读器
    /// </summary>
    /// <returns>屏幕阅读器名称，如果没有检测到则返回空字符串</returns>
    public static string DetectScreenReader()
    {
        return SafeCall(() =>
        {
            IntPtr ptr = Tolk_DetectScreenReader();
            return Marshal.PtrToStringUni(ptr) ?? string.Empty;
        }, string.Empty, nameof(DetectScreenReader));
    }

    /// <summary>
    /// 检查是否支持语音输出
    /// </summary>
    /// <returns>如果支持语音输出返回 true，否则返回 false</returns>
    public static bool HasSpeech()
    {
        return SafeCall(() => Tolk_HasSpeech(), false, nameof(HasSpeech));
    }

    /// <summary>
    /// 检查是否支持盲文输出
    /// </summary>
    /// <returns>如果支持盲文输出返回 true，否则返回 false</returns>
    public static bool HasBraille()
    {
        return SafeCall(() => Tolk_HasBraille(), false, nameof(HasBraille));
    }

    /// <summary>
    /// 输出文本到屏幕阅读器（语音和盲文）
    /// </summary>
    /// <param name="str">要输出的文本</param>
    /// <param name="interrupt">是否打断当前输出</param>
    /// <returns>如果输出成功返回 true，否则返回 false</returns>
    public static bool Output(string str, bool interrupt = false)
    {
        return SafeCall(() => Tolk_Output(str ?? string.Empty, interrupt), false, nameof(Output));
    }

    /// <summary>
    /// 语音输出文本
    /// </summary>
    /// <param name="str">要输出的文本</param>
    /// <param name="interrupt">是否打断当前语音</param>
    /// <returns>如果输出成功返回 true，否则返回 false</returns>
    public static bool Speak(string str, bool interrupt = false)
    {
        return SafeCall(() => Tolk_Speak(str ?? string.Empty, interrupt), false, nameof(Speak));
    }

    /// <summary>
    /// 盲文输出文本
    /// </summary>
    /// <param name="str">要输出的文本</param>
    /// <returns>如果输出成功返回 true，否则返回 false</returns>
    public static bool Braille(string str)
    {
        return SafeCall(() => Tolk_Braille(str ?? string.Empty), false, nameof(Braille));
    }

    /// <summary>
    /// 检查是否正在语音输出
    /// </summary>
    /// <returns>如果正在语音输出返回 true，否则返回 false</returns>
    public static bool IsSpeaking()
    {
        return SafeCall(() => Tolk_IsSpeaking(), false, nameof(IsSpeaking));
    }

    /// <summary>
    /// 停止当前语音输出
    /// </summary>
    /// <returns>如果停止成功返回 true，否则返回 false</returns>
    public static bool Silence()
    {
        return SafeCall(() => Tolk_Silence(), false, nameof(Silence));
    }

    #endregion

}
