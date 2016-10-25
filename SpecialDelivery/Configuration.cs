using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace SpecialDelivery
{
    public class Configuration
    {
        public int version = 0;

        public int CommercialBuildingLoadSize = 4000;
        public int IndustrialBuildingLoadSize = 4000;

        public float TruckCargoCapacityMultiplier = 1;
        public float TrainCargoCapacityMultiplier = 1;
        public float BoatCargoCapacityMultiplier = 1;
        public bool UseRealisticCapacity = true;

        public static Configuration Deserialize(string filename)
        {
            if (!File.Exists(filename)) return null;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Configuration));
            try
            {
                using (System.IO.StreamReader streamReader = new System.IO.StreamReader(filename))
                {
                    return (Configuration)xmlSerializer.Deserialize(streamReader);
                }
            }
            catch (Exception e)
            {
                Debugger.Log("Couldn't load configuration (XML malformed?)");
                throw e;
            }
        }

        public static void Serialize(string filename, Configuration config)
        {
            var xmlSerializer = new XmlSerializer(typeof(Configuration));
            try
            {
                using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(filename))
                {
                    var configCopy = new Configuration();

                    //configCopy.version = config.version;
                    //configCopy.UnlockPolicyPanel = config.UnlockPolicyPanel;
                    //configCopy.CreateBuildingDuplicates = config.CreateBuildingDuplicates;
                    //configCopy.ThemeValidityWarning = config.ThemeValidityWarning;

                    //foreach (var theme in config.themes)
                    //{
                    //    var newTheme = new Theme
                    //    {
                    //        name = theme.name,
                    //        stylePackage = theme.stylePackage
                    //    };
                    //    foreach (var building in theme.buildings.Where(building =>
                    //        // a user-added building has to be included, or we don't need it in the config
                    //        (building.builtInBuilding == null && building.include)

                    //            // a built-in building that was modified by the user: Only add it to the config if the modification differs
                    //        || (building.builtInBuilding != null && !building.Equals(building.builtInBuilding))))
                    //    {
                    //        newTheme.buildings.Add(building);
                    //    }
                    //    if (!theme.isBuiltIn || newTheme.buildings.Count > 0)
                    //    {
                    //        configCopy.themes.Add(newTheme);
                    //    }
                    //}

                    xmlSerializer.Serialize(streamWriter, configCopy);
                }
            }
            catch (Exception e)
            {
                Debugger.Log("Couldn't create configuration file at \"" + Directory.GetCurrentDirectory() + "\"");
                throw e;
            }
        }
    }
}
