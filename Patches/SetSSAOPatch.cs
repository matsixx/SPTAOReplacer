using EFT;
using EFT.CameraControl;
using EFT.Settings.Graphics;
using GPUInstancer;
using HarmonyLib;
using SPT.Reflection.Patching;
using SPTAOReplacer.Source;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;

namespace SPTAOReplacer.ExamplePatches
{
    internal class SetSSAOPatch : ModulePatch
    {

        public static ESSAOMode currentSsaoMode;
        public static Camera aoCamera;
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(CameraClass), nameof(CameraClass.SetSSAO));
        }

        [PatchPrefix]
        static bool Prefix(CameraClass __instance, ESSAOMode ssaoMode)
        {
            __instance.Hbao_0.enabled = false;
            __instance.AmbientOcclusion_0.enabled = false;
            currentSsaoMode = ssaoMode;
            AmplifyGTAO gtaoManager;
            aoCamera = __instance.Camera;
            if (aoCamera.GetComponent<AmplifyGTAO>() != null)
                gtaoManager = aoCamera.GetComponent<AmplifyGTAO>();
            else
                gtaoManager = aoCamera.gameObject.AddComponent<AmplifyGTAO>();

            if (gtaoManager != null)
            {
                gtaoManager.SetAOSettings(ssaoMode);

                AmplifyOcclusionEffect gtaoEffect = aoCamera.GetComponent<AmplifyOcclusionEffect>();
                if (ssaoMode != ESSAOMode.Off)
                    gtaoEffect.enabled = true;
                else
                    gtaoEffect.enabled = false;
            }

            return false;
        }
    }
}
