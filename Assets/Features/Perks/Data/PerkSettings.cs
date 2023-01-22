using UnityEngine;

namespace Features.Perks.Data
{
  [CreateAssetMenu(fileName = "PerkSettings", menuName = "StaticData/Perks/Create Perks Settings", order = 52)]
  public class PerkSettings : ScriptableObject
  {
    public string Name;
    public string Description;
    public PerkType Type;
    public int UseCost;
    public UnlockCondition UnlockCondition;
    public Sprite Icon;
  }
}