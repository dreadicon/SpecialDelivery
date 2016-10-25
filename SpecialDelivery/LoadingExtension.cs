using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICities;

namespace SpecialDelivery
{
    public class LoadingExtension : LoadingExtensionBase
    {
        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);

            Debugger.Initialize();

            Debugger.Log("ON_CREATED");
            Debugger.Log("Special Delivery: Initializing Mod...");

            try
            {
                SpecialDeliveryManager.instance.Reset();

                UpdateConfig();

                try
                {
                    Detour.CommercialBuildingAIDetour.Deploy();
                }
                catch (Exception e)
                {
                    Debugger.LogException(e);
                }
                try
                {
                    Detour.IndustrialBuildingAIDetour.Deploy();
                }
                catch (Exception e)
                {
                    Debugger.LogException(e);
                }


                Debugger.Log("Special Delivery: Mod successfully intialized.");
            }
            catch (Exception e)
            {
                Debugger.LogException(e);
            }
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);
            Debugger.Log("ON_LEVEL_LOADED");
            Debugger.OnLevelLoaded();

            try
            {

                // Don't load if it's not a game
                if (mode != LoadMode.LoadGame && mode != LoadMode.NewGame) return;

                SpecialDeliveryManager.instance.ModifyAllVehicles();

            }
            catch (Exception e)
            {
                Debugger.LogException(e);
            }
        }

        public override void OnLevelUnloading()
        {
            base.OnLevelUnloading();
            Debugger.Log("ON_LEVEL_UNLOADING");
            Debugger.OnLevelUnloading();

            SpecialDeliveryManager.instance.Reset();
        }

        public override void OnReleased()
        {
            base.OnReleased();
            Debugger.Log("ON_RELEASED");

            SpecialDeliveryManager.instance.Reset();

            Debugger.Log("Special Delivery: Reverting detoured methods...");
            try
            {
                Detour.CommercialBuildingAIDetour.Revert();
                Detour.IndustrialBuildingAIDetour.Revert();
            }
            catch (Exception e)
            {
                Debugger.LogException(e);
            }

            Debugger.Log("Special Delivery: Done!");

            Debugger.Deinitialize();
        }

        private void UpdateConfig()
        {
            //SpecialDeliveryManager.instance.Configuration.version = 1;
            //SpecialDeliveryManager.instance.SaveConfig();
        }
    }
}
