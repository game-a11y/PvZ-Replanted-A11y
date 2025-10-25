
namespace PvzReA11y.A11yPatch;

internal class A11yPatches
{
    static void InitAll()
    {
        TextPatch.Init();
        //TextMeshPatch.Init();
    }

    public static void PatchAll()
    {
        try
        {
            InitAll();
        }
        catch (Exception e)
        {
            Core.gLogger.Error($"[A11yPatches.PatchAll] {e}");
        }
    }
}
