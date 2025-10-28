using HarmonyLib;
using Il2CppUI.Scripts;
using UnityEngine;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(CrazyDaveStoreInitializer))]
public class CrazyDaveStoreInitializerPatch
{
    [HarmonyPatch("OnEnable")]
    [HarmonyPostfix]
    public static void OnEnable_Postfix()
    {
        Debug.Log($"[A11y] CrazyDaveStoreInitializer.OnEnable()");
    }
}