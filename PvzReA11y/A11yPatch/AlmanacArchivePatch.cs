using HarmonyLib;
using Il2CppUI.Scripts;
using UnityEngine;

namespace PvzReA11y.A11yPatch;

[HarmonyPatch(typeof(AlmanacArchive))]
public class AlmanacArchivePatch
{
    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    public static void Start_Postfix()
    {
        Core.gLogger.Msg($"[A11y] AlmanacArchive.Start()");
    }

    [HarmonyPatch("closeEntry")]
    [HarmonyPostfix]
    public static void CloseEntry_Postfix()
    {
        Core.gLogger.Msg($"[A11y] AlmanacArchive.closeEntry()");
    }

    [HarmonyPatch("nextEntry")]
    [HarmonyPostfix]
    public static void NextEntry_Postfix()
    {
        Core.gLogger.Msg($"[A11y] AlmanacArchive.nextEntry()");
    }

    [HarmonyPatch("previousEntry")]
    [HarmonyPostfix]
    public static void PreviousEntry_Postfix()
    {
        Core.gLogger.Msg($"[A11y] AlmanacArchive.previousEntry()");
    }
}