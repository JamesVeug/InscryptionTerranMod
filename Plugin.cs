using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using TerranMod.Scripts.Abilities;
using TerranMod.Scripts.Appearances;
using TerranMod.Scripts.Cards;
using TerranMod.Scripts.Encounters;

namespace TerranMod
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {
	    public const string PluginGuid = "jamesgames.inscryption.terranmod";
	    public const string PluginName = "Terran Mod";
	    public const string PluginVersion = "0.2.0.0";
	    public const string DecalPath = "Artwork/watermark.png";

        public static string Directory;
        public static ManualLogSource Log;

        private void Awake()
        {
	        Log = Logger;
            Logger.LogInfo($"Loading {PluginName}...");
            Directory = this.Info.Location.Replace("TerranMod.dll", "");
            new Harmony(PluginGuid).PatchAll();

            // Appearances
            IrradiateAppearance.Initialize();
            
            // Abilities
            MarineDropAbility.Initialize(typeof(MarineDropAbility));
            SpiderMineAbility.Initialize(typeof(SpiderMineAbility));
            SpawnAutoTurretAbility.Initialize(typeof(SpawnAutoTurretAbility));
            TransformIntoOdinAbility.Initialize(typeof(TransformIntoOdinAbility));
            IrradiateAbility.Initialize(typeof(IrradiateAbility));
            ActivatedIrradiateAbility.Initialize(typeof(ActivatedIrradiateAbility));
            
            // Evolution Units
            
            // Units
            XelNagaArtifact.Initialize();

            // Squirrel / Lava
            Logger.LogInfo($"Loaded {PluginName}!");
        }

        private void Start()
        {
	        // Encounters
	        AirEncounter.Initialize();
	        MarineDropEncounter.Initialize();
	        MechEncounter.Initialize();
	        ReaperRushEncounter.Initialize();
	        WidowMinesEncounter.Initialize();
        }
    }
}
