using System;
using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using StarCraftCore.Scripts.Abilities;
using TerranMod.Scripts.Data.Sigils;
using UnityEngine;

namespace TerranMod.Scripts.Abilities
{
    public class IrradiateAbility : ACustomAbilityBehaviour<IrradiateAbility, IrradiateAbilityData>
    {
        public override Ability Ability => ability;
        public static Ability ability;
        
        public static void Initialize(Type declaringType)
        {
            ability = InitializeBase(Plugin.PluginGuid, declaringType, Plugin.Directory);
        }

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            return Card != null && !Card.Dead && playerTurnEnd != Card.OpponentCard;
        }

        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            yield return base.PreSuccessfulTriggerSequence();
            
            // Switch to Combat view
            yield return new WaitForSeconds(0.2f);
            if (Singleton<ViewManager>.Instance.CurrentView != Singleton<BoardManager>.Instance.CombatView)
            {
                Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView, false, false);
                yield return new WaitForSeconds(0.2f);
            }

            CardSlot toLeft = Singleton<BoardManager>.Instance.GetAdjacent(Card.slot, true);
            if (toLeft != null && toLeft.Card != null && !toLeft.Card.Dead)
            {
                yield return DealDamage(toLeft);
            }

            CardSlot toRight = Singleton<BoardManager>.Instance.GetAdjacent(Card.slot, false);
            if (toRight != null && toRight.Card != null && !toRight.Card.Dead)
            {
                yield return DealDamage(toRight);
            }
            
            // Hurt self
            if (Card != null && !Card.Dead)
            {
                yield return DealDamage(Card.slot);
            }

            yield return base.LearnAbility();
            yield return new WaitForSeconds(0.25f);
        }

        private IEnumerator DealDamage(CardSlot slot)
        {
            slot.Card.Anim.PlayHitAnimation();
            yield return slot.Card.TakeDamage(LoadedData.damagePerTurn, null);
            yield return new WaitForSeconds(0.25f);
        }
    }
}