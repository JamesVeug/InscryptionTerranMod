using System.Collections.Generic;
using DiskCardGame;
using InscryptionAPI.Encounters;
using InscryptionAPI.Regions;
using StarCraftCore.Scripts.Regions;

namespace TerranMod.Scripts.Encounters
{
    public class MarineDropEncounter
    {
        public static EncounterBlueprintData Instance = null;

        public static void Initialize()
        {
            EncounterBlueprintData data = EncounterManager.New("Terran Marine Drop");
            data.SetDifficulty(1, 15);
            data.AddDominantTribes(Tribe.Canine);
            data.AddRandomReplacementCards(new string[]
            {
                "Terran_JSON_Marauder",
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
                    card = CardLoader.GetCardByName("Terran_JSON_Marine"),
                },
                new EncounterBlueprintData.CardBlueprint()
                {
                    card = CardLoader.GetCardByName("Terran_JSON_Medivac"),
                    minDifficulty = 4
                },
                new EncounterBlueprintData.CardBlueprint()
                {
                    card = CardLoader.GetCardByName("Terran_JSON_Marine"),
                }
            });
            data.AddTurn(new EncounterBlueprintData.CardBlueprint[]
            {
                new EncounterBlueprintData.CardBlueprint()
                {
                    card = CardLoader.GetCardByName("Terran_JSON_Medivac"),
                    maxDifficulty = 4
                }
            });
            data.AddTurn(new EncounterBlueprintData.CardBlueprint[]
            {
                new EncounterBlueprintData.CardBlueprint()
                {
                    card = CardLoader.GetCardByName("Terran_JSON_Marine"),
                },
                new EncounterBlueprintData.CardBlueprint()
                {
                    card = CardLoader.GetCardByName("Terran_JSON_Medivac"),
                    minDifficulty = 4
                },
                new EncounterBlueprintData.CardBlueprint()
                {
                    card = CardLoader.GetCardByName("Terran_JSON_Marine"),
                }
            });
            data.AddTurn(new EncounterBlueprintData.CardBlueprint[]
            {
                new EncounterBlueprintData.CardBlueprint()
                {
                    card = CardLoader.GetCardByName("Terran_JSON_Medivac"),
                    maxDifficulty = 4
                }
            });
            data.AddTurn(new EncounterBlueprintData.CardBlueprint[]
            {
            });
            data.AddTurn(new EncounterBlueprintData.CardBlueprint[]
            {
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