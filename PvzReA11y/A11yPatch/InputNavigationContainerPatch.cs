using HarmonyLib;
using UnityEngine.EventSystems;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(Il2CppReloaded.Input.InputNavigationContainer))]
internal class InputNavigationContainerPatch
{
    [HarmonyPatch("Awake")]
    [HarmonyPrefix]
    static void Awake1()
    {
        Core.gLogger.Msg($"InputNavigationContainer.Awake()");
    }

    [HarmonyPatch("OnSelect")]
    [HarmonyPrefix]
    static void OnSelect1(ref BaseEventData eventData)
    {
        Core.gLogger.Msg($"InputNavigationContainer.OnSelect(BaseEventData eventData)");
    }
}
