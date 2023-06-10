using System;
using System.Collections;
using System.Collections.Generic;
using APIPlugin;
using DiskCardGame;
using InscryptionAPI.Card;
using StarCraftCore.Scripts.Abilities;
using StarCraftCore.Scripts.Data.Sigils;
using TerranMod.Scripts.Appearances;
using TerranMod.Scripts.Data.Sigils;
using UnityEngine;

namespace TerranMod.Scripts.Abilities
{
    public class ActivatedIrradiateAbility : ACustomActivatedAbility<ActivatedIrradiateAbility, ActivatedIrradiateAbilityData>
    {
        public override Ability Ability => ability;
        public static Ability ability = Ability.None;
        
        private CardSlot m_targetedCardSlot = null;
		
        public static void Initialize(Type declaringType)
        {
            ability = InitializeBase(Plugin.PluginGuid, declaringType, Plugin.Directory);
        }

        public override bool CanActivate()
        {
            bool canActivate = Singleton<BoardManager>.Instance.allSlots.Find((a)=>a.Card != null) != null;
            return canActivate;
        }

        public override IEnumerator Activate()
        {
            BoardManager boardManager = Singleton<BoardManager>.Instance;
            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(boardManager.ChoosingSlotViewMode, false);
            
            // Find who we want to abduct
            IEnumerator choosingTarget = ChooseTarget();
            yield return choosingTarget;
	        
            // Make sure its a valid target
            CardSlot targetSlot = m_targetedCardSlot;
            if (targetSlot != null && targetSlot.Card != null)
            {
                // Move this card to the target slot
                yield return IrradiateCard(targetSlot);
            }
            else
            {
                Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.3f);
            }

            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(boardManager.DefaultViewMode, false);
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView, false, false);
            Singleton<CombatPhaseManager>.Instance.VisualizeClearSniperAbility();
        }
        
        private IEnumerator ChooseTarget()
        {
            CombatPhaseManager combatPhaseManager = Singleton<CombatPhaseManager>.Instance;
            BoardManager boardManager = Singleton<BoardManager>.Instance;
            List<CardSlot> allSlots = new List<CardSlot>(boardManager.AllSlotsCopy);

            Action<CardSlot> callback1 = null;
            Action<CardSlot> callback2 = null;
	        
            combatPhaseManager.VisualizeStartSniperAbility(Card.slot);
	        
            CardSlot cardSlot = Singleton<InteractionCursor>.Instance.CurrentInteractable as CardSlot;
            if (cardSlot != null && allSlots.Contains(cardSlot))
            {
                combatPhaseManager.VisualizeAimSniperAbility(Card.slot, cardSlot);
            }

            List<CardSlot> allTargetSlots = allSlots;
            List<CardSlot> validTargetSlots = allSlots.FindAll((a)=>a.Card != null);

            m_targetedCardSlot = null;
            Action<CardSlot> targetSelectedCallback;
            if ((targetSelectedCallback = callback1) == null)
            {
                targetSelectedCallback = (callback1 = delegate(CardSlot s)
                {
                    m_targetedCardSlot = s;
                    combatPhaseManager.VisualizeConfirmSniperAbility(s);
                });
            }
	        
            Action<CardSlot> invalidTargetCallback = null;
            Action<CardSlot> slotCursorEnterCallback;
            if ((slotCursorEnterCallback = callback2) == null)
            {
                slotCursorEnterCallback = (callback2 = delegate(CardSlot s)
                {
                    combatPhaseManager.VisualizeAimSniperAbility(Card.slot, s);
                });
            }

            yield return boardManager.ChooseTarget(allTargetSlots, validTargetSlots, targetSelectedCallback, invalidTargetCallback, slotCursorEnterCallback, () => false, CursorType.Target);
        }
        
        private IEnumerator IrradiateCard(CardSlot targetSlot)
        {
            targetSlot.Card.AddTemporaryMod(new CardModificationInfo()
            {
                abilities = new List<Ability>()
                {
                    IrradiateAbility.ability,
                }
            });
            targetSlot.Card.Info.AddAppearances(IrradiateAppearance.CustomAppearance);
            targetSlot.Card.Anim.PlayHitAnimation();
            yield return new WaitForSeconds(0.2f);
        }

        // Token: 0x06001498 RID: 5272 RVA: 0x000487D4 File Offset: 0x000469D4
        private void ModifySpawnedCard(CardInfo card)
        {
            List<Ability> abilities = new List<Ability>();
            foreach (CardModificationInfo cardModificationInfo in base.Card.TemporaryMods)
            {
                abilities.AddRange(cardModificationInfo.abilities);
            }
            abilities.RemoveAll((Ability x) => x == this.Ability);
            
            foreach (CardModificationInfo cardModificationInfo in base.Card.Info.mods)
            {
                abilities.AddRange(cardModificationInfo.abilities);
            }
            abilities.RemoveAll((Ability x) => x == this.Ability);
            
            if (abilities.Count > 4)
            {
                abilities.RemoveRange(3, abilities.Count - 4);
            }

            if (abilities.Count > 0)
            {
                CardModificationInfo cardModificationInfo2 = new CardModificationInfo();
                cardModificationInfo2.fromCardMerge = true;
                cardModificationInfo2.abilities = abilities;
                card.Mods.Add(cardModificationInfo2);
            }
        }
    }
}