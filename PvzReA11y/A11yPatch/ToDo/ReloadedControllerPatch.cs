using System.Text;
using HarmonyLib;
using Il2CppReloaded.Gameplay;
using Il2CppSource.Controllers;

namespace PvzReA11y.A11yPatch.ToDo;

[HarmonyPatch(typeof(ReloadedController))]
public class ReloadedControllerPatch
{
    // TODO: 精简输出
    //[HarmonyPostfix]
    //[HarmonyPatch("Awake")]
    //public static void Awake_Postfix(ReloadedController __instance)
    //{
    //    var sb = new StringBuilder();
    //    sb.Append("ReloadedController.Awake() - ");
    //    sb.Append("Instance: ").Append(__instance?.GetType().Name ?? "null").Append("#").Append(__instance?.GetHashCode() ?? 0);
        
    //    if (__instance != null)
    //    {
    //        sb.Append(", ReanimationID: ").Append(__instance.ReanimationID);
    //        sb.Append(", IsAttachment: ").Append(__instance.IsAttachment);
    //    }
        
    //    Core.gLogger.Msg(sb.ToString());
    //}

    //[HarmonyPostfix]
    //[HarmonyPatch("Init")]
    //public static void Init_Postfix(ReloadedController __instance, ReloadedObject obj)
    //{
    //    var sb = new StringBuilder();
    //    sb.Append("ReloadedController.Init() - ");
    //    sb.Append("Instance: ").Append(__instance?.GetType().Name ?? "null").Append("#").Append(__instance?.GetHashCode() ?? 0);
    //    sb.Append(", Object: ").Append(obj?.GetType().Name ?? "null").Append("#").Append(obj?.GetHashCode() ?? 0);
        
    //    if (__instance != null)
    //    {
    //        sb.Append(", ReanimationID: ").Append(__instance.ReanimationID);
    //        sb.Append(", Scale: ").Append(__instance.Scale);
    //        sb.Append(", AnimRate: ").Append(__instance.AnimRate);
    //    }
        
    //    Core.gLogger.Msg(sb.ToString());
    //}

    //[HarmonyPostfix]
    //[HarmonyPatch("Die")]
    //public static void Die_Postfix(ReloadedController __instance)
    //{
    //    var sb = new StringBuilder();
    //    sb.Append("ReloadedController.Die() - ");
    //    sb.Append("Instance: ").Append(__instance?.GetType().Name ?? "null").Append("#").Append(__instance?.GetHashCode() ?? 0);
        
    //    if (__instance != null)
    //    {
    //        sb.Append(", ReanimationID: ").Append(__instance.ReanimationID);
    //        sb.Append(", IsAttachment: ").Append(__instance.IsAttachment);
    //    }
        
    //    Core.gLogger.Msg(sb.ToString());
    //}
}