using System;
using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using Pixelplacement;
using StarCraftCore.Scripts.Abilities;
using StarCraftCore.Scripts.Data.Sigils;
using TerranMod.Scripts.Data.Sigils;
using UnityEngine;

namespace TerranMod.Scripts.Abilities
{
    public class TransformIntoOdinAbility : ACustomActivatedAbility<TransformIntoOdinAbility, TransformIntoOdinAbilityData>
    {
        public override Ability Ability => ability;
        public static Ability ability;
        
        public static void Initialize(Type declaringType)
        {
            ability = InitializeBase(Plugin.PluginGuid, declaringType, Plugin.Directory);
        }

        public override IEnumerator Activate()
        {
            CardInfo evolution = CardLoader.GetCardByName("Terran_JSON_Odin");
            foreach (CardModificationInfo mod in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                CardModificationInfo clone = (CardModificationInfo)mod.Clone();
                bool hasEvolve = clone.HasAbility(Ability.Evolve);
                if (hasEvolve)
                {
                    clone.abilities.Remove(Ability.Evolve);
                }
                bool hasAssimilate = clone.HasAbility(Ability);
                if (hasAssimilate)
                {
                    clone.abilities.Remove(Ability);
                }
                evolution.Mods.Add(clone);
            }

            if (evolution.abilities.Contains(Ability))
            {
                Card.Status.hiddenAbilities.Add(Ability);
            }
            
            yield return PreSuccessfulTriggerSequence();
            yield return Card.TransformIntoCard(evolution);
            yield return new WaitForSeconds(0.5f);
            yield return LearnAbility(0.5f);
        }
        
        protected CardInfo GetTransformCardInfo()
        {
            return (Card.Info.evolveParams != null) ? (Card.Info.evolveParams.evolution.Clone() as CardInfo) : EvolveParams.GetDefaultEvolution(base.Card.Info);
        }
    }
}