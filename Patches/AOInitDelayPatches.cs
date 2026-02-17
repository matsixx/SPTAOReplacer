using EFT.Settings.Graphics;
using HarmonyLib;
using SPT.Reflection.Patching;
using SPTAOReplacer.ExamplePatches;
using SPTAOReplacer.Source;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace SPTAOReplacer.Patches
{

    // Dumb workaround to fix an issue with AO during winter season. Visual artifacts where grass is, need to disable AO then re-enable after grass loads.
    internal class AOInitTimer : ModulePatch
    {

        public static float aoRefreshTimer = -1f;
        public static float aoRefreshDelay = 3f;
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(EFT.UI.PreloaderUI), nameof(EFT.UI.PreloaderUI.ShowRaidStartInfo));
        }

        [PatchPostfix]
        static void Postfix()
        {
            aoRefreshTimer = 0f;
            var aoEffect = SetSSAOPatch.aoCamera?.GetComponent<AmplifyOcclusionEffect>();
            if (aoEffect != null && aoEffect.enabled)
            {
                aoEffect.enabled = false;
            }
        }
    }
    internal class AOInitialize : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(EFT.Player), nameof(EFT.Player.VisualPass));
        }

        [PatchPostfix]
        static void Postfix()
        {
            if (AOInitTimer.aoRefreshTimer < 0f)
                return;

            AOInitTimer.aoRefreshTimer += Time.deltaTime;

            if (AOInitTimer.aoRefreshTimer >= AOInitTimer.aoRefreshDelay)
            {
                AOInitTimer.aoRefreshTimer = -1f;

                var aoEffect = SetSSAOPatch.aoCamera?.GetComponent<AmplifyOcclusionEffect>();
                if (aoEffect != null && SetSSAOPatch.currentSsaoMode != ESSAOMode.Off)
                {
                    Plugin.MyLog.LogInfo("Refreshing AO Effect");
                    aoEffect.enabled = true;
                }
            }
        }
    }
}
