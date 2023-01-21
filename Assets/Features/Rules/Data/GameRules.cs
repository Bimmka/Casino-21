using UnityEngine;

namespace Features.Rules.Data
{
  [CreateAssetMenu(fileName = "GameRules", menuName = "StaticData/Rules/Create Game Rules", order = 52)]
  public class GameRules : ScriptableObject
  {
    public int MaxPoints = 21;
    public bool IsUserWinInDraw = false;
  }
}