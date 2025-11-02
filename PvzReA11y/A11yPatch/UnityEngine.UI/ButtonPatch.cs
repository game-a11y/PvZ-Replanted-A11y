using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PvzReA11y.A11yPatch.UnityEngine.UI;

[HarmonyPatch(typeof(Button))]
internal class ButtonPatch
{
    // TODO: HarmonyLib.HarmonyException: Patching exception in method null
    //[HarmonyPatch("OnSelect")]
    //[HarmonyPostfix]
    //static void OnSelect(Selectable __instance, BaseEventData eventData)
    //{
    //    SelectablePatch.HandleSelectableEvent(__instance, "Button.OnSelect()");
    //}

    //[HarmonyPatch("OnPointerEnter")]
    //[HarmonyPostfix]
    //static void OnPointerEnter(Selectable __instance, PointerEventData eventData)
    //{
    //    SelectablePatch.HandleSelectableEvent(__instance, "Button.OnPointerEnter()");
    //}
}
