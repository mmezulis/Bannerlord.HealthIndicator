using HarmonyLib;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.ViewModelCollection.Multiplayer;
using TaleWorlds.Core;
using System.Diagnostics.CodeAnalysis;

namespace Bannerlord.HealthIndicator.Patches
{
    public class AgentInteractionInterfaceVMPatch
    {
        [AllowNull]
        static AgentInteractionInterfaceVM agentVm;

        [HarmonyPatch(typeof(AgentInteractionInterfaceVM), "SetAgent")]
        public class SetAgentPatch{
            static void Postfix(Agent focusedAgent, AgentInteractionInterfaceVM __instance)
            {
                __instance.PrimaryInteractionMessage = __instance.PrimaryInteractionMessage + $" ({focusedAgent.Health}/{focusedAgent.HealthLimit})";
                agentVm = __instance;
                focusedAgent.OnAgentHealthChanged += HandleOnAgentHealthChanged;
            }
        }

        [HarmonyPatch(typeof(AgentInteractionInterfaceVM), "OnFocusLost")]
        public class OnFocusLostPatch
        {
            static void Postfix(Agent agent)
            {
                agent.OnAgentHealthChanged -= HandleOnAgentHealthChanged;
            }
        }

        private static void HandleOnAgentHealthChanged(Agent agent, float oldHealth, float newHealth)
        {
            agentVm.PrimaryInteractionMessage =  $"{agent.Name} ({newHealth}/{agent.HealthLimit})";
        }
    }
}
