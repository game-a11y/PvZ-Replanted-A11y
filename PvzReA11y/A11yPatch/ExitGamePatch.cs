using HarmonyLib;
using Il2CppReloaded;
using MelonLoader;
using PvzReA11y.A11y;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(ExitGame))]
public class ExitGamePatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(ExitGame.Start))]
    public static void Start_Postfix()
    {
        MelonLogger.Msg($"ExitGame.Start called");
    }
}