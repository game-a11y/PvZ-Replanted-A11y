using MelonLoader;

[assembly: MelonInfo(typeof(PvzReA11y.Core), "PvzReA11y", "1.0.0", "inkydragon@Github", null)]
[assembly: MelonGame("PopCap Games", "PvZ Replanted")]

namespace PvzReA11y
{
    public class Core : MelonMod
    {
        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("Initialized.");
        }
    }
}