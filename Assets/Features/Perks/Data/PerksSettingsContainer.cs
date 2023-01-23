using UnityEngine;

namespace Features.Perks.Data
{
  [CreateAssetMenu(fileName = "PerksSettingsContainer", menuName = "StaticData/Perks/Create Perks Settings Container", order = 52)]
  public class PerksSettingsContainer : ScriptableObject
  {
    public PerkSettings[] Perks;

    public PerkSettings Perk(PerkType type)
    {
      for (int i = 0; i < Perks.Length; i++)
      {
        if (Perks[i].Type == type)
          return Perks[i];
      }

      return null;
    }
  }
}