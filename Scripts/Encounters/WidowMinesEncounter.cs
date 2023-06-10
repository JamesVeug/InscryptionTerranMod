using System.Collections.Generic;
using DiskCardGame;
using InscryptionAPI.Encounters;
using InscryptionAPI.Regions;
using StarCraftCore.Scripts.Regions;

namespace TerranMod.Scripts.Encounters
{
    public class WidowMinesEncounter
    {
        public static EncounterBlueprintData Instance = null;

        public static void Initialize()
        {
            EncounterBlueprintData data = EncounterManager.New("Terran Widow Mines");
            data.SetDifficulty(1, 15);
            data.AddDominantTribes(Tribe.Canine);
            data.AddRandomReplacementCards(new string[]
            {
                
            });
            data.AddTurnMods(new []
            {
                new EncounterBlueprintData.TurnModBlueprint()
                {
                    turn = 1,
                    applyAtDifficulty = 1,
                }
            });
            data.AddTurn(new EncounterBlueprintData.CardBlueprint[]
            {
                new EncounterBlueprintData.CardBlueprint()
                {
                    card = CardLoader.GetCardByName("Terran_JSON_WidowMine"),
                },
                new EncounterBlueprintData.CardBlueprint()
                {
                    card = CardLoader.GetCardByName("Terran_JSON_Raven"),
                    minDifficulty = 10
                },
            });
            data.AddTurn(new EncounterBlueprintData.CardBlueprint[]
            {
            });
            data.AddTurn(new EncounterBlueprintData.CardBlueprint[]
            {
                new EncounterBlueprintData.CardBlueprint()
                {
                    card = CardLoader.GetCardByName("Terran_JSON_WidowMine"),
                },
                new EncounterBlueprintData.CardBlueprint()
                {
                    card = CardLoader.GetCardByName("Terran_JSON_WidowMine"),
                    minDifficulty = 10
                },
            });
            data.AddTurn(new EncounterBlueprintData.CardBlueprint[]
            {
            });
            data.AddTurn(new EncounterBlueprintData.CardBlueprint[]
            {
                new EncounterBlueprintData.CardBlueprint()
                {
                    card = CardLoader.GetCardByName("Terran_JSON_WidowMine"),
                },
                new EncounterBlueprintData.CardBlueprint()
                {
                    card = CardLoader.GetCardByName("Terran_JSON_WidowMine"),
                    minDifficulty = 10
                },
            });
            data.AddTurn(new EncounterBlueprintData.CardBlueprint[]
            {
            });
            data.AddTurn(new EncounterBlueprintData.CardBlueprint[]
            {
                new EncounterBlueprintData.CardBlueprint()
                {
                    card = CardLoader.GetCardByName("Terran_JSON_WidowMine"),
                }
            });

            RegionData charRegion = MarSaraRegion.regionData;
            if (charRegion != null)
            {
                data.regionSpecific = true;
                charRegion.AddEncounters(data);
            }
            Instance = data;
        }
    }
}