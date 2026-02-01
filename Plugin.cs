using BepInEx;
using BepInEx.Logging;
using SPTAOReplacer.ExamplePatches;
using UnityEngine;
using UnityEngine.Rendering;

namespace SPTAOReplacer
{
    [BepInPlugin("com.matsix.sptaoreplacer", "SPTAOReplacer", "1.0.2")]
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
