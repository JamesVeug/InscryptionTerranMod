using System;
using System.Collections;
using DiskCardGame;
using StarCraftCore.Scripts.Abilities;
using TerranMod.Scripts.Data.Sigils;
using UnityEngine;

namespace TerranMod.Scripts.Abilities
{
    public class SpiderMineAbility : ACustomStrafe<SpiderMineAbility, SpiderMineAbilityData>
    {
        public override Ability Ability => ability;
        public static Ability ability;
        
        public static void Initialize(Type declaringType)
        {
            ability = InitializeBase(Plugin.PluginGuid, declaringType, Plugin.Directory);
        }

        public override IEnumerator PostSuccessfulMoveSequence(CardSlot oldSlot)
		{
			CardInfo creepTumorInfo = ScriptableObjectLoader<CardInfo>.AllData.Find((CardInfo info) => info.name == "Terran_JSON_SpiderMine");

			PlayableCard creepTumor = CardSpawner.SpawnPlayableCardWithCopiedMods(creepTumorInfo, base.Card, Ability.TailOnHit);
			creepTumor.transform.position = oldSlot.transform.position + Vector3.back * 2f + Vector3.up * 2f;
			creepTumor.transform.rotation = Quaternion.Euler(110f, 90f, 90f);
			yield return Singleton<BoardManager>.Instance.ResolveCardOnBoard(creepTumor, oldSlot, 0.1f, null, true);
			Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);
			yield return new WaitForSeconds(0.2f);
			creepTumor.Anim.StrongNegationEffect();
			yield return base.StartCoroutine(base.LearnAbility(0.5f));
			yield return new WaitForSeconds(0.2f);
		}
    }
}