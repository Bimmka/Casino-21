using Features.Perks.Strategy;
using UnityEngine;

namespace Features.Perks.Data
{
  [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
  public class PerkSettings : ScriptableObject
  {
    public string Name;
    public string Description;
    public PerkType Type;
    public int UseCost;
    public UnlockCondition UnlockCondition;
  }
}