using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP; 
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
            //Patching and checking for Community Edit.
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

            Log.LogInfo("Loaded DLSMLib!");
        }
    }
}