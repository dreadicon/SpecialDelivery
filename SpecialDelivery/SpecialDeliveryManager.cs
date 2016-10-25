using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ColossalFramework;
using UnityEngine;

namespace SpecialDelivery
{
    class SpecialDeliveryManager : Singleton<SpecialDeliveryManager>
    {
        private int _maxIncomingLoadSizeCommercial;
        private int _maxIncomingLoadSizeIndustrial;

        public int MaxIncomingLoadSizeCommercial
        {
            get { return _maxIncomingLoadSizeCommercial; }
            set { _maxIncomingLoadSizeCommercial = value; }
        }

        public int MaxIncomingLoadSizeIndustrial
        {
            get { return _maxIncomingLoadSizeIndustrial; }
            set { _maxIncomingLoadSizeIndustrial = value; }
        }

        private const string userConfigPath = "BuildingThemes.xml";
        private Configuration _configuration;
        internal Configuration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    try
                    {
                        _configuration = Configuration.Deserialize(userConfigPath);

                        if (Debugger.Enabled)
                        {
                            Debugger.Log("Building Themes: User Configuration loaded.");
                        }

                        if (_configuration == null)
                        {
                            _configuration = new Configuration();
                            //SaveConfig();
                        }

                        Debugger.xmlCorrupt = false;
                    }
                    catch (Exception e)
                    {
                        Debugger.xmlCorrupt = true;
                    }
                }

                return _configuration;
            }
        }

        public void Reset()
        {
            
        }

        public void ModifyAllVehicles()
        {
            var vehicles = VehicleManager.instance.m_vehicles;
            foreach (var vehicle in vehicles.m_buffer)
            {
                //if(vehicle.Info.m_vehicleAI is CargoTruckAI)
            }
        }
    }
}
