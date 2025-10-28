using HarmonyLib;
using Il2CppReloaded.Gameplay;
using Il2CppSource.Controllers;
using UnityEngine;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(BackgroundController))]
public class BackgroundControllerPatch
{
    [HarmonyPostfix]
    [HarmonyPatch("Init")]
    public static void Init_Postfix(Board board)
    {
        Core.gLogger.Msg($"BackgroundController.Init(Board#{board?.GetHashCode()})");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Awake")]
    public static void Awake_Postfix()
    {
        Core.gLogger.Msg("BackgroundController.Awake called");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Enable1SodRow")]
    public static void Enable1SodRow_Postfix(bool value)
    {
        Core.gLogger.Msg($"BackgroundController.Enable1SodRow({value})");
    }

    [HarmonyPostfix]
    [HarmonyPatch("Enable3SodRow")]
    public static void Enable3SodRow_Postfix(bool value)
    {
        Core.gLogger.Msg($"BackgroundController.Enable3SodRow({value})");
    }

    [HarmonyPostfix]
    [HarmonyPatch("EnableNightGrave")]
    public static void EnableNightGrave_Postfix(bool enable)
    {
        Core.gLogger.Msg($"BackgroundController.EnableNightGrave({enable})");
    }

    [HarmonyPostfix]
    [HarmonyPatch("EnableBowlingLine")]
    public static void EnableBowlingLine_Postfix(bool enable, float x)
    {
        Core.gLogger.Msg($"BackgroundController.EnableBowlingLine({enable}, x={x})");
    }
}