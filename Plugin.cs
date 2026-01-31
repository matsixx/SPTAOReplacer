using BepInEx;
using BepInEx.Logging;
using SPTAOReplacer.ExamplePatches;

namespace SPTAOReplacer
{
    [BepInPlugin("com.matsix.sptaoreplacer", "SPTAOReplacer", "1.0.1")]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource MyLog;

        private void Awake()
        {
            MyLog = Logger;
            MyLog.LogInfo("plugin loaded!");

            new SetSSAOPatch().Enable();
        }
    }
}
