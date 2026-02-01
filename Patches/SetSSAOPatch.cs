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
            AmplifyGTAO gtao;
            Camera cam = __instance.Camera;
            if (cam.GetComponent<AmplifyGTAO>() != null)
                gtao = cam.GetComponent<AmplifyGTAO>();
            else
                gtao = cam.gameObject.AddComponent<AmplifyGTAO>();

            if (gtao != null)
                gtao.SetAOSettings(ssaoMode);

            return false;
        }
    }
}
