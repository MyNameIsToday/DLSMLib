using DLS.Simulation;
using DLS.Description;
using DLS.Game;
using System.Diagnostics;

namespace DLSMLib.Chips
{
    public static class Pins
    {
        public static uint low = PinStateValue.LOGIC_LOW;
        public static uint high = PinStateValue.LOGIC_HIGH;
        public static uint disconnected = PinStateValue.LOGIC_DISCONNECTED;

        public static ModdedPinIdentity AddPin(string name, int index, PinType pinType, ushort bitCount)
        {
            return new ModdedPinIdentity
            {
                desc = BuiltinChipCreator.CreatePinDescription(name, index, bitCount),
                type = pinType
            };
        }
    }

    public enum PinType
    {
        Input,
        Output
    }

    public class ModdedPinIdentity
    {
        public PinDescription desc;
        public PinType type;
    }
}
