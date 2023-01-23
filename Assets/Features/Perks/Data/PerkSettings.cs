using UnityEngine;

namespace Features.Perks.Data
{
  [CreateAssetMenu(fileName = "PerkSettings", menuName = "StaticData/Perks/Create Perks Settings", order = 52)]
  public class PerkSettings : ScriptableObject
  {
    public string Name;
    public string Description;
    public PerkType Type;
    public PerkTargetType TargetType;
    public int UseCost;
    public float Coeff;
    public UnlockCondition UnlockCondition;
    public Sprite Icon;
  }
}