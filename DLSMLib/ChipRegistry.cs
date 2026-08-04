using DLS.Description;
using DLS.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DLSMLib.Chips
{
    public static class ChipRegistry
    {
        public static readonly Dictionary<ChipDescription, ModdedChipIdentity> registeredChips = new Dictionary<ChipDescription, ModdedChipIdentity>();
        public static readonly Dictionary<SimChip, ModdedChipIdentity> activeChips = new Dictionary<SimChip, ModdedChipIdentity>();

        public static void AddChip(
            string name,
            string uniqueChipID,
            Color color = default,
            ModdedPinIdentity[] pins = null,
            System.Action<SimChip> function = null,
            NameDisplayLocation nameLocation = NameDisplayLocation.Centre)
        {
            if (function == null)
            {
                function = defaultF =>
                {
                    var value = new PinStateValue();
                    value.a = defaultF.InputPins[0].FirstBitHigh ? PinStateValue.LOGIC_LOW : PinStateValue.LOGIC_HIGH;

                    defaultF.OutputPins[0].State = value;
                };
            }

            if (registeredChips.Keys.Any(x => x.Name == name))
            {
                Main.Logging.LogWarning("Chip with name " + name + " already exists.");
                return;
            }

            if (registeredChips.Values.Any(x => x.uniqueID == uniqueChipID))
            {
                Main.Logging.LogWarning("Chip with ID " + uniqueChipID + " already exists.");
                return;
            }

            var inputs = pins?.Where(x => x.type == PinType.Input).ToArray();
            var outputs = pins?.Where(x => x.type == PinType.Output).ToArray();

            var chip = ChipBuilder.CreateChipData(20, 20, color, nameLocation, inputs, outputs);
            chip.Name = name;
            
            registeredChips[chip] = new ModdedChipIdentity
            {
                Name = name,
                uniqueID = uniqueChipID,
                chipDesc = chip,
                chipFunction = function
            };
        }
    }


    public class ModdedChipIdentity 
    { 
        public string Name;
        public string uniqueID; 
        public ChipDescription chipDesc; 

        public System.Action<SimChip> chipFunction; 
    }
}
