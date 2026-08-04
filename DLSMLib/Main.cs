using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using DLS.Description;
using DLS.Simulation;
using DLSMLib.Chips;
using HarmonyLib;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("DLSMLib.Chips")]
namespace DLSMLib
{
    [BepInPlugin("net.pervalliax.dlsm_lib", "DLSMLib", "0.0.0")]
    public class Main : BasePlugin
    {
        public static ManualLogSource Logging;
        public static bool IsCommunityVersion { get; private set; }

        public override void Load()
        {
            var harmony = new Harmony("net.pervalliax.dlsm_lib");
            Logging = Log;

            Type targetClass = Type.GetType("DLS.Game.BuiltinChipCreator, DLS");
            if (targetClass != null)
            {
                MethodInfo targetMethod = targetClass.GetMethod("GetColor",
                   BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);

                if (targetMethod != null)
                {
                    Log.LogInfo("DLSMLib has detected the Community Edit!");
                    IsCommunityVersion = true;
                }
                else
                {
                    Log.LogInfo("DLSMLib has detected the original DLS!");
                    IsCommunityVersion = false;
                }
            }
            else
            {
                Log.LogInfo("DLSMLib has not detected the class BuiltinChipCreator");
                IsCommunityVersion = false;
            }

            harmony.PatchAll();



            ChipRegistry.AddChip(
                "NEW CHIP TEST",
                "new_chip_test",
                Color.white,
                new ModdedPinIdentity[]
                {
                    Pins.AddPin("IN TEST", 0, PinType.Input, 1),

                    Pins.AddPin("IN TEST", 1, PinType.Input, 8),
                    Pins.AddPin("IN TEST", 2, PinType.Input, 8),

                    Pins.AddPin("OUT TEST", 3, PinType.Output, 8)
                },
                chip =>
                {
                    var value = new PinStateValue();

                    if (chip.InputPins[0].FirstBitHigh)
                    {
                        chip.OutputPins[0].State = chip.InputPins[1].State;
                    }
                    else
                    {
                        chip.OutputPins[0].State = chip.InputPins[2].State;
                    }
                }
            );



            Log.LogInfo("Loaded DLSMLib!");
        }
    }
}