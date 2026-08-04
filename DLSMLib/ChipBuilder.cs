using DLS.Description;
using DLS.Game;
using UnityEngine;
using System.Linq;

namespace DLSMLib.Chips
{
    internal static class ChipBuilder
    {
        internal static ChipDescription CreateChipData(float sizeX, float sizeY, Color col, NameDisplayLocation nameLocation,
            ModdedPinIdentity[] inputs, ModdedPinIdentity[] outputs)
        {
            Color color;
            if (Main.IsCommunityVersion == true)
                color = BuiltinChipCreator.GetColor(col);
            else
                color = col;

            Vector2 size = new Vector2(
                BuiltinChipCreator.CalculateGridSnappedWidth(sizeX / 10),
                sizeY / 10
            );

            var Inputs = inputs?.Select(x => x.desc).ToArray();
            var Outputs = outputs?.Select(x => x.desc).ToArray();

            if (inputs == null)
                Inputs = new PinDescription[] {BuiltinChipCreator.CreatePinDescription("IN", 0)};

            if(outputs == null)
                Outputs = new PinDescription[] { BuiltinChipCreator.CreatePinDescription("OUT", 1) };

            return BuiltinChipCreator.CreateBuiltinChipDescription(
                ChipType.Custom,
                size,
                color,
                Inputs,
                Outputs,
                null,
                nameLocation
            );
        }
    }
}
