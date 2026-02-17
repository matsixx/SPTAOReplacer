using BepInEx;
using BepInEx.Logging;
using SPTAOReplacer.ExamplePatches;
using SPTAOReplacer.Patches;
using UnityEngine;
using UnityEngine.Rendering;

namespace SPTAOReplacer
{
    [BepInPlugin("com.matsix.sptaoreplacer", "SPTAOReplacer", "1.0.3")]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource MyLog;

        private void Awake()
        {
            MyLog = Logger;
            MyLog.LogInfo("plugin loaded!");

            new SetSSAOPatch().Enable();
            new AOInitTimer().Enable();
            new AOInitialize().Enable();
        }
    }
}
