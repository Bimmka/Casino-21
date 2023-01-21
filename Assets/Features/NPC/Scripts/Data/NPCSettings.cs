using UnityEngine;

namespace Features.NPC.Scripts.Data
{
  [CreateAssetMenu(fileName = "NPCSettings", menuName = "StaticData/NPC/Create NPC Settings", order = 52)]
  public class NPCSettings : ScriptableObject
  {
    public int MaxPointsToTake = 16;
    public int MinPointsToCheck = 17;
  }
}