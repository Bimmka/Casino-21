using UnityEngine;

namespace Features.Perks.Data
{
  [CreateAssetMenu(fileName = "PerksSettingsContainer", menuName = "StaticData/Perks/Create Perks Settings Container", order = 52)]
  public class PerksSettingsContainer : ScriptableObject
  {
    public PerkSettings[] Perks;
  }
}