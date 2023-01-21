using System.Collections.Generic;
using UnityEngine;

namespace Features.Level.Scripts.Data
{
  [CreateAssetMenu(fileName = "BetDisplaySettings", menuName = "StaticData/Bet/Create Bet Display Settings", order = 52)]
  public class BetDisplaySettings : ScriptableObject
  {
    public int ElementsInColumn = 10;
    public Dictionary<ChipType, ChipSettings> Prefabs;
  }
}