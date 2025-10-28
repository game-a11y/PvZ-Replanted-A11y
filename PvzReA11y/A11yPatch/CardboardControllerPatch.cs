using HarmonyLib;
using Il2CppSource.Controllers;
using UnityEngine;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(CardboardController))]
public class CardboardControllerPatch
{
    [HarmonyPostfix]
    [HarmonyPatch("ShowCardboard")]
    public static void ShowCardboard_Postfix()
    {
        Core.gLogger.Msg("CardboardController.ShowCardboard()");
    }
}