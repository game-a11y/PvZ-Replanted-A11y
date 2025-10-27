
namespace PvzReA11y.A11y;

/// <summary>
/// Mock class for Tolk.
/// </summary>
internal class TolkMock
{
    // Prevent construction
    private TolkMock() { }

    public static void Load() { }
    public static bool IsLoaded() { return true; }
    public static void Unload() { }
    public static void TrySAPI(bool trySAPI) {  }
    public static void PreferSAPI(bool preferSAPI) {  }

    public static String DetectScreenReader() { return "Mock"; }
    public static bool HasSpeech() { return true; }
    public static bool HasBraille() { return false; }
    public static bool Output(String str, bool interrupt = false) { return false; }
    public static bool Speak(String str, bool interrupt = false) { return false; }
    public static bool Braille(String str) { return false; }
    public static bool IsSpeaking() { return false; }
    public static bool Silence() { return false; }
}
