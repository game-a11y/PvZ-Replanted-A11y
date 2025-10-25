using HarmonyLib;
using UnityEngine.UI;

namespace PvzReA11y.A11yPatch;

internal class TextPatch
{
    public static unsafe void Init()
    {
        Core.gHarmony.Patch(typeof(Text).GetMethod("set_text"), new HarmonyMethod(Utils.GetMethod(nameof(Patch))));
    }

    internal static void Patch(ref string value)
    {
        Core.gLogger.Msg($"Text.set_text({value})");
    }
}
