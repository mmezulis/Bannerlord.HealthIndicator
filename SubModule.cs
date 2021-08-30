using TaleWorlds.MountAndBlade;
using HarmonyLib;
using System;
using TaleWorlds.Core;

namespace Bannerlord.HealthIndicator
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            var harmony = new Harmony("Bannerlord.HealthIndicator");
            try
            {
                harmony.PatchAll();
            }
            catch (Exception e)
            {
                InformationManager.DisplayMessage(new InformationMessage(e.ToString()));
            }
        }
    }
}