using HarmonyLib;
using Il2CppReloaded.Gameplay;
using Il2CppSource.Controllers;
using MelonLoader;
using PvzReA11y;

namespace A11y.SR.Patches;

[HarmonyPatch(typeof(PreviewDrawerController))]
public class PreviewDrawerControllerPatch
{
    // 监控 PreviewDrawerController.SetPreview(SeedType, bool) 方法
    [HarmonyPostfix]
    [HarmonyPatch(nameof(PreviewDrawerController.SetPreview), new Type[] { typeof(SeedType), typeof(bool) })]
    public static void SetPreview_Postfix(PreviewDrawerController __instance, SeedType seedType, bool seedPacket)
    {
        Core.gLogger.Msg($"[PlantPreview] PreviewDrawerController.SetPreview - SeedType: {seedType}, SeedPacket: {seedPacket}, Current m_seedType: {__instance.m_seedType}");
    }

    // 监控 PreviewDrawerController.SetPreviewPlant() 方法
    [HarmonyPostfix]
    [HarmonyPatch(nameof(PreviewDrawerController.SetPreviewPlant))]
    public static void SetPreviewPlant_Postfix(PreviewDrawerController __instance, SeedType seedType)
    {
        
        Core.gLogger.Msg($"[PlantPreview] PreviewDrawerController.SetPreviewPlant - SeedType: {seedType}, Current m_seedType: {__instance.m_seedType}");
        
    }

    // 监控 PreviewDrawerController.SetPreviewSeedPacket() 方法
    [HarmonyPostfix]
    [HarmonyPatch(nameof(PreviewDrawerController.SetPreviewSeedPacket))]
    public static void SetPreviewSeedPacket_Postfix(PreviewDrawerController __instance, SeedType seedType)
    {
        
        Core.gLogger.Msg($"[PlantPreview] PreviewDrawerController.SetPreviewSeedPacket - SeedType: {seedType}, Current m_seedType: {__instance.m_seedType}");
        
    }
}