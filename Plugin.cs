using BepInEx;
using BepInEx.Logging;

namespace SPTAOReplacer
{
    // first string below is your plugin's GUID, it MUST be unique to any other mod. Read more about it in BepInEx docs. Be sure to update it if you copy this project.
    [BepInPlugin("com.matsix.sptaoreplacer", "SPTAOReplacer", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource MyLog;

        // BaseUnityPlugin inherits MonoBehaviour, so you can use base unity functions like Awake() and Update()
        private void Awake()
        {
            MyLog = Logger;
            MyLog.LogInfo("plugin loaded!");

            new SimplePatch().Enable();
        }
    }
}
