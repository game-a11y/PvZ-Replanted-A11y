using HarmonyLib;
using Il2CppReloaded;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(ExitGame))]
public class ExitGamePatch
{
    [HarmonyPostfix]
    [HarmonyPatch(nameof(ExitGame.Start))]
    public static void Start_Postfix()
    {
        Core.gLogger.Msg($"ExitGame.Start()");
    }

    [HarmonyPostfix]
    [HarmonyPatch(nameof(ExitGame.Exit))]
    public static void Exit_Postfix(int exitCode)
    {
        Core.gLogger.Msg($"ExitGame.Exit(exitCode={exitCode})");
    }
}