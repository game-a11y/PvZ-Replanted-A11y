using HarmonyLib;
using Il2CppReloaded.Binders;
using UnityEngine.EventSystems;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(SelectableBinder))]
internal class SelectableBinderPatch
{
    [HarmonyPatch("OnSelect")]
    [HarmonyPostfix]
    static void OnSelect(BaseEventData eventData)
    {
        // Core.gLogger.Msg($"Board.OnSelect(eventData={eventData})");
    }
}
