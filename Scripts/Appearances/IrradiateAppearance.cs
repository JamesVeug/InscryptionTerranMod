using System.IO;
using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
using InscryptionAPI.Resource;
using UnityEngine;

namespace TerranMod.Scripts.Appearances
{
	public class IrradiateAppearance : CardAppearanceBehaviour
	{
		public static Appearance CustomAppearance;
		
		public static Texture2D decal;

		public static Appearance Initialize()
		{
			decal = Utils.GetTextureFromPath("Artwork/decal_irradiate.png");
			decal.name = "IrradiateDecal";
			
			ResourceBank.Resource resource = new ResourceBank.Resource();
			resource.path = "Art/Cards/Decals/IrradiateDecal";
			resource.asset = decal;
			ResourceBankManager.Add(Plugin.PluginGuid, resource);
			
			var newBackgroundBehaviour =
				CardAppearanceBehaviourManager.Add(Plugin.PluginGuid, "IrradiateDecal", typeof(IrradiateAppearance));
			CustomAppearance = newBackgroundBehaviour.Id;
			return CustomAppearance;
		}

		public override void ApplyAppearance()
		{
			if (!Card.Info.TempDecals.Find((a) => a.name == decal.name))
			{
				Card.Info.TempDecals.Add(decal);
			}
		}
	}
}