using HarmonyLib;
using UnityEngine;

namespace PvzReA11y.A11yPatch;

internal class TextMeshPatch
{
    public static unsafe void Init()
    {
        Core.gHarmony.Patch(typeof(TextMesh).GetMethod("set_text"), new HarmonyMethod(Utils.GetMethod(nameof(Patch))));
    }

    internal static void Patch(ref string value)
    {
        Core.gLogger.Msg($"TextMesh.set_text({value})");
    }
}
