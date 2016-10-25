using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using ColossalFramework;
using SpecialDelivery.Detour;

namespace SpecialDelivery.Detour
{
    internal class IndustrialBuildingAIDetour
    {
        private static bool deployed = false;

        private static RedirectCallsState _MaxIncomingLoadSize_state;
        private static MethodInfo _MaxIncomingLoadSize_original;
        private static MethodInfo _MaxIncomingLoadSize_detour;

        public static void Deploy()
        {
            if (!deployed)
            {
                _MaxIncomingLoadSize_original = typeof(CommercialBuildingAI).GetMethod("MaxIncomingLoadSize", BindingFlags.Instance | BindingFlags.NonPublic);
                _MaxIncomingLoadSize_detour = typeof(CommercialBuildingAIDetour).GetMethod("MaxIncomingLoadSize", BindingFlags.Instance | BindingFlags.NonPublic);
                _MaxIncomingLoadSize_state = RedirectionHelper.RedirectCalls(_MaxIncomingLoadSize_original, _MaxIncomingLoadSize_detour);

                deployed = true;

                Debugger.Log("Special Delivery: IndustrialBuildingAI Methods detoured!");
            }
        }

        public static void Revert()
        {
            if (deployed)
            {
                RedirectionHelper.RevertRedirect(_MaxIncomingLoadSize_original, _MaxIncomingLoadSize_state);
                _MaxIncomingLoadSize_original = null;
                _MaxIncomingLoadSize_detour = null;

                deployed = false;

                Debugger.Log("Special Delivery: IndustrialBuildingAI Methods restored!");
            }
        }

        // Detour

        // Return a variable amount rather than a fixed one.

        public static int MaxIncomingLoadSize()
        {
            return SpecialDeliveryManager.instance.MaxIncomingLoadSizeIndustrial;
        } 
    }
}
